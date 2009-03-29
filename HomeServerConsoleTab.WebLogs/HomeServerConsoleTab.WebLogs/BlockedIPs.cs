using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.HomeServer.Controls;
using System.Net;
using IISIP;

namespace HomeServerConsoleTab.WebLogs
{
    public sealed class BlockedIPs
    {
        private static BlockedIPs instance;

        private List<IPAddressV4> ipBlocks;
        private IIsWebSite site;
        private Dictionary<string, IPAddressV4> ipHash;

        public IIsWebSite RootSite
        {
            get
            {
                return site;
            }
        }

        public Dictionary<string, IPAddressV4> BlockedHash
        {
            get
            {
                if (site == null) 
                {
                    return null;
                }
                else 
                {
                    return site.GetBlockedIpAddressDictionary();
                }
            }
        }

        public List<IPAddressV4> BlockedSites
        {
            get
            {
                if (site == null) 
                {
                    return null;
                }
                else 
                {
                    return site.GetBlockedIpAddresses();
                }
            }
        }

        BlockedIPs()
        {
            List<IIsWebSite> iisWebSiteList = IISMetaBase.GetWebSites();
            this.site = null;
            foreach (IIsWebSite site in iisWebSiteList)
            {
                if (site.SiteName.Equals("IIS Root"))
                {
                    this.site = site;
                    break;
                }
            }
            if (this.RootSite == null)
            {
                MyLogger.Log(EventLogEntryType.Warning, "Cannot locate the IIS Root site, IP blocking will be disabled.");
                return;
            }
            else
            {
                this.ipBlocks = this.RootSite.GetBlockedIpAddresses();
                this.ipHash = this.RootSite.GetBlockedIpAddressDictionary();
            }
        }

        private bool SafetyCheck(IPAddressV4 ip)
        {
            bool check1 = (!ip.Address.ToString().StartsWith(LogControl.LOCAL_SUBNET));
            bool check2 = (!ip.Address.ToString().Equals(LogControl.LOCALHOST));
            if (check1 == false) 
            {
                QMessageBox.Show("Sorry, I will not let you block an IP address on the same subnet as your server.  " +
                    "If you accidentally block the IP for this client, you will be unable "
                    + "to connect to the WHS console!", "Web Logs", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return check1;
            }
            else if (check2 == false) 
            {
                QMessageBox.Show("Sorry, I will not let you block the localhost IP address (" 
                    + LogControl.LOCALHOST + ")." + "  If you accidentally block the IP for this client, you will be unable "
                    + "to connect to the WHS console!", "Web Logs", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return check2;
            }
            return true;
        }

        #region BlockingActions

        public bool BlockIP(string ip)
        {
            return BlockIP(new IPAddressV4(ip));
        }

        public bool BlockIP(IPAddressV4 ip)
        {
            if (RootSite == null)
            {
                QMessageBox.Show("IP Blocking is disabled due to an error, refer to the event logs for more details", 
                    "Web Logs", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                if (SafetyCheck(ip))
                {
                    MyLogger.Log(EventLogEntryType.Information, "Blocking IP Address: " + ip.Address.ToString());
                    RootSite.BlockIpAddress(ip);
                    return true;
                }
                return false;
            }
        }

        public bool UnblockIP(string ip)
        {
            return UnblockIP(new IPAddressV4(ip));
        }

        public bool UnblockIP(IPAddressV4 ip)
        {
            if (RootSite == null)
            {
                QMessageBox.Show("IP Blocking is disabled due to an error, refer to the event logs for more details",
                   "Web Logs", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                MyLogger.Log(EventLogEntryType.Information, "Unblocking IP Address: " + ip.Address.ToString());
                RootSite.UnBlockIpAddress(ip);
                return true;
            }
        }

        public void UnblockAllIPs()
        {
            if (RootSite == null)
            {
                QMessageBox.Show("IP Blocking is disabled due to an error, refer to the event logs for more details",
                    "Web Logs", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MyLogger.Log(EventLogEntryType.Information, "Unblocking ALL IP Address");
                RootSite.UnBlockAllIpAddresses();
            }
        }

        #endregion

        public static BlockedIPs GetInstance()
        {
            if (instance == null) 
            {
                instance = new BlockedIPs();
            }
            return instance;
        }

        public bool IsThisIPBlocked(IPAddressV4 ip)
        {
            return IsThisIPBlocked(ip.Address);
        }

        public bool IsThisIPBlocked(string ip)
        {
            return ipHash.ContainsKey(ip);
        }

        public int RemoveDupes()
        {
            int dupes = 0;
            List<IPAddressV4> newList = Utility.RemoveDuplicateIPs(BlockedSites, out dupes);
            if (newList != null)
            {
                site.UnBlockAllIpAddresses();
                site.BlockIpAddressList(newList);
            }
            return dupes;
        }
    }
}
