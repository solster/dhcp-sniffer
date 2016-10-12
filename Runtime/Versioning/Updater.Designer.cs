
namespace System.Runtime.Versioning
{
    using ComponentModel;

    public sealed partial class Updater : Component
    {
        private void InitializeComponent()
        {
            this.timer = new System.Timers.Timer();
            this.timer.Interval = 15000;
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.IsUpdateRequired);
            this.timer.Enabled = true;
        }
        private IContainer components = null;
        private System.Timers.Timer timer;
    };
};