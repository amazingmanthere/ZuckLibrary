using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Text.RegularExpressions;

namespace ZuckLibrary.Utils
{
    public class FileUtil
    {
        /// <summary>
        /// create file by specified file path
        /// </summary>
        /// <param name="path"></param>
        public static void CreateFile(string path)
        {
            FileStream stream = File.Create(path);
            stream.Close();
        }

        /// <summary>
        /// Create a empty file with specify path and size
        /// </summary>
        /// <param name="path"></param>
        /// <param name="size"></param>
        public static void CreateFile(string path, long size)
        {
            if (string.IsNullOrEmpty(path))
                return;

            FileStream stream = File.Create(path);
            stream.SetLength(size);
            stream.Close();
        }

        public static string ReadText(string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;

            return File.ReadAllText(path);
        }

        public static void WriteText(string path, string text)
        {
            if (string.IsNullOrEmpty(path))
                return;

            File.WriteAllText(path, text);
        }

        public static bool FileExist(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;

            return File.Exists(path);
        }

        public static bool DirExist(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;

            return Directory.Exists(path);
        }

        public static string[] GetFileNames(string dir)
        {
            if (!DirExist(dir))
                return null;

            return Directory.GetFiles(dir);
        }

        public static string getNameWithFilePath(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return null;

            char separator = '/';
            if (filePath.IndexOf(separator) < 0)
            {
                separator = '\\';
                if (filePath.IndexOf(separator) < 0)
                    return filePath;
            }

            string[] strs = filePath.Split(separator);
            if (strs == null || strs.Length == 0)
                return null;
            
            return strs[strs.Length - 1];
        }

        /// <summary>
        /// genearate a unique file name in current directory
        /// </summary>
        /// <param name="fileName">file name with full path</param>
        /// <returns></returns>
        public static string generateUniqueFileName(string fileName)
        {
            if (!FileExist(fileName))
            {   // 该文件不存在，证明文件名是当前目录的唯一文件名，直接返回
                return fileName;
            }

            // 获取名称和后缀(如.log)
            string name = fileName;
            string extStr = "";
            int index = fileName.LastIndexOf('.');
            if (index > 0)
            {
                name = fileName.Substring(0, index);
                extStr = fileName.Substring(index);
            }

            // 产生的文件是不存在时才出循环
            do
            {
                // 获取"(数字)"部分，如(2)，找不到时，直接在末尾加(1)，找得到时获取括号内的数字，并增加1
                string pattern = @"\((\d+)\)$";
                Match m = RegexUtil.SafeMatch(name, pattern, RegexOptions.IgnoreCase);
                if (!m.Success)
                {
                    name = name + "(1)";
                }
                else
                {
                    Group g = m.Groups[1];
                    string value = g.Value;
                    name = name.Substring(0, g.Index) + (int.Parse(value) + 1) + ")";
                }

                fileName = name + extStr;
            } while (FileExist(fileName));

            return fileName;
        }

        /// <summary>
        /// Read csv file by a specified file path
        /// </summary>
        /// <param name="filePath">file path</param>
        /// <param name="tableName">table name</param>
        /// <returns></returns>
        public static DataTable ReadCsvTxt(string filePath, string tableName = "MyDataTable")
        {
            try
            {
                int intColCount = 0;
                bool blnFlag = true;
                DataTable mydt = new DataTable(tableName);

                DataColumn mydc;
                DataRow mydr;

                string strline;
                string[] aryline;

                System.IO.StreamReader mysr = new System.IO.StreamReader(filePath, System.Text.Encoding.Default);

                while ((strline = mysr.ReadLine()) != null)
                {
                    aryline = strline.Split(',');

                    if (blnFlag)
                    {
                        blnFlag = false;
                        intColCount = aryline.Length;
                        for (int i = 0; i < aryline.Length; i++)
                        {
                            mydc = new DataColumn(aryline[i]);
                            mydt.Columns.Add(mydc);
                        }
                    }

                    mydr = mydt.NewRow();
                    for (int i = 0; i < intColCount; i++)
                    {
                        mydr[i] = aryline[i];
                    }
                    mydt.Rows.Add(mydr);
                }

                return mydt;
            }
            catch (System.Exception ex)
            {
                Logger.Error(ex.Message, ex.StackTrace);
            }
            return null;
        }

        /// <summary>
        /// Read excel data by a specified file path and query name
        /// </summary>
        /// <param name="filePath">file path</param>
        /// <param name="queryName">query name</param>
        /// <returns></returns>
        public static DataSet ReadExcelData(string filePath, string queryName = "[Sheet1$]")
        {
            if (true == string.IsNullOrEmpty(filePath))
                return null;

            if (false == File.Exists(filePath))
                return null;
            try
            {
                string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=No;IMEX=1'";
                DataSet ds = new DataSet();
                OleDbDataAdapter oada = new OleDbDataAdapter("select * from " + queryName, strConn);
                oada.Fill(ds);
                return ds;
            }
            catch
            {
            }
            return null;
        }

        /// <summary>
        /// Read excel sheet name list by a specified file path
        /// </summary>
        /// <param name="filePath">file path</param>
        /// <returns></returns>
        public static IList GetExcelSheetNames(string filePath)
        {
            try
            {
                IList al = new ArrayList();
                string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=Excel 8.0;";
                OleDbConnection conn = new OleDbConnection(strConn);
                conn.Open();
                DataTable sheetNames = conn.GetOleDbSchemaTable
                    (OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                conn.Close();
                foreach (DataRow dr in sheetNames.Rows)
                {
                    al.Add(dr[2]);
                }
                return al;
            }
            catch
            {
                
            }
            return null;
        } 
    }
}
