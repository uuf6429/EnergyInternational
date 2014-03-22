using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace EnergyInternational
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializePlayer();
        }

        protected CookieContainer cookies = new CookieContainer();

        protected void LoadHtml(string html)
        {
            webBrowser1.Navigate("about:blank");
            if (webBrowser1.Document != null) webBrowser1.Document.Write(string.Empty);
            html = html.Replace("window.close(", "window.external.Close(");
            html = html.Replace("window.resizeTo(", "window.external.Resize(");
            html = html.Replace("window.moveTo(", "window.external.Move(");
            webBrowser1.DocumentText = html;
        }

        protected void InitializePlayer()
        {
            webBrowser1.AllowWebBrowserDrop = false;
            webBrowser1.IsWebBrowserContextMenuEnabled = false;
            webBrowser1.WebBrowserShortcutsEnabled = false;
            webBrowser1.ObjectForScripting = new ExternalInterface(this);

            WebRequest req = WebRequest.Create("https://raw.githubusercontent.com/uuf6429/NRJ.hta/master/NRJ.hta?nocache");
            req.Timeout = 10000;
            if (req.GetType() == typeof(HttpWebRequest)) (req as HttpWebRequest).CookieContainer = cookies;
            WebResponse rsp = req.GetResponse();
            StreamReader reader = new StreamReader(rsp.GetResponseStream());
            this.LoadHtml(reader.ReadToEnd());
            this.Show();
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.Focus();
            this.BringToFront();
            this.TopMost = false;
        }
    }
}
