//-----------------------------------------------------------------
// Copyright Lee Whitney 2008-2009
// http://www.hdgreetings.com/other/Block-IP-IIS
// Feel free to use and modify, please just keep this copyright in place.
//-----------------------------------------------------------------
using System.Collections.Generic;
using System.DirectoryServices;

namespace IISIP
{
    public class IISMetaBase
    {
        /// <summary>
        /// Returns a list of all IIS Sites on the server
        /// </summary>
        public static List<IIsWebSite> GetWebSites()
        {
            DirectoryEntry iisRootDirectoryEntry = null;
            try
            {
                var iisRootPath = "IIS://localhost/W3SVC";
                iisRootDirectoryEntry = new DirectoryEntry(iisRootPath);

                var iisWebSiteList = new List<IIsWebSite>();

                // Add entry for root
                var iisWebSite = new IIsWebSite();
                iisWebSite.SiteName = "IIS Root";
                iisWebSite.MetaBasePath = iisRootPath;
                iisWebSiteList.Add(iisWebSite);

                foreach (DirectoryEntry Entry in iisRootDirectoryEntry.Children)
                {
                    var propertyCollection = Entry.Properties;
                    try
                    {
                        // *** Skip over non site entries
                        if (string.IsNullOrEmpty((string) propertyCollection["ServerComment"].Value))
                            continue;

                        iisWebSite = new IIsWebSite();
                        iisWebSite.SiteName = (string) propertyCollection["ServerComment"].Value;
                        iisWebSite.MetaBasePath = Entry.Path + "/root";
                        iisWebSiteList.Add(iisWebSite);
                    }
                    catch {}
                }
                return iisWebSiteList;
            }
            finally
            {
                if (iisRootDirectoryEntry != null)
                    iisRootDirectoryEntry.Close();
            }
        }
    }
}