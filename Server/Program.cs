﻿using System;
using System.Windows.Forms;

namespace Server
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            
            var mainForm = new MainForm();
            Application.Run(mainForm);
        }
    }
}
