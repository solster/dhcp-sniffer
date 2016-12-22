namespace System.Networking
{
    using Regex = System.Text.RegularExpressions.Regex;
    using NumberStyles = System.Globalization.NumberStyles;
    using StructLayout = Runtime.InteropServices.StructLayoutAttribute;
    using LayoutKind = Runtime.InteropServices.LayoutKind;

    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public sealed class PhysicalAddress
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly byte[] __value;

        /// <summary>
        /// 
        /// </summary>
        public PhysicalAddress()
        {
            this.__value = new byte[6];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        public PhysicalAddress(byte[] address)
        {
            if (address == null)
                throw new ArgumentNullException("address");

            if (address.Length != 6)
                throw new ArgumentOutOfRangeException("address");

            this.__value = address;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public PhysicalAddress(byte[] address, int offset, int count) : this()
        {
            if (address == null)
                throw new ArgumentNullException("address");

            if (count < 6)
                throw new ArgumentOutOfRangeException("count");

            System.Buffer.BlockCopy(address, offset, this.__value, 0, 6);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int CopyTo(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
                throw new ArgumentNullException("buffer");

            if (count < 6)
                throw new ArgumentOutOfRangeException("count");

            Buffer.BlockCopy(this.__value, 0, buffer, offset, 6);

            return Buffer.ByteLength(this.__value);
        }

        /// <summary>
        /// 
        /// </summary>
        public PhysicalMedia MediaType
        {
            get
            {
                return PhysicalMedia.Ethernet;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenString"></param>
        /// <returns></returns>
        public static PhysicalAddress Parse(string addressString)
        {
            if (!Regex.IsMatch(addressString, @"^((([\da-fA-F]{2}):){5})([\da-fA-F]{2})$"))
            {
                throw new FormatException();
            }

            string array = Regex.Replace(addressString, ":", "");

            long v = long.Parse(array, NumberStyles.HexNumber);

            byte[] b = BitConverter.GetBytes(v);

            Array.Reverse(b);

            return new PhysicalAddress(b, 2, 6);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            Text.StringBuilder builder = new System.Text.StringBuilder();

            foreach (byte b in this.__value)
            {
                builder.Append(b.ToString("X2"));
                builder.Append(':');
            }
            return builder.ToString(0, builder.Length - 1);
        }
    };
};