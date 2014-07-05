using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace QRkey
{
    class Generator
    {
        public static Graphics Generate()
        {
            if (XML == null)
            {
                return null;
            }

            if (!XML.HasChildNodes)
            {
                return null;
            }

            Bitmap bmp = new Bitmap(100, 100);
            Graphics graphics = Graphics.FromImage(bmp);

            foreach (XmlNode xmlNode in XML.ChildNodes)
            {
                Render(graphics, xmlNode);
            }
            return null;
        }

        private static void Render(Graphics graphics, XmlNode xmlNode)
        {
            if (xmlNode == null)
            {
                return;
            }

            int x = 0;
            {
                XmlAttribute xmlAttribute = xmlNode.Attributes["x"];
                if (xmlAttribute != null)
                {
                    Int32.TryParse(xmlAttribute.Value, out x);
                }
            }
            
            int y = 0;
            {
                XmlAttribute xmlAttribute = xmlNode.Attributes["y"];
                if (xmlAttribute != null)
                {
                    Int32.TryParse(xmlAttribute.Value, out y);
                }
            }

            switch (xmlNode.Name.ToUpper())
            {
                case "IMAGE":
                    var xmlAttribute = xmlNode.Attributes["file"];
                    if (xmlAttribute == null)
                    {
                        return;
                    }
                    string file = xmlAttribute.Value;
                    if (String.IsNullOrWhiteSpace(file))
                    {
                        return;
                    }
                    if (File.Exists(file))
                    {
                        Image newImage = Image.FromFile(file);
                        graphics.DrawImage(newImage, x, y);
                    }
                    break;
                case "TEXT":
                    var innerText = xmlNode.InnerText;
                    if (String.IsNullOrWhiteSpace(innerText))
                    {
                        return;
                    }
                    break;
            }

        }

        public static String[] Keys
        {
            get;
            set;
        }

        public static XmlNode XML
        {
            get;
            set;
        }

    }
}
