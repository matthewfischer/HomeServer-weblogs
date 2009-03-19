//-----------------------------------------------------------------
// Copyright Lee Whitney 2008-2009
// http://www.hdgreetings.com/other/Block-IP-IIS
// Feel free to use and modify, please just keep this copyright in place.
//-----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml.Serialization;

namespace IISIP
{
    //
    // An IPFeed is an online provider of IP addresses.  
    // For example a site that provides a list of Chinese addresses.
    //
    [Serializable]
    public class IPFeed
    {
        public string Name;
        public string Note;
        public string Provider;
        public string ProviderWebSite;
        public string Url;
        private int _line;
        private int _lineCount;

        public delegate void StatusUpdateEventHandler(string status);
        public event StatusUpdateEventHandler StatusUpdate;

        public override string ToString()
        {
            return Name; // +"  (" + Provider + ")";
        }

        public static IPFeed FromFile(string file)
        {
            var xmlSerializer = new XmlSerializer(typeof (IPFeed));
            TextReader textReader = new StreamReader(file);
            var ipSource = (IPFeed) xmlSerializer.Deserialize(textReader);
            textReader.Close();

            return ipSource;
        }

        public static List<IPFeed> FromFolder(string folder)
        {
            var ipFeedFiles = new List<string>(Directory.GetFiles(folder, "*.xml"));
            var ipFeedObjects = new List<IPFeed>();

            foreach (var path in ipFeedFiles)
            {
                var ipFeed = FromFile(path);
                ipFeedObjects.Add(ipFeed);
            }
            return ipFeedObjects;
        }

        public void Save(string file)
        {
            if (File.Exists(file))
                File.Delete(file);

            var xmlSerializer = new XmlSerializer(typeof (IPFeed));
            TextWriter textWriter = new StreamWriter(file);
            xmlSerializer.Serialize(textWriter, this);
            textWriter.Close();
        }

        public List<IPAddressV4> Download()
        {
            string ipListTextFile;

            if (Url.StartsWith("http"))
            {
                if ( StatusUpdate != null )
                    StatusUpdate("Downloading IPs from " + Url.Replace("http://", ""));
                var webClient = new WebClient();

                ipListTextFile = App.IPFeedFolder + "IPData-" + Name + ".txt";
                if ( File.Exists(ipListTextFile) )
                    File.Delete(ipListTextFile);
                webClient.DownloadFile(Url, ipListTextFile);
            }
            else
                ipListTextFile = Url; // Local file has been provided

            //
            // Process each line in the file.  Each line must have either a
            //   - Single IP address  OR
            //   - Two IP addresses representing a range
            //            
            var ipAddressList = new List<IPAddressV4>();
            var rawIpAddressList = File.ReadAllLines(ipListTextFile);
            _line = 1;
            _lineCount = rawIpAddressList.Length;
            foreach (var rawIpAddressLine in rawIpAddressList)
            {
                if (StatusUpdate != null)
                    StatusUpdate("Processing line " + _line + " of " + _lineCount);

                var countValid = 0;
                var firstIp = new IPAddressV4(); 
                var secondIp = new IPAddressV4();

                if (rawIpAddressLine.StartsWith("#"))   // Allow for comments
                    continue;

                var delimeters = new[] {' ', ',', '-', ';' };
                var fields = rawIpAddressLine.Split(delimeters, StringSplitOptions.RemoveEmptyEntries);
                //
                // Process each field in the line looking for 1 or 2 IP addresses
                //
                for (var j = 0; j < fields.Length; j++)
                {
                    if (IPAddressV4.IsValid(fields[j]) == false)
                        continue;

                    countValid++;
                    if (countValid == 1)
                        firstIp.Address = fields[j].Trim();
                    if (countValid == 2)
                    {                        
                        if ( fields[j].Trim()=="255.255.255.255" )
                            throw new ApplicationException("The address " + firstIp.Address+" is followed by 255.255.255.255, which looks like a subnet mask.  Lists should only have a single IP per line, or two IPs per line representing a range.  No masks should be provided, please remove it from the list.");
                        secondIp.Address = fields[j].Trim();                        
                    }
                }
                if ( countValid == 1 )
                    ipAddressList.Add(firstIp);
                if (countValid == 2)
                    AddIpRangeToList(ipAddressList, firstIp, secondIp, 1, true);

                _line++;
            }
            return ipAddressList;
        }

        //
        // Given a range of addresses, generate items for the list to cover the entire range.
        // We want to do this recursively and need to handle different start and end IPs correctly
        // For example if we have 1.253.253.0 - 3.3.3.3 then we want the following:
        //  1.253.253.0 (255.255.255.0)
        //  1.253.254.0 (255.255.255.0)
        //  1.253.255.0 (255.255.255.0)
        //  1.254.0.0 (255.255.0.0)
        //  1.255.0.0 (255.255.0.0)
        //  2.0.0.0 (255.0.0.0)
        //  3.0.0.0 (255.255.0.0)
        //  3.1.0.0 (255.255.0.0)
        //  3.2.0.0 (255.255.0.0)
        //  3.3.0.0 (255.255.255.0)
        //  3.3.1.0 (255.255.255.0)
        //  3.3.2.0 (255.255.255.0)
        //  3.3.3.0
        //  3.3.3.1
        //  3.3.3.2
        //  3.3.3.3
        protected void AddIpRangeToList(List<IPAddressV4> ipList, IPAddressV4 currentIP, IPAddressV4 endIP, int octPos, bool endOfParentOct)
        {
            byte tend;

            if (octPos == 1)
            {
                //
                // If the corresponding octets for start and end
                // are 0 and 255, then change both to zero which will be
                // treated as a wildcard and represent all 8 bits
                //
                if (currentIP.oct4 == 0 && endIP.oct4 == 255)
                    endIP.oct4 = 0;
                if (currentIP.oct3 == 0 && endIP.oct3 == 255)
                    endIP.oct3 = 0;
                if (currentIP.oct2 == 0 && endIP.oct2 == 255)
                    endIP.oct2 = 0;
                if (currentIP.oct1 == 0 && endIP.oct1 == 255)
                    endIP.oct1 = 0;
            }

            //if end of parent oct, then we only want to loop through to end of end ip range
            //else loop through entire octet
            if (endOfParentOct)
                tend = endIP[octPos];
            else
                tend = 255;

            //
            // Enumerate all addresses in the range and add them to the list
            //
            for (int oct = currentIP[octPos]; oct <= tend; oct++)
            {
                currentIP[octPos] = (byte)oct;

                bool endOfOct = (endOfParentOct && currentIP[octPos] == endIP[octPos]);

                //we want to recurivsely got hrough ip address ranges if we are
                // not at end of ip address octets and either:
                // - the start ip address has subrange; or 
                // - we are are at the end of the ip range and there exists a sub range
                // else we want to add ip address to list
                if (octPos < 4 && (HasSubRanges(currentIP, octPos) || (endOfOct && HasSubRanges(endIP, octPos))))
                {
                    AddIpRangeToList(ipList, currentIP, endIP, octPos + 1, endOfOct);
                }
                else
                {
                    string mask = "";

                    //we need to generate mask, instead of relying on IPAddressV4.MaskFromIp() as we may want 
                    //to block 1.1.1.0 via (255.255.255.255) not 1.1.1.* (255.255.255.0)
                    switch (octPos)
                    {
                        case 1: mask = "255.0.0.0"; break;
                        case 2: mask = "255.255.0.0"; break;
                        case 3: mask = "255.255.255.0"; break;
                        case 4: mask = "255.255.255.255"; break;
                    }

                    IPAddressV4 ipInRange = new IPAddressV4(currentIP.Address);
                    ipInRange.Mask = mask;
                    ipList.Add(ipInRange);
                }
            }

            //rest currrnt IP octet for next parent round
            currentIP[octPos] = 0;
        }

        //
        // Do sub ranges exist e.g. 202.0.110.0 returns true, 202.0.0.0 returns false
        //
        private bool HasSubRanges(IPAddressV4 IPadd, int octPos)
        {
            if (octPos == 4)
            {
                return false;
            }
            else
            {
                if (IPadd[octPos + 1] != 0)
                {
                    return true;
                }
                else
                {
                    return HasSubRanges(IPadd, octPos + 1);
                }
            }
        }
    }
}