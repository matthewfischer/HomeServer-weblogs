using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Microsoft.HomeServer.Extensibility;
using Microsoft.HomeServer.Controls;

namespace HomeServerConsoleTab.WebLogs
{
    partial class LogControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogControl));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.User = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IPWhoIs = new System.Windows.Forms.DataGridViewButtonColumn();
            this.geoIP = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dns = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Block = new System.Windows.Forms.DataGridViewButtonColumn();
            this.URIStem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.localhostCheckBox = new System.Windows.Forms.CheckBox();
            this.localNetworkCheckBox = new System.Windows.Forms.CheckBox();
            this.anonymousCheckBox = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.consoleToolBarButton1 = new Microsoft.HomeServer.Controls.ConsoleToolBarButton();
            this.consoleToolBar1 = new Microsoft.HomeServer.Controls.ConsoleToolBar();
            this.mediaCollectorCheckBox = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.blockList = new System.Windows.Forms.Button();
            this.hideRouterTests = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.consoleToolBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IP,
            this.Date,
            this.User,
            this.IPWhoIs,
            this.geoIP,
            this.dns,
            this.Block,
            this.URIStem});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(0, 57);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.Size = new System.Drawing.Size(979, 480);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // IP
            // 
            this.IP.DataPropertyName = "IP";
            this.IP.HeaderText = "IP";
            this.IP.Name = "IP";
            this.IP.ReadOnly = true;
            this.IP.ToolTipText = "IP address of the requestor.";
            this.IP.Width = 90;
            // 
            // Date
            // 
            this.Date.DataPropertyName = "Date";
            this.Date.HeaderText = "Date / Time";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.ToolTipText = "Date of the request converted to local date";
            this.Date.Width = 140;
            // 
            // User
            // 
            this.User.DataPropertyName = "User";
            this.User.HeaderText = "User";
            this.User.Name = "User";
            this.User.ReadOnly = true;
            this.User.ToolTipText = "The user name for this request.  Will be blank if this no user was logged in.";
            this.User.Width = 140;
            // 
            // IPWhoIs
            // 
            this.IPWhoIs.HeaderText = "whois Lookup";
            this.IPWhoIs.Name = "IPWhoIs";
            this.IPWhoIs.ReadOnly = true;
            this.IPWhoIs.Text = "whois";
            this.IPWhoIs.ToolTipText = "Perform a whois lookup on this IP address.";
            this.IPWhoIs.UseColumnTextForButtonValue = true;
            this.IPWhoIs.Width = 80;
            // 
            // geoIP
            // 
            this.geoIP.HeaderText = "Geo Locate IP";
            this.geoIP.Name = "geoIP";
            this.geoIP.ReadOnly = true;
            this.geoIP.Text = "GeoLocate IP";
            this.geoIP.ToolTipText = "Geographically locate the requestor\'s IP";
            this.geoIP.UseColumnTextForButtonValue = true;
            this.geoIP.Width = 84;
            // 
            // dns
            // 
            this.dns.HeaderText = "DNS Lookup";
            this.dns.Name = "dns";
            this.dns.ReadOnly = true;
            this.dns.Text = "DNS";
            this.dns.ToolTipText = "Perform a DNS lookup on the requestor\'s IP";
            this.dns.UseColumnTextForButtonValue = true;
            this.dns.Width = 75;
            // 
            // Block
            // 
            this.Block.DataPropertyName = "Block";
            this.Block.HeaderText = "Block IP";
            this.Block.Name = "Block";
            this.Block.ReadOnly = true;
            this.Block.Text = "Block IP";
            this.Block.ToolTipText = "Advanced: Block this IP address in IIS.";
            this.Block.Width = 60;
            // 
            // URIStem
            // 
            this.URIStem.DataPropertyName = "URIStem";
            this.URIStem.HeaderText = "URL";
            this.URIStem.Name = "URIStem";
            this.URIStem.ReadOnly = true;
            this.URIStem.ToolTipText = "Requested URL";
            this.URIStem.Width = 313;
            // 
            // localhostCheckBox
            // 
            this.localhostCheckBox.AutoSize = true;
            this.localhostCheckBox.Checked = true;
            this.localhostCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.localhostCheckBox.Location = new System.Drawing.Point(472, 34);
            this.localhostCheckBox.Name = "localhostCheckBox";
            this.localhostCheckBox.Size = new System.Drawing.Size(103, 17);
            this.localhostCheckBox.TabIndex = 2;
            this.localhostCheckBox.Text = "Hide Localhost?";
            this.localhostCheckBox.UseVisualStyleBackColor = true;
            this.localhostCheckBox.CheckedChanged += new System.EventHandler(this.HideOrShowRows);
            // 
            // localNetworkCheckBox
            // 
            this.localNetworkCheckBox.AutoSize = true;
            this.localNetworkCheckBox.Checked = true;
            this.localNetworkCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.localNetworkCheckBox.Location = new System.Drawing.Point(339, 34);
            this.localNetworkCheckBox.Name = "localNetworkCheckBox";
            this.localNetworkCheckBox.Size = new System.Drawing.Size(126, 17);
            this.localNetworkCheckBox.TabIndex = 3;
            this.localNetworkCheckBox.Text = "Hide Local Network?";
            this.localNetworkCheckBox.UseVisualStyleBackColor = true;
            this.localNetworkCheckBox.CheckedChanged += new System.EventHandler(this.HideOrShowRows);
            // 
            // anonymousCheckBox
            // 
            this.anonymousCheckBox.AutoSize = true;
            this.anonymousCheckBox.Location = new System.Drawing.Point(220, 34);
            this.anonymousCheckBox.Name = "anonymousCheckBox";
            this.anonymousCheckBox.Size = new System.Drawing.Size(112, 17);
            this.anonymousCheckBox.TabIndex = 4;
            this.anonymousCheckBox.Text = "Hide Anonymous?";
            this.anonymousCheckBox.UseVisualStyleBackColor = true;
            this.anonymousCheckBox.CheckedChanged += new System.EventHandler(this.HideOrShowRows);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 540);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(982, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(148, 17);
            this.toolStripStatusLabel1.Text = "Loading logs, please wait...";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar1.Enabled = false;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.toolStripProgressBar1.Visible = false;
            // 
            // consoleToolBarButton1
            // 
            this.consoleToolBarButton1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.consoleToolBarButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.consoleToolBarButton1.Image = global::HomeServerConsoleTab.WebLogs.Properties.Resources.icon_reload;
            this.consoleToolBarButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.consoleToolBarButton1.Name = "consoleToolBarButton1";
            this.consoleToolBarButton1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.consoleToolBarButton1.Size = new System.Drawing.Size(117, 28);
            this.consoleToolBarButton1.Text = "Reload Logs";
            this.consoleToolBarButton1.Click += new System.EventHandler(this.consoleToolBarButton1_Click);
            // 
            // consoleToolBar1
            // 
            this.consoleToolBar1.AutoSize = false;
            this.consoleToolBar1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("consoleToolBar1.BackgroundImage")));
            this.consoleToolBar1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.consoleToolBar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.consoleToolBar1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.consoleToolBarButton1});
            this.consoleToolBar1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.consoleToolBar1.Location = new System.Drawing.Point(0, 0);
            this.consoleToolBar1.Name = "consoleToolBar1";
            this.consoleToolBar1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.consoleToolBar1.Size = new System.Drawing.Size(982, 31);
            this.consoleToolBar1.TabIndex = 7;
            this.consoleToolBar1.Text = "consoleToolBar1";
            // 
            // mediaCollectorCheckBox
            // 
            this.mediaCollectorCheckBox.AutoSize = true;
            this.mediaCollectorCheckBox.Checked = true;
            this.mediaCollectorCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mediaCollectorCheckBox.Location = new System.Drawing.Point(582, 34);
            this.mediaCollectorCheckBox.Name = "mediaCollectorCheckBox";
            this.mediaCollectorCheckBox.Size = new System.Drawing.Size(130, 17);
            this.mediaCollectorCheckBox.TabIndex = 9;
            this.mediaCollectorCheckBox.Text = "Hide Media Collector?";
            this.mediaCollectorCheckBox.UseVisualStyleBackColor = true;
            this.mediaCollectorCheckBox.CheckedChanged += new System.EventHandler(this.HideOrShowRows);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(134, 32);
            this.textBox1.MaxLength = 6;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(65, 20);
            this.textBox1.TabIndex = 10;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Maximum Entries to Load:";
            // 
            // blockList
            // 
            this.blockList.Location = new System.Drawing.Point(872, 34);
            this.blockList.Name = "blockList";
            this.blockList.Size = new System.Drawing.Size(107, 23);
            this.blockList.TabIndex = 12;
            this.blockList.Text = "List Blocked IPs";
            this.blockList.UseVisualStyleBackColor = true;
            this.blockList.Click += new System.EventHandler(this.blockList_Click);
            // 
            // hideRouterTests
            // 
            this.hideRouterTests.AutoSize = true;
            this.hideRouterTests.Checked = true;
            this.hideRouterTests.CheckState = System.Windows.Forms.CheckState.Checked;
            this.hideRouterTests.Location = new System.Drawing.Point(718, 34);
            this.hideRouterTests.Name = "hideRouterTests";
            this.hideRouterTests.Size = new System.Drawing.Size(118, 17);
            this.hideRouterTests.TabIndex = 13;
            this.hideRouterTests.Text = "Hide Router Tests?";
            this.hideRouterTests.UseVisualStyleBackColor = true;
            this.hideRouterTests.CheckedChanged += new System.EventHandler(this.HideOrShowRows);
            // 
            // LogControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hideRouterTests);
            this.Controls.Add(this.blockList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.mediaCollectorCheckBox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.anonymousCheckBox);
            this.Controls.Add(this.localNetworkCheckBox);
            this.Controls.Add(this.localhostCheckBox);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.consoleToolBar1);
            this.Name = "LogControl";
            this.Size = new System.Drawing.Size(982, 562);
            this.Load += new System.EventHandler(this.LogControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.consoleToolBar1.ResumeLayout(false);
            this.consoleToolBar1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView dataGridView1;
        private CheckBox localhostCheckBox;
        private CheckBox localNetworkCheckBox;
        private CheckBox anonymousCheckBox;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ConsoleToolBarButton consoleToolBarButton1;
        private ConsoleToolBar consoleToolBar1;
        private ToolStripProgressBar toolStripProgressBar1;
        private CheckBox mediaCollectorCheckBox;
        private TextBox textBox1;
        private Label label1;
        private Button blockList;
        private DataGridViewTextBoxColumn IP;
        private DataGridViewTextBoxColumn Date;
        private DataGridViewTextBoxColumn User;
        private DataGridViewButtonColumn IPWhoIs;
        private DataGridViewButtonColumn geoIP;
        private DataGridViewButtonColumn dns;
        private DataGridViewButtonColumn Block;
        private DataGridViewTextBoxColumn URIStem;
        private CheckBox hideRouterTests;
    }
}
