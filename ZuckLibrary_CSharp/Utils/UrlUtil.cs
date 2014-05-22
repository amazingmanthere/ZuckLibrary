using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuckLibrary.Utils
{
    public class UrlUtil
    {
        //判断网页是否存在
        public static bool JudgeWebURL(string URL)
        {
            bool link = true;
            try
            {
                System.Net.WebResponse myRepTest;
                System.Net.WebRequest myTest = System.Net.WebRequest.Create(URL);
                myTest.Timeout = 5000;
                myRepTest = myTest.GetResponse();
            }
            catch (Exception)
            {
                link = false;
            }
            return link;
        }
    }
}
