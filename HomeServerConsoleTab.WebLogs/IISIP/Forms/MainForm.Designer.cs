namespace IISIP
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.blockedIPListBox = new System.Windows.Forms.TextBox();
            this.siteList = new System.Windows.Forms.ListBox();
            this.metabasePathTextBox = new System.Windows.Forms.TextBox();
            this.blockLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.scanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scanEventLog_menuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_About = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.siteNameLabel = new System.Windows.Forms.Label();
            this.blockedIPCountLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ipFeedListBox = new System.Windows.Forms.ListBox();
            this.UnBlockGroup_Button = new System.Windows.Forms.Button();
            this.blockGroup_Button = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.singleIPTextBox = new System.Windows.Forms.TextBox();
            this.blockSingleIP_Button = new System.Windows.Forms.Button();
            this.UnBlockSingleIP_Button = new System.Windows.Forms.Button();
            this.blockedIPListText_Label = new System.Windows.Forms.Label();
            this.siteNameLabel2 = new System.Windows.Forms.Label();
            this.updateList_Button = new System.Windows.Forms.Button();
            this.providerTextLabel = new System.Windows.Forms.Label();
            this.providerSiteTextLabel = new System.Windows.Forms.Label();
            this.providerSiteLink = new System.Windows.Forms.Label();
            this.providerLabel = new System.Windows.Forms.Label();
            this.singleIPValidLabel = new System.Windows.Forms.Label();
            this.refreshList_Button = new System.Windows.Forms.Button();
            this.UnblockAll_Button = new System.Windows.Forms.Button();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.statusBar1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBarLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.providerLinkTextLabel = new System.Windows.Forms.Label();
            this.providerDataLink = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // blockedIPListBox
            // 
            this.blockedIPListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blockedIPListBox.Location = new System.Drawing.Point(415, 65);
            this.blockedIPListBox.Margin = new System.Windows.Forms.Padding(2);
            this.blockedIPListBox.Multiline = true;
            this.blockedIPListBox.Name = "blockedIPListBox";
            this.blockedIPListBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.blockedIPListBox.Size = new System.Drawing.Size(202, 249);
            this.blockedIPListBox.TabIndex = 0;
            // 
            // siteList
            // 
            this.siteList.FormattingEnabled = true;
            this.siteList.Location = new System.Drawing.Point(9, 50);
            this.siteList.Margin = new System.Windows.Forms.Padding(2);
            this.siteList.Name = "siteList";
            this.siteList.Size = new System.Drawing.Size(202, 212);
            this.siteList.TabIndex = 1;
            this.siteList.SelectedIndexChanged += new System.EventHandler(this.siteList_SelectedIndexChanged);
            // 
            // metabasePathTextBox
            // 
            this.metabasePathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.metabasePathTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metabasePathTextBox.Location = new System.Drawing.Point(11, 366);
            this.metabasePathTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.metabasePathTextBox.Name = "metabasePathTextBox";
            this.metabasePathTextBox.ReadOnly = true;
            this.metabasePathTextBox.Size = new System.Drawing.Size(214, 11);
            this.metabasePathTextBox.TabIndex = 3;
            this.metabasePathTextBox.Text = "-";
            // 
            // blockLabel
            // 
            this.blockLabel.AutoSize = true;
            this.blockLabel.Location = new System.Drawing.Point(9, 316);
            this.blockLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.blockLabel.Name = "blockLabel";
            this.blockLabel.Size = new System.Drawing.Size(67, 13);
            this.blockLabel.TabIndex = 4;
            this.blockLabel.Text = "Blocked IPs:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.scanToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(629, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_Exit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // menuItem_Exit
            // 
            this.menuItem_Exit.Name = "menuItem_Exit";
            this.menuItem_Exit.Size = new System.Drawing.Size(92, 22);
            this.menuItem_Exit.Text = "E&xit";
            this.menuItem_Exit.Click += new System.EventHandler(this.menuItem_Exit_Click);
            // 
            // scanToolStripMenuItem
            // 
            this.scanToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scanEventLog_menuItem});
            this.scanToolStripMenuItem.Name = "scanToolStripMenuItem";
            this.scanToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.scanToolStripMenuItem.Text = "Scan";
            // 
            // scanEventLog_menuItem
            // 
            this.scanEventLog_menuItem.Name = "scanEventLog_menuItem";
            this.scanEventLog_menuItem.Size = new System.Drawing.Size(195, 22);
            this.scanEventLog_menuItem.Text = "Scan Event Log for IPs...";
            this.scanEventLog_menuItem.Click += new System.EventHandler(this.scanEventLog_menuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_About});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // menuItem_About
            // 
            this.menuItem_About.Name = "menuItem_About";
            this.menuItem_About.Size = new System.Drawing.Size(119, 22);
            this.menuItem_About.Text = "&About....";
            this.menuItem_About.Click += new System.EventHandler(this.menuItem_About_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "IIS Sites:";
            // 
            // siteNameLabel
            // 
            this.siteNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.siteNameLabel.Location = new System.Drawing.Point(9, 294);
            this.siteNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.siteNameLabel.Name = "siteNameLabel";
            this.siteNameLabel.Size = new System.Drawing.Size(229, 19);
            this.siteNameLabel.TabIndex = 6;
            this.siteNameLabel.Text = "-";
            // 
            // blockedIPCountLabel
            // 
            this.blockedIPCountLabel.AutoSize = true;
            this.blockedIPCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blockedIPCountLabel.Location = new System.Drawing.Point(9, 330);
            this.blockedIPCountLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.blockedIPCountLabel.Name = "blockedIPCountLabel";
            this.blockedIPCountLabel.Size = new System.Drawing.Size(11, 13);
            this.blockedIPCountLabel.TabIndex = 7;
            this.blockedIPCountLabel.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 279);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Current site:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 353);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Path:";
            // 
            // ipFeedListBox
            // 
            this.ipFeedListBox.FormattingEnabled = true;
            this.ipFeedListBox.Location = new System.Drawing.Point(234, 152);
            this.ipFeedListBox.Margin = new System.Windows.Forms.Padding(2);
            this.ipFeedListBox.Name = "ipFeedListBox";
            this.ipFeedListBox.Size = new System.Drawing.Size(158, 134);
            this.ipFeedListBox.TabIndex = 15;
            this.ipFeedListBox.SelectedIndexChanged += new System.EventHandler(this.ipFeedListBox_SelectedIndexChanged);
            // 
            // UnBlockGroup_Button
            // 
            this.UnBlockGroup_Button.Location = new System.Drawing.Point(317, 291);
            this.UnBlockGroup_Button.Margin = new System.Windows.Forms.Padding(2);
            this.UnBlockGroup_Button.Name = "UnBlockGroup_Button";
            this.UnBlockGroup_Button.Size = new System.Drawing.Size(75, 24);
            this.UnBlockGroup_Button.TabIndex = 16;
            this.UnBlockGroup_Button.Text = "Unblock";
            this.UnBlockGroup_Button.UseVisualStyleBackColor = true;
            this.UnBlockGroup_Button.Click += new System.EventHandler(this.UnBlockGroup_Button_Click);
            // 
            // blockGroup_Button
            // 
            this.blockGroup_Button.Location = new System.Drawing.Point(234, 291);
            this.blockGroup_Button.Margin = new System.Windows.Forms.Padding(2);
            this.blockGroup_Button.Name = "blockGroup_Button";
            this.blockGroup_Button.Size = new System.Drawing.Size(75, 24);
            this.blockGroup_Button.TabIndex = 17;
            this.blockGroup_Button.Text = "Block";
            this.blockGroup_Button.UseVisualStyleBackColor = true;
            this.blockGroup_Button.Click += new System.EventHandler(this.blockGroup_Button_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(233, 135);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "IP Groups:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(233, 34);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Add 1 IP Address:";
            // 
            // singleIPTextBox
            // 
            this.singleIPTextBox.Location = new System.Drawing.Point(235, 51);
            this.singleIPTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.singleIPTextBox.MaxLength = 15;
            this.singleIPTextBox.Name = "singleIPTextBox";
            this.singleIPTextBox.Size = new System.Drawing.Size(158, 20);
            this.singleIPTextBox.TabIndex = 18;
            this.singleIPTextBox.TextChanged += new System.EventHandler(this.singleIPTextBox_TextChanged);
            // 
            // blockSingleIP_Button
            // 
            this.blockSingleIP_Button.Location = new System.Drawing.Point(235, 75);
            this.blockSingleIP_Button.Margin = new System.Windows.Forms.Padding(2);
            this.blockSingleIP_Button.Name = "blockSingleIP_Button";
            this.blockSingleIP_Button.Size = new System.Drawing.Size(56, 22);
            this.blockSingleIP_Button.TabIndex = 19;
            this.blockSingleIP_Button.Text = "Block";
            this.blockSingleIP_Button.UseVisualStyleBackColor = true;
            this.blockSingleIP_Button.Click += new System.EventHandler(this.blockSingleIP_Button_Click);
            // 
            // UnBlockSingleIP_Button
            // 
            this.UnBlockSingleIP_Button.Location = new System.Drawing.Point(296, 75);
            this.UnBlockSingleIP_Button.Margin = new System.Windows.Forms.Padding(2);
            this.UnBlockSingleIP_Button.Name = "UnBlockSingleIP_Button";
            this.UnBlockSingleIP_Button.Size = new System.Drawing.Size(56, 22);
            this.UnBlockSingleIP_Button.TabIndex = 20;
            this.UnBlockSingleIP_Button.Text = "Unblock";
            this.UnBlockSingleIP_Button.UseVisualStyleBackColor = true;
            this.UnBlockSingleIP_Button.Click += new System.EventHandler(this.UnBlockSingleIP_Button_Click);
            // 
            // blockedIPListText_Label
            // 
            this.blockedIPListText_Label.AutoSize = true;
            this.blockedIPListText_Label.Location = new System.Drawing.Point(413, 34);
            this.blockedIPListText_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.blockedIPListText_Label.Name = "blockedIPListText_Label";
            this.blockedIPListText_Label.Size = new System.Drawing.Size(137, 13);
            this.blockedIPListText_Label.TabIndex = 4;
            this.blockedIPListText_Label.Text = "Blocked IPs for Current Site";
            // 
            // siteNameLabel2
            // 
            this.siteNameLabel2.AutoSize = true;
            this.siteNameLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.siteNameLabel2.Location = new System.Drawing.Point(413, 48);
            this.siteNameLabel2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.siteNameLabel2.Name = "siteNameLabel2";
            this.siteNameLabel2.Size = new System.Drawing.Size(90, 13);
            this.siteNameLabel2.TabIndex = 4;
            this.siteNameLabel2.Text = "siteNameLabel";
            // 
            // updateList_Button
            // 
            this.updateList_Button.Enabled = false;
            this.updateList_Button.Location = new System.Drawing.Point(538, 347);
            this.updateList_Button.Margin = new System.Windows.Forms.Padding(2);
            this.updateList_Button.Name = "updateList_Button";
            this.updateList_Button.Size = new System.Drawing.Size(75, 24);
            this.updateList_Button.TabIndex = 21;
            this.updateList_Button.Text = "Update";
            this.updateList_Button.UseVisualStyleBackColor = true;
            this.updateList_Button.Click += new System.EventHandler(this.updateList_Button_Click);
            // 
            // providerTextLabel
            // 
            this.providerTextLabel.AutoSize = true;
            this.providerTextLabel.Location = new System.Drawing.Point(232, 323);
            this.providerTextLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.providerTextLabel.Name = "providerTextLabel";
            this.providerTextLabel.Size = new System.Drawing.Size(49, 13);
            this.providerTextLabel.TabIndex = 4;
            this.providerTextLabel.Text = "Provider:";
            // 
            // providerSiteTextLabel
            // 
            this.providerSiteTextLabel.AutoSize = true;
            this.providerSiteTextLabel.Location = new System.Drawing.Point(232, 340);
            this.providerSiteTextLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.providerSiteTextLabel.Name = "providerSiteTextLabel";
            this.providerSiteTextLabel.Size = new System.Drawing.Size(28, 13);
            this.providerSiteTextLabel.TabIndex = 4;
            this.providerSiteTextLabel.Text = "Site:";
            // 
            // providerSiteLink
            // 
            this.providerSiteLink.AutoSize = true;
            this.providerSiteLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.providerSiteLink.ForeColor = System.Drawing.Color.Blue;
            this.providerSiteLink.Location = new System.Drawing.Point(256, 340);
            this.providerSiteLink.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.providerSiteLink.Name = "providerSiteLink";
            this.providerSiteLink.Size = new System.Drawing.Size(112, 13);
            this.providerSiteLink.TabIndex = 22;
            this.providerSiteLink.Text = "www.hdgreetings.com";
            this.providerSiteLink.Click += new System.EventHandler(this.providerSiteLink_Click);
            // 
            // providerLabel
            // 
            this.providerLabel.AutoSize = true;
            this.providerLabel.Location = new System.Drawing.Point(282, 323);
            this.providerLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.providerLabel.Name = "providerLabel";
            this.providerLabel.Size = new System.Drawing.Size(35, 13);
            this.providerLabel.TabIndex = 23;
            this.providerLabel.Text = "Name";
            // 
            // singleIPValidLabel
            // 
            this.singleIPValidLabel.AutoSize = true;
            this.singleIPValidLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.singleIPValidLabel.Location = new System.Drawing.Point(323, 37);
            this.singleIPValidLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.singleIPValidLabel.Name = "singleIPValidLabel";
            this.singleIPValidLabel.Size = new System.Drawing.Size(21, 9);
            this.singleIPValidLabel.TabIndex = 24;
            this.singleIPValidLabel.Text = "good";
            // 
            // refreshList_Button
            // 
            this.refreshList_Button.Location = new System.Drawing.Point(455, 318);
            this.refreshList_Button.Margin = new System.Windows.Forms.Padding(2);
            this.refreshList_Button.Name = "refreshList_Button";
            this.refreshList_Button.Size = new System.Drawing.Size(75, 24);
            this.refreshList_Button.TabIndex = 25;
            this.refreshList_Button.Text = "Refresh";
            this.refreshList_Button.UseVisualStyleBackColor = true;
            this.refreshList_Button.Click += new System.EventHandler(this.refreshList_Button_Click);
            // 
            // UnblockAll_Button
            // 
            this.UnblockAll_Button.Location = new System.Drawing.Point(538, 318);
            this.UnblockAll_Button.Margin = new System.Windows.Forms.Padding(2);
            this.UnblockAll_Button.Name = "UnblockAll_Button";
            this.UnblockAll_Button.Size = new System.Drawing.Size(75, 24);
            this.UnblockAll_Button.TabIndex = 26;
            this.UnblockAll_Button.Text = "Unblock All";
            this.UnblockAll_Button.UseVisualStyleBackColor = true;
            this.UnblockAll_Button.Click += new System.EventHandler(this.UnblockAll_Button_Click);
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBar1,
            this.statusBarLabel});
            this.statusBar.Location = new System.Drawing.Point(0, 400);
            this.statusBar.Name = "statusBar";
            this.statusBar.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusBar.Size = new System.Drawing.Size(629, 22);
            this.statusBar.SizingGrip = false;
            this.statusBar.TabIndex = 27;
            this.statusBar.Text = "statusStrip1";
            // 
            // statusBar1
            // 
            this.statusBar1.ForeColor = System.Drawing.Color.Blue;
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Size = new System.Drawing.Size(0, 17);
            // 
            // statusBarLabel
            // 
            this.statusBarLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.statusBarLabel.ForeColor = System.Drawing.Color.Green;
            this.statusBarLabel.Name = "statusBarLabel";
            this.statusBarLabel.Size = new System.Drawing.Size(81, 17);
            this.statusBarLabel.Text = "status label";
            // 
            // providerLinkTextLabel
            // 
            this.providerLinkTextLabel.AutoSize = true;
            this.providerLinkTextLabel.Location = new System.Drawing.Point(232, 358);
            this.providerLinkTextLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.providerLinkTextLabel.Name = "providerLinkTextLabel";
            this.providerLinkTextLabel.Size = new System.Drawing.Size(46, 13);
            this.providerLinkTextLabel.TabIndex = 4;
            this.providerLinkTextLabel.Text = "IP Data:";
            // 
            // providerDataLink
            // 
            this.providerDataLink.AutoSize = true;
            this.providerDataLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.providerDataLink.ForeColor = System.Drawing.Color.Blue;
            this.providerDataLink.Location = new System.Drawing.Point(233, 374);
            this.providerDataLink.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.providerDataLink.Name = "providerDataLink";
            this.providerDataLink.Size = new System.Drawing.Size(111, 13);
            this.providerDataLink.TabIndex = 28;
            this.providerDataLink.Text = "www.hdgreetings.com";
            this.providerDataLink.Click += new System.EventHandler(this.providerDataLink_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 422);
            this.Controls.Add(this.providerDataLink);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.UnblockAll_Button);
            this.Controls.Add(this.refreshList_Button);
            this.Controls.Add(this.singleIPValidLabel);
            this.Controls.Add(this.providerLabel);
            this.Controls.Add(this.providerSiteLink);
            this.Controls.Add(this.updateList_Button);
            this.Controls.Add(this.UnBlockSingleIP_Button);
            this.Controls.Add(this.blockSingleIP_Button);
            this.Controls.Add(this.singleIPTextBox);
            this.Controls.Add(this.blockGroup_Button);
            this.Controls.Add(this.UnBlockGroup_Button);
            this.Controls.Add(this.ipFeedListBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.blockedIPCountLabel);
            this.Controls.Add(this.siteNameLabel);
            this.Controls.Add(this.siteNameLabel2);
            this.Controls.Add(this.blockedIPListText_Label);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.providerLinkTextLabel);
            this.Controls.Add(this.providerSiteTextLabel);
            this.Controls.Add(this.providerTextLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.blockLabel);
            this.Controls.Add(this.metabasePathTextBox);
            this.Controls.Add(this.siteList);
            this.Controls.Add(this.blockedIPListBox);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AppName";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox blockedIPListBox;
        private System.Windows.Forms.ListBox siteList;
        private System.Windows.Forms.TextBox metabasePathTextBox;
        private System.Windows.Forms.Label blockLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItem_Exit;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItem_About;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label siteNameLabel;
        private System.Windows.Forms.Label blockedIPCountLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox ipFeedListBox;
        private System.Windows.Forms.Button UnBlockGroup_Button;
        private System.Windows.Forms.Button blockGroup_Button;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox singleIPTextBox;
        private System.Windows.Forms.Button blockSingleIP_Button;
        private System.Windows.Forms.Button UnBlockSingleIP_Button;
        private System.Windows.Forms.Label blockedIPListText_Label;
        private System.Windows.Forms.Label siteNameLabel2;
        private System.Windows.Forms.Button updateList_Button;
        private System.Windows.Forms.Label providerTextLabel;
        private System.Windows.Forms.Label providerSiteTextLabel;
        private System.Windows.Forms.Label providerSiteLink;
        private System.Windows.Forms.Label providerLabel;
        private System.Windows.Forms.Label singleIPValidLabel;
        private System.Windows.Forms.Button refreshList_Button;
        private System.Windows.Forms.Button UnblockAll_Button;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel statusBar1;
        private System.Windows.Forms.ToolStripStatusLabel statusBarLabel;
        private System.Windows.Forms.Label providerLinkTextLabel;
        private System.Windows.Forms.Label providerDataLink;
        private System.Windows.Forms.ToolStripMenuItem scanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scanEventLog_menuItem;
    }
}

