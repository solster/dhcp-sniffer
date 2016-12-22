
namespace Solster.Net.Bootstrap
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;
    using System.Reactive.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public sealed class UdpChannel
    {
        public const Int32 ServerPort = 0x44;

        public static IPEndPoint ServerEndPoint { get; } = new IPEndPoint(IPAddress.Any, ServerPort);

        public static IPEndPoint ClientEndPoint { get; } = new IPEndPoint(IPAddress.Broadcast, 0x43);

        public UdpChannel()
        {
        }

        public async Task Receive()
        {
            using (var client = new UdpClient(ServerEndPoint) { EnableBroadcast = true, ExclusiveAddressUse = false })
            {
                var data = await client.ReceiveAsync();
                ////deserialize buffer

                //BootstrapConfiguration userState = new BootstrapConfiguration(buffer, 0, count);
                //this.backgroundWorker.ReportProgress(0, userState);
            }
        }

        public sealed class BootstrapRequest
        {
            public Int16 Operation { get; set; }
            public Int32 MediaType { get; set; }
            public Byte[] Address { get; set; }
            public Boolean Broadcast { get; set; }
        }

        public sealed class DynamicConfigurationOptions
        {
            public Int32 Type { get; set; }
        }
        
        public async Task SendAsync(BootstrapRequest request, DynamicConfigurationOptions options)
        {
            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream))
            {
                //
                // bootstrap
                //
                writer.Write(request.Operation);// operation
                writer.Write(request.MediaType);// mediatype
                writer.Write(request.Address.Length);// length
                writer.Write(request.Address);// address
                writer.Write(request.GetHashCode());//hash
                writer.Write(request.Broadcast);// flags
                
                //
                // dhcp
                //
                // magic cookie
                writer.Write(0x63825363);
                
                // option
                writer.Write(0x35); // option type
                writer.Write(1); // option length
                writer.Write(options.Type); // option value
                
                // option
                writer.Write(0x3d); // option type
                writer.Write(7); // option length
                writer.Write(request.MediaType);//option value
                writer.Write(request.Address);//option value

                // end
                writer.Write(0xff);

                var content = stream.ToArray();

                using (var client = new UdpClient(ServerEndPoint) { EnableBroadcast = true, ExclusiveAddressUse = false })
                {
                    await client.SendAsync(content, content.Length, ClientEndPoint);
                }
            }
        }
    }
}
