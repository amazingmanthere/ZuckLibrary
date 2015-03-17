using CE.iPhone.PList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuckLibrary.Utils.FileUtils
{
    public class PlistUtil
    {
        private string _path;
        private PListRoot root;
        private PListDict dic;

        public PlistUtil(string path)
        {
            _path = path;
        }
        public void Load()
        {
            root = PListRoot.Load(_path);
            dic = (PListDict)root.Root;
        }

        public bool ContainsKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                return false;

            return dic.ContainsKey(key);
        }

        public Object ReadNode(string nodeName)
        {
            if (string.IsNullOrEmpty(nodeName))
                return dic;

            if (ContainsKey(nodeName))
                return dic[nodeName];
            
            return null;
        }

        public void Save(string path)
        {
            root.Save(path, PListFormat.Xml);

            var utf8WithoutBom = new UTF8Encoding(false);
            StreamReader reader = new StreamReader(path);
            string str = reader.ReadToEnd();
            str = str.Replace("ustring", "string");
            reader.Close();
            File.Delete(path);

            StreamWriter writer = new StreamWriter(path, true, utf8WithoutBom);
            writer.Write(str);
            writer.Close();

            // write data here
//            file.Close(); // save and close it
        }
    }
}
