using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IISIP;

namespace HomeServerConsoleTab.WebLogs
{
    public partial class BlockedSites : Form
    {
        IIsWebSite site;
        private DataSet ipDataSet;
        private BindingSource ipBinding;
        private List<IPAddressV4> blockedSites;

        public BlockedSites(IIsWebSite ws)
        {
            InitializeComponent();
            site = ws;
            InitializeData();
        }

        private void InitializeData()
        {
            blockedSites = site.GetBlockedIpAddresses();

            if (blockedSites.Count == 0)
            {
                toolStripStatusLabel1.Text = "No sites currently blocked.";
                this.unblockAllButton.Enabled = false;
            }
            else
            {
                ipDataSet = BuildDataTable(blockedSites);
                if (ipBinding == null)
                {
                    ipBinding = new BindingSource(ipDataSet, "IP");
                }
                else
                {
                    ipBinding.DataSource = ipDataSet;
                }
                DisplayBlocks(ipBinding);
                toolStripStatusLabel1.Text = "Currently blocking " + blockedSites.Count + " sites.";
            }    
        }

        private DataSet BuildDataTable(List<IPAddressV4> blocks)
        {
            ipDataSet = new DataSet();
            ipDataSet.Tables.Clear();

            DataTable ipDataTable = new DataTable("IP");
            ipDataTable.Columns.Add("IP", typeof(string));
            ipDataSet.Tables.Add(ipDataTable);

            MyLogger.DebugLog("getting ip blocks in a loop");

            ipDataTable.BeginLoadData();
            foreach (IPAddressV4 ip in blocks)
            {
                DataRow row = ipDataTable.NewRow();
                row["IP"] = ip.Address.ToString();
                ipDataTable.Rows.Add(row);
            }
            ipDataTable.EndLoadData();

            return ipDataSet;
        }

        private void DisplayBlocks(BindingSource b)
        {
            this.dataGridView1.DataSource = b;
            this.dataGridView1.Refresh();
        }

        private void okayButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void unblockAllButton_Click(object sender, EventArgs e)
        {
            site.UnBlockAllIpAddresses();
            InitializeData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Unblock")
            {
                site.UnBlockIpAddress(new IPAddressV4(dataGridView1.Rows[e.RowIndex].Cells["IP"].Value.ToString()));
                InitializeData();
            }
        }
    }
}
