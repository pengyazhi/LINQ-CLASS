using LinqLabs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_2 : Form
    {

        public Frm作業_2()
        {
            InitializeComponent();
            FillData();
            LoadSelectedYear();
            //LoadSelectedYearQuarter();
        }
        int selectYear;
        private void LoadSelectedYearQuarter()
        {
            comboBox2.Items.Clear();
            selectYear = (int)comboBox3.SelectedItem;
            if (selectYear == null)
            {
                return;
            }
            //selectYear = Convert.ToInt32(comboBox3.Text);
            IEnumerable<int> month = this.awDataSet1.ProductPhoto.Where(n => n.ModifiedDate.Year == selectYear).Select(n => n.ModifiedDate.Month);
            List<int> months = month.Distinct().ToList();
            foreach (int m in months)
            {
                if(m == 1 || m ==2 ||  m == 3)
                {
                    comboBox2.Items.Add("第一季");
                }
                if (m == 4 || m == 5 || m == 6)
                {
                    comboBox2.Items.Add("第二季");
                }
                if (m == 7 || m == 8 || m == 9)
                {
                    comboBox2.Items.Add("第三季");
                }
                if (m == 10 || m == 11 || m == 12)
                {
                    comboBox2.Items.Add("第四季");
                }
            }
        }

        private void LoadSelectedYear()
        {
            //IEnumerable<int> year = from y in this.awDataSet1.ProductPhoto
            //           select y.ModifiedDate.Year;

            IEnumerable<int> year = this.awDataSet1.ProductPhoto.Select(n => n.ModifiedDate.Year);
            
            List<int>yearList = year.Distinct().ToList();
           
            foreach(int distinctYear in  yearList)
            {
                comboBox3.Items.Add(distinctYear);
            }
           
        }

        private void FillData()
        {
            this.productTableAdapter1.Fill(this.awDataSet1.Product);
            this.productPhotoTableAdapter1.Fill(this.awDataSet1.ProductPhoto);
            this.productProductPhotoTableAdapter1.Fill(this.awDataSet1.ProductProductPhoto);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.awDataSet1.ProductPhoto;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                MessageBox.Show("起始時間不能大於結束時間,請重新選擇");
                dateTimePicker1.Value = dateTimePicker2.Value;
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                MessageBox.Show("結束時間不能小於開始時間,請重新選擇");
                dateTimePicker2.Value = dateTimePicker1.Value;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = this.awDataSet1.ProductPhoto;
            //IEnumerable<AWDataSet.ProductPhotoRow> q  = from d in this.awDataSet1.ProductPhoto
            //                                       where d.ModifiedDate > dateTimePicker1.Value && d.ModifiedDate < dateTimePicker2.Value
            //                                        select d;

            IEnumerable<AWDataSet.ProductPhotoRow> q = this.awDataSet1.ProductPhoto.Where(d =>d.ModifiedDate > dateTimePicker1.Value && d.ModifiedDate < dateTimePicker2.Value);
            dataGridView1.DataSource = q.ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            selectYear = Convert.ToInt32(comboBox3.Text);
            var q = this.awDataSet1.ProductPhoto.Where(n => n.ModifiedDate.Year == selectYear);
            dataGridView1.DataSource = q.ToList();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            LoadSelectedYearQuarter();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var q =this.awDataSet1.ProductPhoto.Where(y=>y.ModifiedDate.Year == Convert.ToInt32(comboBox3.Text)) && y.ModifiedDate.Year == Convert.ToInt32(comboBox2.Text))


        }
    }
}
