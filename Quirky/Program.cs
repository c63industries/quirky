﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace C63.Quirky
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] arguments)
        {
            var form = new Form();
            Application.Run(form);
        }
    }
}
