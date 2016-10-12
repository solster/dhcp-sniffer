
namespace System.Windows.Forms
{
    using System.Networking.Protocols.Bootstrap;

    public sealed partial class Program : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Program));
            this.toolStripEdition = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSearchBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSearchProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripCancelButton = new System.Windows.Forms.ToolStripButton();
            this.listViewConfigurations = new System.Windows.Forms.ListView();
            this.addressColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.fileNameColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.ipColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.bootstrapScanner = new System.Networking.Protocols.Bootstrap.BootstrapScanner(this.components);
            this.updater = new System.Runtime.Versioning.Updater();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripEdition
            // 
            this.toolStripEdition.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripEdition.BackColor = System.Drawing.Color.Transparent;
            this.toolStripEdition.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripEdition.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.toolStripEdition.Margin = new System.Windows.Forms.Padding(5, 5, 0, 5);
            this.toolStripEdition.Name = "toolStripEdition";
            this.toolStripEdition.Size = new System.Drawing.Size(174, 30);
            this.toolStripEdition.Text = "Underground Modems Edition";
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("toolStrip.BackgroundImage")));
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripSearchBox,
            this.toolStripSearchProgress,
            this.toolStripEdition,
            this.toolStripCancelButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(5);
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(534, 50);
            this.toolStrip.TabIndex = 0;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.BackColor = System.Drawing.Color.Transparent;
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel1.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.toolStripLabel1.Margin = new System.Windows.Forms.Padding(5, 5, 0, 5);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(48, 30);
            this.toolStripLabel1.Text = "Search:";
            // 
            // toolStripSearchBox
            // 
            this.toolStripSearchBox.Margin = new System.Windows.Forms.Padding(3);
            this.toolStripSearchBox.Name = "toolStripSearchBox";
            this.toolStripSearchBox.Size = new System.Drawing.Size(120, 34);
            this.toolStripSearchBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolStripSearchBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Search);
            // 
            // toolStripSearchProgress
            // 
            this.toolStripSearchProgress.AutoSize = false;
            this.toolStripSearchProgress.Margin = new System.Windows.Forms.Padding(3);
            this.toolStripSearchProgress.Name = "toolStripSearchProgress";
            this.toolStripSearchProgress.Size = new System.Drawing.Size(100, 24);
            // 
            // toolStripCancelButton
            // 
            this.toolStripCancelButton.AutoSize = false;
            this.toolStripCancelButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripCancelButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripCancelButton.Image")));
            this.toolStripCancelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripCancelButton.Margin = new System.Windows.Forms.Padding(3);
            this.toolStripCancelButton.Name = "toolStripCancelButton";
            this.toolStripCancelButton.Size = new System.Drawing.Size(26, 26);
            this.toolStripCancelButton.Text = "Click to cancel current search";
            this.toolStripCancelButton.Click += new System.EventHandler(this.Cancel);
            // 
            // listViewConfigurations
            // 
            this.listViewConfigurations.BackColor = System.Drawing.Color.LightSteelBlue;
            this.listViewConfigurations.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewConfigurations.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.addressColumnHeader,
            this.fileNameColumnHeader,
            this.ipColumnHeader});
            this.listViewConfigurations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewConfigurations.FullRowSelect = true;
            this.listViewConfigurations.Location = new System.Drawing.Point(0, 50);
            this.listViewConfigurations.Name = "listViewConfigurations";
            this.listViewConfigurations.Size = new System.Drawing.Size(534, 232);
            this.listViewConfigurations.TabIndex = 3;
            this.listViewConfigurations.UseCompatibleStateImageBehavior = false;
            this.listViewConfigurations.View = System.Windows.Forms.View.Details;
            this.listViewConfigurations.SelectedIndexChanged += new System.EventHandler(this.leaseSelected);
            // 
            // addressColumnHeader
            // 
            this.addressColumnHeader.Text = "Physical Address";
            this.addressColumnHeader.Width = 120;
            // 
            // fileNameColumnHeader
            // 
            this.fileNameColumnHeader.Text = "Configuration";
            this.fileNameColumnHeader.Width = 178;
            // 
            // ipColumnHeader
            // 
            this.ipColumnHeader.Text = "IP";
            this.ipColumnHeader.Width = 107;
            // 
            // bootstrapScanner
            // 
            this.bootstrapScanner.Found += new System.EventHandler<System.Networking.Protocols.Bootstrap.BootstrapScannerEventArgs>(this.Found);
            this.bootstrapScanner.SearchProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.SearchProgressChanged);
            this.bootstrapScanner.SearchCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.SearchCompleted);
            // 
            // updater
            // 
            this.updater.Enabled = false;
            this.updater.UpdateRequired += new System.EventHandler(this.UpdateRequired);
            // 
            // Program
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 282);
            this.Controls.Add(this.listViewConfigurations);
            this.Controls.Add(this.toolStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Program";
            this.Opacity = 0.95;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Solster v0.2.7.1 Final (Private) © 2007-2010";
            this.Activated += new System.EventHandler(this.Activate);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Networking.Protocols.Bootstrap.BootstrapScanner bootstrapScanner;
        private System.Runtime.Versioning.Updater updater;
        private ToolStripLabel toolStripEdition;
        private ToolStrip toolStrip;
        private ToolStripButton toolStripCancelButton;
        private ToolStripProgressBar toolStripSearchProgress;
        private ToolStripTextBox toolStripSearchBox;
        private ListView listViewConfigurations;
        private ColumnHeader addressColumnHeader;
        private ColumnHeader fileNameColumnHeader;
        private ColumnHeader ipColumnHeader;
        private ToolStripLabel toolStripLabel1;
    };
};