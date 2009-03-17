using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace HomeServerConsoleTab.WebLogs
{
    public static class MyLogger
    {
        private static string EventLogName = "Application";
        public static int DebugLevel = 0;

        public static void Log(EventLogEntryType logType, Exception e)
        {
            Log(logType, e.Message);
        }

        public static void Log(EventLogEntryType logType, string message) 
        {
            if (EventLog.SourceExists(EventLogName))
            {
                // Inserting into event log
                EventLog Log = new EventLog(EventLogName);
                Log.Source = "weblogs";
                //try
                //{
                    Log.WriteEntry(message, logType);
                //}
                //not much to do here, we're already logging an exception probably.
                //catch { }
            }
        }

        public static void DebugLog(string message)
        {
            if (DebugLevel > 0)
            {
                Log(EventLogEntryType.Information, message);
            }
        }
    }
}
