
namespace System.Networking.Protocols.Bootstrap
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct BootstrapMessage
    {
        public BootstrapOperation operation;
        public PhysicalMedia mediaType;
        public byte length;
        public byte hops;
        public int hash;
        public short seconds;
        public BootstrapOptions flags;
        public uint ip;
        public uint assigned;
        public uint gateway;
        public uint relay;
        
        /// <summary>
        /// 
        /// </summary>
        public enum BootstrapOperation : byte
        {
            None = 0,
            Request = 1,
            Response = 2
        };

        /// <summary>
        /// 
        /// </summary>
        [Flags]
        public enum BootstrapOptions : ushort
        {
            Broadcast = 0x80,
            None = 0
        };
    };
};