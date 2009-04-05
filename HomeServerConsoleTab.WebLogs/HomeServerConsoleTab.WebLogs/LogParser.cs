using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Globalization;

namespace HomeServerConsoleTab.WebLogs
{
    public class LogParser
    {
        private string logDir = @"C:\Windows\System32\LogFiles\W3SVC1\";
        private int max;
        private int numberLoaded = 0;

        public int GetNumberLoaded()
        {
            return numberLoaded;
        }

        internal class SortableFileList : IComparer<FileInfo>
        {
            public int Compare(FileInfo x, FileInfo y)
            {
                //compare in this order to get what I want back...
                return DateTime.Compare(y.LastWriteTime, x.LastWriteTime);
            }
        }

        public List<string[]> ParseAllLogs(int maxEntries, string path)
        {
            logDir = path;
            return ParseAllLogs(maxEntries);
        }

        public List<string[]> ParseAllLogs(int maxEntries)
        {
            numberLoaded = 0;
            max = maxEntries;
            MyLogger.DebugLog("File Looper: Loading max of " + max + " logs");

            List<string[]> parsedLines = new List<string[]>();            

            foreach (FileInfo f in ListLogs())
            {
                List<string[]> tmp = ParseLog(f);
                if (tmp != null)
                {
                    numberLoaded += tmp.Count;
                    parsedLines.AddRange(tmp);
                    //MyLogger.DebugLog("max = " + max + " - pl = " + parsedLines.Count);
                    if (max <= 0)
                    {
                        return parsedLines;
                    }
                }
            }
            MyLogger.DebugLog("No more logs, returning.");
            return parsedLines;
        }

        private IComparer<FileInfo> SortByLastWriteTime()
        {
            return (IComparer<FileInfo>)new SortableFileList();
        }

        public FileInfo[] ListLogs()
        {            
            try
            {
                DirectoryInfo dir = new DirectoryInfo(logDir);
                FileInfo [] flist = dir.GetFiles("*.log");
                //MyLogger.DebugLog("first entry = " + flist[0].LastWriteTime.ToString());
                Array.Sort(flist, SortByLastWriteTime());
                //MyLogger.DebugLog("SORTED first entry = " + flist[0].LastWriteTime.ToString());
                return flist;
            }
            catch (Exception e)
            {
                MyLogger.Log(EventLogEntryType.Error, e);
                return null;
            }            
        }

        public DateTime ParseIISDateTime(string date, string time)
        {
            //date: 2009-03-08  time: 00:17:07            
            string expectedFormat = "yyyy-MM-dd HH:mm:ss";
            IFormatProvider culture = CultureInfo.CurrentCulture;

            try
            {
                DateTime dt = DateTime.ParseExact(date + " " + time, expectedFormat, culture);
                return dt.ToLocalTime();
            }
            catch (Exception e)
            {
                MyLogger.Log(EventLogEntryType.Warning, "Error parsing IIS date/time to DateTime object.  Date: " + date + "\n"
                    + "Time: " + time + "\n" + e.Message);
                return DateTime.MinValue;
            }
        }

        public List<string []> ParseLog(FileInfo log)
        {
            List<string []> parsedLines = new List<string[]>();
            FileStream fs = null;
            StreamReader SR = null;

            try
            {
                fs = File.Open(log.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                SR = new StreamReader(fs);

                string line = SR.ReadLine();
                while ((line != null) && (max > 0))
                {
                    if (line.StartsWith("#") == false)
                    {
                        max--;
                        parsedLines.Add(line.Split(' '));
                    }
                    line = SR.ReadLine();
                }
            }
            catch (Exception e)
            {
                MyLogger.Log(EventLogEntryType.Error, e);
                return null;
            }
            finally
            {
                if (SR != null)
                {
                    SR.Close();
                }
                if (fs != null)
                {
                    fs.Close();
                }
            }
            return parsedLines;
        }
    }
}
