using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Resources;

namespace ZuckLibrary.Utils
{
    public class FileUtil
    {
        /// <summary>
        /// Create a empty file with specify path and size
        /// </summary>
        /// <param name="path"></param>
        /// <param name="size"></param>
        public static void CreateFile(string path, long size)
        {
            FileStream stream = File.Create(path);
            stream.SetLength(size);
            stream.Close();
        }

        /// <summary>
        /// Read the string from resource file
        /// </summary>
        public static void ReadResourceFile(string resourceFile, ref string outBuffer)
        {
            try
            {
                if (string.IsNullOrEmpty(resourceFile))
                {
                    outBuffer = "";
                    return;
                }

                Uri uri = new Uri(resourceFile, UriKind.Relative);
                StreamResourceInfo streamResourceInfo = Application.GetResourceStream(uri);
                if (streamResourceInfo == null)
                {
                    outBuffer = "";
                    return;
                }

                using (StreamReader streamReader = new StreamReader(streamResourceInfo.Stream))
                {
                    if (streamReader == null)
                    {
                        outBuffer = "";
                        return;
                    }

                    outBuffer = streamReader.ReadToEnd();
                    streamReader.Close();
                }
                return;
            }
            catch (System.Exception ex)
            {
                outBuffer = "";
                Logger.Error(ex.Message, ex.StackTrace);
                return;
            }
        }
    }
}
