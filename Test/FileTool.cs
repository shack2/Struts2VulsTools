using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace file
{
    class FileTool
    {

        //读取单个字典文件（文件编码为UTF-8）
        public static List<String> readFileToList(String path)
        {

            List<String> list = new List<String>();
            FileStream fs_dir = null;
            StreamReader reader = null;
            try
            {
                fs_dir = new FileStream(path, FileMode.Open, FileAccess.Read);

                reader = new StreamReader(fs_dir);

                String lineStr;

                while ((lineStr = reader.ReadLine()) != null)
                {
                    if (!lineStr.Equals(""))
                    {
                        list.Add(lineStr);
                    }
                }
            }
            catch (Exception e)
            {
                
            }
            finally {
                if (reader != null)
                {
                    reader.Close();
                }
                if (fs_dir != null)
                {
                    fs_dir.Close();
                }
            }
            return list;
        }

        //读取文件
        public static String readFileToString(String path)
        {
            String str = "";
            FileStream fs_dir=null;
            StreamReader reader = null;
            try
            {
                fs_dir = new FileStream(path, FileMode.Open, FileAccess.Read);
                reader = new StreamReader(fs_dir);
                str = reader.ReadToEnd();
            }
            catch (Exception e)
            {
               
            }finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (fs_dir != null)
                {
                    fs_dir.Close();
                }
            }
            return str;
            
        }
        //读取文件
        public static Byte[] readFileToByte(String path)
        {
            Byte[] buffer = null;
            FileStream fs_dir = null;
            try
            {
                fs_dir = new FileStream(path, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs_dir);
                int len = (int)fs_dir.Length;

                buffer = new byte[len];

                int size = br.Read(buffer, 0, len);

            }
            catch (Exception e)
            {
                
            }
            finally
            {
                if (fs_dir != null)
                {
                    fs_dir.Close();
                }
            }
            return buffer;

        }

        //读取文件
        public static String readFileToStr16(String path)
        {
            Byte[] buffer = null;
            FileStream fs_dir = null;
            StringBuilder str16 = new StringBuilder();
            try
            {
                fs_dir = new FileStream(path, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs_dir);
                int len = (int)fs_dir.Length;

                buffer = new byte[len];

                int size = br.Read(buffer, 0, len);

                foreach (byte c in buffer)
                {
                    str16.Append(c.ToString("x").PadLeft(2, '0'));
                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                if (fs_dir != null)
                {
                    fs_dir.Close();
                }
            }
            return str16.ToString().ToUpper();

        }
        
        public static void AppendLogToFile(String path,String log)
        {
            List<String> list = new List<String>();
            FileStream fs_dir=null;
            StreamWriter sw= null;
            try
            {
                fs_dir = new FileStream(path, FileMode.Append, FileAccess.Write);

                sw = new StreamWriter(fs_dir);

                sw.WriteLine(log);

                sw.Close();

                fs_dir.Close();

            }
            catch (Exception e)
            {
                
            }
            finally 
            {
                if (sw != null)
                {
                    sw.Close();
                }
                if (fs_dir != null)
                {
                    fs_dir.Close();
                }
            }

        }
        public static String getDomainByString(String weburl)
        {
            try
            {
                if (!weburl.StartsWith("http://"))
                {
                    weburl = "http://" + weburl;
                }
                Uri u = new Uri(weburl);

                if (u.Port == 80)
                {
                    return u.Scheme + "://" + u.Host + "/" + u.LocalPath;
                }
                return u.Scheme + "://" + u.Host + ":" + u.Port + "/";

            }
            catch (Exception e)
            {
            }
            return "";
        }
    }
}
