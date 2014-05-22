using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuckLibrary.Utils
{
    public class Logger
    {

        private static object SyncRoot = new Object();
        private static Dictionary<Guid, string> userFileNames = new Dictionary<Guid, string>();

        /// <summary>
        /// log name
        /// </summary>
        private const string BaseFilename = "logger.txt";
        private const int MaxLogSizeMb = 25;

        /// <summary>
        /// whether log with file
        /// </summary>
        private const bool useFileLog = false;

        /// <summary>
        /// whether log with console
        /// </summary>
        private const bool useConsoleLog = true;

        private static string Filename
        {
            get
            {
                return Directory.GetCurrentDirectory() + "\\log\\" + BaseFilename;
            }
        }

        public static void Alert(string title, string info)
        {
            //Console.ForegroundColor = ConsoleColor.Yellow;
            Add("Debug", title, info);
        }

        public static void Error(string title, string info)
        {
            //Console.ForegroundColor = ConsoleColor.Red;
            Add("Error", title, info);
        }

        public static void Info(string title, string info)
        {
            //Console.ForegroundColor = ConsoleColor.White;
            Add("Info", title, info);
        }

        private static void Add(string type, string title, string info)
        {
            lock (SyncRoot)
            {
                try
                {
                    string logStr = string.Format("[{0} {1}] {2}: {3} - {4}", DateTime.Now.ToLongDateString(),
DateTime.Now.ToLongTimeString() + ":" + DateTime.Now.Millisecond, type, title, info);

                    if (useConsoleLog)
                    {
                        System.Diagnostics.Debug.WriteLine(logStr);
                    }

                    if (useFileLog)
                    {
                        FileInfo fi = new FileInfo(Filename);
                        if (fi.Directory.Exists == false)
                        {
                            fi.Directory.Create();
                        }
                        // 1048576 is the number of bytes in a megabyte.
                        if (fi != null && fi.Exists && fi.Length > (1048576 * MaxLogSizeMb))
                        {
                            fi.Delete();
                        }

                        using (var fs = new FileStream(Filename, FileMode.Append))
                        {
                            using (var sw = new StreamWriter(fs))
                            {
                                sw.WriteLine(logStr);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message + " : " + ex.StackTrace);
                }
            }
        }
    }
}
