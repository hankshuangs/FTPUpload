namespace FtpAutoUpload
{
	partial class Form1
	{
		/// <summary>
		/// 設計工具所需的變數。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清除任何使用中的資源。
		/// </summary>
		/// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form 設計工具產生的程式碼

		/// <summary>
		/// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
		/// 這個方法的內容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.txtSource = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtNasIP = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtID = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtPW = new System.Windows.Forms.TextBox();
			this.btnSource = new System.Windows.Forms.Button();
			this.btnDestination = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.lblTime = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.cmbArea = new System.Windows.Forms.ComboBox();
			this.btnSet = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(33, 66);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(77, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "來源的路徑：";
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(359, 176);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(49, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "設定";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// txtSource
			// 
			this.txtSource.Location = new System.Drawing.Point(104, 63);
			this.txtSource.Name = "txtSource";
			this.txtSource.Size = new System.Drawing.Size(328, 22);
			this.txtSource.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(33, 110);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 12);
			this.label2.TabIndex = 0;
			this.label2.Text = "iNas 位址：";
			this.label2.Click += new System.EventHandler(this.label1_Click);
			// 
			// txtNasIP
			// 
			this.txtNasIP.Location = new System.Drawing.Point(104, 107);
			this.txtNasIP.Name = "txtNasIP";
			this.txtNasIP.Size = new System.Drawing.Size(328, 22);
			this.txtNasIP.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(153, 152);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(61, 12);
			this.label3.TabIndex = 0;
			this.label3.Text = "iNas帳號：";
			this.label3.Click += new System.EventHandler(this.label1_Click);
			// 
			// txtID
			// 
			this.txtID.Location = new System.Drawing.Point(224, 149);
			this.txtID.Name = "txtID";
			this.txtID.Size = new System.Drawing.Size(129, 22);
			this.txtID.TabIndex = 2;
			this.txtID.Text = "infor";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(173, 177);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(41, 12);
			this.label4.TabIndex = 0;
			this.label4.Text = "密碼：";
			this.label4.Click += new System.EventHandler(this.label1_Click);
			// 
			// txtPW
			// 
			this.txtPW.Location = new System.Drawing.Point(224, 177);
			this.txtPW.Name = "txtPW";
			this.txtPW.PasswordChar = '*';
			this.txtPW.Size = new System.Drawing.Size(129, 22);
			this.txtPW.TabIndex = 2;
			this.txtPW.Text = "helloibus";
			// 
			// btnSource
			// 
			this.btnSource.Location = new System.Drawing.Point(454, 61);
			this.btnSource.Name = "btnSource";
			this.btnSource.Size = new System.Drawing.Size(75, 23);
			this.btnSource.TabIndex = 3;
			this.btnSource.Text = "瀏覽";
			this.btnSource.UseVisualStyleBackColor = true;
			this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
			// 
			// btnDestination
			// 
			this.btnDestination.Location = new System.Drawing.Point(454, 105);
			this.btnDestination.Name = "btnDestination";
			this.btnDestination.Size = new System.Drawing.Size(75, 23);
			this.btnDestination.TabIndex = 4;
			this.btnDestination.Text = "設定";
			this.btnDestination.UseVisualStyleBackColor = true;
			this.btnDestination.Click += new System.EventHandler(this.btnDestination_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(35, 167);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(100, 22);
			this.textBox1.TabIndex = 5;
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// lblTime
			// 
			this.lblTime.AutoSize = true;
			this.lblTime.Location = new System.Drawing.Point(264, 225);
			this.lblTime.Name = "lblTime";
			this.lblTime.Size = new System.Drawing.Size(33, 12);
			this.lblTime.TabIndex = 6;
			this.lblTime.Text = "label5";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(68, 27);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(41, 12);
			this.label5.TabIndex = 0;
			this.label5.Text = "單位：";
			this.label5.Click += new System.EventHandler(this.label1_Click);
			// 
			// cmbArea
			// 
			this.cmbArea.FormattingEnabled = true;
			this.cmbArea.Location = new System.Drawing.Point(104, 24);
			this.cmbArea.Name = "cmbArea";
			this.cmbArea.Size = new System.Drawing.Size(85, 20);
			this.cmbArea.TabIndex = 7;
			// 
			// btnSet
			// 
			this.btnSet.Location = new System.Drawing.Point(442, 170);
			this.btnSet.Name = "btnSet";
			this.btnSet.Size = new System.Drawing.Size(87, 34);
			this.btnSet.TabIndex = 8;
			this.btnSet.Text = "確認資料設定";
			this.btnSet.UseVisualStyleBackColor = true;
			this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(601, 273);
			this.Controls.Add(this.btnSet);
			this.Controls.Add(this.cmbArea);
			this.Controls.Add(this.lblTime);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.btnDestination);
			this.Controls.Add(this.btnSource);
			this.Controls.Add(this.txtPW);
			this.Controls.Add(this.txtID);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtNasIP);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtSource);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox txtSource;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtNasIP;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtID;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtPW;
		private System.Windows.Forms.Button btnSource;
		private System.Windows.Forms.Button btnDestination;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Label lblTime;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox cmbArea;
		private System.Windows.Forms.Button btnSet;
	}
}

