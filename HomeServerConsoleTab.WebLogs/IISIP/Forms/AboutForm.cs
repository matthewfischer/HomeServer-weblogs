//-----------------------------------------------------------------
// Copyright Lee Whitney 2008-2009
// http://www.hdgreetings.com/other/Block-IP-IIS
// Feel free to use and modify, please just keep this copyright in place.
//-----------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace IISIP
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            label1.Text = App.Name;
            label2.Text = App.HelpUrl;
            label3.Text = App.LastUpdated;
            label4.Text = "Version " + App.Version;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Process.Start(App.HelpUrl);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutForm_Paint(object sender, PaintEventArgs e)
        {
            //    Rectangle r = new Rectangle(253, 20, 200, 200);
            //    ImageUtil.DrawDropShadow(
            //        e.Graphics,
            //        System.Drawing.Color.White,
            //        r.Left, r.Top, r.Width, r.Height);
            //    e.Graphics.FillRectangle(System.Drawing.Brushes.Black, r);

            //    ImageUtil.CreateDrawingVisualText();
        }
    }
}