//-----------------------------------------------------------------
// Copyright Lee Whitney 2008-2009
// http://www.hdgreetings.com/other/Block-IP-IIS
// Feel free to use and modify, please just keep this copyright in place.
//-----------------------------------------------------------------
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace IISIP
{
    public class Utility
    {
        //
        // Check for duplicates within a single list, and return
        // a new list with the duplicates removed.
        //
        public static List<IPAddressV4> RemoveDuplicateIPs(List<IPAddressV4> ipList, out int duplicateCount )
        {
            var uniqueIPList = new List<IPAddressV4>();
            var tempList = new Dictionary<string, IPAddressV4>();
            duplicateCount = 0;

            foreach (var ipAddress in ipList)
            {
                if (tempList.ContainsKey(ipAddress.Address) == false)
                    tempList.Add(ipAddress.Address, ipAddress);
                else
                    duplicateCount++;
            }
            foreach (var ipAddress in tempList.Values)
                uniqueIPList.Add(ipAddress);

            return uniqueIPList;
        }

        //
        // Merge two IP lists removing duplicates across the two lists.
        // Return a single new list with all unique IPs that is the union of the two.
        //
        public static List<IPAddressV4> GetMergedIPLists(Dictionary<string,IPAddressV4> existingBlockedIPs, List<IPAddressV4> newIPsToBlock )
        {
            var uniqueIPList = new List<IPAddressV4>();
            var tempList = new Dictionary<string, IPAddressV4>();

            // Add in all addresses from the first list
            foreach (var ipAddress in existingBlockedIPs.Values)
                uniqueIPList.Add(ipAddress);

            // Add in only non-overlapping addresses from the second list
            foreach (var ipAddress in newIPsToBlock)
            {
                if (existingBlockedIPs.ContainsKey(ipAddress.Address) == false)
                    uniqueIPList.Add(ipAddress);
            }
            return uniqueIPList;
        }

        //
        // Mostly a diagnostic function that can tell how many addresses
        // on the "to block" list are new.
        //
        public static List<IPAddressV4> GetIPsNotAlreadyBlocked(Dictionary<string,IPAddressV4> existingBlockedIPs, List<IPAddressV4> newIPsToBlock )
        {
            var ipsNotAlreadyBlocked = new List<IPAddressV4>();
            foreach (var ipAddress in newIPsToBlock)
            {
                if (existingBlockedIPs.ContainsKey(ipAddress.Address) == false)
                    ipsNotAlreadyBlocked.Add(ipAddress);
            }
            return ipsNotAlreadyBlocked;
        }

        //--------------------------------------------------------------------------
        // NOTE: This directory must be treated as read-only because users who
        // are not admins will not have write access.
        // This is also an XP/Vista logo requirement.
        // For write purposes use the ApplicationData property.
        // NO trailing '\' is added
        //--------------------------------------------------------------------------
        public static string ProcessDirectory
        {
            get
            {
                //-------------------------------------------------------------------
                // Determine application directory
                // Dont use Environment.CurrentDirectory because we when are launched
                // via a Shell execute on a image file it will indicate the image dir
                // instead of the app dir.
                //-------------------------------------------------------------------
                var assembly = Assembly.GetEntryAssembly();
                if (assembly == null)
                    return "";
                var fi = new FileInfo(assembly.Location);
                var appDir = fi.Directory.FullName;
                return appDir;
            }
        }
    }
}