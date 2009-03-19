//-----------------------------------------------------------------
// Copyright Lee Whitney 2008-2009
// http://www.hdgreetings.com/other/Block-IP-IIS
// Feel free to use and modify, please just keep this copyright in place.
//-----------------------------------------------------------------
using System;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IISIP
{
    public partial class MainForm : Form
    {
        private bool _updatingUI;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Text = App.Name;
            statusBarLabel.Text = "";
            ValidateUI();

            try
            {
                //
                // Display list of web sites on this server
                //
                var iisWebSiteList = IISMetaBase.GetWebSites();
                foreach (var site in iisWebSiteList)
                {
                    siteList.Items.Add(site);
                    site.StatusUpdate += new IIsWebSite.StatusUpdateEventHandler(ipFeed_StatusUpdate);
                }
                //
                // Display list of ip list web sources on this server
                //
                var ipFeedList = IPFeed.FromFolder(App.IPFeedFolder);
                foreach (var ipFeed in ipFeedList)
                {
                    ipFeedListBox.Items.Add(ipFeed);
                    ipFeed.StatusUpdate += new IPFeed.StatusUpdateEventHandler(ipFeed_StatusUpdate);
                }

                siteList.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                DisplayError(exp);
            }
        }

        void ipFeed_StatusUpdate(string status)
        {
            ShowStatus(status);
        }

        private void ReloadIpDisplayList(IIsWebSite iisWebSite)
        {
            ReloadIpDisplayList(iisWebSite, -1);
        }

        private void ReloadIpDisplayList(IIsWebSite iisWebSite, int knownCount)
        {
            if (knownCount == -1)
                ShowStatus("Loading IPs...");
            else
                ShowStatus("Loading " + knownCount + " IPs...");

            var blockedIPList = iisWebSite.GetBlockedIpAddresses();
            int count = blockedIPList.Count;

            // Sorting takes a couple seconds so leave out for now
            //ShowStatus("Sorting " + count + " IPs...");            
            //blockedIPList.Sort();

            ShowStatus("Displaying " + count + " IPs...");
            var sb = new StringBuilder();
            foreach (var ip in blockedIPList)
            {
                if (ip.Mask == "255.255.255.255")
                    sb.AppendLine(ip.Address);
                else
                    sb.AppendLine(ip.Address + " (" + ip.Mask + ")");
            }

            blockedIPCountLabel.Text = blockedIPList.Count.ToString("n0");
            blockedIPListBox.Text = sb.ToString();
            ShowStatus();
        }

        private void ShowStatus()
        {
            ShowStatus(string.Empty);
        }

        private void ShowStatus(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                statusBarLabel.Text = "";
                Application.DoEvents();
            }
            else
            {
                statusBarLabel.Text = message;
                Application.DoEvents();
            }
        }

        private void siteList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateUI();
        }

        private void menuItem_About_Click(object sender, EventArgs e)
        {
            var aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void menuItem_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void providerSiteLink_Click(object sender, EventArgs e)
        {
            try
            {
                var ipFeed = (IPFeed) ipFeedListBox.SelectedItem;
                if (ipFeed != null)
                    Process.Start(ipFeed.ProviderWebSite);
            }
            catch (Exception exp)
            {
                DisplayError(exp);
            }
        }

        private void ipFeedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateUI();
        }

        private void ValidateUI()
        {
            _updatingUI.ToString();
            _updatingUI = true;

            var currentWebSite = (IIsWebSite) siteList.SelectedItem;
            if (currentWebSite != null)
            {
                metabasePathTextBox.Text = currentWebSite.MetaBasePath;
                siteNameLabel.Text = currentWebSite.SiteName;
                siteNameLabel2.Text = currentWebSite.SiteName;

                //updateList_Button.Enabled = true;
                refreshList_Button.Enabled = true;
                blockedIPListBox.Enabled = true;
                blockedIPListText_Label.Enabled = true;
                ReloadIpDisplayList(currentWebSite);

                blockSingleIP_Button.Enabled = true;
                UnBlockSingleIP_Button.Enabled = true;
                singleIPTextBox.Enabled = true;
            }
            else
            {
                metabasePathTextBox.Text = "-";
                siteNameLabel.Text = "-";
                siteNameLabel2.Text = "-";

                //updateList_Button.Enabled = false;
                refreshList_Button.Enabled = false;
                blockedIPListText_Label.Enabled = false;
                blockedIPListBox.Text = "";
                blockedIPListBox.Enabled = false;
            }

            var currentIPGroup = (IPFeed) ipFeedListBox.SelectedItem;
            if (currentIPGroup != null)
            {
                providerLabel.Text = currentIPGroup.Provider;
                providerSiteLink.Text = currentIPGroup.ProviderWebSite.Replace("http://", "");
                providerDataLink.Text = currentIPGroup.Url.Replace("http://", "");
                providerTextLabel.Visible = true;
                providerSiteTextLabel.Visible = true;
                providerDataLink.Visible = true;
                providerLinkTextLabel.Visible = true;
            }
            else
            {
                providerLabel.Text = "";
                providerSiteLink.Text = "";
                providerTextLabel.Visible = false;
                providerSiteTextLabel.Visible = false;
                providerDataLink.Visible = false;
                providerLinkTextLabel.Visible = false;
            }

            if (currentWebSite != null && currentIPGroup != null)
            {
                blockGroup_Button.Enabled = true;
                UnBlockGroup_Button.Enabled = true;
            }
            else
            {
                blockGroup_Button.Enabled = false;
                UnBlockGroup_Button.Enabled = false;
            }

            ValidateUI_SingleIP();
            _updatingUI = false;
        }

        private void singleIPTextBox_TextChanged(object sender, EventArgs e)
        {
            ValidateUI_SingleIP();
        }

        private void ValidateUI_SingleIP()
        {
            var currentWebSite = (IIsWebSite) siteList.SelectedItem;
            if (currentWebSite != null)
            {
                singleIPTextBox.Enabled = true;

                if (singleIPTextBox.Text == "")
                {
                    singleIPValidLabel.Text = "";
                    blockSingleIP_Button.Enabled = false;
                    UnBlockSingleIP_Button.Enabled = false;
                }
                else
                {
                    if (IPAddressV4.IsValid(singleIPTextBox.Text))
                    {
                        singleIPValidLabel.Text = "good";
                        singleIPValidLabel.ForeColor = Color.Green;
                        blockSingleIP_Button.Enabled = true;
                        UnBlockSingleIP_Button.Enabled = true;
                    }
                    else
                    {
                        singleIPValidLabel.Text = "incomplete";
                        singleIPValidLabel.ForeColor = Color.Red;
                        blockSingleIP_Button.Enabled = false;
                        UnBlockSingleIP_Button.Enabled = false;
                    }
                }
            }
            if (currentWebSite == null)
            {
                blockSingleIP_Button.Enabled = false;
                UnBlockSingleIP_Button.Enabled = false;
                singleIPTextBox.Enabled = false;
                singleIPValidLabel.Text = "";
            }
        }

        private void refreshList_Button_Click(object sender, EventArgs e)
        {
            try
            {                
                var currentWebSite = (IIsWebSite) siteList.SelectedItem;
                if (currentWebSite != null)
                    ReloadIpDisplayList(currentWebSite);
            }
            catch (Exception exp)
            {
                DisplayError(exp);
            }
            finally
            {
                ShowStatus();
            }
        }

        private void blockGroup_Button_Click(object sender, EventArgs e)
        {
            try
            {
                var currentWebSite = (IIsWebSite) siteList.SelectedItem;
                var currentIPGroup = (IPFeed) ipFeedListBox.SelectedItem;                                
                var selectedGroupOfIps = currentIPGroup.Download();

                ShowStatus("Checking for duplicates...");
                int duplicateCount;
                selectedGroupOfIps = Utility.RemoveDuplicateIPs(selectedGroupOfIps, out duplicateCount);

                // Dump list for debugging purposes
                //StringBuilder sb = new StringBuilder();
                //foreach (var ip in selectedGroupOfIps)
                //{
                //    sb.AppendLine(ip.Address);
                //}
                //File.WriteAllText(@"C:\test.txt", sb.ToString());
                //Process.Start(@"C:\test.txt");
                //return;
                
                currentWebSite.BlockIpAddressList(selectedGroupOfIps);                
                ReloadIpDisplayList(currentWebSite,selectedGroupOfIps.Count);

                if ( duplicateCount > 0 )
                    ShowStatus(duplicateCount + " Duplicate IPs found in group '" + currentIPGroup.Name );
            }
            catch (Exception exp)
            {
                DisplayError(exp);
            }
            finally
            {
                ShowStatus();
            }
        }

        private void UnBlockGroup_Button_Click(object sender, EventArgs e)
        {
            try
            {
                var currentWebSite = (IIsWebSite) siteList.SelectedItem;
                var currentIPGroup = (IPFeed) ipFeedListBox.SelectedItem;

                ShowStatus("Downloading IPs from " + currentIPGroup.Url.Replace("http://", ""));
                var groupIPs = currentIPGroup.Download();

                ShowStatus("Unblocking '" + currentIPGroup.Name + "' IPs...");
                currentWebSite.UnBlockIpAddress(groupIPs);
                ReloadIpDisplayList(currentWebSite, groupIPs.Count);
            }
            catch (Exception exp)
            {
                DisplayError(exp);
            }
            finally
            {
                ShowStatus();
            }
        }

        private void blockSingleIP_Button_Click(object sender, EventArgs e)
        {
            try
            {
                ShowStatus("Blocking IP " + singleIPTextBox.Text + "...");

                if (IPAddressV4.IsValid(singleIPTextBox.Text) == false)
                    throw new ApplicationException("Invalid IP: " + singleIPTextBox.Text);

                var ipAddressV4 = new IPAddressV4(singleIPTextBox.Text);
                var currentWebSite = (IIsWebSite) siteList.SelectedItem;

                currentWebSite.BlockIpAddress(ipAddressV4);
                ReloadIpDisplayList(currentWebSite);
            }
            catch (Exception exp)
            {
                DisplayError(exp);
            }
            finally
            {
                ShowStatus();
            }
        }

        private void UnBlockSingleIP_Button_Click(object sender, EventArgs e)
        {
            try
            {
                ShowStatus("UnBlocking IP " + singleIPTextBox.Text + "...");

                if (IPAddressV4.IsValid(singleIPTextBox.Text) == false)
                    throw new ApplicationException("Invalid IP: " + singleIPTextBox.Text);

                var ipAddressV4 = new IPAddressV4(singleIPTextBox.Text);
                var currentWebSite = (IIsWebSite) siteList.SelectedItem;

                currentWebSite.UnBlockIpAddress(ipAddressV4);
                ReloadIpDisplayList(currentWebSite);
            }
            catch (Exception exp)
            {
                DisplayError(exp);
            }
            finally
            {
                ShowStatus();
            }
        }

        private void updateList_Button_Click(object sender, EventArgs e)
        {
            try
            {
                ShowStatus("Updating IPs From List...");
            }
            catch (Exception exp)
            {
                DisplayError(exp);
            }
            finally
            {
                ShowStatus();
            }
        }

        private void UnblockAll_Button_Click(object sender, EventArgs e)
        {
            try
            {
                ShowStatus("Unblocking all IPs...");
                var currentWebSite = (IIsWebSite) siteList.SelectedItem;

                currentWebSite.UnBlockAllIpAddresses();
                ReloadIpDisplayList(currentWebSite);
            }
            catch (Exception exp)
            {
                DisplayError(exp);
            }
            finally
            {
                ShowStatus();
            }
        }

        protected static void DisplayError(Exception exp)
        {
            var err = exp.Message;
            if (exp.InnerException != null && string.IsNullOrEmpty(exp.InnerException.Message) == false)
                err += "\r\n\r\n" + exp.InnerException.Message;
            MessageBox.Show(err, App.Name);
        }

        private void providerDataLink_Click(object sender, EventArgs e)
        {
            try
            {
                var ipFeed = (IPFeed) ipFeedListBox.SelectedItem;
                if (ipFeed != null)
                    Process.Start(ipFeed.Url);
            }
            catch (Exception exp)
            {
                DisplayError(exp);
            }
            finally
            {
                ShowStatus();
            }
        }

        private void scanEventLog_menuItem_Click(object sender, EventArgs e)
        {
            var scanLogForm = new ScanLogForm();
            scanLogForm.ShowDialog(this);
        }
    }
}