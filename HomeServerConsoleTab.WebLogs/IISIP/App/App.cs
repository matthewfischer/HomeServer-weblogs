//-----------------------------------------------------------------
// Copyright Lee Whitney 2008-2009
// http://www.hdgreetings.com/other/Block-IP-IIS
// Feel free to use and modify, please just keep this copyright in place.
//-----------------------------------------------------------------
using System.IO;
//
// .80 - First release
// .82 - Added event log IP scanner
// .84 - More robust checking for duplicates and properly formed data
//       Keep latest copy of downloaded feed data in txt file for easy inspection
//       Allow for comments by skipping IPFeed data lines starting with #
//       Misc small fixes
// .86 - Main form IP list is now properly sorted
//       Improved event log item info and color coded for event type
//       Minor UI tweaks
// .88 - Enhanced IPFeed parsing to handle ranges of IPs, many were being missed
//       Added Link to IP Geographic Locator
//       Added more detail to status while work is being done
//       Better error checking for IPFeeds
//       Minor UI tweaks
// .90 - Web site list is now sorted
//1.00 - Integrated very helpful work from George Atkins (george@sportsground.co.nz)
//       Improved instructions on Creating IP Feeds in the ReadMe.txt



namespace IISIP
{
    public static class App
    {
        public const string HelpUrl = "http://www.hdgreetings.com/other/Block-IP-IIS";
        public const string LastUpdated = "November 30, 2008";
        public const string Name = "IISIP";
        public const string Version = "1.00";

        public static string IPFeedFolder
        {
            get
            {
                var ipFeedFolder = Utility.ProcessDirectory + @"\IPFeeds\";
                if (Directory.Exists(ipFeedFolder) == false)
                    Directory.CreateDirectory(ipFeedFolder);
                return ipFeedFolder;
            }
        }
    }
}