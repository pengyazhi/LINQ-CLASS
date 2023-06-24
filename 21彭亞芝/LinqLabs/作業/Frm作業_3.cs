using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs.作業
{
    public partial class Frm作業_3 : Form
    {
        List<Student> students_scores; 
        int totalMember;
        public Frm作業_3()
        {
            InitializeComponent();

           students_scores = new List<Student>()
                                         {
                                            new Student{ Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
                                            new Student{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
                                            new Student{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
                                            new Student{ Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
                                            new Student{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
                                            new Student{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },
                                          };
            totalMember = students_scores.Count();
        }
       
        private void button36_Click(object sender, EventArgs e)
        {
            // 共幾個 學員成績 ?						
            lblMaster.Text = "Master";
            IEnumerable<Student> q = from n in students_scores
                    select n;
            dataGridView1.DataSource = q.ToList();
            lblMaster.Text += $"          TotalMember : {totalMember}";
        }
        int rank = 0;
        private void button37_Click(object sender, EventArgs e)
        {
            rank = 0;
            this.lblMaster.Text = "Rank";
            this.lblDetails.Text = "";
            //統計 每個學生個人成績
            var q = students_scores.OrderByDescending(n => n.Chi * 2 + n.Eng + n.Math).
                                                          Select(n => new
                                                          {
                                                              n.Name,
                                                              n.Gender,
                                                              n.Class,
                                                              n.Math,
                                                              n.Chi,
                                                              n.Eng,
                                                              Max = (new[] { n.Math, n.Chi, n.Eng }).Max(),
                                                              Min = (new[] { n.Math, n.Chi, n.Eng }).Min(),
                                                              Total = (new[] { n.Math, n.Chi*2, n.Eng }).Sum(),
                                                              Averge = (new[] { n.Math, n.Chi, n.Chi, n.Eng }).Average(),
                                                              Grade = Grade((int)new[] { n.Math, n.Chi, n.Chi, n.Eng }.Average()),
                                                              Pass = ((n.Math + n.Chi * 2 + n.Eng) / 4) > 60 ? "Pass" : "",
                                                              Rank = ++rank
                                                          });
            dataGridView1.DataSource = q.ToList();

        }

        private string Grade(int avg)
        {
            if (avg > 90) return "A";
            else if (avg > 80) return "B";
            else if (avg > 70) return "C";
            else if (avg > 60) return "D";
            else return "E";
        }

        int nameID = 0;
        private void button33_Click(object sender, EventArgs e)
        {
            lblMaster.Text = "Master";
            //隨機一百個學生資料
            List<Student> rans = new List<Student>();
            Random random= new Random();
            for (int i = 0; i < 100; i++)
            {
                int ranChi = random.Next(0, 100);
                int ranEng = random.Next(0, 100);
                int ranMath = random.Next(0, 100);
                int ranClass =  random.Next(1, 5);
                int ranGender = random.Next(1, 2);
                Student student = new Student
                {
                    Name = $"Student{(i + 1)}",
                    Class = $"CS_{ranClass}",
                    Chi = ranChi,
                    Eng = ranEng,
                    Math= ranMath,
                    Gender = ranGender%2==0?"Male":"Female"
                };
                rans.Add(student);
                
            }
            //統計一百個學生資料
            rank = 0;
            var q = rans.OrderByDescending(n => n.Eng + n.Chi*2 + n.Math).
                Select(n => new
                {
                    n.Name,
                    n.Gender,
                    n.Class,
                    n.Math,
                    n.Chi,
                    n.Eng,
                    Max = (new[] { n.Math, n.Chi, n.Eng }).Max(),
                    Min = (new[] { n.Math, n.Chi, n.Eng }).Min(),
                    Total = (new[] { n.Math, n.Chi * 2, n.Eng }).Sum(),
                    Averge = (new[] { n.Math, n.Chi, n.Chi, n.Eng }).Average(),
                    Grade = Grade((int)new[] { n.Math, n.Chi, n.Chi, n.Eng }.Average()),
                    Pass = ((n.Math + n.Chi * 2 + n.Eng) / 4) > 60 ? "Pass" : "",
                    Rank = ++rank
                
                }
                );
            dataGridView1.DataSource = q.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lblMaster.Text = "Master";
            // 找出 前面三個 的學員所有科目成績		
            IEnumerable<Student> t = students_scores.Take(3);
            dataGridView1.DataSource = t.ToList();
            lblMaster.Text += $"          前三個學員所有科目成績";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lblMaster.Text = "Master";
            // 找出 後面兩個 的學員所有科目成績			
            IEnumerable<Student> l = students_scores.Skip(totalMember - 2);
            dataGridView1.DataSource = l.ToList();
            lblMaster.Text += $"          後兩個學員所有科目成績";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lblMaster.Text = "Master";
            // NOTE 匿名型別
            // 找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績		
            var q = students_scores.Where(n => n.Name == "aaa" || n.Name == "bbb" || n.Name == "ccc").Select(n => new { n.Name,n.Chi, n.Eng });
            dataGridView1.DataSource = q.ToList();
            lblMaster.Text += $"          'aaa','bbb','ccc' 的學員國文英文科目成績";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            lblMaster.Text = "Master";
            // 找出學員 'bbb' 的成績	     
            IEnumerable<Student> q = students_scores.Where(n => n.Name == "bbb");
            dataGridView1.DataSource = q.ToList();
            lblMaster.Text += $"          'bbb'學員的成績";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            lblMaster.Text = "Master";
            // 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)	!=
            IEnumerable<Student> q = students_scores.Where(n => n.Name != "bbb");
            dataGridView1.DataSource= q.ToList();
            lblMaster.Text += $"           除了 'bbb' 學員的學員的所有成績";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            lblMaster.Text = "Master";
            //數學不及格...是誰
            var q = students_scores.Where(n => n.Math < 60).Select(n => new { n.Name });
            dataGridView1.DataSource = q.ToList();
            lblMaster.Text += $"           以下學員的數學不及格";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try 
            {
                lblDetails.Text = "Details";
                chart2.Series.Clear();
                chart2.Series.Add("Scores");
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                    var rc = selectedRow.Cells["Chi"].Value;
                    var re = selectedRow.Cells["Eng"].Value;
                    var rm = selectedRow.Cells["Math"].Value;
                    var a = selectedRow.Cells["Averge"].Value;
                    var t = selectedRow.Cells["Total"].Value;
                    var n = selectedRow.Cells["Name"].Value;

                    chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                    chart2.Series[0].Points.AddXY("Chi", rc);
                    chart2.Series[0].Points.AddXY("Eng", re);
                    chart2.Series[0].Points.AddXY("Math", rm);
                    chart2.Series[0].Points.AddXY("Averge", a);
                    chart2.Series[0].Points.AddXY("Total", t);
                    lblDetails.Text += $"                 {n} 學員的成績圖表";
                }
            }
            catch(Exception ex)
            {
                return;
            }
            
        }
    }
}
public class Student
{
    public string Name { get; set; }
    public string Class { get; set; }
    public double Chi { get; set; }
    public double Eng { get; set; }
    public double Math { get; set; }
    public string Gender { get; set; }

   
}