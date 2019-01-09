namespace EditFileName
{
    partial class form1
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
            this.label2 = new System.Windows.Forms.Label();
            this.rootDir = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDto1 = new System.Windows.Forms.TextBox();
            this.txtDto = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtService = new System.Windows.Forms.TextBox();
            this.txtService1 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtIService = new System.Windows.Forms.TextBox();
            this.txtIService1 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDao = new System.Windows.Forms.TextBox();
            this.txtDao1 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtIDao = new System.Windows.Forms.TextBox();
            this.txtIDao1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "根目录:";
            // 
            // rootDir
            // 
            this.rootDir.Location = new System.Drawing.Point(73, 36);
            this.rootDir.Name = "rootDir";
            this.rootDir.Size = new System.Drawing.Size(437, 21);
            this.rootDir.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "领域层:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "服务层:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 154);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "服务接口:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 191);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "仓储层:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 229);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 7;
            this.label7.Text = "仓储接口:";
            // 
            // txtDto1
            // 
            this.txtDto1.Location = new System.Drawing.Point(73, 76);
            this.txtDto1.Name = "txtDto1";
            this.txtDto1.ReadOnly = true;
            this.txtDto1.Size = new System.Drawing.Size(190, 21);
            this.txtDto1.TabIndex = 8;
            this.txtDto1.Text = "/Tibos.Domain/Dto";
            // 
            // txtDto
            // 
            this.txtDto.Location = new System.Drawing.Point(322, 76);
            this.txtDto.Name = "txtDto";
            this.txtDto.Size = new System.Drawing.Size(188, 21);
            this.txtDto.TabIndex = 9;
            this.txtDto.Text = "@Dto";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(269, 81);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = "修改为:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(268, 116);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 13;
            this.label9.Text = "修改为:";
            // 
            // txtService
            // 
            this.txtService.Location = new System.Drawing.Point(321, 111);
            this.txtService.Name = "txtService";
            this.txtService.Size = new System.Drawing.Size(188, 21);
            this.txtService.TabIndex = 12;
            this.txtService.Text = "@Service";
            // 
            // txtService1
            // 
            this.txtService1.Location = new System.Drawing.Point(72, 111);
            this.txtService1.Name = "txtService1";
            this.txtService1.ReadOnly = true;
            this.txtService1.Size = new System.Drawing.Size(190, 21);
            this.txtService1.TabIndex = 11;
            this.txtService1.Text = "/Tibos.Service";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(268, 153);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 12);
            this.label10.TabIndex = 16;
            this.label10.Text = "修改为:";
            // 
            // txtIService
            // 
            this.txtIService.Location = new System.Drawing.Point(321, 148);
            this.txtIService.Name = "txtIService";
            this.txtIService.Size = new System.Drawing.Size(188, 21);
            this.txtIService.TabIndex = 15;
            this.txtIService.Text = "@IService";
            // 
            // txtIService1
            // 
            this.txtIService1.Location = new System.Drawing.Point(72, 148);
            this.txtIService1.Name = "txtIService1";
            this.txtIService1.ReadOnly = true;
            this.txtIService1.Size = new System.Drawing.Size(190, 21);
            this.txtIService1.TabIndex = 14;
            this.txtIService1.Text = "/Tibos.Service.Contract";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(268, 190);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 12);
            this.label11.TabIndex = 19;
            this.label11.Text = "修改为:";
            // 
            // txtDao
            // 
            this.txtDao.Location = new System.Drawing.Point(321, 185);
            this.txtDao.Name = "txtDao";
            this.txtDao.Size = new System.Drawing.Size(188, 21);
            this.txtDao.TabIndex = 18;
            this.txtDao.Text = "@Dao";
            // 
            // txtDao1
            // 
            this.txtDao1.Location = new System.Drawing.Point(72, 185);
            this.txtDao1.Name = "txtDao1";
            this.txtDao1.ReadOnly = true;
            this.txtDao1.Size = new System.Drawing.Size(190, 21);
            this.txtDao1.TabIndex = 17;
            this.txtDao1.Text = "/Tibos.Repository.Service";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(267, 228);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 12);
            this.label12.TabIndex = 22;
            this.label12.Text = "修改为:";
            // 
            // txtIDao
            // 
            this.txtIDao.Location = new System.Drawing.Point(320, 223);
            this.txtIDao.Name = "txtIDao";
            this.txtIDao.Size = new System.Drawing.Size(188, 21);
            this.txtIDao.TabIndex = 21;
            this.txtIDao.Text = "I@";
            // 
            // txtIDao1
            // 
            this.txtIDao1.Location = new System.Drawing.Point(71, 223);
            this.txtIDao1.Name = "txtIDao1";
            this.txtIDao1.ReadOnly = true;
            this.txtIDao1.Size = new System.Drawing.Size(190, 21);
            this.txtIDao1.TabIndex = 20;
            this.txtIDao1.Text = "/Tibos.Repository.Contract";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 12);
            this.label1.TabIndex = 23;
            this.label1.Text = "@为占位符,相当于表名";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(190, 264);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(105, 30);
            this.btnOK.TabIndex = 24;
            this.btnOK.Text = "开始";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 315);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtIDao);
            this.Controls.Add(this.txtIDao1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtDao);
            this.Controls.Add(this.txtDao1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtIService);
            this.Controls.Add(this.txtIService1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtService);
            this.Controls.Add(this.txtService1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtDto);
            this.Controls.Add(this.txtDto1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rootDir);
            this.Controls.Add(this.label2);
            this.Name = "form1";
            this.Text = "批量修改文件名称";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox rootDir;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDto1;
        private System.Windows.Forms.TextBox txtDto;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtService;
        private System.Windows.Forms.TextBox txtService1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtIService;
        private System.Windows.Forms.TextBox txtIService1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtDao;
        private System.Windows.Forms.TextBox txtDao1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtIDao;
        private System.Windows.Forms.TextBox txtIDao1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOK;
    }
}

