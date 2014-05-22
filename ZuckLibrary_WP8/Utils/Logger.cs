using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ZuckLibrary.Utils
{
    public class Logger
    {
        private static Object SyncRoot = new Object();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="info"></param>
        public static void Error(string title, string info)
        {
            Add("Error", title, info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="info"></param>
        public static void Alert(string title, string info)
        {
            Add("Alert", title, info);
        }

        private static void Add(string type, string title, string info)
        {
            lock (SyncRoot)
            {
                try
                {
                    //if (null != LogWrite && LogWrite.BaseStream.CanWrite)
                    {

                        Debug.WriteLine("[{0} {1}] {2}: {3} - {4}", DateTime.Now.ToLongDateString(),
                            DateTime.Now.ToLongTimeString() + ":" + DateTime.Now.Millisecond, type, title, info);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }
    }
}
