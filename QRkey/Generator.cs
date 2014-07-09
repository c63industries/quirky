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

        public static string BasePath
        {
            get;
            set;
        }

        public static Bitmap Generate()
        {
            if (XML == null)
            {
                return null;
            }

            if (!XML.HasChildNodes)
            {
                return null;
            }

            int height = 1080;
            {
                XmlAttribute xmlAttribute = XML.Attributes["HEIGHT"];
                if (xmlAttribute != null)
                {
                    Int32.TryParse(xmlAttribute.Value, out height);
                }
            }

            int width = 1920;
            {
                XmlAttribute xmlAttribute = XML.Attributes["WIDTH"];
                if (xmlAttribute != null)
                {
                    Int32.TryParse(xmlAttribute.Value, out width);
                }
            }

            Bitmap bmp = new Bitmap(height, width);
            
            using (Graphics graphics = Graphics.FromImage(bmp))
            {
                foreach (XmlNode xmlNode in XML.ChildNodes)
                {
                    Render(graphics, xmlNode);
                }
            }

            return bmp;
        }

        private static void Render(Graphics graphics, XmlNode xmlNode)
        {
            if (xmlNode == null)
            {
                return;
            }

            int x = 0;
            {
                XmlAttribute xmlAttribute = xmlNode.Attributes["X"];
                if (xmlAttribute != null)
                {
                    Int32.TryParse(xmlAttribute.Value, out x);
                }
            }
            
            int y = 0;
            {
                XmlAttribute xmlAttribute = xmlNode.Attributes["Y"];
                if (xmlAttribute != null)
                {
                    Int32.TryParse(xmlAttribute.Value, out y);
                }
            }

            switch (xmlNode.Name.ToUpper())
            {
                case "IMAGE":
                    {
                        XmlAttribute xmlAttribute = xmlNode.Attributes["FILE"];
                        if (xmlAttribute == null)
                        {
                            return;
                        }
                        string file = xmlAttribute.Value;
                        if (String.IsNullOrWhiteSpace(file))
                        {
                            return;
                        }

                        file = Path.Combine(BasePath, file);

                        if (File.Exists(file))
                        {
                            Image newImage = Image.FromFile(file);
                            graphics.DrawImage(newImage, x, y);
                        }
                    }
                    break;
                case "TEXT":
                    var innerText = xmlNode.InnerText;
                    if (String.IsNullOrWhiteSpace(innerText))
                    {
                        return;
                    }

                    string fontName = "Arial";
                    {
                        XmlAttribute xmlAttribute = xmlNode.Attributes["FONT"];

                        if (xmlAttribute != null)
                        {
                            fontName = xmlAttribute.Value;
                        }
                    }

                    float fontSize = 12;
                    {
                        XmlAttribute xmlAttribute = xmlNode.Attributes["SIZE"];

                        if (xmlAttribute != null)
                        {
                            float.TryParse(xmlAttribute.Value, out fontSize);
                        }
                    }

                    System.Drawing.Color brushColor = Color.Black;
                    {
                        XmlAttribute xmlAttribute = xmlNode.Attributes["COLOR"];

                        if (xmlAttribute != null)
                        {
                            brushColor = Color.FromName(xmlAttribute.Value);
                        }
                    }
                    Font font = new Font(fontName, fontSize);
                    SolidBrush brush = new SolidBrush(brushColor);
                    PointF point = new PointF(x, y);
                    graphics.DrawString(innerText, font, brush, point);
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
