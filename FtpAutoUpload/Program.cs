using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FtpAutoUpload
{
    internal static class Program
    {
        public static string[] dirArgs; //全域變數

        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            if (args.Length != 0)
            {
            }
            dirArgs = args; //執行程式後面帶入的參數
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}