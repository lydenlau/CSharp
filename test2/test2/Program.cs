using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test2
{
    class Program
    {
        static void Main(string[] args)//刘一达第二次作业
        {
            EWordDocument eWordDocument = new EWordDocument();
            eWordDocument.Open(@"C:\Use\github\CSharp\test2\科研细则.docx");
            List<string> Intlist;
            Intlist = new List<string>();
            Intlist.Add(eWordDocument.Text);

            foreach (string i in Intlist)
            {
                Console.WriteLine(i);
            }

            /* string wordway = @"C:\Use\github\CSharp\test2\科研细则.docx";//设置word文档的地址，并将文件路径赋给变量“wordway”
             using (WordprocessingDocument doc = WordprocessingDocument.Open(wordway, true))//使用WordprocessingDocument类的Open函数打开word文档
             {
                 Body body = doc.MainDocumentPart.Document.Body;//使用MainDocumentPart.Document.Body提取word文档正文部分
             foreach (var text in body.Elements<Paragraph>())//使用foreach循环语句和Elements<Paragraph>遍历正文中的每个段落
             {
                     Console.WriteLine(text.InnerText);//将每个段落进行打印输出
                 }
             }*/

             Console.ReadLine();
          }
        }
    }
