using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using System.IO;

namespace FtpAutoUpload
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void btnSource_Click(object sender, EventArgs e)
		{
			Stream myStream = null;
			OpenFileDialog openFileDialog1 = new OpenFileDialog();

			openFileDialog1.InitialDirectory = "c:\\";
			openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
			openFileDialog1.FilterIndex = 2;
			openFileDialog1.RestoreDirectory = true;

			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				try
				{
					if ((myStream = openFileDialog1.OpenFile()) != null)
					{
						using (myStream)
						{

								string strPath = openFileDialog1.FileName;
								this.texSource.Text = strPath;

								//ListPathInfo(strPath);

						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
				}
			}
		}

		private void btnDestination_Click(object sender, EventArgs e)
		{
			//OpenFileDialog ofd = new OpenFileDialog();
			//ofd.InitialDirectory = "ftp://<username>:<password>@<host>";
			//ofd.ShowDialog();

			string curDir = System.IO.Directory.GetCurrentDirectory();
			OpenFileDialog openFileDialog1 = new OpenFileDialog();
			openFileDialog1.InitialDirectory = "ftp://infor:helloibus@192.168.168.102:21/";
			openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
			openFileDialog1.FilterIndex = 2;
			openFileDialog1.RestoreDirectory = true;
			DialogResult res = openFileDialog1.ShowDialog();
			string dirPlusFile = openFileDialog1.FileName;
			Path.GetDirectoryName(dirPlusFile);
			
			int index = dirPlusFile.LastIndexOf(@"\");
			if (index != -1)
			{
				//this.textBox1.Text = dirPlusFile.Substring(index + 1, dirPlusFile.Length - index - 1);
				this.textBox1.Text = this.textBox2.Text = dirPlusFile;
			}




		}

		public static bool Upload(string fileName, string uploadUrl, string UserName, string Password)
		{

			Stream requestStream = null;
			FileStream fileStream = null;
			FtpWebResponse uploadResponse = null;
			try
			{
				FtpWebRequest uploadRequest = (FtpWebRequest)WebRequest.Create(uploadUrl);
				uploadRequest.Method = WebRequestMethods.Ftp.UploadFile;//設定Method上傳檔案
				uploadRequest.Proxy = null;

				if (UserName.Length > 0)//如果需要帳號登入
				{
					NetworkCredential nc = new NetworkCredential(UserName, Password);
					uploadRequest.Credentials = nc; //設定帳號
				}

				requestStream = uploadRequest.GetRequestStream();
				fileStream = File.Open(fileName, FileMode.Open);
				byte[] buffer = new byte[1024];
				int bytesRead;
				while (true)
				{//開始上傳資料流
					bytesRead = fileStream.Read(buffer, 0, buffer.Length);
					if (bytesRead == 0)
						break;
					requestStream.Write(buffer, 0, bytesRead);
				}

				requestStream.Close();
				uploadResponse = (FtpWebResponse)uploadRequest.GetResponse();
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			finally
			{
				if (uploadResponse != null)
					uploadResponse.Close();
				if (fileStream != null)
					fileStream.Close();
				if (requestStream != null)
					requestStream.Close();
			}
		}

		//private void goToAdirectory()
		//{
		//	if (this.rtfFrontPage.SelectedText != String.Empty)
		//	{
		//		string directory = this.rtfFrontPage.SelectedText.Trim();
		//		OpenFileDialog openFileDialog1 = new OpenFileDialog();
		//		Console.WriteLine("Directory: " + directory);
		//		openFileDialog1.InitialDirectory = directory;
		//		openFileDialog1.Filter = "dll files (*.dll)|*.dll|All files (*.*)|*.*";
		//		openFileDialog1.FilterIndex = 2;
		//		openFileDialog1.RestoreDirectory = true;
		//		openFileDialog1.ShowDialog();
		//	}
		//}
	}
}
