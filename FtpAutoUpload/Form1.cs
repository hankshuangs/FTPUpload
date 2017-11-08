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
			#region 取得之前設定值
			//地區下拉選單的內容設定
			this.cmbArea.Items.AddRange(new object[] { "高雄區", "台中區" }); //0:高雄區 1:台中區

			//iNas 路徑
			if (!String.IsNullOrEmpty(FtpAutoUpload.Properties.Settings.Default.nasIP))
			{
				txtNasIP.Text = FtpAutoUpload.Properties.Settings.Default.nasIP;
			}
			//資料來源碟
			if (!String.IsNullOrEmpty(FtpAutoUpload.Properties.Settings.Default.source))
			{
				txtSource.Text = FtpAutoUpload.Properties.Settings.Default.source;
			}
			//單位(地區)
			if (!String.IsNullOrEmpty(FtpAutoUpload.Properties.Settings.Default.area))
			{
				cmbArea.SelectedItem = FtpAutoUpload.Properties.Settings.Default.area;
			}
			//帳號
			if (!String.IsNullOrEmpty(FtpAutoUpload.Properties.Settings.Default.ID))
			{
				txtID.Text = FtpAutoUpload.Properties.Settings.Default.ID;
			}
			//密碼
			if (!String.IsNullOrEmpty(FtpAutoUpload.Properties.Settings.Default.PW))
			{
				txtPW.Text = FtpAutoUpload.Properties.Settings.Default.PW;
			}
			#endregion

			//計數器 開始每1秒的速度執行中
			timer1.Enabled = true;
			timer1.Interval = 1000;
			timer1.Start();

		}
		string state = "RUNOUT"; //RUNNING:正在跑 RUNOUT:之前跑完了
		private void timer1_Tick(object sender, EventArgs e)
		{
			lblTime.Text = DateTime.Now.ToString();
			string T = System.DateTime.Now.ToString("T");

			//每日凌晨01:00:00備份作業
			if (T.Equals("01:00:00")&& state.Equals("RUNOUT"))
			{
				state = "RUNNING"; //正在跑



				state = "RUNOUT"; //跑完了
			}

		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void btnSource_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog path = new FolderBrowserDialog();
			path.ShowDialog();
			this.txtSource.Text = path.SelectedPath;

			#region 設定檔名測試
			/*
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

							strPath = Path.GetDirectoryName(strPath);
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

			List<string> myList = new List<string>();
			List<string> myList2 = new List<string>();
			// 執行檔路徑下的 MyDir 資料夾
			//string folderName = System.Windows.Forms.Application.StartupPath + @"\MyDir";
			//string folderName = System.Windows.Forms.Application.StartupPath + this.texSource.Text;
			string folderName = this.texSource.Text;

			// 取得資料夾內所有檔案
			foreach (string fname in System.IO.Directory.GetFiles(folderName))
			{
				//string line;


				myList2.Add(Path.GetFileName(fname));
				MessageBox.Show(Path.GetFileName(fname));
				// 一次讀取一行
				//System.IO.StreamReader file = new System.IO.StreamReader(fname);
				//while ((line = file.ReadLine()) != null)
				//{

				//	myList.Add(line.Trim());

				//}

				//file.Close();
			}
			*/
			#endregion




		}

		private void btnDestination_Click(object sender, EventArgs e)
		{
			
			



			#region MyRegion
			/*
			 * 
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
			this.textBox1.Text = this.txtNasIP.Text = dirPlusFile;
		}
			 * 
			 */
			#endregion






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

		private void button1_Click(object sender, EventArgs e)
		{
			string ID = this.txtID.Text;
			string PW = this.txtPW.Text;
			bool isOK = Upload("C:\\ABC\\t1108.txt", "ftp://192.168.168.102:21//行車紀錄器//高雄區//啟品//t1108.txt", ID, PW);
			MessageBox.Show(isOK.ToString());

		}



		private void btnSet_Click(object sender, EventArgs e)
		{
			//設定值存檔
			FtpAutoUpload.Properties.Settings.Default.nasIP = txtNasIP.Text;
			FtpAutoUpload.Properties.Settings.Default.area = cmbArea.SelectedItem.ToString();
			FtpAutoUpload.Properties.Settings.Default.source = txtSource.Text;
			FtpAutoUpload.Properties.Settings.Default.ID = txtID.Text;
			FtpAutoUpload.Properties.Settings.Default.PW = txtPW.Text;
			FtpAutoUpload.Properties.Settings.Default.Save();
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
