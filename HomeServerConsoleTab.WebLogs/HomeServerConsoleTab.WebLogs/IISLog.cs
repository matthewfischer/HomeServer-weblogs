using System;
using System.Collections.Generic;
using System.Text;
using System.Web;


namespace HomeServerConsoleTab.WebLogs
{
    public class IISLogEntry
    {
        public const int STATUS_UNKNOWN = -1;
        public string date;
        public string time;
        public string cs_uri_stem;
        public string cs_username;
        public string c_ip;
        public int sc_status;

        public IISLogEntry(string eDate, string eTime, string eUriStem, string eUsername, string eIp, string eScStatus)
        {
            date = eDate;
            time = eTime;
            cs_uri_stem = eUriStem;
            cs_username = eUsername;
            c_ip = eIp;
            try
            {
                if (!String.IsNullOrEmpty(eScStatus))
                {
                    sc_status = Int32.Parse(eScStatus);
                }
                else
                {
                    sc_status = STATUS_UNKNOWN;
                }
            }
            catch
            {
                sc_status = STATUS_UNKNOWN;
            }
        }
    };   

    public class IISLogEntryIndex
    {
        public const string DATE = "date";                 //used
        public const string TIME = "time";                 //used
        public const string CS_URI_STEM = "cs-uri-stem";   //used
        public const string CS_USERNAME = "cs-username";   //used
        public const string C_IP = "c-ip";                 //used
        public const string SC_STATUS = "sc-status";       //optional

        public int date = -1;
        public int time = -1;
        public int cs_uri_stem = -1;
        public int cs_username = -1;
        public int c_ip = -1;
        public int sc_status = -1;
    }
}
