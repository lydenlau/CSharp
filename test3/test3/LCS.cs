using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace test3
{
    public enum ITEM_MODE { XY, X, Y }
    
    public class Item<T>
    {
        ITEM_MODE Mode;
        T Value;
        public Item(ITEM_MODE rMODE, T item)
        {
            Mode = rMODE;
            Value = item;
        }
        public override string ToString()
        {
            
            string mode;
            if (Mode == ITEM_MODE.XY)
                mode = "  ";
            else if (Mode == ITEM_MODE.X)
                mode = "- ";

            else
            {
                mode = "+ "; 
            }
                
            return String.Format("{0}{1}", mode, Value);
        }
    }

    /// <summary>
    /// LCS类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LCS<T>
    {
        private T[] x;
        private T[] y;
        private Item<T>[] items;
        private T[] itemscommon;

        /// <summary>
        /// 第1个数组
        /// </summary>
        public T[] X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
                GenerateLCSItems();
            }
        }
        /// <summary>
        /// 第2个数组
        /// </summary>
        public T[] Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
                GenerateLCSItems();
            }
        }

        /// <summary>
        /// 比较后的结果数组，是两个集合的比较结果的全集
        /// </summary>
        public Item<T>[] Items { get { return items; } }

        /// <summary>
        /// 比较后的结果数组，是两个集合的最长公共子序列（LCS）
        /// </summary>
        public T[] ItemsCommon { get { return itemscommon; } }
        public LCS(T[] x, T[] y)
        {
            this.x = x;
            this.y = y;
            GenerateLCSItems();
        }

        /*
        算法
        LCS（Longest Common Subsequence），即：最长公共子序列，它是求两个字符串最长公共子序列的问题。
        https://blog.csdn.net/rrrfff/article/details/7523437
        */
        private void GenerateLCSItems()
        {
            //初始化二维数组，数组中的值全为0
            int[,] c = new int[X.Length + 1, Y.Length + 1];

            //循环第i行，从1开始
            for (int i = 1; i < X.Length + 1; i++)
            {
                //循环第j列，从1开始
                for (int j = 1; j < Y.Length + 1; j++)
                {
                    if (X[i - 1].Equals(Y[j - 1]))
                        c[i, j] = c[i - 1, j - 1] + 1;
                    //先上边，后左边，取上边和左边两个数字的最大值，这个顺序必须和下面的GetLCS()函数一致！
                    else if (c[i - 1, j] >= c[i, j - 1])
                        c[i, j] = c[i - 1, j];
                    else
                        c[i, j] = c[i, j - 1];
                }
            }

            int LCSLength = c[X.Length, Y.Length];

            itemscommon = new T[LCSLength];

            items = new Item<T>[X.Length + Y.Length - LCSLength];

            GetLCS(Items, itemscommon, c, X, Y, X.Length, Y.Length);

        }

        /// <summary>
        /// 递归获取LCS字符串
        /// </summary>
        /// <param name="rArray">输出参数</param>
        /// <param name="outLCS"></param>
        /// <param name="c">输入：c是二维表</param>
        /// <param name="x">输入：是原始字符串x</param>
        /// <param name="y">输入：是原始字符串y</param>
        /// <param name="i">输入：左下角的行坐标</param>
        /// <param name="j">输入：左下角的列坐标</param>
        private void GetLCS(Item<T>[] rArray, T[] outLCS, int[,] c, T[] x, T[] y, int i, int j)
        {
            if (i == 0 && j > 0)
            {//只剩下y[]
                while (j > 0)
                {
                    Item<T> r = new Item<T>(ITEM_MODE.Y, y[j - 1]);
                    InsertBefore(rArray, r);
                    j--;
                }
                return;
            }
            else
            if (i > 0 && j == 0)
            {//只剩下x[]
                while (i > 0)
                {
                    Item<T> r = new Item<T>(ITEM_MODE.X, x[i - 1]);
                    InsertBefore(rArray, r);
                    i--;
                }
                return;
            }
            else if (i == 0 && j == 0)
            {
                return;
            }
            if (x[i - 1].Equals(y[j - 1]))
            {
                Item<T> r = new Item<T>(ITEM_MODE.XY, x[i - 1]);
                InsertBefore(rArray, r);
                outLCS[c[i, j] - 1] = x[i - 1];

                GetLCS(rArray, outLCS, c, x, y, i - 1, j - 1);
            }
            //先上边，后左边回溯，必须与GetLCSResult()一致
            else if (c[i - 1, j] >= c[i, j - 1])
            {
                Item<T> r = new Item<T>(ITEM_MODE.X, x[i - 1]);
                InsertBefore(rArray, r);
                GetLCS(rArray, outLCS, c, x, y, i - 1, j);
            }
            else
            {
                Item<T> r = new Item<T>(ITEM_MODE.Y, y[j - 1]);
                InsertBefore(rArray, r);
                GetLCS(rArray, outLCS, c, x, y, i, j - 1);
            }
        }

        /// <summary>
        /// 从后往前插入，将r添加到rArray最后一个不为null的位置中。
        /// </summary>
        /// <param name="rArray"></param>
        /// <param name="r"></param>
        private void InsertBefore(Item<T>[] rArray, Item<T> r)
        {
            int i = 0;
            for (i = 0; i < rArray.Length; i++)
            {
                if (rArray[i] != null)
                    break;
            }
            rArray[i - 1] = r;
        }
        public override string ToString()
        {
            //使用数组接收不一样的元素
            int i = 0;
            //存放"+"
            List<string> scp = new List<string>();
            //存放"-"
            List<string> scm = new List<string>();
            //存放新的不一样的元素
            List<string> disn = new List<string>();
            List<string> disp = new List<string>();

            StringBuilder stringBuilder = new StringBuilder();
            foreach (Item<T> item in Items)
            {
                //stringBuilder.Append(item).AppendLine();
                if(item.ToString().Contains("- "))
                {
                    scm.Add(item.ToString()); //把所有的“-”的放在一起
                    //遍历
                    //通过循环将所有相同的字符串精简的提取出来，如“ABABAB”提取为“AB”
                    i = 1;
                    for(int j = 0; j < scm.Count; j++)
                    {
                        if(item.ToString() != scm[j])
                        {
                            i++;
                        }
                        
                    }
                    if(i == scm.Count)
                    {
                        disn.Add(item.ToString());
                    }
                    //遍历
                }
                else if(item.ToString().Contains("+ "))
                {
                    scp.Add(item.ToString());  //把所有的“+”的放在一起
                    //遍历
                    //通过循环将所有相同的字符串精简的提取出来，如“ABABAB”提取为“AB”
                    i = 1;
                    for (int j = 0; j < scp.Count; j++)
                    {
                        if (item.ToString() != scp[j])
                        {
                            i++;
                        }

                    }
                    if (i == scp.Count)
                    {
                        disp.Add(item.ToString());
                    }
                    //遍历
                }
            }
           
            //更改的部分转化为字符串
            string strdisn = string.Join("", disn.ToArray());
            string strscm = string.Join("", scm.ToArray());
            string strdisp = string.Join("", disp.ToArray());
            //Console.WriteLine("disn={0}",g);
            //Console.WriteLine("scm={0}", gscm);
            //Console.WriteLine("scp={0}", strdisp);
            //得到分数
            //调用Match进行匹配得分
            Match mach = new Match();
            char[] inputarr = strscm.ToCharArray();
            char[] pa = strdisn.ToCharArray();
            int idx = 0;
            int Count = 0;
            bool found = false;
            while (idx < inputarr.Length)
            {
                idx = mach.Getnum(idx, inputarr, pa, ref found);
                if (found)
                    Count++;
                else
                    break;
            }
            //Console.WriteLine("最终得分是：{0}", Count);
            //输出
            //string output1 = $"替换题：请将文中所有的文字{strdisn}替换为{strdisp}。总分：{Count}分";
            //string output3 = $"替换题：请删除文中所有的文字{strdisn}分：{Count}分";
            //去掉“- +”
            Change CH = new Change();
            string chdisn = CH.changeArr(disn);
            string chdisp = CH.changeArr(disp);


            string output1 = $"替换题：请将文中所有的文字\"{chdisn}\"替换为\"{chdisp}\"。总分：{Count}分";
            string output3 = $"替换题：请删除文中所有的文字\"{chdisn}\"。总分：{Count}分";
            if ((scp.Count == 0) && (scm.Count == 0))
            {
                return "没有替换题";
            }
            else if((scp.Count == 0)&&(scm.Count != 0))
            {
                return output3;
            }
            else
            {
                return output1;
            }
            
            
        }

        public void Demo()
        {
            //Console.WriteLine($"类型{typeof(T)}演示：\n=========================================================");

            //输出LCS结果：
            //Console.WriteLine("\nLCS结果:");

            //调用this.ToString()
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine(this);
            
            
        }
    }
}
