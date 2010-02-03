using System;
using System.Collections.Generic;
using System.Text;

namespace HomeServerConsoleTab.WebLogs
{
    public class IISLogEntry
    {
        public string date;
        public string time;
        public string cs_uri_stem;
        public string cs_username;
        public string c_ip;

        public IISLogEntry(string eDate, string eTime, string eUriStem, string eUsername, string eIp)
        {
            date = eDate;
            time = eTime;
            cs_uri_stem = eUriStem;
            cs_username = eUsername;
            c_ip = eIp;
        }
    };   

    public class IISLogEntryIndex
    {
        public const string DATE = "date";                 //used
        public const string TIME = "time";                 //used
        public const string CS_URI_STEM = "cs-uri-stem";   //used
        public const string CS_USERNAME = "cs-username";   //used
        public const string C_IP = "c-ip";                 //used

        public int date = -1;
        public int time = -1;
        public int cs_uri_stem = -1;
        public int cs_username = -1;
        public int c_ip = -1;
    }
}
