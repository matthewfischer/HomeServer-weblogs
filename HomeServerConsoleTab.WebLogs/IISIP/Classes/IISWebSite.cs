//-----------------------------------------------------------------
// Copyright Lee Whitney 2008-2009
// http://www.hdgreetings.com/other/Block-IP-IIS
// Feel free to use and modify, please just keep this copyright in place.
//-----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Reflection;
using System.Runtime.InteropServices;

namespace IISIP
{
    /// <summary>
    /// Container class that holds information about an IIS Web site
    /// </summary>
    public class IIsWebSite
    {
        public string MetaBasePath;
        public string SiteName;

        public delegate void StatusUpdateEventHandler(string status);
        public event StatusUpdateEventHandler StatusUpdate;

        public override string ToString()
        {
            return SiteName;
        }

        //
        // Unblock a single address
        //
        public void UnBlockIpAddress(IPAddressV4 ipAddress)
        {
            var blockedIPList = GetBlockedIpAddressDictionary();
            if (blockedIPList.ContainsKey(ipAddress.Address) == false)
                throw new ApplicationException("The IP Address " + ipAddress.Address + " is not currently blocked.");

            var ipAddressList = new List<IPAddressV4> {ipAddress};
            UnBlockIpAddress(ipAddressList);
        }

        //
        // Unblock a list of addresses
        //
        public void UnBlockIpAddress(List<IPAddressV4> ipAddressesToUnblock)
        {
            DirectoryEntry iisAdmin = null;
            try
            {
                var blockedIPList = GetBlockedIpAddressDictionary();
                foreach (var addressToUnblock in ipAddressesToUnblock)
                {
                    if (blockedIPList.ContainsKey(addressToUnblock.Address))
                        blockedIPList.Remove(addressToUnblock.Address);
                }

                iisAdmin = new DirectoryEntry(MetaBasePath);
                var ipSecurity = iisAdmin.Properties["IPSecurity"].Value;
                var ipSecurityType = ipSecurity.GetType();

                // *** IMPORTANT: This list MUST be object type or COM call will fail
                var metabaseIPList = new List<object>();
                foreach (var pair in blockedIPList)
                    metabaseIPList.Add(pair.Value.MetabaseFormat);

                ipSecurityType.InvokeMember("GrantByDefault",
                                            BindingFlags.SetProperty,
                                            null, ipSecurity, new object[] {true});

                var metabaseIPArray = metabaseIPList.ToArray();
                ipSecurityType.InvokeMember("IPDeny",
                                            BindingFlags.SetProperty,
                                            null, ipSecurity, new object[] {metabaseIPArray});

                iisAdmin.Properties["IPSecurity"].Value = ipSecurity;
                iisAdmin.CommitChanges();
            }
            catch (COMException exp)
            {
                // This error happens everytime but the unblock seems to still work.
                // Until we figure out how to fix it, suppress the error for now.
                // COMException: 0x800700b7: Cannot create a file when that file already exists.
                if ((uint) exp.ErrorCode == 0x800700b7)
                    return;

                throw;
            }
            finally
            {
                if (iisAdmin != null)
                    iisAdmin.Close();
            }
        }

        public void UnBlockAllIpAddresses()
        {
            DirectoryEntry iisAdmin = null;
            try
            {
                iisAdmin = new DirectoryEntry(MetaBasePath);
                var ipSecurity = iisAdmin.Properties["IPSecurity"].Value;
                var ipSecurityType = ipSecurity.GetType();

                // Clear existing blocked addresses
                var emptyArray = new object[] {};
                ipSecurityType.InvokeMember("IPDeny",
                                            BindingFlags.SetProperty,
                                            null, ipSecurity, new object[] {emptyArray});

                iisAdmin.Properties["IPSecurity"].Value = ipSecurity;
                iisAdmin.CommitChanges();
            }
            finally
            {
                if (iisAdmin != null)
                    iisAdmin.Close();
            }
        }

        // 
        // Any wildcarded IP Addresses will return .0 for the
        // wild card characters.
        //
        // There are two methods to return blocked IPs:
        //      GetBlockedIpAddressDictionary  and
        //      GetBlockedIpAddresses
        //
        // The first method returning a dictionary allows for very fast duplicate checks.
        // The second method is simpler to iterate for the UI.
        //
        public List<IPAddressV4> GetBlockedIpAddresses()
        {
            var blockedIpDictionary = GetBlockedIpAddressDictionary();
            var blockedIPList = new List<IPAddressV4>();

            foreach (var ip in blockedIpDictionary.Values)
                blockedIPList.Add(ip);

            return blockedIPList;
        }

        // 
        // Any wildcarded IP Addresses will return .0 for the
        // wild card characters.
        //
        // There are two methods to return blocked IPs:
        //      GetBlockedIpAddressDictionary  and
        //      GetBlockedIpAddresses
        //
        // The first method returning a dictionary allows for very fast duplicate checks.
        // The second method is simpler to iterate for the UI.
        //
        public Dictionary<string, IPAddressV4> GetBlockedIpAddressDictionary()
        {
            DirectoryEntry iisAdmin = null;
            try
            {
                var list = new Dictionary<string, IPAddressV4>();

                iisAdmin = new DirectoryEntry(MetaBasePath);
                var ipSecurity = iisAdmin.Properties["IPSecurity"].Value;
                var ipSecurityType = ipSecurity.GetType();

                // *** Grab the IP list
                var metabaseIPDenyList = (Array) ipSecurityType.InvokeMember("IPDeny",
                                                                             BindingFlags.Public |
                                                                             BindingFlags.Instance | BindingFlags.GetProperty,
                                                                             null, ipSecurity, null);

                // Format and extract into a string list
                foreach (string metabaseIP in metabaseIPDenyList)
                {
                    var ipAddressV4 = IPAddressV4.FromMetabaseFormat(metabaseIP);
                    list.Add(ipAddressV4.Address, ipAddressV4);
                }
                return list;
            }
            finally
            {
                if (iisAdmin != null)
                    iisAdmin.Close();
            }
        }

        public void BlockIpAddress(IPAddressV4 ipAddress)
        {
            var ipAddressList = new List<IPAddressV4> {ipAddress};
            BlockIpAddressList(ipAddressList);
        }

        //
        // No duplicates allowed
        //
        public void BlockIpAddressList(List<IPAddressV4> newIPsToBlock)
        {
            DirectoryEntry iisAdmin = null;
            try
            {
                if (StatusUpdate != null)
                    StatusUpdate("Building IP dictionary...");
                var existingBlockedIPs = GetBlockedIpAddressDictionary();
                if (StatusUpdate != null)
                    StatusUpdate("Merging IP groups...");
                var ipsToBlock = Utility.GetMergedIPLists(existingBlockedIPs, newIPsToBlock);

                // This list MUST be object type or COM call will fail
                var ipsToBlock_MetabaseFormat = new List<object>();
                foreach (var ip in ipsToBlock)
                {
                    if (ip.Mask == null || ip.Mask == "255.255.255.255")
                        ipsToBlock_MetabaseFormat.Add(ip.Address);
                    else
                        ipsToBlock_MetabaseFormat.Add(ip.Address + "," + ip.Mask);
                }

                iisAdmin = new DirectoryEntry(MetaBasePath);
                var ipSecurity = iisAdmin.Properties["IPSecurity"].Value;
                var ipSecurityType = ipSecurity.GetType();

                if (StatusUpdate != null)
                    StatusUpdate("Updating IIS Metabase for " + this.SiteName + "...");
                ipSecurityType.InvokeMember("GrantByDefault",
                                            BindingFlags.SetProperty,
                                            null, ipSecurity, new object[] { true });

                var metabaseIPArray = ipsToBlock_MetabaseFormat.ToArray();
                ipSecurityType.InvokeMember("IPDeny",
                                            BindingFlags.SetProperty,
                                            null, ipSecurity, new object[] { metabaseIPArray });

                if (StatusUpdate != null)
                    StatusUpdate("Committing Metabase changes for " + this.SiteName + "...");
                iisAdmin.Properties["IPSecurity"].Value = ipSecurity;
                iisAdmin.CommitChanges();
                iisAdmin.RefreshCache();
            }
            catch (COMException exp)
            {
                // This error happens everytime but the block seems to still work.
                // Until we figure out how to fix it, suppress the error for now.
                // COMException: 0x800700b7: Cannot create a file when that file already exists.
                if ((uint)exp.ErrorCode == 0x800700b7)
                    return;

                throw;
            }
            finally
            {
                if (iisAdmin != null)
                    iisAdmin.Close();
            }
        }
    }
}