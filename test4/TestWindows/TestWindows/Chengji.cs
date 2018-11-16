using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWindows
{
    class Chengji
    {
        public string title = "";
        public int grade = 0;
        /// <summary>
        /// 从index开始在strLCS中往后搜索替换字符串
        /// </summary>
        /// <param name="index">开始搜索的位置</param>
        /// <param name="strLCS">LCS串</param>
        /// <param name="strBefore">返回：原字符串，如果未找到替换，strBefore为空</param>
        /// <param name="strAfter">返回：替换目标字符串</param>
        /// <returns>返回最后的Item的下一个Item的位置</returns>
        static int GetNextReplace(int index, LCS<char> strLCS, ref string strBefore, ref string strAfter)
        {
            //ITEM_MODE.X表示源文件中的原始文字，这里是原题，ITEM_MODE.Y表示目标文件中的替换后的新文字，这里是答案。
            //本实验只有两种情况
            //情况1，全文替换文字：先出现ITEM_MODE.Y后出现ITEM_MODE.X
            //情况2，全文删除文字：直接出现ITEM_MODE.X
            //出现ITEM_MODE.Y后没有出现ITEM_MODE.X表示增加文字，不是本实验研究范围。
            strBefore = null;
            strAfter = null;
            int i;
            for (i = index; i < strLCS.Items.Length; i++)
            {
                Item<char> item = strLCS.Items[i];
                if (item.Mode == ITEM_MODE.Y)
                {
                    
                    //如果遇到下一组替换，本次替换结束
                    if (strBefore != null)
                        break;
                    strAfter += item.Value;
                }
                else if (item.Mode == ITEM_MODE.X)
                {
                    
                    strBefore += item.Value;
                }
                else
                {
                    if (strBefore != null)
                        break;
                    else if (strAfter != null)//如果只是增加，不认为是替换，继续往后找
                        strAfter = null;
                }
            }
            return i;
        }
        public Chengji(string url1,string url2)
        {
            //替换前的字符串
            string strBefore = null;
            //替换后的字符串
            string strAfter = null;

            //替换出现的次数
            int Count = 0;

            EWordDocument eWordDocument原题 = new EWordDocument();
            eWordDocument原题.Open(url1);

            EWordDocument eWordDocument答案 = new EWordDocument();
            eWordDocument答案.Open(url2);
            int idx;
            for (int i = 0; i < eWordDocument原题.LText.Count(); i++)
            {
                char[] arrayX = eWordDocument原题.LText[i].ToArray();
                char[] arrayY = eWordDocument答案.LText[i].ToArray();
                LCS<char> strLCS = new LCS<char>(arrayX, arrayY);
                idx = 0;
                string strBefore_ = null;
                string strAfter_ = null;
                while (idx < strLCS.Items.Length)
                {
                    //如果未找到替换，strBefore为空
                    if (strBefore == null)
                    {
                        idx = GetNextReplace(idx, strLCS, ref strBefore, ref strAfter);
                        if (strBefore != null)
                            Count++;
                    }
                    else
                    {
                        idx = GetNextReplace(idx, strLCS, ref strBefore_, ref strAfter_);
                        if (strBefore == strBefore_ && strAfter == strAfter_)
                            Count++;
                    }
                }
            }
            if (strBefore != null)
            {
                grade = Count;
                if (strAfter != null)
                {
                    
                    title = "替换题：请将文中所有的文字" + strBefore + "替换为" + strAfter + "。总分：" + Count + "分";
                }
                //Console.WriteLine("替换题：请将文中所有的文字“{0}”替换为“{1}”。总分：{2}分", strBefore, strAfter, Count);
                else
                    title = "替换题：请删除文中所有的文字" + strBefore + "总分：" + Count + "分";
                //Console.WriteLine("替换题：请删除文中所有的文字“{0}”。总分：{1}分", strBefore, Count);
            }
            else
                title = "没有替换题！";
            //Console.WriteLine("没有替换题！");
        }
    }
}
