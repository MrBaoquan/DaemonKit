namespace DaemonKit
{
    partial class DaemonKit
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DaemonKit));
            this.textbox_MainProcess = new System.Windows.Forms.TextBox();
            this.label_MainProcess = new System.Windows.Forms.Label();
            this.btn_confirm = new System.Windows.Forms.Button();
            this.textbox_delayTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textbox_intervalTime = new System.Windows.Forms.TextBox();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_selectProcess = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_processDir = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_configDir = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.ts_options = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_autoStart = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_keepTop = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_RunAs = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_about = new System.Windows.Forms.ToolStripMenuItem();
            this.text_information = new System.Windows.Forms.TextBox();
            this.menu_backup = new System.Windows.Forms.ToolStripMenuItem();
            this.text_logbox = new System.Windows.Forms.TextBox();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // textbox_MainProcess
            // 
            this.textbox_MainProcess.BackColor = System.Drawing.Color.White;
            this.textbox_MainProcess.CausesValidation = false;
            this.textbox_MainProcess.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textbox_MainProcess.ForeColor = System.Drawing.Color.Red;
            this.textbox_MainProcess.Location = new System.Drawing.Point(118, 110);
            this.textbox_MainProcess.Name = "textbox_MainProcess";
            this.textbox_MainProcess.ReadOnly = true;
            this.textbox_MainProcess.Size = new System.Drawing.Size(276, 26);
            this.textbox_MainProcess.TabIndex = 1;
            this.textbox_MainProcess.TabStop = false;
            this.textbox_MainProcess.Text = "notepad.exe";
            this.textbox_MainProcess.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textbox_MainProcess.Click += new System.EventHandler(this.OnSelectMainProcess);
            // 
            // label_MainProcess
            // 
            this.label_MainProcess.AutoSize = true;
            this.label_MainProcess.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_MainProcess.ForeColor = System.Drawing.Color.Green;
            this.label_MainProcess.Location = new System.Drawing.Point(53, 113);
            this.label_MainProcess.Name = "label_MainProcess";
            this.label_MainProcess.Size = new System.Drawing.Size(59, 16);
            this.label_MainProcess.TabIndex = 0;
            this.label_MainProcess.Text = "进程名";
            // 
            // btn_confirm
            // 
            this.btn_confirm.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_confirm.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_confirm.ForeColor = System.Drawing.Color.Green;
            this.btn_confirm.Location = new System.Drawing.Point(137, 318);
            this.btn_confirm.Name = "btn_confirm";
            this.btn_confirm.Size = new System.Drawing.Size(120, 40);
            this.btn_confirm.TabIndex = 4;
            this.btn_confirm.Text = "暂停守护";
            this.btn_confirm.UseVisualStyleBackColor = false;
            this.btn_confirm.Click += new System.EventHandler(this.OnApply);
            // 
            // textbox_delayTime
            // 
            this.textbox_delayTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textbox_delayTime.ForeColor = System.Drawing.Color.Red;
            this.textbox_delayTime.Location = new System.Drawing.Point(118, 159);
            this.textbox_delayTime.Name = "textbox_delayTime";
            this.textbox_delayTime.Size = new System.Drawing.Size(118, 26);
            this.textbox_delayTime.TabIndex = 2;
            this.textbox_delayTime.Text = "10";
            this.textbox_delayTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textbox_delayTime.TextChanged += new System.EventHandler(this.OnDelayInput);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Green;
            this.label1.Location = new System.Drawing.Point(18, 162);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "延迟启动/s";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Green;
            this.label2.Location = new System.Drawing.Point(18, 214);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "监控间隔/s";
            // 
            // textbox_intervalTime
            // 
            this.textbox_intervalTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textbox_intervalTime.ForeColor = System.Drawing.Color.Red;
            this.textbox_intervalTime.Location = new System.Drawing.Point(118, 211);
            this.textbox_intervalTime.Name = "textbox_intervalTime";
            this.textbox_intervalTime.Size = new System.Drawing.Size(118, 26);
            this.textbox_intervalTime.TabIndex = 3;
            this.textbox_intervalTime.Text = "30";
            this.textbox_intervalTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textbox_intervalTime.TextChanged += new System.EventHandler(this.OnIntervalInput);
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.CheckOnClick = true;
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_configDir,
            this.menu_selectProcess,
            this.menu_processDir,
            this.menu_backup});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // menu_selectProcess
            // 
            this.menu_selectProcess.Name = "menu_selectProcess";
            this.menu_selectProcess.Size = new System.Drawing.Size(180, 22);
            this.menu_selectProcess.Text = "选择进程";
            // 
            // menu_processDir
            // 
            this.menu_processDir.Name = "menu_processDir";
            this.menu_processDir.Size = new System.Drawing.Size(180, 22);
            this.menu_processDir.Text = "进程目录";
            // 
            // menu_configDir
            // 
            this.menu_configDir.Name = "menu_configDir";
            this.menu_configDir.Size = new System.Drawing.Size(180, 22);
            this.menu_configDir.Text = "配置文件";
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.ts_options,
            this.关于ToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(884, 25);
            this.mainMenuStrip.TabIndex = 10;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // ts_options
            // 
            this.ts_options.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_autoStart,
            this.menu_keepTop,
            this.menu_RunAs});
            this.ts_options.Name = "ts_options";
            this.ts_options.Size = new System.Drawing.Size(44, 21);
            this.ts_options.Text = "选项";
            // 
            // menu_autoStart
            // 
            this.menu_autoStart.Name = "menu_autoStart";
            this.menu_autoStart.Size = new System.Drawing.Size(180, 22);
            this.menu_autoStart.Text = "开机自启";
            // 
            // menu_keepTop
            // 
            this.menu_keepTop.Name = "menu_keepTop";
            this.menu_keepTop.Size = new System.Drawing.Size(180, 22);
            this.menu_keepTop.Text = "窗口置顶";
            // 
            // menu_RunAs
            // 
            this.menu_RunAs.Name = "menu_RunAs";
            this.menu_RunAs.Size = new System.Drawing.Size(180, 22);
            this.menu_RunAs.Text = "管理员身份运行";
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_about});
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.关于ToolStripMenuItem.Text = "关于";
            // 
            // menu_about
            // 
            this.menu_about.Name = "menu_about";
            this.menu_about.Size = new System.Drawing.Size(180, 22);
            this.menu_about.Text = "关于";
            // 
            // text_information
            // 
            this.text_information.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.text_information.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.text_information.ForeColor = System.Drawing.Color.Green;
            this.text_information.Location = new System.Drawing.Point(429, 28);
            this.text_information.Multiline = true;
            this.text_information.Name = "text_information";
            this.text_information.ReadOnly = true;
            this.text_information.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.text_information.Size = new System.Drawing.Size(443, 222);
            this.text_information.TabIndex = 5;
            this.text_information.Text = "硬件信息示例文本";
            // 
            // menu_backup
            // 
            this.menu_backup.Name = "menu_backup";
            this.menu_backup.Size = new System.Drawing.Size(180, 22);
            this.menu_backup.Text = "备份进程";
            // 
            // text_logbox
            // 
            this.text_logbox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.text_logbox.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.text_logbox.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.text_logbox.Location = new System.Drawing.Point(429, 256);
            this.text_logbox.Multiline = true;
            this.text_logbox.Name = "text_logbox";
            this.text_logbox.ReadOnly = true;
            this.text_logbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.text_logbox.Size = new System.Drawing.Size(443, 143);
            this.text_logbox.TabIndex = 11;
            this.text_logbox.Text = "日志信息示例文本";
            // 
            // DaemonKit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(884, 411);
            this.Controls.Add(this.text_logbox);
            this.Controls.Add(this.text_information);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textbox_intervalTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textbox_delayTime);
            this.Controls.Add(this.btn_confirm);
            this.Controls.Add(this.label_MainProcess);
            this.Controls.Add(this.textbox_MainProcess);
            this.Controls.Add(this.mainMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "DaemonKit";
            this.Text = "DaemonKit CreatedBy @MrBaoquan";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DaemonKit_Unload);
            this.Load += new System.EventHandler(this.DaemonKit_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textbox_MainProcess;
        private System.Windows.Forms.Label label_MainProcess;
        private System.Windows.Forms.Button btn_confirm;
        private System.Windows.Forms.TextBox textbox_delayTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textbox_intervalTime;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_processDir;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem menu_configDir;
        private System.Windows.Forms.ToolStripMenuItem ts_options;
        private System.Windows.Forms.ToolStripMenuItem menu_autoStart;
        private System.Windows.Forms.ToolStripMenuItem menu_keepTop;
        private System.Windows.Forms.ToolStripMenuItem menu_RunAs;
        private System.Windows.Forms.ToolStripMenuItem menu_selectProcess;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_about;
        private System.Windows.Forms.TextBox text_information;
        private System.Windows.Forms.ToolStripMenuItem menu_backup;
        private System.Windows.Forms.TextBox text_logbox;
    }
}

