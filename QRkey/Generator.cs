using System;
using System.Collections.Generic;
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
            switch (xmlNode.Name.ToUpper())
            {
                case "IMAGE":
                    break;
                case "TEXT":
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
