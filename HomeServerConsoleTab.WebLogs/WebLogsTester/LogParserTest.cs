using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using NUnit.Framework;
using NUnit.Core;
using HomeServerConsoleTab.WebLogs;

namespace WebLogsTest
{
    [TestFixture] 
    public class WebLogsTest
    {
        [Test]
        public void LogParseTest()
        {
            string path = @"..\..\sample logs\";
            LogParser lp = new LogParser();

            //max logs count check
            List<IISLogEntry> logs2 = lp.ParseAllLogs(1, path);
            Console.Out.WriteLine("Found " + logs2.Count + " logs");
            Assert.AreEqual(1, logs2.Count);

            //make sure the iterators work
            List<IISLogEntry> logs1 = lp.ParseAllLogs(50000, path);
            //make sure we read the indexes right
            Assert.AreNotEqual(-1, lp.GetEntryIndexes.c_ip);
            Assert.AreNotEqual(-1, lp.GetEntryIndexes.cs_uri_stem);
            Assert.AreNotEqual(-1, lp.GetEntryIndexes.cs_username);
            Assert.AreNotEqual(-1, lp.GetEntryIndexes.date);
            Assert.AreNotEqual(-1, lp.GetEntryIndexes.time);
            Console.Out.WriteLine("Found " + logs1.Count + " logs");

            //check the log files for the entries we use.
            foreach (IISLogEntry log in logs1)
            {
                Assert.IsNotNull(log.date);
                Assert.IsNotEmpty(log.date);
                Assert.IsNotNull(log.time);
                Assert.IsNotEmpty(log.time);
                Assert.IsNotNull(log.c_ip);
                Assert.IsNotEmpty(log.c_ip);
                Assert.IsNotNull(log.cs_uri_stem);
                Assert.IsNotEmpty(log.cs_uri_stem);
                Assert.IsNotNull(log.cs_username);
                Assert.IsNotEmpty(log.cs_username);
                Assert.IsNotNull(log.time);
                Assert.IsNotEmpty(log.time);


                //check date/time parsing
                DateTime dt = lp.ParseIISDateTime(log.date, log.time);
                Assert.IsNotNull(dt);
                Assert.AreNotEqual(dt, DateTime.MinValue);
            }

            //file listing and date sorting
            FileInfo[] logs = lp.ListLogs();
            FileInfo prev = null;
            foreach (FileInfo f in logs)
            {
                if (prev != null)
                {
                    Assert.LessOrEqual(f.LastWriteTime, prev.LastWriteTime);
                }
                prev = f;
            }

            Assert.AreEqual(logs.Length, new DirectoryInfo(path).GetFiles().Length);
        }

        [Test]
        public void LicenseTest()
        {
            //?            
        }

        [Test]
        public void TestSameSubnet()
        {
            List<LocalNetworkInfo> lans = new List<LocalNetworkInfo>();
            LocalNetworkInfo lan1 = new LocalNetworkInfo(IPAddress.Parse("192.168.0.1"), IPAddress.Parse("255.255.255.0"));
            LocalNetworkInfo lan2 = new LocalNetworkInfo(IPAddress.Parse("1.2.3.4"), IPAddress.Parse("255.255.248.0"));
            LocalNetworkInfo lan3 = new LocalNetworkInfo(IPAddress.Parse("5.6.7.8"), IPAddress.Parse("255.255.0.0"));
            LocalNetworkInfo lan4 = new LocalNetworkInfo(IPAddress.Parse("9.10.11.12"), IPAddress.Parse("255.0.0.0"));
            lans.Add(lan1);
            lans.Add(lan2);

            Assert.IsTrue(IPAddressExtensions.IsInSameSubnet(IPAddress.Parse("192.168.0.200"), lan1.IP, lan1.Subnet));
            Assert.IsTrue(IPAddressExtensions.IsInSameSubnet(IPAddress.Parse("192.168.0.254"), lan1.IP, lan1.Subnet));
            Assert.IsTrue(IPAddressExtensions.IsInSameSubnet(IPAddress.Parse("192.168.0.1"), lan1.IP, lan1.Subnet));
            Assert.IsFalse(IPAddressExtensions.IsInSameSubnet(IPAddress.Parse("191.168.0.254"), lan1.IP, lan1.Subnet));
            Assert.IsFalse(IPAddressExtensions.IsInSameSubnet(IPAddress.Parse("192.168.1.254"), lan1.IP, lan1.Subnet));
            Assert.IsFalse(IPAddressExtensions.IsInSameSubnet(IPAddress.Parse("192.168.1.1"), lan1.IP, lan1.Subnet));
        }
    }
}
