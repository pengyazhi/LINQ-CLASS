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
using static System.Net.WebRequestMethods;

namespace LinqLabs.作業
{
    public partial class Frm作業_1 : Form
    {
        
        public Frm作業_1()
        {
            InitializeComponent();
            FillData();
            SelectOrderYear();

        }

        private void SelectOrderYear()
        {
            foreach (DataRow row in nwDataSet1.Orders)
            {
                DateTime dateTime = (DateTime)row["OrderDate"];
                int year = dateTime.Year;
                if (!comboBox1.Items.Contains(year))
                {
                    comboBox1.Items.Add(year);
                }
            }
        }

        private void FillData()
        {
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
            this.order_DetailsTableAdapter1.Fill(this.nwDataSet1.Order_Details);
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            
        }

        FileInfo[] files;
        System.IO.DirectoryInfo dir;

        void Load_C_Data()
        {
            dir = new System.IO.DirectoryInfo(@"C:\Windows");
            files = dir.GetFiles();
            dataGridView1.DataSource = files;
        }
        private void button14_Click(object sender, EventArgs e)
        {
            Load_C_Data();
            IEnumerable<FileInfo> q = from log in files
                    where log.Extension == ".log"
                    select log;
            dataGridView1.DataSource = q.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Load_C_Data();
            IEnumerable<FileInfo> q = from ct in files
                where ct.CreationTime.Year == 2022
                select ct;
            
            dataGridView1.DataSource = q.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Load_C_Data();
            IEnumerable<FileInfo> q =from large in files
                   where large.Length >10000
                   orderby large.Length descending
                  select large;

            dataGridView1.DataSource = q.ToList();
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            IEnumerable<NWDataSet.OrdersRow> q = from or in this.nwDataSet1.Orders
                    select or;
            dataGridView1.DataSource = q.ToList();

            IEnumerable<NWDataSet.Order_DetailsRow> a = from od in this.nwDataSet1.Order_Details select od;
            dataGridView2.DataSource = a.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //顯示選中的年份的全部order
            if (comboBox1.SelectedItem == null)
                return;
            int selectYear = (int)comboBox1.SelectedItem;

            IEnumerable<NWDataSet.OrdersRow> q = from date in this.nwDataSet1.Orders
                                                 where date.OrderDate.Year == selectYear
                                                 select date;
            dataGridView1.DataSource = q.ToList();
            

            //============================================================================

            //顯示選中的年份的order的第一筆資料的ordder_details呈現在dataGridView2上

            //使用List<>裝取每一row的資料
            List<int> OrderIDLists = new List<int>();
            //遍歷每一筆dataGridView1的OrderID放到OrderIDLists
            foreach (DataGridViewRow dataRow in dataGridView1.Rows)
            {
                int orderID = (int)dataRow.Cells["OrderID"].Value;
                OrderIDLists.Add(orderID);
            }

            IEnumerable<NWDataSet.Order_DetailsRow> qq = from row in nwDataSet1.Order_Details
                                                         where OrderIDLists.Contains(row.OrderID)
                                                         select row;
            dataGridView2.DataSource = qq.ToList();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                int selectedOrderID = (int)selectedRow.Cells["OrderID"].Value;

                IEnumerable<NWDataSet.Order_DetailsRow> q = from row in nwDataSet1.Order_Details
                                                            where row.OrderID == selectedOrderID
                                                            select row;

                dataGridView2.DataSource = q.ToList();
            }
        }

        //一頁顯示幾筆資料
        int showRows = 0;
        //當前頁數,0為第一頁
        int cntPage = 0;
        //判斷上或下一頁是否為第一次按
        bool isFirstClick = true;
        
        private void NextPages_Click(object sender, EventArgs e)
        {
            
            showRows = Convert.ToInt32(textBox1.Text);
            
            if (cntPage < 0)
            {
                cntPage = 0;
            }
           
            if (!isFirstClick)
            {
                cntPage++;
            }
            else
            {
                isFirstClick = false;
            }
            //double totalPage = Math.Ceiling((double)nwDataSet1.Products.Count / (double)showRows);
            int totalPage = nwDataSet1.Products.Count / showRows;
            if (nwDataSet1.Products.Count % showRows != 0)
            {
                totalPage += 1;
            }
            if (cntPage < totalPage)
            {
                IEnumerable<NWDataSet.ProductsRow> q = from p in this.nwDataSet1.Products.Skip(cntPage * showRows).Take(showRows)
                                                       select p;
                this.dataGridView2.DataSource = q.ToList();
            }
            else
            {
                MessageBox.Show("已經為最後一頁");
            }
            
            
        }

        private void PreviousPages_Click(object sender, EventArgs e)
        {
            showRows = Convert.ToInt32(textBox1.Text);
            
            if (cntPage <0)
            {
                cntPage = 0;  
            }
            if (!isFirstClick)
            {
                cntPage--;
            }
            else
            {
                isFirstClick = false;
            }

            IEnumerable<NWDataSet.ProductsRow> q = from p in this.nwDataSet1.Products.Skip(cntPage * showRows).Take(showRows)
                    select p;
            this.dataGridView2.DataSource = q.ToList();
            
        }

        
    }
}
