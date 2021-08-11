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
            this.checkbox_KeepTop = new System.Windows.Forms.CheckBox();
            this.textbox_MainProcess = new System.Windows.Forms.TextBox();
            this.label_MainProcess = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textbox_delayTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textbox_intervalTime = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // checkbox_KeepTop
            // 
            this.checkbox_KeepTop.AutoSize = true;
            this.checkbox_KeepTop.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkbox_KeepTop.ForeColor = System.Drawing.Color.Green;
            this.checkbox_KeepTop.Location = new System.Drawing.Point(400, 59);
            this.checkbox_KeepTop.Name = "checkbox_KeepTop";
            this.checkbox_KeepTop.Size = new System.Drawing.Size(108, 23);
            this.checkbox_KeepTop.TabIndex = 0;
            this.checkbox_KeepTop.Text = "窗口置顶";
            this.checkbox_KeepTop.UseVisualStyleBackColor = true;
            // 
            // textbox_MainProcess
            // 
            this.textbox_MainProcess.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textbox_MainProcess.ForeColor = System.Drawing.Color.Red;
            this.textbox_MainProcess.Location = new System.Drawing.Point(124, 59);
            this.textbox_MainProcess.Name = "textbox_MainProcess";
            this.textbox_MainProcess.Size = new System.Drawing.Size(244, 26);
            this.textbox_MainProcess.TabIndex = 1;
            this.textbox_MainProcess.Text = "notepad.exe";
            this.textbox_MainProcess.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textbox_MainProcess.Click += new System.EventHandler(this.OnSelectMainProcess);
            // 
            // label_MainProcess
            // 
            this.label_MainProcess.AutoSize = true;
            this.label_MainProcess.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_MainProcess.ForeColor = System.Drawing.Color.Green;
            this.label_MainProcess.Location = new System.Drawing.Point(59, 62);
            this.label_MainProcess.Name = "label_MainProcess";
            this.label_MainProcess.Size = new System.Drawing.Size(59, 16);
            this.label_MainProcess.TabIndex = 2;
            this.label_MainProcess.Text = "进程名";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.Green;
            this.button1.Location = new System.Drawing.Point(400, 132);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 40);
            this.button1.TabIndex = 3;
            this.button1.Text = "应用";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnApply);
            // 
            // textbox_delayTime
            // 
            this.textbox_delayTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textbox_delayTime.ForeColor = System.Drawing.Color.Red;
            this.textbox_delayTime.Location = new System.Drawing.Point(124, 102);
            this.textbox_delayTime.Name = "textbox_delayTime";
            this.textbox_delayTime.Size = new System.Drawing.Size(83, 26);
            this.textbox_delayTime.TabIndex = 4;
            this.textbox_delayTime.Text = "10";
            this.textbox_delayTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textbox_delayTime.TextChanged += new System.EventHandler(this.OnDelayInput);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Green;
            this.label1.Location = new System.Drawing.Point(24, 105);
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
            this.label2.Location = new System.Drawing.Point(24, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "监控间隔/s";
            // 
            // textbox_intervalTime
            // 
            this.textbox_intervalTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textbox_intervalTime.ForeColor = System.Drawing.Color.Red;
            this.textbox_intervalTime.Location = new System.Drawing.Point(124, 142);
            this.textbox_intervalTime.Name = "textbox_intervalTime";
            this.textbox_intervalTime.Size = new System.Drawing.Size(83, 26);
            this.textbox_intervalTime.TabIndex = 6;
            this.textbox_intervalTime.Text = "30";
            this.textbox_intervalTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textbox_intervalTime.TextChanged += new System.EventHandler(this.OnIntervalInput);
            // 
            // DaemonKit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 225);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textbox_intervalTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textbox_delayTime);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label_MainProcess);
            this.Controls.Add(this.textbox_MainProcess);
            this.Controls.Add(this.checkbox_KeepTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Name = "DaemonKit";
            this.Text = "DaemonKit";
            this.Load += new System.EventHandler(this.DaemonKit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkbox_KeepTop;
        private System.Windows.Forms.TextBox textbox_MainProcess;
        private System.Windows.Forms.Label label_MainProcess;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textbox_delayTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textbox_intervalTime;
    }
}

