using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Net.Sockets;
using System.Threading;
using System.Globalization;
using System.Security.Cryptography;
using System.Windows.Forms;
using file;
using Microsoft.Win32;
using System.Net.NetworkInformation;
using System.Management;

namespace http
{
    class Tools
    {
        public const String httpLogPath = "logs/";


        public static String GetRootPath(String url) {
           
            Uri uri = new Uri(url.ToLower());
            String rootPath = "";
            if (("http".Equals(uri.Scheme) && uri.Port == 80) || ("https".Equals(uri.Scheme) && uri.Port == 443))
            {

                rootPath = uri.Scheme + "://" + uri.Host;
            }
            else
            {
                rootPath = uri.Scheme + "://" + uri.Host + ":" + uri.Port;
            }
            return rootPath;
        }

        public static String getContent(String body,String vulType)
        {

            if (!String.IsNullOrEmpty(body)) {
                if (vulType.Equals("S2-048")) { 
                    int s = body.IndexOf("start:");
                    int e = body.IndexOf(":end");
                    if (e > s)
                    {
                        return body.Substring(s + 6, e - s - 6);
                    }
                    
                }
               return body;
            }
            else{
            return "";
            }
            
        }
        public static long currentMillis()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }

        /// <summary>
        /// 将16进制转换成10进制
        /// </summary>
        /// <param name="str">16进制字符串</param>
        /// <returns></returns>
        public static int convertToIntBy16(String str)
        {
            try
            {
                return Convert.ToInt32(str, 16);
            }
            catch (Exception e)
            {

            }
            return 0;

        }

        public static void SysLog(String log)
        {
            FileTool.AppendLogToFile("logs/" + DateTime.Now.ToLongDateString() + ".log.txt", log + "----" + DateTime.Now);
        }

        public static String fomartTime(String time)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(time);
                String newtime = dt.ToLocalTime().ToString();
                return newtime;
            }
            catch (Exception e)
            {
                SysLog(e.Message);
            }
            return time;

        }
        

        
        /// <summary>
        /// 将数组转换成字符串
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public static String convertToString(String[] strs){

            StringBuilder sb = new StringBuilder();
            foreach(String s in strs){
                sb.Append(s);
            }
            return sb.ToString();
        
        }

        /// <summary>
        /// 将字符串转换成数字，错误返回0
        /// </summary>
        /// <param name="strs">字符串</param>
        /// <returns></returns>
        public static int convertToInt(String str)
        {

            try
            {
                return int.Parse(str);
            }
            catch (Exception e) {
                Tools.SysLog("waring:-"+e.Message);
            }
            return 0;

        }
 

        public static int findKeyByTime(int trueTime, int falseTime,int maxTime)
        {
            if (trueTime > maxTime&&falseTime<maxTime) {
                return maxTime;
            }
   
                return 0;
        }

        public static String clearURLParams(String url)
        {
                int index = url.IndexOf("?");
                if (index > 0)
                {
                    return url.Substring(0,index);

                }
                else {

                    return url;
                }
        }

        public static String getCurrentPath(String url)
        {
            int index =url.LastIndexOf("/");

            if (index != -1)
            {
                return url.Substring(0,index)+"/";
            }
            else {
                return "";
            }
        }

        public static String getRootDomain(String domain)
        {
            int index = domain.LastIndexOf(".");

            if (index>0)
            {
                int index2 = domain.LastIndexOf(".", index - 1);
                if (index2 != -1)
                {
                    return domain.Substring(index2+1);
                }
               
            }
            return domain;
        }

        public static String md5_16(String str){
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            String t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(str)), 4, 8);
            t2 = t2.Replace("-", "");
            t2 = t2.ToLower();
            return t2;
        }
        public static String md5_32(String str)
        {
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            String pwd = "";
            for (int i = 0; i < s.Length; i++)
            {
                //将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("X");
            }
            return pwd;

        }


        public static String changeRequestMethod(String datapack)
        {
            if (datapack.StartsWith("GET"))
            {
                int pl = datapack.IndexOf("?");
                if (pl != -1) {
                    int el = datapack.IndexOf(" ",pl);
                    if (el != -1) {

                       String cparams= datapack.Substring(pl+1,el-pl-1);
                       datapack = datapack.Replace("?"+ cparams,"");
                       int sl= datapack.IndexOf("\r\n");
                       datapack= datapack.Insert(sl, "\r\nContent-Type: application/x-www-form-urlencoded\r\nContent-Length: 0");
                       int ssl = datapack.IndexOf("\r\n\r\n");
                        if (!datapack.EndsWith("\r\n\r\n")) {

                            datapack += "\r\n\r\n";
                        }
                       datapack+=cparams;

                       int me = datapack.IndexOf(" ");
                        if (me != -1) {

                            datapack = "POST" + datapack.Substring(me, datapack.Length - me);
                        }

                       return datapack;
                    }
                }
            }

            else if (datapack.StartsWith("POST"))
            {
                int ssl = datapack.IndexOf("\r\n\r\n");

                if (ssl != -1) {

                  
                    String cparams = datapack.Substring(ssl+4,datapack.Length- ssl - 4);
                    datapack = datapack.Substring(0, ssl+1);
                    int cys = datapack.IndexOf("Content-Type");
                    int cye = datapack.IndexOf("\r\n",cys);

                    if (cye > cys) {
                        datapack=datapack.Remove(cys, cye - cys+2);
                    }
                    int cls = datapack.IndexOf("Content-Length");
                    int cle = datapack.IndexOf("\r\n", cls+1);
                    if (cle > cls)
                    {
                        datapack = datapack.Remove(cls, cle - cls+2);
                    }

                    int hl = datapack.IndexOf(" HTTP");
                    if (hl != -1) {

                        datapack = datapack.Insert(hl, "?"+cparams);
                    }
                   
                    int me = datapack.IndexOf(" ");

                    if (me != -1)
                    {

                        datapack = "GET" + datapack.Substring(me, datapack.Length - me);
                    }
                }
            }
            
            return datapack;

        }

        /**
       * 获取系统相关唯一ID,用于统计
       */
        public static String getSystemSid()
        {

            String sid = "";
            try
            {
                //获得系统名称
                RegistryKey rk = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion");
                sid = rk.GetValue("ProductName").ToString();
                rk.Close();
                //获得系统唯一号，系统安装id和mac组合
                sid += "_";

                var officeSoftware = new ManagementObjectSearcher("SELECT ID, ApplicationId, PartialProductKey, LicenseIsAddon, Description, Name, OfflineInstallationId FROM SoftwareLicensingProduct where PartialProductKey <> null");
                var result = officeSoftware.Get();
                foreach (var item in result)
                {
                    String c = item.GetPropertyValue("name").ToString();

                    if (item.GetPropertyValue("name").ToString().StartsWith("Windows"))
                    {

                        sid += item.GetPropertyValue("OfflineInstallationId").ToString() + "_";
                        break;
                    }
                }

            }
            catch (Exception e)
            {
                sid += "ex_";
            }
            String mac = "";
            try
            {
                NetworkInterface[] fNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface adapter in fNetworkInterfaces)
                {
                    String fCardType = "o";
                    String fRegistryKey = "SYSTEM\\CurrentControlSet\\Control\\Network\\{4D36E972-E325-11CE-BFC1-08002BE10318}\\" + adapter.Id + "\\Connection";
                    RegistryKey rk = Registry.LocalMachine.OpenSubKey(fRegistryKey, false);
                    if (rk != null)
                    {
                        String fPnpInstanceID = rk.GetValue("PnpInstanceID", "").ToString();
                        int fMediaSubType = Convert.ToInt32(rk.GetValue("MediaSubType", 0));
                        if (!String.IsNullOrEmpty(fPnpInstanceID) && fPnpInstanceID.StartsWith("PCI"))
                        {
                            if (fMediaSubType == 2)
                            {
                                fCardType = "w";
                            }
                            else
                            {
                                fCardType = "n";
                            }
                            mac = fCardType + ":" + adapter.GetPhysicalAddress().ToString() + "--";
                        }
                    }
                }
                if (mac.EndsWith("--"))
                {
                    mac = mac.Substring(0, mac.Length - 2);
                }
            }
            catch
            {
            }
            return sid + mac;

        }
    }
}
