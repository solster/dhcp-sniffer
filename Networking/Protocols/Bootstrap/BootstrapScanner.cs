
namespace System.Networking.Protocols.Bootstrap
{
    using ComponentModel;
    using System.Net;
    using System.Net.Sockets;
    using Threading;

    /// <summary>
    /// 
    /// </summary>
    public sealed partial class BootstrapScanner : Component
    {
        /// <summary>
        /// 
        /// </summary>
        public BootstrapScanner()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public BootstrapScanner(IContainer container) : this()
        {
            if (container == null)
                throw new ArgumentNullException("container");

            container.Add(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addressString"></param>
        public void Search(PhysicalAddress address)
        {
            if (address == null)
            {
                throw new ArgumentNullException("address");
            }

            if (this.bruteforceWorker.IsBusy)
            {
                this.bruteforceWorker.CancelAsync();

                while (this.bruteforceWorker.CancellationPending)
                {
                    Thread.Sleep(100);
                }
            }

            if (!this.backgroundWorker.IsBusy)
            {
                this.backgroundWorker.RunWorkerAsync();
            }

            this.bruteforceWorker.RunWorkerAsync(address);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Cancel()
        {
            if (this.bruteforceWorker.IsBusy)
            {
                this.bruteforceWorker.CancelAsync();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search(object sender, DoWorkEventArgs e)
        {
            byte[] addr = new byte[6];

            ((PhysicalAddress)e.Argument).CopyTo(addr, 0, 6);

            Random r = new Random(addr[5]);

            int num = 0;

            while ((num++ < 0xff) && !this.bruteforceWorker.CancellationPending)
            {
                byte[] mac = addr;

                mac[5] = (Byte)r.Next(0, Byte.MaxValue);

                BootstrapConfiguration.Query(new PhysicalAddress(mac), BootstrapQueryType.Discover);

                this.bruteforceWorker.ReportProgress((num * 100) / 0xff);

                Thread.Sleep(200);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Listen(object sender, DoWorkEventArgs e)
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
                socket.Bind(new IPEndPoint(IPAddress.Any, 0x44));
                while (!this.backgroundWorker.CancellationPending)
                {
                    byte[] buffer = new byte[0x800];
                    EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
                    int count = socket.ReceiveFrom(buffer, ref remoteEP);
                    BootstrapConfiguration userState = new BootstrapConfiguration(buffer, 0, count);
                    this.backgroundWorker.ReportProgress(0, userState);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Received(object sender, ProgressChangedEventArgs e)
        {
            BootstrapConfiguration userState = e.UserState as BootstrapConfiguration;

            System.IO.File.AppendAllText("macs.txt", string.Format("{0}\t{1}\r\n", userState.PhysicalAddress, userState.Name));

            if (this.Found != null)
            {
                this.Found(sender, new BootstrapScannerEventArgs(userState));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public event ProgressChangedEventHandler SearchProgressChanged
        {
            add
            {
                this.bruteforceWorker.ProgressChanged += value;
            }
            remove
            {
                this.bruteforceWorker.ProgressChanged -= value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public event RunWorkerCompletedEventHandler SearchCompleted
        {
            add
            {
                this.bruteforceWorker.RunWorkerCompleted += value;
            }
            remove
            {
                this.bruteforceWorker.RunWorkerCompleted -= value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<BootstrapScannerEventArgs> Found;
    };
};