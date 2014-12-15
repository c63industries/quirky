using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace C63.Quirky
{
    public class Form : System.Windows.Forms.Form
    {
        public static void Error(string error)
        {
            if (error != null)
            {
                MessageBox.Show(error);
            }
        }

        public Form()
            : base()
        {
            Menustrip = new MenuStrip();

            {
                var menuItem = new ToolStripMenuItem("File");
                Menustrip.Items.Add(menuItem);

                {
                    var subMenuItem = new ToolStripMenuItem("Open Keys");
                    subMenuItem.Click += (object sender, System.EventArgs e) =>
                    {
                        subMenuItem.Checked = true;
                        Stream myStream = null;
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.InitialDirectory = "c:\\";
                        openFileDialog.Filter = "csv files (*.csv)|*.csv|txt files (*.txt)|*txt";
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
                                        string path = openFileDialog.FileName;
                                        string[] readText = File.ReadAllLines(path);
                                        Generator.Keys = readText;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Error("Error: Could not read file from disk. Original error: " + ex.Message);
                            }
                        }
                    };
                    menuItem.DropDown.Items.Add(subMenuItem);
                }

                {
                    var subMenuItem = new ToolStripMenuItem("Open XML");
                    subMenuItem.Click += (object sender, System.EventArgs e) =>
                    {
                        subMenuItem.Checked = true;
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
                                        Generator.XML = doc.DocumentElement;
                                        Generator.BasePath = Path.GetDirectoryName(openFileDialog.FileName);
                                        this.BackgroundImage = Generator.Generate();
                                        this.BackgroundImageLayout = ImageLayout.Center;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Error("Error: Could not read file from disk. Original error: " + ex.Message);
                            }
                        }
                    };
                    menuItem.DropDown.Items.Add(subMenuItem);
                }

                {
                    var subMenuItem = new ToolStripMenuItem("Exit");
                    subMenuItem.Click += (sender, e) => Application.Exit();
                    menuItem.DropDown.Items.Add(subMenuItem);
                }
            }

            {
                var menuItem = new ToolStripMenuItem("Actions");
                Menustrip.Items.Add(menuItem);

                {
                    var subMenuItem = new ToolStripMenuItem("Generate");
                    subMenuItem.Click += (sender, e) => this.BackgroundImage = Generator.Generate();
                    menuItem.DropDown.Items.Add(subMenuItem);
                }
                {
                    var subMenuItem = new ToolStripMenuItem("Execute");
                    subMenuItem.Click += (sender, e) => Generator.Execute();
                    menuItem.DropDown.Items.Add(subMenuItem);
                }
            }

            Controls.Add(Menustrip);
            Text = "qUIrKY";
        }

        public MenuStrip Menustrip
        {
            get;
            private set;
        }
    }
}
