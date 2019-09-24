using System;
using System.Collections.Generic;
using System.Text;

namespace http.model
{
    public class HttpRequest
    {

        public HttpRequest(String url)
        {

            try
            {
                Uri uri = new Uri(url);
                this.Host = uri.Host;
                this.Port = uri.Port;

                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    this.ISSSL = true;
                }
                this.PathAndQuery = uri.PathAndQuery;
            }
            catch (Exception e)
            {

                throw e;
            }


        }
        private Dictionary<String, String> Headers = new Dictionary<String, String>();
        private Dictionary<String, String> MUData = new Dictionary<String, String>();

        public void AddHeader(String name, String val) {
            if (!this.Headers.ContainsKey(name))
            {
                this.Headers.Add(name, val);
            }
           
        }
        public void AddMuHeader(String name, String val)
        {
            if (!this.MUData.ContainsKey(name))
            {
                this.MUData.Add(name, val);
            }

        }

        public static String murTemplate = "POST {0} HTTP/1.1\r\nAccept-Encoding: gzip, deflate\r\nConnection: Keep-Alive\r\nContent-Length: 51\r\nUser-Agent: Mozilla/5.0 (baidu spider)\r\nHost: {1}\r\nCookie: {2}\r\nContent-Type: multipart/form-data; boundary=------------------------4a606c052a893987\r\n\r\n--------------------------4a606c052a893987\r\nContent-Disposition: form-data; name=\"{3}\"\r\n\r\n-1\r\n--------------------------4a606c052a893987--";

        public String GetBody(String data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Method + " " + PathAndQuery + " HTTP/1.1\r\n");
            sb.Append("Host:"+Host +":"+Port+"\r\n");
            if (!"".Equals(Accept)) {
                sb.Append("Accept: "+Accept + "\r\n");
            }
            if (!"".Equals(AcceptLanguage))
            {
                sb.Append("Accept-Language: " + AcceptLanguage + "\r\n");
            }
            if (!"".Equals(Referer))
            {
                sb.Append("Referer: " + Referer + "\r\n");
            }
            if (!"".Equals(UserAgent))
            {
                sb.Append("User-Agent: " + UserAgent + "\r\n");
            }
            if (!"".Equals(AcceptEncoding))
            {
                sb.Append("Accept-Encoding: " + AcceptEncoding + "\r\n");
            }
            if (!"".Equals(Connection))
            {
                sb.Append("Connection: " + Connection + "\r\n");
            }
            foreach (var c in this.Headers)
            {
                sb.Append(c.Key + ": " + c.Value + "\r\n");
            }
            if ("POST".Equals(Method))
            {
                sb.Append("Content-Length: 0\r\n");
            }
            if (!"".Equals(ContentType))
            {
                
                if ("multipart/form-data".Equals(ContentType))
                {
                    sb.Append("Content-Type: " + ContentType + "; boundary=---------------------------7e116d19044c\r\n");
                }
                else {
                    sb.Append("Content-Type: " + ContentType + "\r\n");
                }

            }
            
            if (!"".Equals(Cookie))
            {
                sb.Append("Cookie: " + Cookie + "\r\n");
            }
            sb.Append("\r\n");
            if (MUData.Count > 0)
            {
                foreach (var c in this.MUData)
                {
                    sb.Append("-----------------------------7e116d19044c\r\nContent-Disposition: form-data; name=" + c.Key + "\r\n\r\n" + c.Value + "\r\n");
                }
                sb.Append("-----------------------------7e116d19044c--");
            }
            else if (!"".Equals(data))
            {
                sb.Append(data);
            }                  
            return sb.ToString();

        }
        /// <summary>
        /// 默认GET
        /// </summary>
        public String Referer = "";
        
        /// <summary>
        /// 默认GET
        /// </summary>
        public String PathAndQuery = "/";

        /// <summary>
        /// 默认空
        /// </summary>
        public String Accept = "";

        /// <summary>
        /// 默认zh_CN
        /// </summary>
        public String AcceptLanguage = "zh_CN";

        /// <summary>
        /// 默认Auto Spider 1.0
        /// </summary>
        public String UserAgent = "Auto Spider 1.0";

        /// <summary>
        /// 默认空
        /// </summary>
        public String AcceptEncoding = "gzip, deflate";

        /// <summary>
        /// 默认空
        /// </summary>
        public String Connection = "close";
        /// <summary>
        /// 默认空
        /// </summary>
        public String ContentType = "";

        /// <summary>
        /// 默认空
        /// </summary>
        public String Cookie = "";

        /// <summary>
        /// 默认GET
        /// </summary>
        public String Method = "GET";

        /// <summary>
        /// 默认GET
        /// </summary>
        public Boolean UpdateContentLength = true;

        /// <summary>
        /// 请求包
        /// </summary>
        public String Body = "";

        /// <summary>
        /// 默认不是HTTPS
        /// </summary>
        public Boolean ISSSL = false;


        /// <summary>
        ///发包失败重发次数，默认0次
        /// </summary>
        /// 
        public int TryCount = 0;

        /// <summary>
        ///域名、IP、主机头
        /// </summary>
        /// 
        public String Host = "";

        /// <summary>
        ///默认80
        /// </summary>
        /// 
        public int Port = 80;


        /// <summary>
        ///默认null,自动识别编码
        /// </summary>
        /// 
        public String Encode = "";


        /// <summary>
        ///默认重定向
        /// </summary>
        /// 
        public Boolean Redirect = false;

        /// <summary>
        ///重定向使用GET方法默认,否则使用POST
        /// </summary>
        /// 
        public Boolean RedirectByGet = true;

        /// <summary>
        ///超时时间毫秒，默认15秒
        /// </summary>
        /// 
        public int Timeout = 15000;
    }
}
