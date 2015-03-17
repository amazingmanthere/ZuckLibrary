using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ZuckLibrary.Utils.FileUtils
{
    public class XmlUtil
    {
        private string _path;
        private XmlDocument _doc;
        public XmlUtil(string path)
        {
            _path = path;
        }

        public void Load()
        {
            _doc = new XmlDocument();
            _doc.Load(_path);
        }

        public XmlNodeList ReadNodeList(string nodeListName)
        {
            XmlNodeList nodeList = _doc.SelectNodes(nodeListName);
            return nodeList;
        }
    }
}
