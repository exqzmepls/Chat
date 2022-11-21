using System;

namespace Server.Core
{
    internal class Logger
    {
        private Action<string> _logAction;

        public void SetLogAction(Action<string> logAction)
        {
            _logAction = logAction;
        }

        public void Log(string message)
        {
            _logAction(message);
        }
    }
}
