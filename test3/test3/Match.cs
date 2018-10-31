using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test3
{
    class Match
    {
        //从str的第idx个字符开始找subStr在str出现的位置，返回下一个查找的起始位置，如果找到，found为true，否则为false
        //比如：GetNextString(0, "AABC1AB2", "AB", ref bool found)，返回值是3,found=true 找到第1个匹配
        //比如：GetNextString(3, "AABC1AB2", "AB", ref bool found)，返回值是7,found=true 找到第2个匹配
        //比如：GetNextString(7, "AABC1AB2", "AB", ref bool found)，返回值是8,found=false 未找到
        public int Getnum(int idx, char[] str, char[] subStr, ref bool found)
        {
            int idx_org = idx;
            int i;
            while (idx < str.Length)
            {
                //找到第一个字符的位置
                while (idx < str.Length)
                {
                    if (str[idx++] == subStr[0])
                        break;
                }
                //如果第一个字符都不匹配，或者如果strAll中剩余的字符不足，返回false
                if (idx == str.Length || subStr.Length - 1 > str.Length - idx)
                    break;

                //找到第一个字符之后，以后的每个字符都必须相同，才是完全匹配
                for (i = 1; i < subStr.Length; i++, idx++)
                {
                    if (subStr[i] != str[idx])
                    {//如果不匹配
                        idx_org++;
                        idx = idx_org;
                        break;
                    }
                }
                //如果找到了整个匹配
                if (i == subStr.Length)
                {
                    found = true;
                    return idx;
                }
            }
            found = false;
            return str.Length;
        }
    }
}
