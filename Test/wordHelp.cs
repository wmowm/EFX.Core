using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    public class wordHelp
    {
        public static bool WordToHtml(string wordFileName, string htmlFileName)
        {
            try
            {
                Object oMissing = System.Reflection.Missing.Value;
                Microsoft.Office.Interop.Word._Application WordApp = new Microsoft.Office.Interop.Word.Application();
                WordApp.Visible = false;
                object filename = wordFileName;
                _Document WordDoc = WordApp.Documents.Open(ref filename, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                // Type wordType = WordApp.GetType();
                // 打开文件
                Type docsType = WordApp.Documents.GetType();
                // 转换格式，另存为
                Type docType = WordDoc.GetType();
                object saveFileName = htmlFileName;
                docType.InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod, null, WordDoc,
                    new object[] { saveFileName, WdSaveFormat.wdFormatHTML });

                //保存
                WordDoc.Save();
                WordDoc.Close(ref oMissing, ref oMissing, ref oMissing);
                WordApp.Quit(ref oMissing, ref oMissing, ref oMissing);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
    }
}
