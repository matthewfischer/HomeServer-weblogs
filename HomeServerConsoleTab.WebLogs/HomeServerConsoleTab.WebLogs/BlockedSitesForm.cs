using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.HomeServer.Controls;
using IISIP;

namespace HomeServerConsoleTab.WebLogs
{
    public partial class BlockedSitesForm : Form
    {       
        private DataSet ipDataSet;
        private BindingSource ipBinding;
        
        public BlockedSitesForm()
        {
            InitializeComponent();          
        }

        private void InitializeData()
        {
            if (BlockedIPs.GetInstance().BlockedSites.Count == 0)
            {
                toolStripStatusLabel1.Text = "No sites currently blocked.";

                if (ipDataSet != null)
                {
                    ipDataSet.Clear();
                }
                this.dataGridView1.Refresh();

                this.unblockAllButton.Enabled = false;
                //this.removeDupesButton.Enabled = false;
            }
            else
            {
                this.dataGridView1.SuspendLayout();
                ipDataSet = BuildDataTable(BlockedIPs.GetInstance().BlockedSites);
                if (ipBinding == null)
                {
                    ipBinding = new BindingSource(ipDataSet, "IP");
                }
                else
                {
                    ipBinding.DataSource = ipDataSet;
                }
                DisplayBlocks(ipBinding);
                this.dataGridView1.ResumeLayout();
                
                toolStripStatusLabel1.Text = "Currently blocking " + BlockedIPs.GetInstance().BlockedSites.Count + " sites.";
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
            BlockedIPs.GetInstance().UnblockAllIPs();
            InitializeData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {          
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Unblock")
            {
                BlockedIPs.GetInstance().UnblockIP(dataGridView1.Rows[e.RowIndex].Cells["IP"].Value.ToString());
                InitializeData();
            }
        }

        private void removeDupesButton_Click(object sender, EventArgs e)
        {
            int dupes = BlockedIPs.GetInstance().RemoveDupes();
            QMessageBox.Show("Removed " + dupes + " duplicate entries.", 
                "Web Logs", MessageBoxButtons.OK, MessageBoxIcon.Error);
            InitializeData();
        }

        private void BlockedSitesForm_Load(object sender, EventArgs e)
        {
            InitializeData();
        }
    }
}
