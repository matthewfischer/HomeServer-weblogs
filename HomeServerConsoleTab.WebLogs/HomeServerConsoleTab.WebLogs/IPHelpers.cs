using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Management;
using System.Diagnostics;

namespace HomeServerConsoleTab.WebLogs
{
    public class LocalNetworkInfo
    {
        private readonly IPAddress m_Ip;
        private readonly IPAddress m_Subnet;

        public IPAddress IP
        {
            get { return m_Ip; }
        }

        public IPAddress Subnet
        {
            get { return m_Subnet; }
        }

        public LocalNetworkInfo(IPAddress ip, IPAddress subnet)
        {
            m_Ip = ip;
            m_Subnet = subnet;
        }
    }

    public class IPAddressExtensions
    {        
        private static IPAddressExtensions instance;
        private List<LocalNetworkInfo> localLanInfo;

        public IPAddressExtensions()
        {
            localLanInfo = new List<LocalNetworkInfo>();
            UpdateLANInfo();
        }

        public static IPAddressExtensions GetInstance()
        {
            if (instance == null)
            {
                instance = new IPAddressExtensions();
            }
            return instance;
        }

        public void UpdateLANInfo()
        {
            ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 'TRUE'");
            ManagementObjectCollection queryCollection = query.Get();
            foreach (ManagementObject mo in queryCollection)
            {
                string[] subnets = (string[])mo["IPSubnet"];
                string[] addresses = (string[])mo["IPAddress"];
                if (subnets.Length != addresses.Length)
                {
                    MyLogger.Log(EventLogEntryType.Warning, "Subnets length = " + subnets.Length + " but Addresses length = " + addresses.Length);
                }

                for (int i = 0; i < addresses.Length; i++)
                {
                    try
                    {
                        IPAddress ip_address = IPAddress.Parse(addresses[i]);
                        IPAddress ip_subnet = IPAddress.Parse(subnets[i]);
                        if (ip_address.Equals(IPAddress.Parse("0.0.0.0")))
                        {
                            //skip this one
                        }
                        else
                        {
                            localLanInfo.Add(new LocalNetworkInfo(ip_address, ip_subnet));
                        }
                    }
                    catch (Exception e)
                    {
                        MyLogger.Log(EventLogEntryType.Error, e);
                    }
                }
            }
        }

        public bool IsLocalAddress(IPAddress ip)
        {
            foreach (LocalNetworkInfo lni in localLanInfo)
            {
                if (IsInSameSubnet(ip, lni.IP, lni.Subnet))
                {
                    return true;
                }
            }
            return false;
        }

        public static IPAddress GetBroadcastAddress(IPAddress address, IPAddress subnetMask)
        {
            byte[] ipAdressBytes = address.GetAddressBytes();
            byte[] subnetMaskBytes = subnetMask.GetAddressBytes();

            if (ipAdressBytes.Length != subnetMaskBytes.Length)
                throw new ArgumentException("Lengths of IP address and subnet mask do not match.");

            byte[] broadcastAddress = new byte[ipAdressBytes.Length];
            for (int i = 0; i < broadcastAddress.Length; i++)
            {
                broadcastAddress[i] = (byte)(ipAdressBytes[i] | (subnetMaskBytes[i] ^ 255));
            }
            return new IPAddress(broadcastAddress);
        }

        public static IPAddress GetNetworkAddress(IPAddress address, IPAddress subnetMask)
        {
            byte[] ipAdressBytes = address.GetAddressBytes();
            byte[] subnetMaskBytes = subnetMask.GetAddressBytes();

            if (ipAdressBytes.Length != subnetMaskBytes.Length)
                throw new ArgumentException("Lengths of IP address and subnet mask do not match.");

            byte[] broadcastAddress = new byte[ipAdressBytes.Length];
            for (int i = 0; i < broadcastAddress.Length; i++)
            {
                broadcastAddress[i] = (byte)(ipAdressBytes[i] & (subnetMaskBytes[i]));
            }
            return new IPAddress(broadcastAddress);
        }

        public static bool IsInSameSubnet(IPAddress address2, IPAddress address, IPAddress subnetMask)
        {
            IPAddress network1 = GetNetworkAddress(address, subnetMask);
            IPAddress network2 = GetNetworkAddress(address2, subnetMask);
            return network1.Equals(network2);
        }
    }
}
