using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ZuckLibrary.Utils
{
    public class UrlUtil
    {
        //判断网页是否存在
        public static bool JudgeWebURL(string url)
        {
            bool link = true;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                IAsyncResult result1 = request.BeginGetResponse(new AsyncCallback((ar) => { }), request);
                while (!result1.IsCompleted)
                {
                    System.Threading.Thread.Sleep(50);
                }
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result1);
                Logger.Alert("response.StatusCode", "" + response.StatusCode);
            }
            catch (Exception e)
            {
                link = false;
            }
            return link;
        }
    }
}
