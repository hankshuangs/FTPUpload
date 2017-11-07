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
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.texSource = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.btnSource = new System.Windows.Forms.Button();
			this.btnDestination = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(33, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(77, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "來源的路徑：";
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(359, 138);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(49, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "確定";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// texSource
			// 
			this.texSource.Location = new System.Drawing.Point(104, 25);
			this.texSource.Name = "texSource";
			this.texSource.Size = new System.Drawing.Size(328, 22);
			this.texSource.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(33, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77, 12);
			this.label2.TabIndex = 0;
			this.label2.Text = "目的地路徑：";
			this.label2.Click += new System.EventHandler(this.label1_Click);
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(104, 69);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(328, 22);
			this.textBox2.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(153, 114);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(61, 12);
			this.label3.TabIndex = 0;
			this.label3.Text = "iNas帳號：";
			this.label3.Click += new System.EventHandler(this.label1_Click);
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(224, 111);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(129, 22);
			this.textBox3.TabIndex = 2;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(173, 139);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(41, 12);
			this.label4.TabIndex = 0;
			this.label4.Text = "密碼：";
			this.label4.Click += new System.EventHandler(this.label1_Click);
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(224, 139);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(129, 22);
			this.textBox4.TabIndex = 2;
			// 
			// btnSource
			// 
			this.btnSource.Location = new System.Drawing.Point(454, 23);
			this.btnSource.Name = "btnSource";
			this.btnSource.Size = new System.Drawing.Size(75, 23);
			this.btnSource.TabIndex = 3;
			this.btnSource.Text = "瀏覽";
			this.btnSource.UseVisualStyleBackColor = true;
			this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
			// 
			// btnDestination
			// 
			this.btnDestination.Location = new System.Drawing.Point(454, 60);
			this.btnDestination.Name = "btnDestination";
			this.btnDestination.Size = new System.Drawing.Size(75, 23);
			this.btnDestination.TabIndex = 4;
			this.btnDestination.Text = "瀏覽";
			this.btnDestination.UseVisualStyleBackColor = true;
			this.btnDestination.Click += new System.EventHandler(this.btnDestination_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(59, 110);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(100, 22);
			this.textBox1.TabIndex = 5;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(573, 198);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.btnDestination);
			this.Controls.Add(this.btnSource);
			this.Controls.Add(this.textBox4);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.texSource);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.button1);
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
		private System.Windows.Forms.TextBox texSource;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.Button btnSource;
		private System.Windows.Forms.Button btnDestination;
		private System.Windows.Forms.TextBox textBox1;
	}
}

