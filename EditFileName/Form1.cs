using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.IO;

namespace EditFileName
{
    public partial class form1 : Form
    {
        public form1()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var path = this.rootDir.Text;
            if(!Directory.Exists(path))
            {
                //文件夹不存在
                MessageBox.Show("该路径不存在!");
                return;
            }
            var DtoPath = path + this.txtDto1.Text;
            var ServicePath = path + this.txtService1.Text;
            var IServicePath = path + this.txtIService1.Text;
            var DaoPath = path + this.txtDao1.Text;
            var IDaoPath = path + this.txtIDao1.Text;
            var msg = "";
            //领域层
            if (Directory.Exists(DtoPath))
            {
                EditFile(DtoPath, txtDto.Text);
            }
            if (Directory.Exists(ServicePath))
            {
                EditFile(ServicePath, txtService.Text);
            }

            if (Directory.Exists(IServicePath))
            {
                EditFile(IServicePath, txtIService.Text);
            }
            if (Directory.Exists(DaoPath))
            {
                EditFile(DaoPath, txtDao.Text);
            }
            if (Directory.Exists(IDaoPath))
            {
                EditFile(IDaoPath, txtIDao.Text);
            }
            MessageBox.Show("完成!");
        }

        /// <summary>
        /// 替换字符串
        /// </summary>
        /// <param name="oldName">旧字符串</param>
        /// <param name="str">匹配字符串</param>
        /// <returns></returns>
        private string ReplaceStr(string oldName,string str)
        {
            string [] strArray = oldName.Split(new char[] { '.' });
            var str1 = strArray[0];
            var str2 = strArray[1];
            str = str.Replace("@", str1);
            return str + "." + str2;
        }

        private void EditFile(string DirPath,string str)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(DirPath);
                FileInfo[] fiArray = di.GetFiles();
                for (int i = 0; i < fiArray.Length; i++)
                {
                    string oldFileName = fiArray[i].ToString();//旧文件名
                    string oldFileFullName = di.ToString() + "\\" + oldFileName;//旧文件路径
                    FileInfo file = new FileInfo(oldFileFullName);
                    if (file.Exists)
                    {
                        //新文件
                        string newFileFullName = di.ToString() + "\\" + ReplaceStr(oldFileName, str);
                        File.Move(oldFileFullName, newFileFullName);
                    }
                }
            }
            catch (Exception ex)
            {

            }
           
        }
    }
}
