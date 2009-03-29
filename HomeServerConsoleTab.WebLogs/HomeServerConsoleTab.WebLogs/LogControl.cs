using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Microsoft.HomeServer.Extensibility;
using Microsoft.HomeServer.Controls;
using System.Security.Permissions;
using Microsoft.Win32;
using IISIP;


namespace HomeServerConsoleTab.WebLogs
{
    public partial class LogControl : UserControl
    {
        private BindingSource logBinding = new BindingSource();
        private LogParser parser = new LogParser();
        public static String LOCALHOST = "127.0.0.1";
        public static String LOCAL_SUBNET = "192.168.";
        private const String EMPTY_USER = "-";
        private const String MEDIA_COLLECTOR_CONF = "/med_agg_cfg.xml";
        private const String whoisServerUrl = "http://ws.arin.net/whois/?queryinput={0}";
        private const String geoUrl = "http://api.hostip.info/get_html.php?ip={0}&position=true";
        private const String dnsUrl = "http://network-tools.com/default.asp?prog=dnsrec&host={0}";
        private const String countText = "Showing {0} logs out of {1} total entries.";
        private const String updatingString = "Updating display, please wait...";
        private const int defaultMaxEntries = 500;
        private int MaxEntries = defaultMaxEntries;
        
        private IConsoleServices m_CS;
        private DataSet logEntries = null;
        private int showCount = 0;

        public LogControl(IConsoleServices cs)
        {
            m_CS = cs;
            ReadRegistryEntries();
            InitializeComponent();
            SetupIPBlocking();
        }

        private void ReadRegistryEntries()
        {          
            try
            {
                RegistryKey rk = Registry.LocalMachine;
                rk = rk.OpenSubKey(@"SOFTWARE\WebLogsAddIn", false);
                MyLogger.DebugLevel = (int)rk.GetValue("debug_level");
                MaxEntries = (int)rk.GetValue("DefaultLoadedLogs", MaxEntries);
            }
            catch (Exception e)
            {
                MyLogger.DebugLevel = 0;
                MaxEntries = defaultMaxEntries;
                MyLogger.Log(EventLogEntryType.Warning, "Error reading the debug logging level\n\n" + e.Message);
            }
        }

        private void SetupIPBlocking()
        {           
            if (BlockedIPs.GetInstance().RootSite == null)
            {
                blockList.Enabled = false;
                DisableBlockButtons();
            }
        }

        private void DisableBlockButtons()
        {
            //dataGridView1.Columns["Block"].S
        }

        #region DataHandlerAndLoader

        private void LoadLogsWorker(object sender, DoWorkEventArgs e)
        {
            MyLogger.DebugLog("worker started");
            try
            {              
                logBinding.SuspendBinding();
                logEntries = GetEntriesAsDataSet();
                logBinding.DataSource = logEntries;
                logBinding.DataMember = "Logs";
                logBinding.ResumeBinding();
                dataGridView1.DataSource = logBinding;               

                //hide or show based on the defaults
                HideOrShowRows();
            }

            catch (Exception ex)
            {
                QMessageBox.Show("Error while loading the logs, please report this to the author: matt@mattfischer.com.  Thanks!",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MyLogger.Log(EventLogEntryType.Error, ex);
                return;
            }
            MyLogger.DebugLog("worker done");
        }

        private void LoadLogsWorker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {        
            dataGridView1.ScrollBars = ScrollBars.Vertical;
            dataGridView1.ResumeLayout();
            dataGridView1.Refresh();
            toolStripProgressBar1.Visible = false;
            toolStripProgressBar1.Enabled = false;
            consoleToolBarButton1.Enabled = true;
            textBox1.Enabled = true;
                        
            this.UseWaitCursor = false;
            this.Cursor = Cursors.Default;

            UpdateLogCount();
        }

        private void UpdateLogCount()
        {
            toolStripStatusLabel1.Text = String.Format(countText, showCount, dataGridView1.RowCount);
        }

        private void AddTableRow(DataTable table, string[] entry)
        {
            DataRow row = table.NewRow();
            row["IP"] = entry[(int)IISLog.c_ip];
            row["Date"] = parser.ParseIISDateTime(entry[(int)IISLog.date], entry[(int)IISLog.time]);
            row["User"] = entry[(int)IISLog.cs_username];
            row["URIStem"] = entry[(int)IISLog.cs_uri_stem];
            table.Rows.Add(row);
        }

        private DataSet GetEntriesAsDataSet()
        {
            DataSet logData = new DataSet();
            logData.Tables.Clear();

            DataTable logDataTable = new DataTable("Logs");

            logDataTable.Columns.Add("IP", typeof(string));
            logDataTable.Columns.Add("Date", typeof(DateTime));
            logDataTable.Columns.Add("User", typeof(string));
            logDataTable.Columns.Add("URIStem", typeof(string));

            logData.Tables.Add(logDataTable);

            MyLogger.DebugLog("getting entries in a loop");

            logDataTable.BeginLoadData();
            foreach (string[] s in parser.ParseAllLogs(MaxEntries))
            {
                AddTableRow(logDataTable, s);
            }
            logDataTable.EndLoadData();
            return logData;
        }

        private void LoadLogs()
        {
            //setup background worker
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += new DoWorkEventHandler(LoadLogsWorker);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(LoadLogsWorker_Completed);

            //clear out the datagrid
            if (logEntries != null)
            {
                logEntries.Clear();
            }

            //setup the UI stuff
            toolStripStatusLabel1.Text = "Loading logs, please wait...";
            this.UseWaitCursor = true;
            consoleToolBarButton1.Enabled = false;
            dataGridView1.SuspendLayout();
            toolStripProgressBar1.Enabled = true;
            textBox1.Enabled = false;
            toolStripProgressBar1.Visible = true;
            dataGridView1.ScrollBars = ScrollBars.None;

            //go do stuff
            backgroundWorker.RunWorkerAsync();
        }

        //unused!
        //private void SaveSettingsToRegistry(int numberOfLogs)
        //{
        //    try
        //    {
        //        RegistryKey HKLMKey = Registry.LocalMachine;
        //        RegistryKey SoftwareKey = HKLMKey.OpenSubKey("SOFTWARE");
        //        RegistryKey WebLogsKey = HKLMKey.OpenSubKey("WebLogsAddIn");
        //        if (WebLogsKey == null)
        //        {
        //            WebLogsKey = SoftwareKey.CreateSubKey("WebLogsAddIn");
        //            WebLogsKey.SetValue("DefaultLoadedLogs", defaultMaxEntries);
        //        }
        //        else
        //        {
        //            MyLogger.DebugLog("No keys to set!");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        MyLogger.Log(EventLogEntryType.Error, e);
        //    }
        //}

        private void LogControl_Load(object sender, EventArgs e)
        {
            textBox1.Text = defaultMaxEntries.ToString();
            dataGridView1.Columns["Date"].DefaultCellStyle.Format = "G";
            LoadLogs();
        }
        
        #endregion

        #region RowHideShow

        //called after the block list form closes to refresh the form
        private void ChangeButtonNames() 
        {
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                string ip = r.Cells["IP"].Value.ToString();

                //set the button name.
                if (BlockedIPs.GetInstance().IsThisIPBlocked(ip) == true)
                {
                    (r.Cells["Block"] as DataGridViewButtonCell).UseColumnTextForButtonValue = false;
                    (r.Cells["Block"] as DataGridViewButtonCell).Value = "Unblock";
                }
                else
                {
                    (r.Cells["Block"] as DataGridViewButtonCell).UseColumnTextForButtonValue = true;
                }
            }
        }

        /// <summary>
        /// walks the data and hides or shows rows based on the state of the check-boxes.
        /// </summary>
        private void HideOrShowRows()
        {
            showCount = dataGridView1.RowCount;
            dataGridView1.SuspendLayout();

            CurrencyManager cm = (CurrencyManager)BindingContext[dataGridView1.DataSource];
            cm.SuspendBinding();

            dataGridView1.ClearSelection();  //we can't hide cells that are selected, so clear the selection.

            foreach (DataGridViewRow r in dataGridView1.Rows)
            {               
                string ip = r.Cells["IP"].Value.ToString();

                //set the button name.
                if (BlockedIPs.GetInstance().IsThisIPBlocked(ip) == true)
                {
                    (r.Cells["Block"] as DataGridViewButtonCell).UseColumnTextForButtonValue = false;
                    (r.Cells["Block"] as DataGridViewButtonCell).Value = "Unblock";
                }

                string user = r.Cells["User"].Value.ToString();
                string uristem = r.Cells["URIStem"].Value.ToString();
  
                if ((checkBox1.Checked) && (ip.Equals(LOCALHOST)))
                {
                    showCount--;
                    r.Visible = false;
                    continue;
                }

                else if ((checkBox2.Checked) && (ip.StartsWith(LOCAL_SUBNET)))
                {
                    showCount--;
                    r.Visible = false;
                    continue;
                }

                else if ((checkBox3.Checked) && (user.Equals(EMPTY_USER)))
                {
                    showCount--;
                    r.Visible = false;
                    continue;
                }

                else if ((checkBox4.Checked) && (uristem.Equals(MEDIA_COLLECTOR_CONF)))
                {
                    showCount--;
                    r.Visible = false;
                    continue;
                }
                else
                {
                    r.Visible = true;  //default to visible
                }
            }
            
            cm.ResumeBinding();
            dataGridView1.ResumeLayout();
            UpdateLogCount();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = updatingString;
            //hide or unhide localhost
            HideOrShowRows();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = updatingString;
            //hide or unhide private subnet (192.168)
            HideOrShowRows();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = updatingString;
            //hide or show anonymous users (blank username)
            HideOrShowRows();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = updatingString;
            //hide or show media collector requests
            HideOrShowRows();
        }
        
        #endregion       

        #region ClickHandlers

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "IPWhoIs")
            {
                OpenWebpage(String.Format(whoisServerUrl, dataGridView1.Rows[e.RowIndex].Cells["IP"].Value));
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "geoIP")
            {
                OpenWebpage(String.Format(geoUrl, dataGridView1.Rows[e.RowIndex].Cells["IP"].Value));
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "dns")
            {
                OpenWebpage(String.Format(dnsUrl, dataGridView1.Rows[e.RowIndex].Cells["IP"].Value));
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "Block")
            {
                if (BlockedIPs.GetInstance().BlockIP(dataGridView1.Rows[e.RowIndex].Cells["IP"].Value.ToString()))
                {
                    (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell).UseColumnTextForButtonValue = false;
                    (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell).Value = "Unblock";
                }
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "Unblock")
            {
                if (BlockedIPs.GetInstance().UnblockIP(dataGridView1.Rows[e.RowIndex].Cells["IP"].Value.ToString()))
                {
                    (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell).UseColumnTextForButtonValue = true;
                }
            }
        }

        //reload
        private void consoleToolBarButton1_Click(object sender, EventArgs e)
        {
            LoadLogs();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadLogs();
            }
        }

        private void blockList_Click(object sender, EventArgs e)
        {
            BlockedSitesForm bs = new BlockedSitesForm();
            bs.ShowDialog();
            ChangeButtonNames();
        }

        #endregion

        private void OpenWebpage(string url)
        {
            try
            {
                m_CS.OpenUrl(url);
            }
            catch (Exception e)
            {
                MyLogger.Log(EventLogEntryType.Error, "WebLogs is unable to open the URL.  Full error:\n" + e.Message);
                QMessageBox.Show("WebLogs is unable to open the URL for some reason.  If the issue continues, please contact the developer.", "Error");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MaxEntries = Int32.Parse(textBox1.Text);
                MyLogger.DebugLog("MaxEntries changed to: " + MaxEntries);
                RegistryKey rootKey = Registry.LocalMachine;
                RegistryKey swKey = rootKey.OpenSubKey(@"SOFTWARE\WebLogsAddIn", true);              
                swKey.SetValue("DefaultLoadedLogs", MaxEntries);
            }
            catch (Exception ex)
            {
                MyLogger.Log(EventLogEntryType.Warning, ex);
                MaxEntries = defaultMaxEntries;
            }
        }
    }
}

