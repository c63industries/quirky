using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace QRkey
{
    class Generator
    {
        public static void Generate()
        {
            if (XML == null)
            {
                return;
            }

            if (!XML.HasChildNodes)
            {
                return;
            }

            foreach (XmlNode xmlNode in XML.ChildNodes)
            {
                Render(xmlNode);
            }
        }

        private static void Render(XmlNode xmlNode)
        {
            if (xmlNode == null)
            {
                return;
            }

            var x = xmlNode.Attributes["x"];
            var y = xmlNode.Attributes["y"];

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
                        return;
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
