
namespace System.Runtime.Versioning
{
    using IO;
    using Net;
    using Diagnostics;
    using ComponentModel;

    public sealed partial class Updater : Component
    {
        public event EventHandler UpdateRequired;

        public Updater()
        {
            InitializeComponent();
        }

        static string ExecutablePath
        {
            get
            {
                return System.Reflection.Assembly.GetEntryAssembly().Location;
            }
        }

        static Version Version
        {
            get
            {
                return System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
            }
        }

        public bool Enabled
        {
            get
            {
                return this.timer.Enabled;
            }
            set
            {
                this.timer.Enabled = value;
            }
        }

        private void IsUpdateRequired(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                string uri = string.Format("http://www.solster.eu/download.php?version={0}", Version);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    fileName = Path.GetTempFileName();

                    byte[] buffer = new byte[0x1000];

                    using (Stream stream = response.GetResponseStream())
                    using (Stream s = File.Create(fileName))
                    {
                        int count;

                        while ((count = stream.Read(buffer, 0, 0x1000)) > 0)
                        {
                            s.Write(buffer, 0, count);
                        }
                    }

                    File.WriteAllText(Path.ChangeExtension(fileName, "bat"),
                        "move /Y %2 %1\r\nstart Solster %1\r\ndel %0");

                    AppDomain.CurrentDomain.ProcessExit += new EventHandler(this.DoUpdate);

                    if (this.UpdateRequired != null)
                    {
                        this.UpdateRequired(this, EventArgs.Empty);
                    }
                }
            }
            catch (TimeoutException)
            {
                // Do nothing.
            }
        }

        private string fileName;

        private void DoUpdate(object sender, EventArgs e)
        {
            using (Process p = new Process())
            {
                p.StartInfo.FileName = Path.ChangeExtension(fileName, "bat");
                p.StartInfo.Arguments = "\"" + ExecutablePath + "\" \"" + fileName + "\"";
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.Start();
            }
        }
    };
};