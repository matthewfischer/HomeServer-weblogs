using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using Microsoft.HomeServer.Controls;
using System.Windows.Forms;

namespace HomeServerConsoleTab.WebLogs
{
    public class LogParser
    {
        private string logDir = @"C:\Windows\System32\LogFiles\W3SVC1\";
        private int max;
        private int numberLoaded = 0;

        private IISLogEntryIndex eIdx;
        public IISLogEntryIndex GetEntryIndexes
        {
            get { return eIdx; }
        }

        public LogParser()
        {
            eIdx = new IISLogEntryIndex();
        }

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

        public List<IISLogEntry> ParseAllLogs(int maxEntries, string path)
        {
            logDir = path;
            return ParseAllLogs(maxEntries);
        }

        public List<IISLogEntry> ParseAllLogs(int maxEntries)
        {
            numberLoaded = 0;
            max = maxEntries;
            MyLogger.DebugLog("File Looper: Loading max of " + max + " logs");

            List<IISLogEntry> parsedLines = new List<IISLogEntry>();

            foreach (FileInfo f in ListLogs())
            {
                List<IISLogEntry> tmp = ParseLog(f);
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

        public void UpdateLogFormat(string line)
        {
            line = line.Substring("#Fields: ".Length);
            string[] fields = line.Split(' ');
            if ((fields == null) || (fields.Length < 1))
            {
                MyLogger.Log(EventLogEntryType.Warning, "Field delimited line is malformed: " + line);
                return;
            }

            for (int i = 0; i < fields.Length; i++)
            {
                if (fields[i].Equals(IISLogEntryIndex.DATE))
                {
                    eIdx.date = i;
                }
                else if (fields[i].Equals(IISLogEntryIndex.TIME))
                {
                    eIdx.time = i;
                }
                else if (fields[i].Equals(IISLogEntryIndex.C_IP))
                {
                    eIdx.c_ip = i;
                }
                else if (fields[i].Equals(IISLogEntryIndex.CS_URI_STEM))
                {
                    eIdx.cs_uri_stem = i;
                }
                else if (fields[i].Equals(IISLogEntryIndex.CS_USERNAME))
                {
                    eIdx.cs_username = i;
                }
                else if (fields[i].Equals(IISLogEntryIndex.SC_STATUS))
                {
                    MyLogger.Log(EventLogEntryType.Warning, "Found sc_status at index " + i);
                    eIdx.sc_status = i;
                }
            }
        }

        /// <summary>
        /// Returns true if all the required fields are set, optional fields don't count
        /// </summary>
        /// <returns></returns>
        public bool AreIndexesSet()
        {
            return ((eIdx.c_ip != -1) && (eIdx.cs_uri_stem != -1) && (eIdx.cs_username != -1)
                && (eIdx.date != -1) && (eIdx.time != -1));
        }

        public List<IISLogEntry> ParseLog(FileInfo log)
        {
            List<IISLogEntry> parsedLines = new List<IISLogEntry>();
            FileStream fs = null;
            StreamReader SR = null;

            eIdx = new IISLogEntryIndex();

            try
            {
                fs = File.Open(log.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                SR = new StreamReader(fs);

                string line = SR.ReadLine();
                while ((line != null) && (max > 0))
                {
                    if (line.StartsWith("#Fields: "))
                    {
                        UpdateLogFormat(line);
                    }
                    else if (line.StartsWith("#") == false)
                    {
                        if (AreIndexesSet())
                        {
                            string[] fields = line.Split(' ');
                            try
                            {
                                string sc_status = String.Empty;
                                if (eIdx.sc_status >= 0)
                                {
                                    sc_status = fields[eIdx.sc_status];
                                }
                                IISLogEntry entry = new IISLogEntry(fields[eIdx.date], fields[eIdx.time],
                                    fields[eIdx.cs_uri_stem], fields[eIdx.cs_username], fields[eIdx.c_ip], 
                                    sc_status);
                                parsedLines.Add(entry);
                            }
                            catch (Exception ex)
                            {
                                MyLogger.Log(EventLogEntryType.Warning, "Error when parsing line: " + line + "\n" + ex.Message);
                            }
                            max--;
                        }
                        else
                        {
                            string message = "File is missing critical fields or there was an earlier error in"
                             + " parsing field headers.  File = " + log.FullName;
                            QMessageBox.Show(message + "\n  Please report this to matt+whs@mattfischer.com and send the file mentioned if possible",
                                "Log File Format Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            MyLogger.Log(EventLogEntryType.Error, message);
                        }
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
