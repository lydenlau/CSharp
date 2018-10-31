using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test3
{
    class Change
    {
        public char[] changeCh(string[] str, int length)
        {
            char[] chArr = new char[length];
            //chArr = null;
            for (int i = 0; i < str.Length; i++)
            {
                char[] ch1 = str[i].ToCharArray();
                chArr = chArr.Concat(ch1).ToArray();
            }
            return chArr;
        }
        public string[] changeStr(char[] ch)
        {
            string[] str = new string[ch.Length];
            for(int i = 0; i < ch.Length; i++)
            {
                str[i] = ch[i].ToString();
            }
            return str;
        }
        public string changeArr(List<string> list)
        {
            string[] str = list.ToArray();
            List<string> lch = new List<string>();
            for(int i = 0; i < str.Length; i++)
            {
                string sj = str[i];
                for(int j= 0; j < sj.Length; j++)
                {
                    if((sj[j] != '-') && (sj[j] != ' ')&&(sj[j] != '+'))
                    {
                        lch.Add(sj[j].ToString());
                    }
                }
            }
            string res = string.Join("", lch.ToArray());
            return res;
        }
    }
}
