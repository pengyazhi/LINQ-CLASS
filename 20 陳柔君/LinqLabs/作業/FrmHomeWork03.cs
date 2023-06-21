using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs.HomeWork
{
    public partial class FrmHomeWork03 : Form
    {

        List<Student> students = new List<Student>()
            {
                                            new Student{ Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
                                            new Student{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
                                            new Student{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
                                            new Student{ Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
                                            new Student{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
                                            new Student{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },
             };

        public FrmHomeWork03()
        {
            InitializeComponent();

            RandomStudents();
            cmbBoxSubject.SelectedIndex = 0;
        }

        void RandomStudents()
        {
            for (int i = 0; i < 100; i++)
            {
                Random random = new Random();
                Student stu = new Student
                {
                    Name = (i+1).ToString(),
                    Class = random.Next(0,100) > 50 ? "CS_101" : "CS_102",
                    Chi = random.Next(0,100),
                    Eng = random.Next(0,100),
                    Math = random.Next(0,100),
                    Gender = random.Next(0,100) >50 ? "Male" : "Female",
                };

                students.Add(stu);
                Thread.Sleep(1);
            }
        }

        private void btnRandomScores_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = students;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
        }

        private void btnSearchScores_Click(object sender, EventArgs e)
        {
            // 共幾個 學員成績 ?
            // 找出 前面三個 的學員所有科目成績
            this.lblDetails.Text = $"前{txtTop.Text}位學生成績，共{students.Count()}筆學員成績";
            dataGridView2.DataSource = students.Take(int.Parse(txtTop.Text)).ToList();
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            this.lblDetails.Text = $"最後{txtLast.Text}位學生成績，共{students.Count()}筆學員成績";
            dataGridView2.DataSource = students.Skip(students.Count - int.Parse(txtLast.Text)).ToList();
        }
        private void btnABC_Click(object sender, EventArgs e)
        {
            this.lblDetails.Text = "aaa,bbb,ccc的成績";
            // 找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績
            // NOTE 匿名型別
            var q = from n in students
                    where n.Name == "aaa" || n.Name == "bbb" || n.Name == "ccc"
                    select new
                    {
                        n.Name,
                        n.Chi,
                        n.Eng
                    };

            dataGridView2.DataSource = q.ToList();
        }
        private void btnSearchName_Click(object sender, EventArgs e)
        {
            this.lblDetails.Text = $"{txtName.Text}的成績";
            // 找出學員 'bbb' 的成績
            dataGridView2.DataSource = students.Where(n => n.Name == txtName.Text).ToList();

        }
        private void btnElse_Click(object sender, EventArgs e)
        {
            this.lblDetails.Text = $"{txtName.Text}以外的學生成績";
            // 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)	!=
            dataGridView2.DataSource = students.Where(n => n.Name != txtName.Text).ToList();
        }
        private void btnFailed_Click(object sender, EventArgs e)
        {
            this.lblDetails.Text = $"{cmbBoxSubject.SelectedItem}不及格的學生";
            //數學不及格...是誰
            if (cmbBoxSubject.SelectedItem.ToString() == "國文")
            {
                dataGridView2.DataSource = students.Where(n => n.Chi < 60).ToList();
                this.lblDetails.Text += $"，共{students.Where(n => n.Chi < 60).Count()}位";
            }
            else if (cmbBoxSubject.SelectedItem.ToString() == "英文")
            {
                dataGridView2.DataSource = students.Where(n => n.Eng < 60).ToList();
                this.lblDetails.Text += $"，共{students.Where(n => n.Eng < 60).Count()}位";
            }
            else
            {
                dataGridView2.DataSource = students.Where(n => n.Math < 60).ToList();
                this.lblDetails.Text += $"，共{students.Where(n => n.Math < 60).Count()}位";
            }

        }
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var q = from s in students
                    where s.Name == dataGridView1.Rows[e.RowIndex].Cells["Name"].Value.ToString()
                    select new
                    {
                        s.Name,
                        Min = (new int[] { s.Math, s.Eng, s.Chi }).Min(),
                        Max = (new int[] { s.Math, s.Eng, s.Chi }).Max(),
                        Avg = (s.Chi + s.Eng + s.Math) / 3,
                        Sum = s.Chi+s.Eng+s.Math,
                        WeightAvg = (s.Chi*50 + s.Eng*25 + s.Math*25) / 100,
                        Pass = ((s.Chi * 50 + s.Eng * 25 + s.Math * 25) / 100)>=60? "及格":"不及格",
                        Grade = Grade((s.Chi * 50 + s.Eng * 25 + s.Math * 25) / 100)
                        //Rank = ++rank,
                    };

            dataGridView2.DataSource = q.ToList();
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;

        }
        string Grade(int score)
        {
            if (score >= 90)
                return "優等";
            else if (score >= 80)
                return "甲等";
            else if (score >= 70)
                return "乙等";
            else if (score >= 60)
                return "丙等";
            else
                return "丁等";
        }

    }

    class Student
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public int Chi { get; set; }
        public int Eng { get; set; }
        public int Math { get; set; }
        public string Gender { get; set; }
    }
}
