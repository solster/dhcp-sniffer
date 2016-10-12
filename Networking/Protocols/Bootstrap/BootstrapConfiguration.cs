
namespace System.Networking.Protocols.Bootstrap
{
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    public sealed class BootstrapConfiguration
    {
        private PhysicalAddress address;
        private string fileName;
        private IPAddress ip;
        private string serverName;

        public unsafe BootstrapConfiguration(byte[] bytes, int offset, int count)
        {
            fixed (void* ptr = &bytes[offset])
            {
                BootstrapMessage* messagePtr = (BootstrapMessage*) ptr;
                this.ip = new IPAddress((long) messagePtr->assigned);
            }
            this.address = new PhysicalAddress(bytes, offset + 0x1c, 0x10);
            this.serverName = Encoding.ASCII.GetString(bytes, offset + 0x2c, 0x40);
            this.fileName = Encoding.ASCII.GetString(bytes, offset + 0x6c, 0x80).Trim(new char[1]);
            this.CheckOptions(bytes, 0xec, count - 0xec);
        }

        private void CheckOptions(byte[] buffer, int offset, int count)
        {
            if (this.IsValid(buffer, offset, count))
            {
                int num = 4;
                while (count > 0)
                {
                    num += this.GetOption(buffer, offset + num, count);
                    count -= num;
                }
            }
        }

        private int GetOption(byte[] buffer, int offset, int count)
        {
            return this.GetOption(buffer, offset + 2, buffer[offset + 1], (BootstrapConfigurationSetting) buffer[offset]);
        }

        private int GetOption(byte[] buffer, int offset, int count, BootstrapConfigurationSetting type)
        {
            switch (type)
            {
                case BootstrapConfigurationSetting.DhcpMessageType :
                case BootstrapConfigurationSetting.BootFileName :
                    break;
            }
            return count;
        }

        private unsafe bool IsValid(byte[] buffer, int offset, int count)
        {
            fixed (void* ptr = &buffer[offset])
            {
                return (*(((int*) ptr)) == 0x63538263);
            }
        }

        public static unsafe void Query(PhysicalAddress addr, BootstrapQueryType queryType)
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
                socket.Bind(new IPEndPoint(IPAddress.Any, 0x44));
                byte[] buffer = new byte[0x800];
                Random random = new Random();

                fixed (void* ptr = buffer)
                {
                    BootstrapMessage* messagePtr = (BootstrapMessage*)ptr;
                    messagePtr->operation = BootstrapMessage.BootstrapOperation.Request;
                    messagePtr->mediaType = addr.MediaType;
                    messagePtr->length = (byte)addr.CopyTo(buffer, 0x1c, 0x10);
                    messagePtr->hash = random.Next(0, 0x7fffffff);
                    messagePtr->flags = BootstrapMessage.BootstrapOptions.Broadcast;
                }

                buffer[0xec] = 0x63;
                buffer[0xed] = 130;
                buffer[0xee] = 0x53;
                buffer[0xef] = 0x63;
                buffer[240] = 0x35;
                buffer[0xf1] = 1;
                buffer[0xf2] = (byte) queryType;
                buffer[0xf3] = 0x3d;
                buffer[0xf4] = 7;
                buffer[0xf5] = (byte) addr.MediaType;
                addr.CopyTo(buffer, 0xf6, 6);
                buffer[0xfc] = 0x37;
                buffer[0xfd] = 5;
                buffer[0xfe] = 1;
                buffer[0xff] = 2;
                buffer[0x100] = 3;
                buffer[0x101] = 4;
                buffer[0x102] = 7;
                string s = "docsis1.1:05240101010201010301010401010501010601010701FF0801080901030A01010B01180C0101";
                buffer[0x103] = 60;
                buffer[0x104] = (byte) s.Length;
                int count = 0x105 + Encoding.ASCII.GetBytes(s, 0, s.Length, buffer, 0x105);
                buffer[count++] = 0xff;
                EndPoint remoteEP = new IPEndPoint(IPAddress.Broadcast, 0x43);
                if (socket.SendTo(buffer, 0, count, SocketFlags.None, remoteEP) != count)
                {

                }
            }
        }

        public IPAddress Address
        {
            get
            {
                return this.ip;
            }
        }

        public string Name
        {
            get
            {
                return string.IsNullOrEmpty(this.fileName) ? "{Baaah, Humbug!}" : this.fileName;
            }
        }

        public PhysicalAddress PhysicalAddress
        {
            get
            {
                return this.address;
            }
        }
    };
};