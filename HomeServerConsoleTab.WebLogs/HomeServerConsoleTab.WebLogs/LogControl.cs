using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using System.Data;
using System.Text;
using System.Net;
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
        private const String EMPTY_USER = "-";
        private const String MEDIA_COLLECTOR_CONF = @"/med_agg_cfg.xml";
        private const String MEDIA_COLLECTOR_30_CONF = @"/MediaCollector/*";
        private const String MEDIA_COLLECTOR_PUSH = @"/PushClient/*";
        private const String ROUTER_TEST_INT = @"/router_test_int_*";
        private const String ROUTER_TEST_EXT = @"/router_test_ext_*";
        private const String whoisServerUrl = "http://ws.arin.net/whois/?queryinput={0}";
        private const String geoUrl = "http://api.hostip.info/get_html.php?ip={0}&position=true";
        private const String dnsUrl = "http://network-tools.com/default.asp?prog=dnsrec&host={0}";
        private const String countText = "Showing {0} logs out of {1} total entries.";
        private const String updatingString = "Updating display, please wait...";
        private const int defaultMaxEntries = 500;
        private int MaxEntries = defaultMaxEntries;

        private const string BLOCK_IP = "Block IP";
        private const string UNBLOCK_IP = "Unblock";

        private const int LICENSE_REFUSED = 0;
        private const int LICENSE_ACCEPTED = 33;
        private int licenseAccepted = LICENSE_REFUSED;
        
        private IConsoleServices m_CS;
        private DataSet logEntries = null;

        #region Initialization

        public LogControl(IConsoleServices cs)
        {
            m_CS = cs;
            InitializeComponent();
            ReadRegistryEntries();
            SetupIPBlocking();
            dataGridView1.Columns["Date"].DefaultCellStyle.Format = "G";
        }

        private void ReadRegistryEntries()
        {          
            try
            {
                RegistryKey rk = Registry.LocalMachine;
                rk = rk.OpenSubKey(@"SOFTWARE\WebLogsAddIn", false);

                MyLogger.DebugLevel = (int)rk.GetValue("debug_level");

                MaxEntries = (int)rk.GetValue("DefaultLoadedLogs", MaxEntries);
                textBox1.Text = MaxEntries.ToString();

                licenseAccepted = (int)rk.GetValue("LicenseAccepted", licenseAccepted);                
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

        }

        #endregion

        #region DataHandlerAndLoader

        private void UpdateLogCount()
        {
            toolStripStatusLabel1.Text = String.Format(countText, dataGridView1.RowCount, parser.GetNumberLoaded());
        }

        private void AddTableRow(DataTable table, IISLogEntry entry)
        {
            DataRow row = table.NewRow();
            row["IP"] = entry.c_ip;
            row["Date"] = parser.ParseIISDateTime(entry.date, entry.time);
            row["User"] = entry.cs_username;
            row["URIStem"] = entry.cs_uri_stem;

            if (BlockedIPs.GetInstance().IsThisIPBlocked(entry.c_ip))
            {
                row["Block"] = UNBLOCK_IP;
            }
            else
            {
                row["Block"] = BLOCK_IP;
            }
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
            logDataTable.Columns.Add("Block", typeof(string));

            logData.Tables.Add(logDataTable);

            logDataTable.BeginLoadData();
            foreach (IISLogEntry e in parser.ParseAllLogs(MaxEntries))
            {
                AddTableRow(logDataTable, e);
            }
            logDataTable.EndLoadData();
            return logData;
        }

        private void LoadLogs()
        {
            dataGridView1.SuspendLayout();

            //clear out the datagrid
            if (logEntries != null)
            {
                logEntries.Clear();
            }

            //setup the UI stuff
            toolStripStatusLabel1.Text = "Loading logs, please wait...";
            this.UseWaitCursor = true;
            consoleToolBarButton1.Enabled = false;            
            toolStripProgressBar1.Enabled = true;
            textBox1.Enabled = false;
            toolStripProgressBar1.Visible = true;

            //load the data
            try
            {
                logBinding.SuspendBinding();
                logEntries = GetEntriesAsDataSet();
                logBinding.DataSource = logEntries;
                logBinding.DataMember = "Logs";
                logBinding.ResumeBinding();
                dataGridView1.DataSource = logBinding;
            }

            catch (Exception ex)
            {
                QMessageBox.Show("Error while loading the logs, please report this to the author: matt+whs@mattfischer.com.  Thanks!",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MyLogger.Log(EventLogEntryType.Error, ex);
                return;
            }

            toolStripProgressBar1.Visible = false;
            toolStripProgressBar1.Enabled = false;
            consoleToolBarButton1.Enabled = true;
            textBox1.Enabled = true;
            this.UseWaitCursor = false;
            this.Cursor = Cursors.Default;

            HideOrShowRows();
        }

        private void LogControl_Load(object sender, EventArgs e)
        {
            if (licenseAccepted == LICENSE_REFUSED)
            {
                DialogResult dr = new DialogResult();
                LicenseForm lf = new LicenseForm();
                dr = lf.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    licenseAccepted = LICENSE_ACCEPTED;
                }
            }

            //if it's still false, disable the form, if true, load the logs.
            if (licenseAccepted == LICENSE_ACCEPTED)
            {               
                LoadLogs();
            }
            else
            {
                DisableForm();
            }
        }
        
        #endregion

        #region RowHideShow

        //private void HideLocalNetworkRows()
        //{
        //    int i=0;
        //    logBinding.SuspendBinding();
        //    foreach (DataGridViewRow r in logEntries.Tables["Logs"].Rows)
        //    {
        //        i++;
        //        try
        //        {
        //            string ip = r.Cells["IP"].ToString();
        //            if (ip.Equals(LOCALHOST))
        //            {
        //                MyLogger.DebugLog("IP = Localhost");
        //                continue;
        //            }
        //            else if (!IPAddressExtensions.GetInstance().IsLocalAddress(IPAddress.Parse(ip)))
        //            {
        //                MyLogger.DebugLog("IP " + ip + " is on local subnet");
        //                //dataGridView1.Rows[i].Visible = false;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MyLogger.DebugLog(ex.Message);
        //        }
        //    }
        //    logBinding.ResumeBinding();
        //    dataGridView1.Refresh();
        //}

        private void MarkIPButtons()
        {
            logBinding.SuspendBinding();

            foreach (DataRow r in logEntries.Tables["Logs"].Rows)
            {
                //set the button name.              
                if (BlockedIPs.GetInstance().IsThisIPBlocked(r["IP"].ToString()) == true)
                {
                    r["Block"] = UNBLOCK_IP;
                }
                else
                {
                    r["Block"] = BLOCK_IP;
                }
            }
            logBinding.ResumeBinding();
            dataGridView1.Refresh();
        }

        private void HideOrShowRows()
        {
            HideOrShowRows(null, EventArgs.Empty);
        }

        // walks the data and hides or shows rows based on the state of the check-boxes.
        private void HideOrShowRows(object sender, EventArgs e)
        {
            string filter = "";
            bool firstClause = false;

            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            dataGridView1.SuspendLayout();

            CurrencyManager cm = (CurrencyManager)BindingContext[dataGridView1.DataSource];
            cm.SuspendBinding();

            dataGridView1.ClearSelection();  //we can't hide cells that are selected, so clear the selection.

            toolStripStatusLabel1.Text = updatingString;

            if (localhostCheckBox.Checked)
            {
                filter += "IP <> '" + LOCALHOST + "'";
                firstClause = true;
            }
            if (anonymousCheckBox.Checked)
            {
                if (firstClause == true) {
                    filter += " AND ";
                }
                else {
                    firstClause = true;
                }
                filter += "USER <> '" + EMPTY_USER + "'";
            }
            if (mediaCollectorCheckBox.Checked)
            {
                if (firstClause == true)
                {
                    filter += " AND ";
                }
                else
                {
                    firstClause = true;
                }
                filter += "URIStem not like '" + MEDIA_COLLECTOR_CONF + "' AND URIStem not like '" + MEDIA_COLLECTOR_30_CONF 
                    + "' AND URIStem not like '" + MEDIA_COLLECTOR_PUSH + "'";
            }
            if (hideRouterTests.Checked)
            {
                if (firstClause == true)
                {
                    filter += " AND ";
                }
                else
                {
                    firstClause = true;
                }
                filter += "URIStem not like '" + ROUTER_TEST_EXT + "' AND URIStem not like '" + ROUTER_TEST_INT + "'";
            }

            MyLogger.DebugLog("filter = " + filter);
            logBinding.Filter = filter;

            //if (localNetworkCheckBox.Checked)
            //{
            //    HideLocalNetworkRows();
            //}
           
            cm.ResumeBinding();
            dataGridView1.ResumeLayout();
            dataGridView1.Refresh();

            UpdateLogCount();

            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
        }
       
        #endregion       

        #region ClickHandlers

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //ignore row header clicks
            if (e.RowIndex < 0)
            {
                return;
            }

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
            else if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == BLOCK_IP)
            {
                if (BlockedIPs.GetInstance().BlockIP(dataGridView1.Rows[e.RowIndex].Cells["IP"].Value.ToString()))
                {
                    MarkIPButtons();
                }
            }
            else if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == UNBLOCK_IP)
            {
                if (BlockedIPs.GetInstance().UnblockIP(dataGridView1.Rows[e.RowIndex].Cells["IP"].Value.ToString()))
                {
                    MarkIPButtons();
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
            MarkIPButtons();
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
                int val = Int32.Parse(textBox1.Text);
                MaxEntries = val;
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

        private void DisableForm()
        {
            if (licenseAccepted == LICENSE_ACCEPTED)
            {
                this.Enabled = true;
            }
            else
            {
                this.Enabled = false;
            }
        }
    }
}

