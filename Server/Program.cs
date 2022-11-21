using Server.Core;
using System;
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

            var logger = new Logger();
            var chatService = new ChatService(logger, "first", "second");
            var mainForm = new MainForm(chatService);
            logger.SetLogAction((m) =>
            {
                mainForm.Invoke((MethodInvoker)delegate
                {
                    mainForm.LogMessage(m);
                });
            });
            Application.Run(mainForm);
        }
    }
}
