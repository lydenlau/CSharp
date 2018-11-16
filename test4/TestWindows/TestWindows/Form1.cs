using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestWindows
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //textBox1.Text = "请选择文件夹";
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择Txt所在文件夹";
            dialog.SelectedPath = textBox1.Text;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                   
                    return;
                }
                textBox1.Text = dialog.SelectedPath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Clear();

            //增加题目
            string subject = @"C:\Use\github\CSharp\test4\TestWindows\原题.docx";
            string answer = @"C:\Use\github\CSharp\test4\TestWindows\标准答案.docx";

            Chengji grade = new Chengji(subject,answer);
            label1.Text = grade.title;


            DirectoryInfo directoryInfo = new DirectoryInfo(textBox1.Text);
            FileSystemInfo[] files = directoryInfo.GetFileSystemInfos();
            int index;
            //文件全路径
            string fullName;
            //考试学生文件名
            string fileName;
            //student
            string name = "";
            string number = "";
            string score = "";
            
            List<Xuesheng> students=new List<Xuesheng>();
            foreach (FileSystemInfo file in files)
            {
                //计算每个学生成绩
                fullName = file.FullName;
                Chengji studentGrade = new Chengji(subject, fullName);

                fileName = file.Name;
                char[] separator = { '_' };
                string[] fileNames = fileName.Split(separator);
                name = fileNames[1];
                number = fileNames[0];
                char[] separator1 = { '.' };
                string[] names = name.Split(separator1);
                name = names[0];
                Xuesheng student = new Xuesheng (name,number, studentGrade.grade);
                students.Add(student);
                
            }
            //存储成绩
            Files gradeFile = new Files(students);
            //读取成绩
            ReadGradeFile readGradeFile = new ReadGradeFile();
            string[] grades = readGradeFile.grades;
            foreach(string stuStr in grades)
            {
                //stuStr学生信息
                if (stuStr!="")
                {
                    char[] separator2 = { ',' };
                    string[] stuInfos = stuStr.Split(separator2);
                    foreach(string stuInfo in stuInfos)
                    {
                        char[] separator3 = { ':' };
                        string[] items = stuInfo.Split(separator3);
                        if (items[0] == "name")
                        {
                            name = items[1];
                        }else if (items[0] == "number")
                        {
                            number = items[1];
                        }else if (items[0] == "grade")
                        {
                            score = items[1];
                        }
                    }
                    index = this.dataGridView1.Rows.Add();
                    this.dataGridView1.Rows[index].Cells[0].Value = number;
                    this.dataGridView1.Rows[index].Cells[1].Value = name;
                    this.dataGridView1.Rows[index].Cells[2].Value = score;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text !="")
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(textBox1.Text);
                if (directoryInfo.Exists == false)
                {
                    button1.Enabled = false;
                    ErrorMsg.Text = "目录不存在";
                    ErrorMsg.Visible = true;
                }
                else
                {
                    button1.Enabled = true;
                    ErrorMsg.Visible = false;
                }
            }
            else
            {
                button1.Enabled = false;
                ErrorMsg.Text = "请输入目录";
                ErrorMsg.Visible = true;
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string DocPath = Properties.Settings.Default.DocPath;
            textBox1.Text = DocPath;
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.DocPath = textBox1.Text;
            Properties.Settings.Default.Save();
            

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ErrorMsg_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
