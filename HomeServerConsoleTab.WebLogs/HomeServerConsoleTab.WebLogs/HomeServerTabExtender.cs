using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.HomeServer.Extensibility;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

//To Do:
// figure out how to install it and ship it


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
            return false;
        }

        //??? where did this come from?        
        public Guid SettingsGuid { get { return new Guid("DEADBEEF-3954-4084-B64C-6EF492840BCE"); } }
    }
}
