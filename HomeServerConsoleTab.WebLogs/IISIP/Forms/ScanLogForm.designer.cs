namespace IISIP
{
    partial class ScanLogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanLogForm));
            this.label1 = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.eventTextBox = new System.Windows.Forms.TextBox();
            this.textToFind = new System.Windows.Forms.TextBox();
            this.find_Button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.statusBar1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBarLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ignoreCaseCheckBox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.extract_ComboBox = new System.Windows.Forms.ComboBox();
            this.resultsLabel = new System.Windows.Forms.Label();
            this.extractedItem_ListBox = new System.Windows.Forms.ListBox();
            this.copyItems_Button = new System.Windows.Forms.Button();
            this.removeItem_Button = new System.Windows.Forms.Button();
            this.clearLog_Button = new System.Windows.Forms.Button();
            this.copyItem_Button = new System.Windows.Forms.Button();
            this.blockList_Button = new System.Windows.Forms.Button();
            this.eventColor = new System.Windows.Forms.Panel();
            this.locateIP_Button = new System.Windows.Forms.Button();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(89, 77);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Extracted items:";
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.BackColor = System.Drawing.SystemColors.Control;
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(543, 321);
            this.closeButton.Margin = new System.Windows.Forms.Padding(2);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(73, 23);
            this.closeButton.TabIndex = 6;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // eventTextBox
            // 
            this.eventTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.eventTextBox.Location = new System.Drawing.Point(217, 93);
            this.eventTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.eventTextBox.Multiline = true;
            this.eventTextBox.Name = "eventTextBox";
            this.eventTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.eventTextBox.Size = new System.Drawing.Size(400, 217);
            this.eventTextBox.TabIndex = 6;
            // 
            // textToFind
            // 
            this.textToFind.Location = new System.Drawing.Point(408, 22);
            this.textToFind.Margin = new System.Windows.Forms.Padding(2);
            this.textToFind.Name = "textToFind";
            this.textToFind.Size = new System.Drawing.Size(142, 20);
            this.textToFind.TabIndex = 1;
            // 
            // find_Button
            // 
            this.find_Button.BackColor = System.Drawing.SystemColors.Control;
            this.find_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.find_Button.Location = new System.Drawing.Point(6, 15);
            this.find_Button.Margin = new System.Windows.Forms.Padding(2);
            this.find_Button.Name = "find_Button";
            this.find_Button.Size = new System.Drawing.Size(87, 30);
            this.find_Button.TabIndex = 3;
            this.find_Button.Text = "Find";
            this.find_Button.UseVisualStyleBackColor = false;
            this.find_Button.Click += new System.EventHandler(this.find_Button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(408, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Filter events containing text:";
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBar1,
            this.statusBarLabel});
            this.statusBar.Location = new System.Drawing.Point(0, 349);
            this.statusBar.Name = "statusBar";
            this.statusBar.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusBar.Size = new System.Drawing.Size(624, 22);
            this.statusBar.SizingGrip = false;
            this.statusBar.TabIndex = 7;
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
            this.statusBarLabel.BackColor = System.Drawing.SystemColors.Control;
            this.statusBarLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.statusBarLabel.ForeColor = System.Drawing.Color.Green;
            this.statusBarLabel.Name = "statusBarLabel";
            this.statusBarLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // ignoreCaseCheckBox
            // 
            this.ignoreCaseCheckBox.AutoSize = true;
            this.ignoreCaseCheckBox.Checked = true;
            this.ignoreCaseCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ignoreCaseCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ignoreCaseCheckBox.ForeColor = System.Drawing.Color.White;
            this.ignoreCaseCheckBox.Location = new System.Drawing.Point(409, 45);
            this.ignoreCaseCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.ignoreCaseCheckBox.Name = "ignoreCaseCheckBox";
            this.ignoreCaseCheckBox.Size = new System.Drawing.Size(83, 17);
            this.ignoreCaseCheckBox.TabIndex = 2;
            this.ignoreCaseCheckBox.Text = "Ignore Case";
            this.ignoreCaseCheckBox.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(110, 7);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Search and extract:";
            // 
            // extract_ComboBox
            // 
            this.extract_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.extract_ComboBox.FormattingEnabled = true;
            this.extract_ComboBox.Location = new System.Drawing.Point(112, 22);
            this.extract_ComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.extract_ComboBox.Name = "extract_ComboBox";
            this.extract_ComboBox.Size = new System.Drawing.Size(268, 21);
            this.extract_ComboBox.TabIndex = 8;
            this.extract_ComboBox.SelectedIndexChanged += new System.EventHandler(this.extract_ComboBox_SelectedIndexChanged);
            // 
            // resultsLabel
            // 
            this.resultsLabel.AutoSize = true;
            this.resultsLabel.ForeColor = System.Drawing.Color.White;
            this.resultsLabel.Location = new System.Drawing.Point(234, 77);
            this.resultsLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.resultsLabel.Name = "resultsLabel";
            this.resultsLabel.Size = new System.Drawing.Size(73, 13);
            this.resultsLabel.TabIndex = 4;
            this.resultsLabel.Text = "Events found:";
            // 
            // extractedItem_ListBox
            // 
            this.extractedItem_ListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.extractedItem_ListBox.FormattingEnabled = true;
            this.extractedItem_ListBox.IntegralHeight = false;
            this.extractedItem_ListBox.Location = new System.Drawing.Point(92, 93);
            this.extractedItem_ListBox.Margin = new System.Windows.Forms.Padding(2);
            this.extractedItem_ListBox.Name = "extractedItem_ListBox";
            this.extractedItem_ListBox.Size = new System.Drawing.Size(117, 217);
            this.extractedItem_ListBox.Sorted = true;
            this.extractedItem_ListBox.TabIndex = 9;
            this.extractedItem_ListBox.SelectedIndexChanged += new System.EventHandler(this.ipListBox_SelectedIndexChanged);
            // 
            // copyItems_Button
            // 
            this.copyItems_Button.BackColor = System.Drawing.SystemColors.Control;
            this.copyItems_Button.Location = new System.Drawing.Point(10, 222);
            this.copyItems_Button.Margin = new System.Windows.Forms.Padding(2);
            this.copyItems_Button.Name = "copyItems_Button";
            this.copyItems_Button.Size = new System.Drawing.Size(75, 23);
            this.copyItems_Button.TabIndex = 10;
            this.copyItems_Button.Text = "&Copy List";
            this.copyItems_Button.UseVisualStyleBackColor = false;
            this.copyItems_Button.Click += new System.EventHandler(this.copyItems_Button_Click);
            // 
            // removeItem_Button
            // 
            this.removeItem_Button.BackColor = System.Drawing.SystemColors.Control;
            this.removeItem_Button.Location = new System.Drawing.Point(10, 181);
            this.removeItem_Button.Name = "removeItem_Button";
            this.removeItem_Button.Size = new System.Drawing.Size(75, 23);
            this.removeItem_Button.TabIndex = 11;
            this.removeItem_Button.Text = "Hide Item";
            this.removeItem_Button.UseVisualStyleBackColor = false;
            this.removeItem_Button.Click += new System.EventHandler(this.removeItem_Button_Click);
            // 
            // clearLog_Button
            // 
            this.clearLog_Button.BackColor = System.Drawing.SystemColors.Control;
            this.clearLog_Button.Location = new System.Drawing.Point(309, 48);
            this.clearLog_Button.Name = "clearLog_Button";
            this.clearLog_Button.Size = new System.Drawing.Size(71, 22);
            this.clearLog_Button.TabIndex = 12;
            this.clearLog_Button.Text = "Clear Log...";
            this.clearLog_Button.UseVisualStyleBackColor = false;
            this.clearLog_Button.Click += new System.EventHandler(this.clearLog_Button_Click);
            // 
            // copyItem_Button
            // 
            this.copyItem_Button.BackColor = System.Drawing.SystemColors.Control;
            this.copyItem_Button.Location = new System.Drawing.Point(10, 117);
            this.copyItem_Button.Name = "copyItem_Button";
            this.copyItem_Button.Size = new System.Drawing.Size(75, 23);
            this.copyItem_Button.TabIndex = 13;
            this.copyItem_Button.Text = "Copy IP";
            this.copyItem_Button.UseVisualStyleBackColor = false;
            this.copyItem_Button.Click += new System.EventHandler(this.copyItem_Button_Click);
            // 
            // blockList_Button
            // 
            this.blockList_Button.BackColor = System.Drawing.SystemColors.Control;
            this.blockList_Button.Enabled = false;
            this.blockList_Button.Location = new System.Drawing.Point(10, 251);
            this.blockList_Button.Name = "blockList_Button";
            this.blockList_Button.Size = new System.Drawing.Size(75, 23);
            this.blockList_Button.TabIndex = 14;
            this.blockList_Button.Text = "Block List...";
            this.blockList_Button.UseVisualStyleBackColor = false;
            // 
            // eventColor
            // 
            this.eventColor.Location = new System.Drawing.Point(218, 77);
            this.eventColor.Name = "eventColor";
            this.eventColor.Size = new System.Drawing.Size(14, 14);
            this.eventColor.TabIndex = 15;
            // 
            // locateIP_Button
            // 
            this.locateIP_Button.BackColor = System.Drawing.SystemColors.Control;
            this.locateIP_Button.Location = new System.Drawing.Point(10, 145);
            this.locateIP_Button.Name = "locateIP_Button";
            this.locateIP_Button.Size = new System.Drawing.Size(75, 23);
            this.locateIP_Button.TabIndex = 16;
            this.locateIP_Button.Text = "Locate IP";
            this.locateIP_Button.UseVisualStyleBackColor = false;
            this.locateIP_Button.Click += new System.EventHandler(this.locateIP_Button_Click);
            // 
            // ScanLogForm
            // 
            this.AcceptButton = this.find_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(624, 371);
            this.Controls.Add(this.locateIP_Button);
            this.Controls.Add(this.eventColor);
            this.Controls.Add(this.blockList_Button);
            this.Controls.Add(this.copyItem_Button);
            this.Controls.Add(this.clearLog_Button);
            this.Controls.Add(this.removeItem_Button);
            this.Controls.Add(this.copyItems_Button);
            this.Controls.Add(this.extractedItem_ListBox);
            this.Controls.Add(this.extract_ComboBox);
            this.Controls.Add(this.ignoreCaseCheckBox);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.find_Button);
            this.Controls.Add(this.textToFind);
            this.Controls.Add(this.eventTextBox);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.resultsLabel);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ScanLogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search Event Log";
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.TextBox eventTextBox;
        private System.Windows.Forms.TextBox textToFind;
        private System.Windows.Forms.Button find_Button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel statusBar1;
        private System.Windows.Forms.ToolStripStatusLabel statusBarLabel;
        private System.Windows.Forms.CheckBox ignoreCaseCheckBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox extract_ComboBox;
        private System.Windows.Forms.Label resultsLabel;
        private System.Windows.Forms.ListBox extractedItem_ListBox;
        private System.Windows.Forms.Button copyItems_Button;
        private System.Windows.Forms.Button removeItem_Button;
        private System.Windows.Forms.Button clearLog_Button;
        private System.Windows.Forms.Button copyItem_Button;
        private System.Windows.Forms.Button blockList_Button;
        private System.Windows.Forms.Panel eventColor;
        private System.Windows.Forms.Button locateIP_Button;
    }
}