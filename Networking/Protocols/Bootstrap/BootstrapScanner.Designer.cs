
namespace System.Networking.Protocols.Bootstrap
{
    using ComponentModel;
    
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class BootstrapScanner : Component
    {        
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.backgroundWorker.IsBusy)
                {
                    this.backgroundWorker.CancelAsync();
                }
                if (this.bruteforceWorker.IsBusy)
                {
                    this.bruteforceWorker.CancelAsync();
                }
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new Container();
            this.backgroundWorker = new BackgroundWorker();
            this.bruteforceWorker = new BackgroundWorker();
            //
            // backgroundWorker
            //
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new DoWorkEventHandler(this.Listen);
            this.backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(this.Received);
            //
            // bruteforceWorker
            //
            this.bruteforceWorker.WorkerSupportsCancellation = true;
            this.bruteforceWorker.WorkerReportsProgress = true;
            this.bruteforceWorker.DoWork += new DoWorkEventHandler(this.Search);
        }

        #endregion

        private BackgroundWorker backgroundWorker;
        private BackgroundWorker bruteforceWorker;
    };
};