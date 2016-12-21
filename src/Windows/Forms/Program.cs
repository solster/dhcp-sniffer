
namespace System.Windows.Forms
{
    using Networking;
    
    public sealed partial class Program : Form
    {
        public Program()
        {
            InitializeComponent();
        }

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Program());
        }

        private void Found(object sender, Networking.Protocols.Bootstrap.BootstrapScannerEventArgs e)
        {
            ListViewItem item = this.listViewConfigurations.Items.Add(e.Configuration.PhysicalAddress.ToString());

            item.SubItems.Add(e.Configuration.Name);
            item.SubItems.Add(e.Configuration.Address.ToString());
        }

        private void Search(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                try
                {
                    PhysicalAddress addr = PhysicalAddress.Parse(this.toolStripSearchBox.Text);
                    
                    this.bootstrapScanner.Search(addr);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Please enter an address in the format\r\n\r\n00:11:22:33:44:55", "Invalid Address Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                e.SuppressKeyPress = true;
            }
        }

        private void Cancel(object sender, EventArgs e)
        {
            this.bootstrapScanner.Cancel();
        }

        private void UpdateRequired(object sender, EventArgs e)
        {
            MessageBox.Show("Whoohoo, an update has been found!\r\n\r\nThe application will now restart so the update can take effect.", "Restart Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            Application.Exit();
        }

        private void Activate(object sender, EventArgs e)
        {
            this.updater.Enabled = true;
        }

        private void Donate(object sender, EventArgs e)
        {
            string uri = "https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=solster@underground-modems.com&lc=UK&item_name=Donation&currency_code=GBP&bn=PP%2dDonationsBF";

            System.Diagnostics.Process.Start(uri);
        }

        private void SearchCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.toolStripSearchProgress.Value = 0;
        }

        private void SearchProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this.toolStripSearchProgress.Value = e.ProgressPercentage;
        }

        private void leaseSelected(object sender, EventArgs e)
        {
            if (this.listViewConfigurations.SelectedItems.Count > 0)
            {
                this.toolStripSearchBox.Text =
                    this.listViewConfigurations.SelectedItems[0].Text;
            }
        }
    };
};