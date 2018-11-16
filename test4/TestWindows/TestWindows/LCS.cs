using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWindows
{
    public enum ITEM_MODE { XY, X, Y }
    public class Item<T>
    {
        public ITEM_MODE Mode;
        public T Value;
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
                mode = "+ ";
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
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Item<T> item in Items)
            {
                stringBuilder.Append(item).AppendLine();
            }
            return stringBuilder.ToString();
        }

        public void Demo()
        {
            Console.WriteLine($"类型{typeof(T)}演示：\n=========================================================");

            Console.WriteLine("list1:");
            foreach (T i in x)
            {
                Console.Write(string.Format("{0}  ", i));
            }
            Console.WriteLine();

            Console.WriteLine("list2:");
            foreach (T i in y)
            {
                Console.Write($"{i}  ");
            }
            Console.WriteLine();

            //输出LCS结果：
            Console.WriteLine("\nLCS结果:");

            //调用this.ToString()
            Console.WriteLine(this);
        }
    }
}
