using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using IISIP;

namespace HomeServerConsoleTab.WebLogs
{
    public sealed class BlockedIPs
    {
        static readonly BlockedIPs instance=new BlockedIPs();

        private List<IPAddressV4> ipBlocks;
        private IIsWebSite site;

        public IIsWebSite rootSite
        {
            get
            {
                return site;
            }
        }

        public List<IPAddressV4> blockedSites
        {
            get
            {
                return blockedSites;
            }
        }

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static BlockedIPs()
        {

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
            if (this.rootSite == null)
            {
                MyLogger.Log(EventLogEntryType.Warning, "Cannot locate the IIS Root site, IP blocking will be disabled.");              
            }
            this.ipBlocks = this.rootSite.GetBlockedIpAddresses();
        }

        public void BlockIP(string ip)
        {
            BlockIP(new IPAddressV4(ip));
        }

        public void BlockIP(IPAddressV4 ip)
        {
            if (rootSite == null)
            {
                MessageBox.Show("IP Blocking is disabled due to an error, refer to the event logs for more details", "Web Logs", MessageBoxButtons.OK);
                return;
            }
            else
            {
                rootSite.BlockIpAddress(ip);
            }
        }

        public void UnblockIP(string ip)
        {
            UnblockIP(new IPAddressV4(ip));
        }

        public void UnblockIP(IPAddressV4 ip)
        {
            if (rootSite == null)
            {
                MessageBox.Show("IP Blocking is disabled due to an error, refer to the event logs for more details", "Web Logs", MessageBoxButtons.OK);
                return;
            }
            else
            {
                rootSite.UnBlockIpAddress(ip);
            }
        }

        public static BlockedIPs Instance
        {
            get
            {
                return instance;
            }
        }

        public int RemoveDupes()
        {
            int dupes = 0;
            List<IPAddressV4> newList = Utility.RemoveDuplicateIPs(blockedSites, out dupes);
            if (newList != null)
            {
                site.UnBlockAllIpAddresses();
                site.BlockIpAddressList(newList);
            }
            return dupes;
        }
    }
}
