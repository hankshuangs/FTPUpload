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
using System.Collections;

namespace FtpAutoUpload
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}
		public delegate void MyDelegate(string str); //我的委派
		MyDelegate moveAction;

		private void toAction(string pMsg, MyDelegate moveAction)
		{
			//去移動某公司的行車紀錄器
			moveAction(pMsg);
		}

		enum DrivingRecorder { Autotrak, Jasslin }; //列舉DrivingRecorder.Autotrak = 0 啟品 ; DrivingRecorder.Jasslin = 1 捷世林

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

			#endregion 取得之前設定值

			if (Program.dirArgs.Length != 0)
			{
				foreach (string str in Program.dirArgs)
				{
					if (str.Equals("/s"))
					{
						//TODO:執行備份
						MessageBox.Show(str);
					}
				}
			}

			//計數器 開始每1秒的速度執行中
			timer1.Enabled = true;
			timer1.Interval = 2000;
			timer1.Start();
		}

		private string state = "RUNOUT"; //RUNNING:正在跑 RUNOUT:之前跑完了

		private void timer1_Tick(object sender, EventArgs e)
		{
			lblTime.Text = DateTime.Now.ToString();
			//設定目錄路徑
			string baseDir1 = "啟品紀錄器";
			string baseDir2 = "捷世林行車紀錄器資料夾";
			string strDate = DateTime.Now.ToString("yyyy年 MM月 dd日");
			string strDir1 = baseDir1 + " " + strDate;
			string strDir2 = baseDir2 + " " + strDate;
			string[] arrDir1 = null;
			string[] arrDir2 = null;
			arrDir1 = strDir1.Split(' ');
			arrDir2 = strDir2.Split(' ');
			string strPath1 = FtpAutoUpload.Properties.Settings.Default.source; //根目錄 例如D:\
			string strPath2 = FtpAutoUpload.Properties.Settings.Default.source;
			//建立目錄
			strPath1 = setPath(arrDir1, strPath1);
			strPath2 = setPath(arrDir2, strPath2);
			//取得目錄下的檔案清單
			ArrayList lstFiles1 = getFiles(baseDir1, strPath1);
			ArrayList lstFiles2 = getFiles(baseDir2, strPath2);

			//搬移檔案
			//TODO:判斷檔案並移至到某個目錄下
			//MyDelegate moveAction = Autotrak;

			MyDelegate moveAction = null;
			moveAction = Autotrak;
			toAction("30萬", moveAction);

			moveAction = Jasslin;
			toAction("60萬", moveAction);
			WriteLog("記錄一下LOG");
			string T = DateTime.Now.ToString("T");

			//每日凌晨01:00:00備份作業
			//if (T.Equals("01:00:00")&& state.Equals("RUNOUT"))
			{
				state = "RUNNING"; //正在跑

				//本機端
				//確認目錄是否存在
				if (Directory.Exists(@"D:\啟品紀錄檔\"))
				{
					MessageBox.Show(@"D:\啟品紀錄檔 存在");
				}
				else
				{
					MessageBox.Show(@"D:\啟品紀錄檔 不存在");
				}

				state = "RUNOUT"; //跑完了
			}
		}
		public void Autotrak(string pMsg)
		{
			MessageBox.Show(pMsg);
		}
		public void Jasslin(string pMsg)
		{
			MessageBox.Show(pMsg);
		}
		/// <summary>
		/// 取得目錄下的檔案清單
		/// </summary>
		/// <param name="baseDir1">例如某個路徑上的目錄"\啟品紀錄檔\"</param>
		/// <param name="strPath1">例如根目錄"D:\"</param>
		/// <returns></returns>
		public static ArrayList getFiles(string baseDir1, string strPath1)
		{
			ArrayList arrlist = new ArrayList();
			if (Directory.Exists(strPath1))
			{
				string[] files = Directory.GetFiles(FtpAutoUpload.Properties.Settings.Default.source + baseDir1); //目錄存在就取得目錄下的檔案清單
				foreach (var item in files)
				{
					arrlist.Add(item);
				}
			}
			return arrlist;
		}

		/// <summary>
		/// 若目錄不存在就新增目錄
		/// </summary>
		/// <param name="arrDir1">例如是日期目錄字串"yyyy年 MM月 dd日"用Split(' ')切割出來的陣列</param>
		/// <param name="strPath1">例如是個根目錄"D:/" </param>
		/// <returns></returns>
		public static string setPath(string[] arrDir1, string strPath1)
		{
			//確認目錄與新增目錄
			foreach (string dir1 in arrDir1)
			{
				strPath1 = strPath1 + dir1 + @"\";
				if (!Directory.Exists(strPath1))
				{
					Directory.CreateDirectory(strPath1); //不存在就建立目錄
				}
			}
			return strPath1;
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

			#endregion 設定檔名測試
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

			#endregion MyRegion
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

		private void button2_Click(object sender, EventArgs e)
		{
			//停止時間
			timer1.Stop();
		}
		//寫LOG檔 參考:AlexWangの雲端筆記本 https://dotblogs.com.tw/alexwang/2016/12/08/112052
		public static void WriteLog(string message)
		{
			string DIRNAME = AppDomain.CurrentDomain.BaseDirectory + @"\Log\";
			string FILENAME = DIRNAME + DateTime.Now.ToString("yyyyMMdd") + ".txt";

			if (!Directory.Exists(DIRNAME))
				Directory.CreateDirectory(DIRNAME);

			if (!File.Exists(FILENAME))
			{
				// The File.Create method creates the file and opens a FileStream on the file. You neeed to close it.
				File.Create(FILENAME).Close();
			}
			using (StreamWriter sw = File.AppendText(FILENAME))
			{
				Log(message, sw);
			}
		}

		private static void Log(string logMessage, TextWriter w)
		{
			w.Write("\r\nLog Entry : ");
			w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
			w.WriteLine("  :");
			w.WriteLine("  :{0}", logMessage);
			w.WriteLine("-------------------------------");
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