namespace five_in_line
{
    partial class frmFive
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
            this.pic = new System.Windows.Forms.PictureBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.chkAIenable = new System.Windows.Forms.CheckBox();
            this.chkBlackFirst = new System.Windows.Forms.CheckBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnRegret = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.SuspendLayout();
            // 
            // pic
            // 
            this.pic.Location = new System.Drawing.Point(12, 12);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(433, 433);
            this.pic.TabIndex = 0;
            this.pic.TabStop = false;
            this.pic.Click += new System.EventHandler(this.pic_Click);
            this.pic.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pic_MouseClick);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(458, 12);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 43);
            this.btnNew.TabIndex = 1;
            this.btnNew.Text = "新游戏";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStatus.Location = new System.Drawing.Point(456, 67);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(104, 16);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "游戏尚未开始";
            // 
            // chkAIenable
            // 
            this.chkAIenable.AutoSize = true;
            this.chkAIenable.Checked = true;
            this.chkAIenable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAIenable.Location = new System.Drawing.Point(459, 100);
            this.chkAIenable.Name = "chkAIenable";
            this.chkAIenable.Size = new System.Drawing.Size(96, 16);
            this.chkAIenable.TabIndex = 3;
            this.chkAIenable.Text = "与计算机对战";
            this.chkAIenable.UseVisualStyleBackColor = true;
            // 
            // chkBlackFirst
            // 
            this.chkBlackFirst.AutoSize = true;
            this.chkBlackFirst.Checked = true;
            this.chkBlackFirst.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBlackFirst.Location = new System.Drawing.Point(459, 122);
            this.chkBlackFirst.Name = "chkBlackFirst";
            this.chkBlackFirst.Size = new System.Drawing.Size(132, 16);
            this.chkBlackFirst.TabIndex = 4;
            this.chkBlackFirst.Text = "人机时玩家执黑先手";
            this.chkBlackFirst.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(458, 298);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(80, 25);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Visible = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnRegret
            // 
            this.btnRegret.Location = new System.Drawing.Point(458, 173);
            this.btnRegret.Name = "btnRegret";
            this.btnRegret.Size = new System.Drawing.Size(80, 25);
            this.btnRegret.TabIndex = 6;
            this.btnRegret.Text = "悔棋";
            this.btnRegret.UseVisualStyleBackColor = true;
            this.btnRegret.Click += new System.EventHandler(this.btnRegret_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(457, 212);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 36);
            this.label1.TabIndex = 7;
            this.label1.Text = "每次只退一步棋，\r\n\r\n可以跟电脑换手！";
            // 
            // frmFive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 454);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRegret);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.chkBlackFirst);
            this.Controls.Add(this.chkAIenable);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.pic);
            this.MaximizeBox = false;
            this.Name = "frmFive";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "五子棋";
            this.Load += new System.EventHandler(this.frmFive_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.CheckBox chkAIenable;
        private System.Windows.Forms.CheckBox chkBlackFirst;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnRegret;
        private System.Windows.Forms.Label label1;
    }
}

