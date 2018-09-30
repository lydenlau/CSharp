using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCS
{
    class LCS
    {
        /// <summary>
        /// b[i,j]的作用是记录指示c[i,j]的值是由哪一个子问题的解达到的
        /// c[i,j]的作用是存储Xi与Yj的最长公共子序列的长度
        /// </summary>
        string[,] b;        //定义b数组用于存储字符串
        int[,] c;           //定义的存放矩阵的空间
        char a;

        static void Main(string[] args)   //程序从main函数开始运行
        {
            int[] L1 = { 34, 72, 13, 44, 25, 30, 10 };   //自定义的数组
            int[] L2 = { 34, 13, 44, 7, 25 };
            string[] P = { "我", "的", "名字", "是", "刘", "一", "达" };  //自定义的对象
            string[] Q = { "刘", "一达", "是", "个", "好", "名字" };
            LCS lcs = new LCS();     //实例化一个lcs对象
            lcs.LCS_LENGTH(L1, L2);   //整形情况
            for (int i = 0; i < L1.Length; i++)     //遍历这个矩阵依次打印回溯左上角的元素
            {
                for (int j = 0; j < L2.Length; j++)
                {                  
                    if (lcs.b[i, j] == "left_up")
                    {
                        Console.WriteLine("{0}", L1[i]);
                    }                   
                }
            }
            lcs.LCS_LENGTH1(P, Q);    //字符串情况
            for (int i = 0; i < P.Length; i++)
            {
                for (int j = 0; j < Q.Length; j++)
                {                    
                    if (lcs.b[i, j] == "left_up")
                    {
                        Console.WriteLine("{0}", P[i]);
                    }

                }

            }
            Console.ReadLine();
        }

        public void LCS_LENGTH(int[] X, int[] Y)
        {
            b = new string[X.Length, Y.Length];         //存放左/上/左上
            c = new int[X.Length + 1, Y.Length + 1];    //位于左上右下角元素用于回溯，从而找出公共数值
            for (int i = 0; i <= X.Length; i++)
            {
                c[i, 0] = 0;//j=0,c[i,j]=0;表示最长公共子序列的长度为0
            }
            for (int j = 0; j <= Y.Length; j++)
            {
                c[0, j] = 0;//i=0,c[i,j]=0;表示最长公共子序列的长度为0
            }
            for (int i = 0; i < X.Length; i++)
            {
                for (int j = 0; j < Y.Length; j++)
                {
                    if (X[i] == Y[j])
                    {
                        a = ' ';
                        c[i + 1, j + 1] = c[i, j] + 1;
                        b[i, j] = "left_up";  //表示向左上回溯
                    }
                    else if (c[i, j + 1] >= c[i + 1, j])
                    {
                        a = '+';
                        c[i + 1, j + 1] = c[i, j + 1];
                        b[i, j] = "up";  //表示向上回溯
                    }
                    else
                    {
                        a = '+';
                        c[i + 1, j + 1] = c[i + 1, j];
                        b[i, j] = "left";  //表示向左回溯
                    }

                }
            }
        }
        public void LCS_LENGTH1(string[] P, string[] Q)
        {
            b = new string[P.Length, Q.Length];         //存放左/上/左上
            c = new int[P.Length + 1, Q.Length + 1];    //位于左上右下角元素用于回溯，从而找出公共数值
            for (int i = 0; i <= P.Length; i++)
            {
                c[i, 0] = 0;//j=0,c[i,j]=0;表示最长公共子序列的长度为0
            }
            for (int j = 0; j <= Q.Length; j++)
            {
                c[0, j] = 0;//i=0,c[i,j]=0;表示最长公共子序列的长度为0
            }
            for (int i = 0; i < P.Length; i++)
            {
                for (int j = 0; j < Q.Length; j++)
                {
                    if (P[i] == Q[j])
                    {
                        c[i + 1, j + 1] = c[i, j] + 1;
                        b[i, j] = "left_up";  //表示向左上回溯
                    }
                    else if (c[i, j + 1] >= c[i + 1, j])
                    {
                        c[i + 1, j + 1] = c[i, j + 1];
                        b[i, j] = "up";  //表示向上回溯
                    }
                    else
                    {
                        c[i + 1, j + 1] = c[i + 1, j];
                        b[i, j] = "left";  //表示向左回溯
                    }

                }
            }
        }
        public void LCSW(char a, string[,] b, int[] X, int i, int j)  //根据b的内容打印出X,Y序列最长公共子序列
        {
            if (i == 0 || j == 0)
            {
                return;
            }
            if (b[i, j] == "left_up")
            {
                LCSW(a, b, X, i - 1, j - 1);
                Console.WriteLine("{0}", X[i]);
            }
            else if (b[i, j] == "up")
            {
                LCSW(a, b, X, i - 1, j);
            }
            else
            {
                LCSW(a, b, X, i, j - 1);
            }
        }

    }
}



