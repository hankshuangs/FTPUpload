﻿using System;
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

        public delegate void MyDelegate(ArrayList lstFiles); //我的委派

        public void toAction(ArrayList lstFiles, MyDelegate moveAction)
        {
            //去移動某公司的行車紀錄器
            moveAction(lstFiles);
        }

        private enum DrivingRecorder
        { Autotrak, Jasslin }; //列舉DrivingRecorder.Autotrak = 0 啟品 ; DrivingRecorder.Jasslin = 1 捷世林

        private enum Inf
        { success, fail, complete }; //0:成功 1:失敗 2:完成

        //private int isbakComplete = (int)Inf.fail; //預設備分失敗

        private void Form1_Load(object sender, EventArgs e)
        {
            #region 取得之前設定值

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
                txtArea.Text = FtpAutoUpload.Properties.Settings.Default.area;
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
                    //參數為/s表示跑備份
                    if (str.Equals("/s"))
                    {
                        try
                        {
                            //設定目錄路徑
                            string baseDir1 = "啟品紀錄器";
                            string baseDir2 = "捷世林行車紀錄器資料夾";
                            string strPath1 = FtpAutoUpload.Properties.Settings.Default.source; //根目錄 例如D:\
                            string strPath2 = FtpAutoUpload.Properties.Settings.Default.source;

                            //取得目錄下的檔案清單
                            ArrayList lstFiles1 = getFiles(baseDir1, strPath1);
                            ArrayList lstFiles2 = getFiles(baseDir2, strPath2);

                            //搬移檔案  判斷檔案然後去做搬移至到某個目錄下
                            MyDelegate moveAction = null;
                            int codeNum = (int)DrivingRecorder.Autotrak;
                            if (codeNum == 0)
                            {
                                moveAction = Autotrak;
                                toAction(lstFiles1, moveAction);
                            }
                            codeNum = (int)DrivingRecorder.Jasslin;
                            if (codeNum == 1)
                            {
                                moveAction = Jasslin;
                                toAction(lstFiles2, moveAction);
                            }
                            FtpAutoUpload.Properties.Settings.Default.isbakComplete = (int)Inf.complete; //備份完成
                            WriteLog("備份完成");
                        }
                        catch (Exception ex)
                        {
                            WriteLog("有未知的錯誤造成備份失敗而中斷，可能原因: \n\r" + ex);
                            FtpAutoUpload.Properties.Settings.Default.isbakComplete = (int)Inf.fail;
                        }
                        FtpAutoUpload.Properties.Settings.Default.Save();
                        this.Close();
                        Environment.Exit(Environment.ExitCode);
                    }
                    if (str.Equals("/t"))
                    {
                        FtpAutoUpload.Properties.Settings.Default.isbakComplete = (int)Inf.fail;
                        FtpAutoUpload.Properties.Settings.Default.Save();
                    }
                }
            }
            //若備份失敗顯示LOG檔內容
            if (FtpAutoUpload.Properties.Settings.Default.isbakComplete == 1)
            {
                ArrayList lstFiles = getFiles("Log", AppDomain.CurrentDomain.BaseDirectory);
                lstFiles.Sort();
                lstFiles.Reverse();
                string fileName = lstFiles[0].ToString();
                using (StreamReader sr = new StreamReader(fileName, System.Text.Encoding.UTF8))
                {
                    FileReader(sr, fileName);
                }
            }
        }

        public async void FileReader(StreamReader sr, string filePath)

        {
            long size = sr.BaseStream.Length;
            int countLine = 0;
            string result = "";

            //亦可用while(sr.Peek() != -1)來判斷
            while (sr.EndOfStream != true)
            {
                result = result + await sr.ReadLineAsync() + "\n";
                countLine = countLine + 1;
            }
            sr.Close();
            MessageBox.Show(result, "錯誤訊息");
        }

        //啟品紀錄器 備份
        public void Autotrak(ArrayList lstFiles)
        {
            string area = FtpAutoUpload.Properties.Settings.Default.area; //FTP上目錄分高雄區與台中區
            string baseDir1 = "啟品紀錄器";
            int State = 0;
            foreach (string filePath in lstFiles)
            {
                State = FTP_FileUuload(filePath, baseDir1, area); //啟品_FTP_移動
                if (State == 1)
                    break;//若該檔案上送FTP失敗,可能重複檔名或FTP主機離線,該檔案本機就不移動。詳情請查看LOG檔。
                State = FileMove(filePath, baseDir1); //啟品_本機_移動
            }
        }

        //捷世林行車紀錄器資料夾 備份
        public void Jasslin(ArrayList lstFiles)
        {
            string area = FtpAutoUpload.Properties.Settings.Default.area; //FTP上目錄分高雄區與台中區
            string baseDir2 = "捷世林行車紀錄器資料夾";
            int State = 0;
            foreach (string filePath in lstFiles)
            {
                State = FTP_FileUuload(filePath, baseDir2, area); //捷世林_FTP_移動
                if (State == 1)
                    break;//若該檔案上送FTP失敗,可能重複檔名或FTP主機離線,該檔案本機就不移動。詳情請查看LOG檔。
                State = FileMove(filePath, baseDir2); //捷世林_本機_移動
            }
        }

        public int FTP_FileUuload(string filePath, string baseDir, string area)
        {
            string pathName = Path.GetDirectoryName(filePath); //路徑的名稱
            string fileName = Path.GetFileName(filePath); //檔名與副檔名
            int fileNameLen = fileName.Length;
            string UserName = FtpAutoUpload.Properties.Settings.Default.ID;
            string Password = FtpAutoUpload.Properties.Settings.Default.PW;
            if (baseDir.Equals("啟品紀錄器"))
            {
                try
                {
                    //正常檔
                    if (Path.GetExtension(filePath).Equals(".SDT") && fileNameLen == 50)
                    {
                        //建立檔案日期所對應的目錄
                        string[] arrDateInf = Path.GetFileNameWithoutExtension(filePath).Split('_');
                        string statDate = arrDateInf[4]; //開始日期時
                        string dir = area + " " + baseDir + " " + ("20" + statDate.Substring(0, 2) + "年 " + statDate.Substring(2, 2) + "月 " + statDate.Substring(4, 2) + "日");
                        string[] arrDir1 = dir.Split(' ');
                        string FTP_Path = setFTP_Path(arrDir1, UserName, Password);
                        FTP_Path = FTP_Path + "//" + fileName; //目的地
                        bool isOK = Upload(filePath, FTP_Path, UserName, Password);
                    }
                    //異常檔
                    if (Path.GetExtension(filePath).Equals(".SDT") && (fileNameLen < 50 || fileNameLen > 50))
                    {
                        //建立檔案日期所對應的目錄
                        string[] arrDateInf = Path.GetFileNameWithoutExtension(filePath).Split('_');
                        string statDate = arrDateInf[0]; //開始日期時間
                        string dir = area + " " + baseDir + " 異常檔";
                        string[] arrDir1 = dir.Split(' ');
                        string FTP_Path = setFTP_Path(arrDir1, UserName, Password);
                        FTP_Path = FTP_Path + "//" + fileName; //目的地
                        bool isOK = Upload(filePath, FTP_Path, UserName, Password);
                    }
                }
                catch (Exception IOEx)
                {
                    WriteLog("FTP搬移啟品行車紀錄器檔" + fileName + "時發生 \n" + IOEx);
                    return (int)Inf.fail;
                }
                return (int)Inf.success;
            }
            if (baseDir.Equals("捷世林行車紀錄器資料夾"))
            {
                try
                {
                    //正常檔
                    if (Path.GetExtension(filePath).Equals(".jas") && fileNameLen == 19)
                    {
                        //建立檔案日期所對應的目錄
                        string[] arrDateInf = Path.GetFileNameWithoutExtension(filePath).Split('_');
                        string statDate = arrDateInf[0]; //開始日期時間
                        string dir = area + " " + baseDir + " " + (statDate.Substring(0, 4) + "年 " + statDate.Substring(4, 2) + "月 " + statDate.Substring(6, 2) + "日");
                        string[] arrDir1 = dir.Split(' ');
                        string FTP_Path = setFTP_Path(arrDir1, UserName, Password);
                        FTP_Path = FTP_Path + "//" + fileName; //目的地
                        bool isOK = Upload(filePath, FTP_Path, UserName, Password);
                    }
                    //異常檔
                    if (Path.GetExtension(filePath).Equals(".jas") && (fileNameLen < 19 || fileNameLen > 19))
                    {
                        //建立檔案日期所對應的目錄
                        string[] arrDateInf = Path.GetFileNameWithoutExtension(filePath).Split('_');
                        string statDate = arrDateInf[0]; //開始日期時間
                        string dir = area + " " + baseDir + " 異常檔";
                        string[] arrDir1 = dir.Split(' ');
                        string FTP_Path = setFTP_Path(arrDir1, UserName, Password);
                        FTP_Path = FTP_Path + "//" + fileName; //目的地
                        bool isOK = Upload(filePath, FTP_Path, UserName, Password);
                    }
                }
                catch (Exception IOEx)
                {
                    WriteLog("FTP搬移捷世林行車紀錄器檔" + fileName + "時發生 \n" + IOEx);
                    return (int)Inf.fail;
                }
                return (int)Inf.success;
            }
            return (int)Inf.fail; //目錄名稱不符
        }

        public int FileMove(string strPath, string baseDir)
        {
            string pathName = Path.GetDirectoryName(strPath); //路徑的名稱
            string fileName = Path.GetFileName(strPath); //檔名與副檔名
            int fileNameLen = fileName.Length;
            if (baseDir.Equals("啟品紀錄器"))
            {
                try
                {
                    //正常檔
                    if (Path.GetExtension(strPath).Equals(".SDT") && fileNameLen == 50)
                    {
                        //建立檔案日期所對應的目錄
                        string[] arrDateInf = Path.GetFileNameWithoutExtension(strPath).Split('_');
                        string statDate = arrDateInf[4]; //開始日期時
                        string dir = baseDir + " " + ("20" + statDate.Substring(0, 2) + "年 " + statDate.Substring(2, 2) + "月 " + statDate.Substring(4, 2) + "日");
                        string[] arrDir1 = dir.Split(' ');
                        string strPath1 = strPath.Substring(0, 3);
                        string path1 = setPath(arrDir1, strPath1);

                        //目的地目錄
                        string newpPath = pathName + @"\" + "20" + statDate.Substring(0, 2) + "年" + @"\" + statDate.Substring(2, 2) + "月" + @"\" + statDate.Substring(4, 2) + "日" + @"\" + fileName;
                        File.Copy(strPath, newpPath, true); //true 為覆寫
                        File.Delete(strPath);
                    }
                    //異常檔
                    if (Path.GetExtension(strPath).Equals(".SDT") && (fileNameLen < 50 || fileNameLen > 50))
                    {
                        //建立檔案日期所對應的目錄
                        string[] arrDateInf = Path.GetFileNameWithoutExtension(strPath).Split('_');
                        string statDate = arrDateInf[0]; //開始日期時間
                        string dir = baseDir + " 異常檔";
                        string[] arrDir1 = dir.Split(' ');
                        string strPath1 = strPath.Substring(0, 3);
                        string path1 = setPath(arrDir1, strPath1);

                        //目的地目錄
                        string newpPath = pathName + @"\" + "異常檔" + @"\" + fileName;
                        File.Copy(strPath, newpPath, true); //true 為覆寫
                        File.Delete(strPath);
                    }
                }
                catch (Exception IOEx)
                {
                    WriteLog("本機搬移啟品行車紀錄器檔" + fileName + "時發生 \n" + IOEx);
                    return (int)Inf.fail;
                }
                return (int)Inf.success;
            }
            if (baseDir.Equals("捷世林行車紀錄器資料夾"))
            {
                try
                {
                    //正常檔
                    if (Path.GetExtension(strPath).Equals(".jas") && fileNameLen == 19)
                    {
                        //建立檔案日期所對應的目錄
                        string[] arrDateInf = Path.GetFileNameWithoutExtension(strPath).Split('_');
                        string statDate = arrDateInf[0]; //開始日期時間
                        string dir = baseDir + " " + (statDate.Substring(0, 4) + "年 " + statDate.Substring(4, 2) + "月 " + statDate.Substring(6, 2) + "日");
                        string[] arrDir1 = dir.Split(' ');
                        string strPath1 = strPath.Substring(0, 3);
                        string path1 = setPath(arrDir1, strPath1);

                        //目的地目錄
                        string newpPath = pathName + @"\" + statDate.Substring(0, 4) + "年" + @"\" + statDate.Substring(4, 2) + "月" + @"\" + statDate.Substring(6, 2) + "日" + @"\" + fileName;
                        File.Copy(strPath, newpPath, true); //true 為覆寫
                        File.Delete(strPath);
                    }
                    //異常檔
                    if (Path.GetExtension(strPath).Equals(".jas") && (fileNameLen < 19 || fileNameLen > 19))
                    {
                        //建立檔案日期所對應的目錄
                        string[] arrDateInf = Path.GetFileNameWithoutExtension(strPath).Split('_');
                        string statDate = arrDateInf[0]; //開始日期時間
                        string dir = baseDir + " 異常檔";
                        string[] arrDir1 = dir.Split(' ');
                        string strPath1 = strPath.Substring(0, 3);
                        string path1 = setPath(arrDir1, strPath1);

                        //目的地目錄
                        string newpPath = pathName + @"\" + "異常檔" + @"\" + fileName;
                        File.Copy(strPath, newpPath, true); //true 為覆寫
                        File.Delete(strPath);
                    }
                }
                catch (Exception IOEx)
                {
                    WriteLog("本機搬移捷世林行車紀錄器檔" + fileName + "時發生 \n" + IOEx);
                    return (int)Inf.fail;
                }
                return (int)Inf.success;
            }
            return (int)Inf.fail; //目錄名稱不符
        }

        /// <summary>
        /// 若FTP上的目錄不存在就新增目錄
        /// </summary>
        /// <param name="arrDir1"></param>
        /// <param name="strPath1"></param>
        /// <returns></returns>
        private string setFTP_Path(string[] arrDir1, string sUserName, string sPassword)
        {
            string iNasIP = FtpAutoUpload.Properties.Settings.Default.nasIP;
            string sFullPath = iNasIP;
            foreach (string Forder in arrDir1)
            {
                sFullPath = sFullPath + "//" + Forder;
                CreateFTP_Forder(sFullPath, sUserName, sPassword);
            }
            return sFullPath;
        }

        /// <summary>
        /// 取得目錄下的檔案清單
        /// </summary>
        /// <param name="baseDir">例如某個路徑上的目錄"\啟品紀錄檔\"</param>
        /// <param name="strPath">例如根目錄"D:\"</param>
        /// <returns></returns>
        public static ArrayList getFiles(string baseDir, string strPath)
        {
            ArrayList arrlist = new ArrayList();
            try
            {
                if (Directory.Exists(strPath))
                {
                    string[] files = Directory.GetFiles(strPath + baseDir); //目錄存在就取得目錄下的檔案清單
                    foreach (var item in files)
                    {
                        arrlist.Add(item);
                    }
                }
            }
            catch (Exception)
            {
                WriteLog("根目錄或者baseDir目錄：可能不存在或是名稱錯誤。");
                Environment.Exit(Environment.ExitCode);
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

        private void btnSource_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            path.ShowDialog();
            this.txtSource.Text = path.SelectedPath;
        }

        public static bool Upload(string fileName, string uploadUrl, string UserName, string Password)
        {
            Stream requestStream = null;
            FileStream fileStream = null;
            FtpWebResponse uploadResponse = null;
            bool isUpOk = false;
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
                isUpOk = true;
            }
            catch (Exception)
            {
                throw;
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
            return isUpOk;
        }

        public void CreateFTP_Forder(string sFullPath, string sUserName, string sPassword)
        {
            try
            {
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(sFullPath);
                ftpRequest.Proxy = null;
                ftpRequest.Credentials = new NetworkCredential(sUserName, sPassword);
                ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                FtpWebResponse CreateForderResponse = (FtpWebResponse)ftpRequest.GetResponse();
                CreateForderResponse.Close();
            }
            catch (Exception)
            {
                //建立目錄若出現例外不做處理
            }
        }

        //設定值存檔
        private void btnSet_Click(object sender, EventArgs e)
        {
            FtpAutoUpload.Properties.Settings.Default.nasIP = txtNasIP.Text;
            FtpAutoUpload.Properties.Settings.Default.area = txtArea.Text;
            FtpAutoUpload.Properties.Settings.Default.source = txtSource.Text;
            FtpAutoUpload.Properties.Settings.Default.ID = txtID.Text;
            FtpAutoUpload.Properties.Settings.Default.PW = txtPW.Text;
            FtpAutoUpload.Properties.Settings.Default.Save();
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

        //記錄檔格式
        private static void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
            w.WriteLine("  :");
            w.WriteLine("  :{0}", logMessage);
            w.WriteLine("-------------------------------");
        }
    }

    //         lstPathInfo.Items.Add("路徑完整名稱 : " + Path.GetFullPath(strPath));
    //         lstPathInfo.Items.Add("路徑根目錄 : " + Path.GetPathRoot(strPath));
    //         lstPathInfo.Items.Add("路徑目錄資訊 : " + Path.GetDirectoryName(strPath));
    //         lstPathInfo.Items.Add("路徑檔案完整名稱 : " + Path.GetFileName(strPath));
    //         lstPathInfo.Items.Add("路徑檔案名稱 : " + Path.GetFileNameWithoutExtension(strPath));
    //         lstPathInfo.Items.Add("路徑檔案副檔名 : " + Path.GetExtension(strPath));
}