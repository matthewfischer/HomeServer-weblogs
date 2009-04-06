using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.HomeServer.Extensibility;
using Microsoft.HomeServer.Controls;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Diagnostics;

//Stuff to Fix:
//1) background color of the checkboxes
//2) have blocked sites dialog return a value if something changes and only re-draw then.

//1.0.5.1
//-------
//1) fixed button names after blocking/unblocking

namespace HomeServerConsoleTab.WebLogs
{
    public class HomeServerTabExtender : IConsoleTab
    {
        private IConsoleServices services;
        private Control tabControl;

        public HomeServerTabExtender(int width, int height, IConsoleServices consoleServices)
        {
            tabControl = new LogControl(consoleServices);
            tabControl.Size = new Size(width, height);
            this.services = consoleServices;           
        }
        public string TabText { get { return "Web Logs"; } }
        public Bitmap TabImage { get { return Properties.Resources.logs2; } }
        public Control TabControl { get { return tabControl; } }

        public bool GetHelp()
        {
            string msgBoxTxt;
            string version;

            //get the version info
            Assembly me = Assembly.GetExecutingAssembly();
            FileVersionInfo fv = FileVersionInfo.GetVersionInfo(me.Location);
            version = fv.ProductVersion.ToString();

            string ipBlockCredits = "\nWith IP Blocking code courtesy of:\nGeorge Atkins, Lee Whitney, and www.okean.com.\n" + 
                "Source available from: http://www.hdgreetings.com/other/Block-IP-IIS";

            msgBoxTxt = "WebLogs Add-In for Windows Home Server\n\nVersion: " + version + 
                "\nFor more help, please go to http://mattfischer.com/whs/help.html\n\n" + ipBlockCredits;

            QMessageBox.Show(msgBoxTxt, "WebLogs", MessageBoxButtons.OK, MessageBoxIcon.Question);
            return true;
        }

        //??? where did this come from?        
        public Guid SettingsGuid { get { return new Guid("DEADBEEF-3954-4084-B64C-6EF492840BCE"); } }
    }
}
