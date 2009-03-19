//-----------------------------------------------------------------
// Copyright Lee Whitney 2008-2009
// http://www.hdgreetings.com/other/Block-IP-IIS
// Feel free to use and modify, please just keep this copyright in place.
//-----------------------------------------------------------------
using System;
using System.Text.RegularExpressions;

namespace IISIP
{
    public class IPAddressV4 : IComparable
    {
        public string Mask;
        public byte oct1, oct2, oct3, oct4;

        //
        // Use this instead of IPAddress.TryParse because we don't want to allow IPv6 addresses
        //

        public IPAddressV4()
        {
        }

        public IPAddressV4(string ipAddress, string ipMask)
        {
            Address = ipAddress;
            Mask = ipMask;
        }

        public IPAddressV4( byte octet1, byte octet2, byte octet3, byte octet4 )
        {
            string ipAddress = octet1.ToString() + "." + octet2.ToString() + "." + octet3.ToString() + "." + octet4.ToString();
            Address = ipAddress;
            Mask = MaskFromIp(ipAddress);
        }

        public IPAddressV4(string ipAddress)
        {
            Address = ipAddress;
            Mask = MaskFromIp(ipAddress);
        }

        public static string MaskFromIp(string ipAddress)
        {
            if (ipAddress.EndsWith(".0.0.0"))
                return "255.0.0.0";
            else if (ipAddress.EndsWith(".0.0"))
                return "255.255.0.0";
            else if (ipAddress.EndsWith(".0"))
                return "255.255.255.0";
            else
                return "255.255.255.255";            
        }

        public byte this[int indexer]
        {
            get
            {
                switch (indexer)
                {
                    case 1: return this.oct1;
                    case 2: return this.oct2;
                    case 3: return this.oct3;
                    case 4: return this.oct4;
                    default:
                        throw new IndexOutOfRangeException("Not a valid index");
                }
            }
            set
            {
                switch (indexer)
                {
                    case 1: this.oct1 = value; break;
                    case 2: this.oct2 = value; break;
                    case 3: this.oct3 = value; break;
                    case 4: this.oct4 = value; break;
                    default:
                        throw new IndexOutOfRangeException("Not a valid index");
                }
            }
        }

        public string MetabaseFormat
        {
            get
            {
                var s = Address + "," + Mask;
                return s;
            }
        }

        public string Address
        {
            get { return oct1 + "." + oct2 + "." + oct3 + "." + oct4; }
            set
            {
                var delimeters = new[] { '.' };
                var addressBytes = value.Split(delimeters);
                oct1 = Convert.ToByte(addressBytes[0]);
                oct2 = Convert.ToByte(addressBytes[1]);
                oct3 = Convert.ToByte(addressBytes[2]);
                oct4 = Convert.ToByte(addressBytes[3]);                
            }
        }

        public static bool IsValid(string ipAddress)
        {
            try
            {
                var pattern = @"\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b";
                var result = Regex.IsMatch(ipAddress, pattern);
                return result;    
            }
            catch
            {
                return false;
            }            
        }

        public static IPAddressV4 FromMetabaseFormat(string metabaseIpAddress)
        {
            var delimeters = new[] {','};
            var addressPair = metabaseIpAddress.Split(delimeters, StringSplitOptions.RemoveEmptyEntries);
            var ipAddress = addressPair[0].Trim();
            var ipMask = addressPair[1].Trim();

            var ipAddressV4 = new IPAddressV4(ipAddress, ipMask);
            return ipAddressV4;
        }

        public int CompareTo(object obj)
        {
            var ipAddress = (IPAddressV4)obj;
            return this.ToStringSortableRepresentation().CompareTo(ipAddress.ToStringSortableRepresentation());
        }

        public string ToStringSortableRepresentation()
        {
            string full = "";
            var delimeters = new[] {'.' };
            var fields = Address.Split(delimeters);
            for (int j=0; j<fields.Length; j++)
            {
                int number = Convert.ToInt32(fields[j]);                
                full += number.ToString("000");
                if (j != 3)
                    full += ".";
            }
            return full;
        }
    }
}