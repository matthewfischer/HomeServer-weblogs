using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using Microsoft.Win32;
using System.Diagnostics;

namespace HomeServerConsoleTab.WebLogs
{
    public partial class LicenseForm : Form
    {
        public LicenseForm()
        {
            InitializeComponent();
            //richTextBox1.Rtf = Properties.Resources.License;
            richTextBox1.Text = Properties.Resources.License; ;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.radioAccepted.Checked)
            {
                this.DialogResult = DialogResult.OK;
                try
                {
                    MyLogger.DebugLog("License was accepted.");
                    RegistryKey rootKey = Registry.LocalMachine;
                    RegistryKey swKey = rootKey.OpenSubKey(@"SOFTWARE\WebLogsAddIn", true);
                    swKey.SetValue("LicenseAccepted", 33);
                }
                catch (Exception ex)
                {
                    MyLogger.Log(EventLogEntryType.Warning, "Error changing license setting!\n" + ex.Message);
                }
            }
            //license refused
            else
            {
                this.DialogResult = DialogResult.Cancel;
                //do nothing, main form will deal with it
            }
        }
    }
}
