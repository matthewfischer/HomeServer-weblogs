﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using System.Data;
using System.Text;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.HomeServer.Extensibility;
using Microsoft.HomeServer.Controls;
using System.Security.Permissions;
using Microsoft.Win32;


namespace HomeServerConsoleTab.WebLogs
{
    public partial class LogControl : UserControl
    {
        private BindingSource logBinding;
        private LogParser parser = new LogParser();
        private const String LOCALHOST = "127.0.0.1";
        private const String LOCAL_SUBNET = "192.168.";
        private const String EMPTY_USER = "-";
        private const String MEDIA_COLLECTOR_CONF = "/med_agg_cfg.xml";
        private const String whoisServerUrl = "http://ws.arin.net/whois/?queryinput={0}";
        private const String geoUrl = "http://api.hostip.info/get_html.php?ip={0}&position=true";
        private const String dnsUrl = "http://network-tools.com/default.asp?prog=dnsrec&host={0}";
        private const String countText = "Showing {0} logs out of {1} total entries.";
        private const String updatingString = "Updating display, please wait...";
        private const int defaultMaxEntries = 2000;

        private IConsoleServices m_CS;
        private DataSet logEntries;

        private int showCount = 0;

        public LogControl(IConsoleServices cs)
        {
            m_CS = cs;
            try
            {
                RegistryKey rk = Registry.LocalMachine;
                rk = rk.OpenSubKey(@"SOFTWARE\weblogs", false);
                if (rk != null)
                {
                    MyLogger.DebugLevel = (int)rk.GetValue("debug_level");
                }
            }
            catch (Exception e)
            {
                MyLogger.Log(EventLogEntryType.Warning, "Error reading the debug logging level\n\n" + e.Message);
            }

            InitializeComponent();
            textBox1.Text = defaultMaxEntries.ToString();
        }

        private void LoadLogsWorker(object sender, DoWorkEventArgs e)
        {
            MyLogger.DebugLog("worker started");
            logEntries = GetEntriesAsDataSet();
            if (logBinding == null)
            {
                logBinding = new BindingSource(logEntries, "Logs");
            }
            else
            {
                logBinding.DataSource = logEntries;
            }

            DisplayLogs(logBinding);

            //hide or show based on the defaults
            HideOrShowRows();
            MyLogger.DebugLog("worker done");
        }

        private void LoadLogsWorker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            MyLogger.DebugLog("worker completed started");
            toolStripProgressBar1.Visible = false;
            toolStripProgressBar1.Enabled = false;
            consoleToolBarButton1.Enabled = true;
            textBox1.Enabled = true;
            UpdateLogCount();
            dataGridView1.ResumeLayout();
            dataGridView1.ScrollBars = ScrollBars.Vertical;
            this.UseWaitCursor = false;
            this.Cursor = Cursors.Default;
            MyLogger.DebugLog("worker completed done");
        }

        private DateTime ParseIISDateTime(string date, string time)
        {
            //date: 2009-03-08  time: 00:17:07            
            string expectedFormat = "yyyy-MM-dd HH:mm:ss";
            IFormatProvider culture = CultureInfo.CurrentCulture;

            try
            {
                DateTime dt = DateTime.ParseExact(date + " " + time, expectedFormat, culture);
                return dt.ToLocalTime();
            }
            catch (Exception e)
            {
                MyLogger.Log(EventLogEntryType.Warning, "Error parsing IIS date/time to DateTime object.  Date: " + date + "\n"
                    + "Time: " + time + "\n" + e.Message);
                return DateTime.MinValue;
            }
        }

        private void UpdateLogCount()
        {
            toolStripStatusLabel1.Text = String.Format(countText, showCount, dataGridView1.RowCount);
        }

        private void AddTableRow(DataTable table, string[] entry)
        {
            DataRow row = table.NewRow();
            DateTime iis_dt = ParseIISDateTime(entry[(int)IISLog.date], entry[(int)IISLog.time]);

            row["IP"] = entry[(int)IISLog.c_ip];

            if (iis_dt != DateTime.MinValue)
            {
                row["Date"] = iis_dt.ToShortDateString();
                row["Time"] = iis_dt.ToShortTimeString();
            }
            else
            {
                row["Date"] = "unknown";
                row["Time"] = "unknown";
            }
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
            logDataTable.Columns.Add("Date", typeof(string));
            logDataTable.Columns.Add("Time", typeof(string));
            logDataTable.Columns.Add("User", typeof(string));
            logDataTable.Columns.Add("URIStem", typeof(string));

            logData.Tables.Add(logDataTable);

            MyLogger.DebugLog("getting entries in a loop");

            logDataTable.BeginLoadData();
            foreach (string[] s in parser.parseAllLogs(Int32.Parse(textBox1.Text)))
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

        public bool GetHelp()
        {
            return false;
        }

        public void DisplayLogs(BindingSource b)
        {
            this.dataGridView1.DataSource = b;
            this.dataGridView1.Refresh();
        }

        private void LogControl_Load(object sender, EventArgs e)
        {
            LoadLogs();
        }

        /// <summary>
        /// walks the data and hides or shows rows based on the state of the check-boxes.
        /// </summary>
        private void HideOrShowRows()
        {
            showCount = dataGridView1.RowCount;
            MyLogger.DebugLog("RowCount: " + showCount);
            dataGridView1.SuspendLayout();

            CurrencyManager cm = (CurrencyManager)BindingContext[dataGridView1.DataSource];
            cm.SuspendBinding();

            dataGridView1.ClearSelection();  //we can't hide cells that are selected, so clear the selection.

            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                r.Visible = true;  //default to visible
                string ip = r.Cells["IP"].Value.ToString();
                string user = r.Cells["User"].Value.ToString();
                string uri_stem = r.Cells["URIStem"].Value.ToString();

                if ((checkBox1.Checked) && (ip.Equals(LOCALHOST)))
                {
                    showCount--;
                    r.Visible = false;
                    continue;
                }

                if ((checkBox2.Checked) && (ip.StartsWith(LOCAL_SUBNET)))
                {
                    showCount--;
                    r.Visible = false;
                    continue;
                }

                if ((checkBox3.Checked) && (user.Equals(EMPTY_USER)))
                {
                    showCount--;
                    r.Visible = false;
                    continue;
                }

                if ((checkBox4.Checked) && (uri_stem.Equals(MEDIA_COLLECTOR_CONF)))
                {
                    showCount--;
                    r.Visible = false;
                    continue;
                }
            }
            cm.ResumeBinding();
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

        private void dataGridView1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            // Try to sort based on the cells in the current column.
            try
            {

                DateTime dt1 = DateTime.Parse(e.CellValue1, CultureInfo.CurrentCulture);
                DateTime dt2 = DateTime.Parse(e.CellValue2, CultureInfo.CurrentCulture);
            }
            catch (Exception e)
            {
                MyLogger.DebugLog(e.Message);
            }

            e.SortResult = DateTime.Compare(dt1, dt2);
            e.Handled = true;
        }


        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn newColumn = dataGridView1.Columns[e.ColumnIndex];
            DataGridViewColumn oldColumn = dataGridView1.SortedColumn;
            ListSortDirection direction;

            // If oldColumn is null, then the DataGridView is not sorted.
            if (oldColumn != null)
            {
                // Sort the same column again, reversing the SortOrder.
                if (oldColumn == newColumn &&
                    dataGridView1.SortOrder == SortOrder.Ascending)
                {
                    direction = ListSortDirection.Descending;
                }
                else
                {
                    // Sort a new column and remove the old SortGlyph.
                    direction = ListSortDirection.Ascending;
                    oldColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
                }
            }
            else
            {
                direction = ListSortDirection.Ascending;
            }

            if (newColumn.Name == "Date")
            {
                //date sort

            }
            else
            {
                //normal sort
                dataGridView1.Sort(newColumn, direction);
            }
            newColumn.HeaderCell.SortGlyphDirection =
                direction == ListSortDirection.Ascending ?
                SortOrder.Ascending : SortOrder.Descending;
        }
    }
}

