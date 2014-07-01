using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

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
                    var subMenuItem1 = new ToolStripMenuItem("Open");
                    subMenuItem1.Click += (object sender, System.EventArgs e) =>
                    {
                        Stream myStream = null;
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.InitialDirectory = "c:\\";
                        openFileDialog.Filter = "xml files (*.xml)|*.xml";
                        openFileDialog.FilterIndex = 2;
                        openFileDialog.RestoreDirectory = true;

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                if ((myStream = openFileDialog.OpenFile()) != null)
                                {
                                    using (myStream)
                                    {
                                        XmlDocument doc = new XmlDocument();
                                        doc.Load(myStream);
                                        foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                                        {
                                            string text = node.InnerText;
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                            }
                        }
                    };
                    menuItem.DropDown.Items.Add(subMenuItem1);
                }

                    var subMenuItem0 = new ToolStripMenuItem("Exit");
                    subMenuItem0.Click += (sender, e) => Application.Exit();
                    menuItem.DropDown.Items.Add(subMenuItem0);
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
