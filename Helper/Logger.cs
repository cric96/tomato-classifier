using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Helper
{
    public interface ILogger
    {
        void Log(String g);

        void LogNewLine(String g);
    }
    public static class Logger
    { 
        public static bool Enabled { get; set; }

        private class ConsoleLogger : ILogger
        {
            public void Log(string g)
            {
                if(Logger.Enabled)
                {
                    Console.Write(g);
                }
            }

            public void LogNewLine(string g)
            {
                if (Logger.Enabled)
                {
                    Console.WriteLine(g);
                }
            }
        }
        public static ILogger OnConsole()
        {
            return new ConsoleLogger();
        }
    }  
}
