using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ZuckLibrary.Net
{
    public class HttpInfoArgs : EventArgs
    {
        public HttpWebResponse Response { get; set; }
        public Byte[] Data { get; set; }
    }

    public class HttpEngine
    {
        public readonly int READ_BUFFER_SIZE = 10 * 1024;
        public readonly int WRITE_BUFFER_SIZE = 12 * 1024;
        private HttpWebRequest _webRequest = null;
        private HttpWebResponse _response = null;
        private Stream _requestStream = null;
        private Stream _responseStream = null;
        private MemoryStream _receivedStream = null;
        private byte[] _sentData = null;
        private byte[] _receivedData = null;

        private CookieContainer _cookieContainer = null;

        public CookieContainer CookieContainer
        {
            get
            {
                return _cookieContainer;
            }
            set
            {
                _cookieContainer = value;
            }
        }

        public event EventHandler<HttpInfoArgs> ResponseNotify;
        public event EventHandler<HttpInfoArgs> ReadProcessNotify;
        public event EventHandler<HttpInfoArgs> WriteProcessNotify;

        public HttpEngine()
        {
            _cookieContainer = new CookieContainer();
        }

        public HttpWebRequest CreateRequest(string url)
        {
            _webRequest = null;
            _webRequest = (HttpWebRequest)WebRequest.Create(url);
            _webRequest.AllowAutoRedirect = false;
            _webRequest.UseDefaultCredentials = true;
            return _webRequest;
        }

        //public HttpWebRequest Request
        //{
        //    get { return _webRequest; }
        //}

        public byte[] SentData
        {
            get { return _sentData; }
            set { _sentData = value; }
        }

        public void SendRequest(bool bWrite = false)
        {
            if (bWrite == false)
            {
                _webRequest.BeginGetResponse(new AsyncCallback(iWebRewuest_BeginGetResponse), this);
            }
            else
            {
                _webRequest.BeginGetRequestStream(new AsyncCallback(iWebRewuest_BeginGetRequestStream), this);
            }
        }

        private void iWebRewuest_BeginGetResponse(IAsyncResult rs)
        {
            _response = (HttpWebResponse)_webRequest.EndGetResponse(rs);
            _responseStream = _response.GetResponseStream();
            _receivedData = new byte[READ_BUFFER_SIZE];
            _responseStream.BeginRead(_receivedData, 0, _receivedData.Length, new AsyncCallback(ReadCallBack), this);
            if (ResponseNotify != null)
            {
                HttpInfoArgs args = new HttpInfoArgs();
                args.Response = _response;
                ResponseNotify(null, args);
            }
        }

        private void iWebRewuest_BeginGetRequestStream(IAsyncResult rs)
        {
            _requestStream = _webRequest.EndGetRequestStream(rs);
            if (_sentData != null)
            {
                _requestStream.BeginWrite(_sentData, 0, _sentData.Length, new AsyncCallback(WriteCallBack), this);
                if (ResponseNotify != null)
                {
                    HttpInfoArgs args = new HttpInfoArgs();
                    args.Response = _response;
                    ResponseNotify(null, args);
                }
            }
        }

        private void ReadCallBack(IAsyncResult rs)
        {
            int readIndex = _responseStream.EndRead(rs);
            if (readIndex > 0)
            {
                //_receivedStream.Write(_receivedData, 0, readIndex);
                _responseStream.BeginRead(_receivedData, 0, _receivedData.Length, new AsyncCallback(ReadCallBack), this);
            }
            else
            {
                _responseStream.Close();
            }
        }

        private void WriteCallBack(IAsyncResult rs)
        {
            if (_requestStream != null)
            {
                _requestStream.EndWrite(rs);
                _requestStream.Close();
                SendRequest();
            }
        }
    }
}
