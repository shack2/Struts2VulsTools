using System;
using System.Collections.Generic;
using System.Text;

namespace http
{
    public class Config
    {
        public List<String> VulNameS=new List<String>();
        public String FileName = "";
        public String FilePath = "";
        public String Method = "";
        public String Data = "";
        public String Cookie = "";
        public String FileContent = "";
        public Boolean isExeCMD = false;
        public Boolean isUpFile = false;
        public Boolean isSetUploadPath = false;
    }
}
