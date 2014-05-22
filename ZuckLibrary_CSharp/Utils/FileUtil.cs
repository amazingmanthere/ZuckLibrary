using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Collections;

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
