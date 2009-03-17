using System;
using System.Collections.Generic;
using System.Text;

namespace HomeServerConsoleTab.WebLogs
{
    //Example:
    //2008-11-13 21:30:02 W3SVC1 127.0.0.1 GET /MediaSmartUpdate/rest/catalog/cancelupdate - 80 - 127.0.0.1 - 200 0 0
    public enum IISLog
    {
        date,          //used
        time,          //used
        s_sitename,    //not useful
        s_ip,          //not useful
        cs_method,     //not useful
        cs_uri_stem,   //future
        cs_uri_query,  //?
        s_port,        //not useful
        cs_username,   //used
        c_ip,          //used
        cs_user_agent, //future
        sc_status,     //not used
        sc_substatus,  //not used
        sc_win32_status//not used
    };    
}
