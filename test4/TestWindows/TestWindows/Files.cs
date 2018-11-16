using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWindows
{
    public class Xuesheng
   
    {
        public string Name;
        public string Number;
        public int Grade;
        public Xuesheng (string name,string number, int grade)
        {
            Name = name;
            Number = number;
            Grade = grade;
        }
    }
    class MyFile
    {
        public FileStream F;
        public MyFile(FileStream F)
        {
            this.F = F;
        }
        public void WriteInt(int i)
        {
            byte[] intBuff = BitConverter.GetBytes(i); // 将 int 转换成字节数组      
            F.Write(intBuff, 0, 4);
        }
        public void WriteString(string str)
        {
            byte[] strArray = System.Text.Encoding.Default.GetBytes(str);
            WriteInt(strArray.Length);
            F.Write(strArray, 0, strArray.Length);
        }
        public int ReadInt()
        {
            byte[] intArray = new byte[4];
            F.Read(intArray, 0, 4);
            int iRead = BitConverter.ToInt32(intArray, 0);
            return iRead;
        }
        public string ReadString()
        {
            int len = ReadInt();
            byte[] strArray = new byte[len];
            F.Read(strArray, 0, len);
            string strRead = System.Text.Encoding.Default.GetString(strArray);
            return strRead;
        }
    }
    class Files
    {
        public Files(List<Xuesheng> students)
        {
            FileStream F = new FileStream("C:\\Use\\github\\CSharp\\test4\\TestWindows\\result.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);

            MyFile MyFile = new MyFile(F);
            string strWrite = "";
            //考号、学生姓名、分数
            //Student student = new Student(name,number,grade);
            //strWrite = JsonConvert.DeserializeObject(students);
            foreach (Xuesheng student in students)
            {
                //stuStrs.Add(student);
                strWrite += "name:" + student.Name + ",number:" + student.Number + ",grade:" + student.Grade + ";";
            }
            MyFile.WriteString(strWrite);
            
            F.Position = 0;
            string strRead = MyFile.ReadString();
            F.Close();
        }
       
    }
    class ReadGradeFile
    {
        public string[] grades;
        public ReadGradeFile()
        {
            FileStream F = new FileStream("C:\\Use\\github\\CSharp\\test4\\TestWindows\\result.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);

            MyFile MyFile = new MyFile(F);
            //考号、学生姓名、分数
            //Student student = new Student(name,number,grade);
            F.Position = 0;
            string strRead = MyFile.ReadString();
            //int intRead = MyFile.ReadInt();
            char[] separator = { ';' };
            grades = strRead.Split(separator);

            F.Close();
        }

    }
}
