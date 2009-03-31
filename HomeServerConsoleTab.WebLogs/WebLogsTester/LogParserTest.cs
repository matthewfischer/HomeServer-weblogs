using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            //max logs count
            List<string[]> logs1 = lp.ParseAllLogs(100, path);
            Console.Out.WriteLine("Found " + logs1.Count + " logs");
            Assert.AreEqual(100, logs1.Count);
            List<string[]> logs2 = lp.ParseAllLogs(1, path);
            Console.Out.WriteLine("Found " + logs2.Count + " logs");
            Assert.AreEqual(1, logs2.Count);
            List<string[]> logs3 = lp.ParseAllLogs(500, path);
            Console.Out.WriteLine("Found " + logs3.Count + " logs");
            Assert.AreEqual(500, logs3.Count);
            List<string[]> logs4 = lp.ParseAllLogs(500000, path);
            Console.Out.WriteLine("Found " + logs4.Count + " logs");
            Assert.GreaterOrEqual(500000, logs4.Count);

            //check the log files for the entries we use.
            foreach (string[] log in logs4)
            {
                Assert.IsNotNull(log[0]);
                Assert.IsNotNull(log[1]);
                Assert.IsNotNull(log[5]);
                Assert.IsNotNull(log[8]);
                Assert.IsNotNull(log[9]);

                //check date/time parsing
                DateTime dt = lp.ParseIISDateTime(log[0], log[1]);
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
    }
}
