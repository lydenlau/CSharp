using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test3
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadWord rw = new ReadWord(); 
            bool a = true;
            while (a)
            {
                Console.WriteLine("请输入1，2，3：");
                Console.WriteLine("输入1: 对国考_标准答案1.docx进行检测");
                Console.WriteLine("输入2: 对国考_标准答案2.docx进行检测");
                Console.WriteLine("输入3: 对国考_标准答案3.docx进行检测");
                Console.WriteLine("请输入对应数字:");
                string s1 = Console.ReadLine();
                if (s1.Equals("1"))
                {
                    LCS<string> strLCS = new LCS<string>(rw.readList(), rw.readList1());
                    strLCS.Demo();
                }
                else if (s1.Equals("2"))
                {
                    LCS<string> strLCS = new LCS<string>(rw.readList(), rw.readList2());
                    strLCS.Demo();
                }
                else if (s1.Equals("3"))
                {
                    LCS<string> strLCS = new LCS<string>(rw.readList(), rw.readList3());
                    strLCS.Demo();
                }
                else
                {
                    Console.WriteLine("错误!请输入1，2，3其中任意一个", s1);
                    Console.WriteLine("按任意键退出");
                    a = false;

                }

                Console.ReadKey();
            }
            
        }
    }
}
