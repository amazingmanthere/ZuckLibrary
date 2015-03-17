using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ZuckLibrary.Utils.FileUtils
{
    public class IniUtil
    {
        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(string Section, string Key,
               string Value, StringBuilder Result, int Size, string FileName);

        // Second Method
        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(string Section, int Key,
               string Value, [MarshalAs(UnmanagedType.LPArray)] byte[] Result,
               int Size, string FileName);

        // Third Method
        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(int Section, string Key,
               string Value, [MarshalAs(UnmanagedType.LPArray)] byte[] Result,
               int Size, string FileName);

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, 
            string val, string filePath);

        // Set the IniFileName passed from the Main Application.
        private string _path;
        public IniUtil(string path)
        {
            _path = path;
        }

        // The Function called to obtain the SectionHeaders,
        // and returns them in an Dynamic Array.
        public string[] GetSectionNames()
        {
            //    Sets the maxsize buffer to 500, if the more
            //    is required then doubles the size each time.
            for (int maxsize = 500; true; maxsize *= 2)
            {
                //    Obtains the information in bytes and stores
                //    them in the maxsize buffer (Bytes array)
                byte[] bytes = new byte[maxsize];
                int size = GetPrivateProfileString(0, "", "", bytes, maxsize, _path);

                // Check the information obtained is not bigger
                // than the allocated maxsize buffer - 2 bytes.
                // if it is, then skip over the next section
                // so that the maxsize buffer can be doubled.
                if (size < maxsize - 2)
                {
                    // Converts the bytes value into an ASCII char. This is one long string.
                    string Selected = Encoding.ASCII.GetString(bytes, 0,
                                               size - (size > 0 ? 1 : 0));
                    // Splits the Long string into an array based on the "\0"
                    // or null (Newline) value and returns the value(s) in an array
                    return Selected.Split(new char[] { '\0' });
                }
            }
        }
        // The Function called to obtain the EntryKey's from the given
        // SectionHeader string passed and returns them in an Dynamic Array
        public string[] GetEntryNames(string section)
        {
            //    Sets the maxsize buffer to 500, if the more
            //    is required then doubles the size each time. 
            for (int maxsize = 500; true; maxsize *= 2)
            {
                //    Obtains the EntryKey information in bytes
                //    and stores them in the maxsize buffer (Bytes array).
                //    Note that the SectionHeader value has been passed.
                byte[] bytes = new byte[maxsize];
                int size = GetPrivateProfileString(section, 0, "", bytes, maxsize, _path);

                // Check the information obtained is not bigger
                // than the allocated maxsize buffer - 2 bytes.
                // if it is, then skip over the next section
                // so that the maxsize buffer can be doubled.
                if (size < maxsize - 2)
                {
                    // Converts the bytes value into an ASCII char.
                    // This is one long string.
                    string entries = Encoding.ASCII.GetString(bytes, 0,
                                              size - (size > 0 ? 1 : 0));
                    // Splits the Long string into an array based on the "\0"
                    // or null (Newline) value and returns the value(s) in an array
                    return entries.Split(new char[] { '\0' });
                }
            }
        }

        // The Function called to obtain the EntryKey Value from
        // the given SectionHeader and EntryKey string passed, then returned
        public object GetEntryValue(string section, string entry)
        {
            //    Sets the maxsize buffer to 250, if the more
            //    is required then doubles the size each time. 
            for (int maxsize = 250; true; maxsize *= 2)
            {
                //    Obtains the EntryValue information and uses the StringBuilder
                //    Function to and stores them in the maxsize buffers (result).
                //    Note that the SectionHeader and EntryKey values has been passed.
                StringBuilder result = new StringBuilder(maxsize);
                int size = GetPrivateProfileString(section, entry, "",
                                                   result, maxsize, _path);
                if (size < maxsize - 1)
                {
                    // Returns the value gathered from the EntryKey
                    return result.ToString();
                }
            }
        }

        /// <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="Section">项目名称(如 [TypeName] )</param>
        /// <param name="Key">键</param>
        /// <param name="Value">值</param>
        public void IniWriteValue(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, _path);
        }
        /// <summary>
        /// 读出INI文件
        /// </summary>
        /// <param name="Section">项目名称(如 [TypeName] )</param>
        /// <param name="Key">键</param>
        public string IniReadValue(string section, string key)
        {
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(section, key, "", temp, 500, _path);
            return temp.ToString();
        }
    }
}
