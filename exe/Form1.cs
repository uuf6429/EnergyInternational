using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Cache;
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

        protected void ShowError()
        {
            this.LoadHtml(@"<!DOCTYPE html>
            <html>
                <head>
                    <meta http-equiv='x-ua-compatible' content='ie=9'/>
                    <style type='text/css'>
                        html, body {
				            background: #1A1A1A;
                            overflow: hidden;
				            font: 13px Arial;
                            padding: 0;
                            margin: 0;
                        }
                        a, a:hover, a:visited {
                            color: red;
                            text-decoration: none;
                            text-transform: uppercase;
                        }
                        div {
                            border: 8px solid #C51B24;
                            width: 48px;
                            height: 48px;
                            margin: 8px auto;
                            color: #C51B24;
                            font-size: 42px;
                            text-align: center;
                            line-height: 48px;
                            border-radius: 128px;
                            -ms-border-radius: 128px;
                        }
                        #close {
                            position: absolute;
                            top: 4px;
                            right: 4px;
                            background: #000;
                            padding: 4px;
                            font: 16px Arial;
                            line-height: 10px;
                        }
                    </style>
                </head>
                <body>
                    <div>!</div>
                    <center>
                        <a id='retry' href='javascript:window.external.Reload();'>Retry</a>
                        <a id='close' href='javascript:window.external.Close();'>&times;</a>
                    </center>
                </body>
            </html>");
        }

        public void TryConnect()
        {
            try
            {
                WebRequest req = WebRequest.Create("https://raw.githubusercontent.com/uuf6429/EnergyInternational/master/hta/NRJ.hta?nocache");
                req.Timeout = 10000;
                req.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                if (req.GetType() == typeof(HttpWebRequest)) (req as HttpWebRequest).CookieContainer = cookies;
                WebResponse rsp = req.GetResponse();
                StreamReader reader = new StreamReader(rsp.GetResponseStream());
                this.LoadHtml(reader.ReadToEnd());
            }
            catch (Exception)
            {
                this.ShowError();
            }
        }

        protected void InitializePlayer()
        {
            webBrowser1.AllowWebBrowserDrop = false;
            webBrowser1.WebBrowserShortcutsEnabled = false;
            webBrowser1.ObjectForScripting = new ExternalInterface(this);
            this.TryConnect();
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
