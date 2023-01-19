using System;

namespace IridiUploadConsole.Utility
{
    class Logging
    {
        public bool ShowEmergency = true;
        public bool ShowCritical = true;
        public bool ShowAlert = true;
        public bool ShowError = true;
        public bool ShowNotice = true;
        public bool ShowWarning = true;
        public bool ShowInformational = true;
        public bool ShowDebug = false;

        public Logging()
        {

        }

        void AppendWriteLine(object message)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        void AppendWrite(object message)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Emergency(object message)
        {
            if (ShowEmergency)
            {
                Console.ForegroundColor = ConsoleColor.White;
                AppendWriteLine(message);
            }
        }
        public void Emergency(object name, object message)
        {
            if (ShowEmergency)
            {
                Console.ForegroundColor = ConsoleColor.White;
                AppendWrite(name);

                Console.ForegroundColor = ConsoleColor.Red;
                AppendWriteLine(message);
            }
        }
        public void Emergency(object message, ConsoleColor Color)
        {
            if (ShowEmergency)
            {
                Console.ForegroundColor = Color;
                AppendWriteLine(message);
            }
        }

        public void Alert(object message)
        {
            if (ShowAlert)
            {
                Console.ForegroundColor = ConsoleColor.White;
                AppendWriteLine(message);
            }
        }
        public void Alert(object name, object message)
        {
            if (ShowAlert)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                AppendWrite(name);

                Console.ForegroundColor = ConsoleColor.White;
                AppendWriteLine(message);
            }
        }
        public void Alert(object message, ConsoleColor Color)
        {
            if (ShowAlert)
            {
                Console.ForegroundColor = Color;
                AppendWriteLine(message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void Critical(object message)
        {
            if (ShowCritical)
            {
                Console.ForegroundColor = ConsoleColor.White;
                AppendWriteLine(message);
            }
        }
        public void Critical(object name, object message)
        {
            if (ShowCritical)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                AppendWrite(name);

                Console.ForegroundColor = ConsoleColor.White;
                AppendWriteLine(message);
            }
        }
        public void Critical(object message, ConsoleColor Color)
        {
            if (ShowCritical)
            {
                Console.ForegroundColor = Color;
                AppendWriteLine(message);
            }
        }

        public void Error(object message)
        {
            if (ShowError)
            {
                Console.ForegroundColor = ConsoleColor.White;
                AppendWriteLine(message);  
            }
        }
        public void Error(object name, object message)
        {
            if (ShowError)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                AppendWrite(name);

                Console.ForegroundColor = ConsoleColor.White;
                AppendWriteLine(message);
            }
        }
        public void Error(object message, ConsoleColor Color)
        {
            if (ShowError)
            {
                Console.ForegroundColor = Color;
                AppendWriteLine(message);
            }
        }

        public void Notice(object message)
        {
            if (ShowNotice)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                AppendWriteLine(message);
            }
        }
        public void Notice(object name, object message)
        {
            if (ShowNotice)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                AppendWrite(name);

                Console.ForegroundColor = ConsoleColor.Cyan;
                AppendWriteLine(message);
            }
        }
        public void Notice(object message, ConsoleColor Color)
        {
            if (ShowNotice)
            {
                Console.ForegroundColor = Color;
                AppendWriteLine(message);
            }
        }
        public void Warning(object message)
        {
            if (ShowWarning)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                AppendWriteLine(message);
            }
        }
        public void Warning(object name, object message)
        {
            if (ShowWarning)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                AppendWrite(name);

                Console.ForegroundColor = ConsoleColor.Cyan;
                AppendWriteLine(message + "");
            }
        }
        public void Warning(object message, ConsoleColor Color)
        {
            if (ShowWarning)
            {
                Console.ForegroundColor = Color;
                AppendWriteLine(message);
            }
        }

        public void Informational(object message)
        {
            if (ShowInformational)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                AppendWriteLine(message);
            }
        }
        public void Informational(object name, object message)
        {
            if (ShowInformational)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                AppendWrite(name);

                Console.ForegroundColor = ConsoleColor.Cyan;
                AppendWriteLine(message);
            }
        }
        public void Informational(object message, ConsoleColor Color)
        {
            if (ShowInformational)
            {
                Console.ForegroundColor = Color;
                AppendWriteLine(message);
            }
        }

        public void Debug(object message)
        {
            if (ShowDebug)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                AppendWriteLine(message);
            }
        }
        public void Debug(object name, object message)
        {
            if (ShowDebug)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                AppendWrite(name);

                Console.ForegroundColor = ConsoleColor.Cyan;
                AppendWriteLine(message);
            }
        }
        public void Debug(object message, ConsoleColor Color)
        {
            if (ShowDebug)
            {
                Console.ForegroundColor = Color;
                AppendWriteLine(message);
            }
        }
    }
}
