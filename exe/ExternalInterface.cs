using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnergyInternational
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisibleAttribute(true)]
    public class ExternalInterface
    {
        protected Form1 Form;

        public ExternalInterface(Form1 form)
        {
            this.Form = form;
        }

        public void Close()
        {
            Form.Close();
        }

        public void Resize(int w, int h)
        {
            Form.Size = new Size(w, h);
        }

        public void Move(int x, int y)
        {
            Form.Location = new Point(x, y);
        }

        public String GetCookie(String name)
        {
            return Registry.CurrentUser.CreateSubKey("Software\\" + Application.ProductName, RegistryKeyPermissionCheck.ReadWriteSubTree)
                .GetValue(name, "").ToString();
        }

        public void SetCookie(String name, String value)
        {
            Registry.CurrentUser.CreateSubKey("Software\\" + Application.ProductName, RegistryKeyPermissionCheck.ReadWriteSubTree)
                .SetValue(name, value);
        }

        public void DelCookie(String name)
        {
            Registry.CurrentUser.CreateSubKey("Software\\" + Application.ProductName, RegistryKeyPermissionCheck.ReadWriteSubTree)
                .DeleteValue(name);
        }

        public void Reload()
        {
            Form.TryConnect();
        }
    }
}
