using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QRkey
{
    public class Form : System.Windows.Forms.Form
    {
        public Form()
            : base()
        {
            Menustrip = new MenuStrip();

            {
                var menuItem = new ToolStripMenuItem("File");
                Menustrip.Items.Add(menuItem);
                {
                    var subMenuItem = new ToolStripMenuItem("Exit");
                    subMenuItem.Click += (sender, e) => Application.Exit();
                    menuItem.DropDown.Items.Add(subMenuItem);
                }
            }

            Controls.Add(Menustrip);
            Text = "QRkey";
        }

        public MenuStrip Menustrip
        {
            get;
            private set;
        }
    }
}
