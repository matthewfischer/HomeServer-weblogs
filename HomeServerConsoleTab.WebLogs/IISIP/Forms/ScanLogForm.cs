//-----------------------------------------------------------------
// Copyright Lee Whitney 2008-2009
// http://www.hdgreetings.com/other/Block-IP-IIS
// Feel free to use and modify, please just keep this copyright in place.
//-----------------------------------------------------------------
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace IISIP
{
    public partial class ScanLogForm : Form
    {
        private const string smtpMarker = "domain";
        private readonly int markerLength = smtpMarker.Length;
        private int count;
        private EventLog eventLog;
        private int eventsMatched, itemsExtracted;
        private StringBuilder stringBuilderEvents;

        public ScanLogForm()
        {
            InitializeComponent();

            var ipType = new ExtractItemMode("IP Addresses from Application Events", ExtractItemType.IPAddress);
            var emailType = new ExtractItemMode("EMail Addresses from the SMTP Service", ExtractItemType.EMailAddress);
            extract_ComboBox.Items.Add(ipType);
            extract_ComboBox.Items.Add(emailType);
            extract_ComboBox.SelectedIndex = 0;
            SetEventLog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetEventLog()
        {
            var extractItem = (ExtractItemMode) extract_ComboBox.SelectedItem;

            if (extractItem.Type == ExtractItemType.IPAddress)
                eventLog = new EventLog("Application");
            else if (extractItem.Type == ExtractItemType.EMailAddress)
                //eventLog = new EventLog("System");
                eventLog = new EventLog("System", ".", "SMTPSVC");
            else
                throw new ApplicationException("invalid");

            statusBarLabel.Text = eventLog.LogDisplayName + " log contains " + eventLog.Entries.Count + " events";
        }

        private void find_Button_Click(object sender, EventArgs e)
        {
            extractedItem_ListBox.Items.Clear();
            resultsLabel.Text = "Events found:";
            eventColor.BackColor = Color.Gray;
            count = 0;
            eventsMatched = 0;
            itemsExtracted = 0;
            stringBuilderEvents = new StringBuilder();
            var searchText = textToFind.Text;
            if (ignoreCaseCheckBox.Checked)
                searchText = searchText.ToLower();
            var extractItem = (ExtractItemMode) extract_ComboBox.SelectedItem;

            var entryCount = eventLog.Entries.Count;
            for (var j = entryCount - 1; j >= 0; j--)
            {
                var entry = eventLog.Entries[j];
                if (searchText == "")
                    EventMatched(entry, extractItem, j);
                else if (ignoreCaseCheckBox.Checked)
                {
                    if (entry.Message.ToLower().Contains(searchText))
                        EventMatched(entry, extractItem, j);
                }
                else
                {
                    if (entry.Message.Contains(searchText))
                        EventMatched(entry, extractItem, j);
                }
                count++;
                if (count/50.0F == count/50)
                {
                    statusBarLabel.Text = "Scanning " + count + " of " + entryCount;
                    Application.DoEvents();
                }
            }
            if (extractedItem_ListBox.Items.Count > 0)
            {
                extractedItem_ListBox.SelectedIndex = 0;
                extractedItem_ListBox.Focus();
            }

            statusBarLabel.Text = "Loading " + eventsMatched + " items...";
            Application.DoEvents();
            eventTextBox.Text = stringBuilderEvents.ToString();

            statusBarLabel.Text = "Matched " + eventsMatched + " events, containing " + itemsExtracted + " items.";
        }

        public void EventMatched(EventLogEntry entry, ExtractItemMode extractItem, int index)
        {
            var lastValue = "";
            var lastIndex = -1;

            if (extractItem.Type == ExtractItemType.IPAddress)
            {
                var pattern = @"\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b";
                var matchCollection = Regex.Matches(entry.Message, pattern, RegexOptions.Compiled);
                foreach (Match ipAddressFound in matchCollection)
                {
                    if (!ipAddressFound.Value.Contains("00") &&
                        !ipAddressFound.Value.StartsWith("0.") &&
                        !ipAddressFound.Value.StartsWith("1.") &&
                        !ipAddressFound.Value.StartsWith("2."))
                    {
                        if (lastValue == ipAddressFound.Value && lastIndex == index)
                            continue;
                        var item = new ExtractItem(ipAddressFound.Value, index);
                        extractedItem_ListBox.Items.Add(item);
                        itemsExtracted++;
                        lastValue = ipAddressFound.Value;
                        lastIndex = index;
                    }
                }
            }
            else if (extractItem.Type == ExtractItemType.EMailAddress)
            {
                if (entry.Source.Contains("smtpsvc") == false)
                    return;

                string domain;
                var start = entry.Message.IndexOf(smtpMarker);
                if (start >= 0)
                {
                    domain = entry.Message.Substring(start + markerLength);
                    start = domain.IndexOf("'");

                    if (start >= 0)
                    {
                        domain = domain.Substring(start + 1);
                        var length = domain.IndexOf("'");
                        domain = domain.Substring(0, length);

                        var item = new ExtractItem(domain, index);
                        extractedItem_ListBox.Items.Add(item);
                        itemsExtracted++;
                    }
                }
            }
            stringBuilderEvents.AppendLine("-----------------------------------------");
            stringBuilderEvents.AppendLine(entry.Source);
            stringBuilderEvents.AppendLine(entry.TimeGenerated.ToLongTimeString() + " " + entry.TimeGenerated.ToShortDateString());
            stringBuilderEvents.AppendLine(entry.Message);
            stringBuilderEvents.AppendLine();
            eventsMatched++;
        }

        private void extract_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetEventLog();
        }

        private void copyItems_Button_Click(object sender, EventArgs e)
        {
            var unique = 0;
            var extractItem = (ExtractItemMode) extract_ComboBox.SelectedItem;
            var lastItemName = "";
            var items = new StringBuilder();
            foreach (ExtractItem item in extractedItem_ListBox.Items)
            {
                if (lastItemName != item.Name)
                {
                    if (extractItem.Type == ExtractItemType.IPAddress)
                        items.AppendLine(item.Name + ", 255.255.255.255");
                    else
                        items.AppendLine(item.Name);
                    unique++;
                }
                lastItemName = item.Name;
            }
            Clipboard.SetData(DataFormats.Text, items.ToString());
            MessageBox.Show(unique + " items copied to the clipboard.", App.Name);
        }

        private void ipListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (extractedItem_ListBox.SelectedItems.Count == 0)
                return;

            var item = (ExtractItem) extractedItem_ListBox.SelectedItem;
            eventTextBox.Text = eventLog.Entries[item.Index].Message;

            var date = eventLog.Entries[item.Index].TimeGenerated;

            var caption = date.ToLongTimeString() + " " + date.ToShortDateString() + ", ";
            var eventId = eventLog.Entries[item.Index].EventID;
            if (eventId != 0)
                caption += eventId + ", ";
            caption += eventLog.Entries[item.Index].Source;

            statusBarLabel.Text = caption;
            resultsLabel.Text = caption;

            if (eventLog.Entries[item.Index].EntryType == EventLogEntryType.Error)
                eventColor.BackColor = Color.Red;
            else if (eventLog.Entries[item.Index].EntryType == EventLogEntryType.Warning)
                eventColor.BackColor = Color.Yellow;
            else if (eventLog.Entries[item.Index].EntryType == EventLogEntryType.Information)
                eventColor.BackColor = Color.White;
            else
                eventColor.BackColor = Color.Gray;
        }

        private void removeItem_Button_Click(object sender, EventArgs e)
        {
            if (extractedItem_ListBox.SelectedItems.Count == 0)
                return;

            var itemToRemove = (ExtractItem) extractedItem_ListBox.SelectedItem;
            //DialogResult dr = MessageBox.Show("Remove " + itemToRemove.Name + " from the list?",App.Name, MessageBoxButtons.YesNo);
            //if (dr == DialogResult.No)
            //    return;

            var itemList = new List<ExtractItem>();
            foreach (ExtractItem item in extractedItem_ListBox.Items)
                itemList.Add(item);

            foreach (var item in itemList)
            {
                if (itemToRemove.Name == item.Name)
                    extractedItem_ListBox.Items.Remove(item);
            }
        }

        private void clearLog_Button_Click(object sender, EventArgs e)
        {
            var dr = MessageBox.Show("Remove all events from the " + eventLog.LogDisplayName + " log?", App.Name, MessageBoxButtons.YesNo);
            if (dr == DialogResult.No)
                return;

            eventLog.Clear();
            extractedItem_ListBox.Items.Clear();
            MessageBox.Show(eventLog.LogDisplayName + " log cleared.", App.Name);
        }

        private void copyItem_Button_Click(object sender, EventArgs e)
        {
            if (extractedItem_ListBox.SelectedItems.Count == 0)
                return;

            var item = (ExtractItem) extractedItem_ListBox.SelectedItem;
            Clipboard.SetData(DataFormats.Text, item.Name);
        }

        private void locateIP_Button_Click(object sender, EventArgs e)
        {
            if (extractedItem_ListBox.SelectedItems.Count == 0)
                return;

            var item = (ExtractItem)extractedItem_ListBox.SelectedItem;
            Clipboard.SetData(DataFormats.Text, item.Name);
            string url = "http://www.ip2location.com/free.asp";
            Process.Start(url);
        }
    }

    public class ExtractItemMode
    {
        public string Name;
        public ExtractItemType Type;

        public ExtractItemMode(string name, ExtractItemType type)
        {
            Name = name;
            Type = type;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public enum ExtractItemType
    {
        IPAddress,
        EMailAddress
    }

    public class ExtractItem
    {
        public int Index;
        public string Name;

        public ExtractItem(string name, int index)
        {
            Name = name;
            Index = index;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}


    