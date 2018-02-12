using MyControl;

namespace MainTool
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.开始ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重置配置文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开日志文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DataProblemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.systemErrorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdMarkDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工作日志ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.ipconfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iisresetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.inetmgrToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.servicesmscToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.regeditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gpeditmscToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.计算器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.记事本ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.画图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据库脚本ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oracle脚本生成工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oracle流程定义ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SqlServer脚本生成工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SqlServer流程定义ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sqlServer备份与还原ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加密解密ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.加密解密ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.parserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.进制转换ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.字符串格式化toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.反编译ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.翻译助手ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.前端工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pOST提交工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.采集工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.博客园博客采集工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数字影像ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图像水印ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oCRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.截图工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.二维码生成器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.明源ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.建模文档中心ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.后端服务生成前端脚本ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.元数据比对ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.枚举导出工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.数据字典ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据升级比较ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开始ToolStripMenuItem,
            this.系统工具ToolStripMenuItem,
            this.数据库脚本ToolStripMenuItem,
            this.加密解密ToolStripMenuItem,
            this.前端工具ToolStripMenuItem,
            this.采集工具ToolStripMenuItem,
            this.数字影像ToolStripMenuItem,
            this.明源ToolStripMenuItem,
            this.testToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1284, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 开始ToolStripMenuItem
            // 
            this.开始ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.重置配置文件ToolStripMenuItem,
            this.打开日志文件ToolStripMenuItem,
            this.toolStripSeparator2,
            this.toolStripMenuItem1,
            this.toolStripSeparator3,
            this.cmdMarkDownToolStripMenuItem,
            this.工作日志ToolStripMenuItem});
            this.开始ToolStripMenuItem.Name = "开始ToolStripMenuItem";
            this.开始ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.开始ToolStripMenuItem.Text = "开始";
            // 
            // 重置配置文件ToolStripMenuItem
            // 
            this.重置配置文件ToolStripMenuItem.Name = "重置配置文件ToolStripMenuItem";
            this.重置配置文件ToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.重置配置文件ToolStripMenuItem.Text = "重置配置文件";
            this.重置配置文件ToolStripMenuItem.Click += new System.EventHandler(this.重置配置文件ToolStripMenuItem_Click);
            // 
            // 打开日志文件ToolStripMenuItem
            // 
            this.打开日志文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DataProblemToolStripMenuItem,
            this.systemErrorToolStripMenuItem});
            this.打开日志文件ToolStripMenuItem.Name = "打开日志文件ToolStripMenuItem";
            this.打开日志文件ToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.打开日志文件ToolStripMenuItem.Text = "打开日志文件";
            // 
            // DataProblemToolStripMenuItem
            // 
            this.DataProblemToolStripMenuItem.Name = "DataProblemToolStripMenuItem";
            this.DataProblemToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.DataProblemToolStripMenuItem.Text = "DataProblem";
            this.DataProblemToolStripMenuItem.Click += new System.EventHandler(this.dataProblemToolStripMenuItem_Click);
            // 
            // systemErrorToolStripMenuItem
            // 
            this.systemErrorToolStripMenuItem.Name = "systemErrorToolStripMenuItem";
            this.systemErrorToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.systemErrorToolStripMenuItem.Text = "SystemError";
            this.systemErrorToolStripMenuItem.Click += new System.EventHandler(this.systemErrorToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(168, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(171, 22);
            this.toolStripMenuItem1.Text = "选择皮肤";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(168, 6);
            // 
            // cmdMarkDownToolStripMenuItem
            // 
            this.cmdMarkDownToolStripMenuItem.Name = "cmdMarkDownToolStripMenuItem";
            this.cmdMarkDownToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.cmdMarkDownToolStripMenuItem.Text = "Cmd MarkDown";
            this.cmdMarkDownToolStripMenuItem.Click += new System.EventHandler(this.cmdMarkDownToolStripMenuItem_Click);
            // 
            // 工作日志ToolStripMenuItem
            // 
            this.工作日志ToolStripMenuItem.Name = "工作日志ToolStripMenuItem";
            this.工作日志ToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.工作日志ToolStripMenuItem.Text = "工作日志";
            this.工作日志ToolStripMenuItem.Click += new System.EventHandler(this.工作日志ToolStripMenuItem_Click);
            // 
            // 系统工具ToolStripMenuItem
            // 
            this.系统工具ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripSeparator5,
            this.计算器ToolStripMenuItem,
            this.记事本ToolStripMenuItem,
            this.wordToolStripMenuItem,
            this.excelToolStripMenuItem,
            this.画图ToolStripMenuItem});
            this.系统工具ToolStripMenuItem.Name = "系统工具ToolStripMenuItem";
            this.系统工具ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.系统工具ToolStripMenuItem.Text = "系统工具";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ipconfigToolStripMenuItem,
            this.iisresetToolStripMenuItem,
            this.toolStripSeparator6,
            this.inetmgrToolStripMenuItem,
            this.servicesmscToolStripMenuItem,
            this.regeditToolStripMenuItem,
            this.gpeditmscToolStripMenuItem});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItem2.Text = "命令行";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // ipconfigToolStripMenuItem
            // 
            this.ipconfigToolStripMenuItem.Name = "ipconfigToolStripMenuItem";
            this.ipconfigToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.ipconfigToolStripMenuItem.Text = "ipconfig";
            this.ipconfigToolStripMenuItem.Click += new System.EventHandler(this.ipconfigToolStripMenuItem_Click);
            // 
            // iisresetToolStripMenuItem
            // 
            this.iisresetToolStripMenuItem.Name = "iisresetToolStripMenuItem";
            this.iisresetToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.iisresetToolStripMenuItem.Text = "iisreset";
            this.iisresetToolStripMenuItem.Click += new System.EventHandler(this.iisresetToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(145, 6);
            // 
            // inetmgrToolStripMenuItem
            // 
            this.inetmgrToolStripMenuItem.Name = "inetmgrToolStripMenuItem";
            this.inetmgrToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.inetmgrToolStripMenuItem.Text = "inetmgr";
            this.inetmgrToolStripMenuItem.Click += new System.EventHandler(this.inetmgrToolStripMenuItem_Click);
            // 
            // servicesmscToolStripMenuItem
            // 
            this.servicesmscToolStripMenuItem.Name = "servicesmscToolStripMenuItem";
            this.servicesmscToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.servicesmscToolStripMenuItem.Text = "services.msc";
            this.servicesmscToolStripMenuItem.Click += new System.EventHandler(this.servicesmscToolStripMenuItem_Click);
            // 
            // regeditToolStripMenuItem
            // 
            this.regeditToolStripMenuItem.Name = "regeditToolStripMenuItem";
            this.regeditToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.regeditToolStripMenuItem.Text = "regedit";
            this.regeditToolStripMenuItem.Click += new System.EventHandler(this.regeditToolStripMenuItem_Click);
            // 
            // gpeditmscToolStripMenuItem
            // 
            this.gpeditmscToolStripMenuItem.Name = "gpeditmscToolStripMenuItem";
            this.gpeditmscToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.gpeditmscToolStripMenuItem.Text = "gpedit.msc";
            this.gpeditmscToolStripMenuItem.Click += new System.EventHandler(this.gpeditmscToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(109, 6);
            // 
            // 计算器ToolStripMenuItem
            // 
            this.计算器ToolStripMenuItem.Name = "计算器ToolStripMenuItem";
            this.计算器ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.计算器ToolStripMenuItem.Text = "计算器";
            this.计算器ToolStripMenuItem.Click += new System.EventHandler(this.计算器ToolStripMenuItem_Click);
            // 
            // 记事本ToolStripMenuItem
            // 
            this.记事本ToolStripMenuItem.Name = "记事本ToolStripMenuItem";
            this.记事本ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.记事本ToolStripMenuItem.Text = "记事本";
            this.记事本ToolStripMenuItem.Click += new System.EventHandler(this.记事本ToolStripMenuItem_Click);
            // 
            // wordToolStripMenuItem
            // 
            this.wordToolStripMenuItem.Name = "wordToolStripMenuItem";
            this.wordToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.wordToolStripMenuItem.Text = "Word";
            this.wordToolStripMenuItem.Click += new System.EventHandler(this.wordToolStripMenuItem_Click);
            // 
            // excelToolStripMenuItem
            // 
            this.excelToolStripMenuItem.Name = "excelToolStripMenuItem";
            this.excelToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.excelToolStripMenuItem.Text = "Excel";
            this.excelToolStripMenuItem.Click += new System.EventHandler(this.excelToolStripMenuItem_Click);
            // 
            // 画图ToolStripMenuItem
            // 
            this.画图ToolStripMenuItem.Name = "画图ToolStripMenuItem";
            this.画图ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.画图ToolStripMenuItem.Text = "画图";
            this.画图ToolStripMenuItem.Click += new System.EventHandler(this.画图ToolStripMenuItem_Click);
            // 
            // 数据库脚本ToolStripMenuItem
            // 
            this.数据库脚本ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oracle脚本生成工具ToolStripMenuItem,
            this.oracle流程定义ToolStripMenuItem,
            this.toolStripSeparator1,
            this.SqlServer脚本生成工具ToolStripMenuItem,
            this.SqlServer流程定义ToolStripMenuItem,
            this.sqlServer备份与还原ToolStripMenuItem});
            this.数据库脚本ToolStripMenuItem.Name = "数据库脚本ToolStripMenuItem";
            this.数据库脚本ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.数据库脚本ToolStripMenuItem.Text = "数据库脚本";
            // 
            // oracle脚本生成工具ToolStripMenuItem
            // 
            this.oracle脚本生成工具ToolStripMenuItem.Name = "oracle脚本生成工具ToolStripMenuItem";
            this.oracle脚本生成工具ToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.oracle脚本生成工具ToolStripMenuItem.Text = "Oracle脚本生成";
            this.oracle脚本生成工具ToolStripMenuItem.Click += new System.EventHandler(this.oracle脚本生成工具ToolStripMenuItem_Click);
            // 
            // oracle流程定义ToolStripMenuItem
            // 
            this.oracle流程定义ToolStripMenuItem.Name = "oracle流程定义ToolStripMenuItem";
            this.oracle流程定义ToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.oracle流程定义ToolStripMenuItem.Text = "Oracle流程定义";
            this.oracle流程定义ToolStripMenuItem.Click += new System.EventHandler(this.Oracle流程定义ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(192, 6);
            // 
            // SqlServer脚本生成工具ToolStripMenuItem
            // 
            this.SqlServer脚本生成工具ToolStripMenuItem.Name = "SqlServer脚本生成工具ToolStripMenuItem";
            this.SqlServer脚本生成工具ToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.SqlServer脚本生成工具ToolStripMenuItem.Text = "Sql Server脚本生成";
            this.SqlServer脚本生成工具ToolStripMenuItem.Click += new System.EventHandler(this.SqlServer脚本生成工具ToolStripMenuItem_Click);
            // 
            // SqlServer流程定义ToolStripMenuItem
            // 
            this.SqlServer流程定义ToolStripMenuItem.Name = "SqlServer流程定义ToolStripMenuItem";
            this.SqlServer流程定义ToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.SqlServer流程定义ToolStripMenuItem.Text = "Sql Server流程定义";
            this.SqlServer流程定义ToolStripMenuItem.Click += new System.EventHandler(this.SqlServer流程定义ToolStripMenuItem_Click);
            // 
            // sqlServer备份与还原ToolStripMenuItem
            // 
            this.sqlServer备份与还原ToolStripMenuItem.Name = "sqlServer备份与还原ToolStripMenuItem";
            this.sqlServer备份与还原ToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.sqlServer备份与还原ToolStripMenuItem.Text = "Sql Server备份与还原";
            this.sqlServer备份与还原ToolStripMenuItem.Click += new System.EventHandler(this.sqlServer备份与还原ToolStripMenuItem_Click);
            // 
            // 加密解密ToolStripMenuItem
            // 
            this.加密解密ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.加密解密ToolStripMenuItem1,
            this.parserToolStripMenuItem,
            this.进制转换ToolStripMenuItem,
            this.字符串格式化toolStripMenuItem2,
            this.toolStripSeparator4,
            this.反编译ToolStripMenuItem,
            this.翻译助手ToolStripMenuItem});
            this.加密解密ToolStripMenuItem.Name = "加密解密ToolStripMenuItem";
            this.加密解密ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.加密解密ToolStripMenuItem.Text = "数据转换";
            // 
            // 加密解密ToolStripMenuItem1
            // 
            this.加密解密ToolStripMenuItem1.Name = "加密解密ToolStripMenuItem1";
            this.加密解密ToolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.加密解密ToolStripMenuItem1.Text = "加密解密";
            this.加密解密ToolStripMenuItem1.Click += new System.EventHandler(this.加密解密ToolStripMenuItem1_Click);
            // 
            // parserToolStripMenuItem
            // 
            this.parserToolStripMenuItem.Name = "parserToolStripMenuItem";
            this.parserToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.parserToolStripMenuItem.Text = "解析器";
            this.parserToolStripMenuItem.Click += new System.EventHandler(this.parserToolStripMenuItem_Click);
            // 
            // 进制转换ToolStripMenuItem
            // 
            this.进制转换ToolStripMenuItem.Name = "进制转换ToolStripMenuItem";
            this.进制转换ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.进制转换ToolStripMenuItem.Text = "进制转换";
            this.进制转换ToolStripMenuItem.Click += new System.EventHandler(this.进制转换ToolStripMenuItem_Click);
            // 
            // 字符串格式化toolStripMenuItem2
            // 
            this.字符串格式化toolStripMenuItem2.Name = "字符串格式化toolStripMenuItem2";
            this.字符串格式化toolStripMenuItem2.Size = new System.Drawing.Size(148, 22);
            this.字符串格式化toolStripMenuItem2.Text = "字符串格式化";
            this.字符串格式化toolStripMenuItem2.Click += new System.EventHandler(this.字符串格式化toolStripMenuItem2_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(145, 6);
            // 
            // 反编译ToolStripMenuItem
            // 
            this.反编译ToolStripMenuItem.Name = "反编译ToolStripMenuItem";
            this.反编译ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.反编译ToolStripMenuItem.Text = "反编译";
            this.反编译ToolStripMenuItem.Click += new System.EventHandler(this.反编译ToolStripMenuItem_Click);
            // 
            // 翻译助手ToolStripMenuItem
            // 
            this.翻译助手ToolStripMenuItem.Name = "翻译助手ToolStripMenuItem";
            this.翻译助手ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.翻译助手ToolStripMenuItem.Text = "翻译助手";
            this.翻译助手ToolStripMenuItem.Click += new System.EventHandler(this.翻译助手ToolStripMenuItem_Click);
            // 
            // 前端工具ToolStripMenuItem
            // 
            this.前端工具ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pOST提交工具ToolStripMenuItem});
            this.前端工具ToolStripMenuItem.Name = "前端工具ToolStripMenuItem";
            this.前端工具ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.前端工具ToolStripMenuItem.Text = "前端工具";
            // 
            // pOST提交工具ToolStripMenuItem
            // 
            this.pOST提交工具ToolStripMenuItem.Name = "pOST提交工具ToolStripMenuItem";
            this.pOST提交工具ToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.pOST提交工具ToolStripMenuItem.Text = "POST提交工具";
            this.pOST提交工具ToolStripMenuItem.Click += new System.EventHandler(this.pOST提交工具ToolStripMenuItem_Click);
            // 
            // 采集工具ToolStripMenuItem
            // 
            this.采集工具ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.博客园博客采集工具ToolStripMenuItem});
            this.采集工具ToolStripMenuItem.Name = "采集工具ToolStripMenuItem";
            this.采集工具ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.采集工具ToolStripMenuItem.Text = "采集工具";
            // 
            // 博客园博客采集工具ToolStripMenuItem
            // 
            this.博客园博客采集工具ToolStripMenuItem.Name = "博客园博客采集工具ToolStripMenuItem";
            this.博客园博客采集工具ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.博客园博客采集工具ToolStripMenuItem.Text = "博客园博客采集工具";
            // 
            // 数字影像ToolStripMenuItem
            // 
            this.数字影像ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.图像水印ToolStripMenuItem,
            this.oCRToolStripMenuItem,
            this.截图工具ToolStripMenuItem,
            this.二维码生成器ToolStripMenuItem});
            this.数字影像ToolStripMenuItem.Name = "数字影像ToolStripMenuItem";
            this.数字影像ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.数字影像ToolStripMenuItem.Text = "数字影音";
            // 
            // 图像水印ToolStripMenuItem
            // 
            this.图像水印ToolStripMenuItem.Name = "图像水印ToolStripMenuItem";
            this.图像水印ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.图像水印ToolStripMenuItem.Text = "图像水印";
            // 
            // oCRToolStripMenuItem
            // 
            this.oCRToolStripMenuItem.Name = "oCRToolStripMenuItem";
            this.oCRToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.oCRToolStripMenuItem.Text = "OCR";
            this.oCRToolStripMenuItem.Click += new System.EventHandler(this.oCRToolStripMenuItem_Click);
            // 
            // 截图工具ToolStripMenuItem
            // 
            this.截图工具ToolStripMenuItem.Name = "截图工具ToolStripMenuItem";
            this.截图工具ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.截图工具ToolStripMenuItem.Text = "截图工具";
            // 
            // 二维码生成器ToolStripMenuItem
            // 
            this.二维码生成器ToolStripMenuItem.Name = "二维码生成器ToolStripMenuItem";
            this.二维码生成器ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.二维码生成器ToolStripMenuItem.Text = "二维码生成器";
            this.二维码生成器ToolStripMenuItem.Click += new System.EventHandler(this.二维码生成器ToolStripMenuItem_Click);
            // 
            // 明源ToolStripMenuItem
            // 
            this.明源ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.建模文档中心ToolStripMenuItem,
            this.后端服务生成前端脚本ToolStripMenuItem,
            this.元数据比对ToolStripMenuItem,
            this.枚举导出工具ToolStripMenuItem,
            this.数据字典ToolStripMenuItem,
            this.数据升级比较ToolStripMenuItem});
            this.明源ToolStripMenuItem.Name = "明源ToolStripMenuItem";
            this.明源ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.明源ToolStripMenuItem.Text = "明源";
            // 
            // 建模文档中心ToolStripMenuItem
            // 
            this.建模文档中心ToolStripMenuItem.Name = "建模文档中心ToolStripMenuItem";
            this.建模文档中心ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.建模文档中心ToolStripMenuItem.Text = "建模文档中心";
            this.建模文档中心ToolStripMenuItem.Click += new System.EventHandler(this.建模文档中心ToolStripMenuItem_Click);
            // 
            // 后端服务生成前端脚本ToolStripMenuItem
            // 
            this.后端服务生成前端脚本ToolStripMenuItem.Name = "后端服务生成前端脚本ToolStripMenuItem";
            this.后端服务生成前端脚本ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.后端服务生成前端脚本ToolStripMenuItem.Text = "后端服务生成前端脚本";
            this.后端服务生成前端脚本ToolStripMenuItem.Click += new System.EventHandler(this.后端服务生成前端脚本ToolStripMenuItem_Click);
            // 
            // 元数据比对ToolStripMenuItem
            // 
            this.元数据比对ToolStripMenuItem.Name = "元数据比对ToolStripMenuItem";
            this.元数据比对ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.元数据比对ToolStripMenuItem.Text = "元数据比对";
            this.元数据比对ToolStripMenuItem.Click += new System.EventHandler(this.元数据比对ToolStripMenuItem_Click);
            // 
            // 枚举导出工具ToolStripMenuItem
            // 
            this.枚举导出工具ToolStripMenuItem.Name = "枚举导出工具ToolStripMenuItem";
            this.枚举导出工具ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.枚举导出工具ToolStripMenuItem.Text = "枚举导出工具";
            this.枚举导出工具ToolStripMenuItem.Click += new System.EventHandler(this.枚举导出工具ToolStripMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.testToolStripMenuItem.Text = "Test";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1284, 705);
            this.panel1.TabIndex = 4;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 708);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1284, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 数据字典ToolStripMenuItem
            // 
            this.数据字典ToolStripMenuItem.Name = "数据字典ToolStripMenuItem";
            this.数据字典ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.数据字典ToolStripMenuItem.Text = "数据字典";
            this.数据字典ToolStripMenuItem.Click += new System.EventHandler(this.数据字典ToolStripMenuItem_Click);
            // 
            // 数据升级比较ToolStripMenuItem
            // 
            this.数据升级比较ToolStripMenuItem.Name = "数据升级比较ToolStripMenuItem";
            this.数据升级比较ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.数据升级比较ToolStripMenuItem.Text = "数据升级比较";
            this.数据升级比较ToolStripMenuItem.Click += new System.EventHandler(this.数据升级比较ToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1284, 730);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(960, 600);
            this.Name = "MainForm";
            this.Text = "软件开发辅助工具";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 开始ToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem 重置配置文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据库脚本ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oracle脚本生成工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SqlServer脚本生成工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加密解密ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 加密解密ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem parserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 前端工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pOST提交工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 采集工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 博客园博客采集工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数字影像ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图像水印ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oracle流程定义ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem SqlServer流程定义ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oCRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sqlServer备份与还原ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 进制转换ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开日志文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DataProblemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem systemErrorToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 截图工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem cmdMarkDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 二维码生成器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem 反编译ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系统工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 计算器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 记事本ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 工作日志ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 字符串格式化toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem ipconfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iisresetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inetmgrToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem servicesmscToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem regeditToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem 画图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gpeditmscToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem 明源ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 建模文档中心ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 后端服务生成前端脚本ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 元数据比对ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 翻译助手ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 枚举导出工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据字典ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据升级比较ToolStripMenuItem;
    }
}

