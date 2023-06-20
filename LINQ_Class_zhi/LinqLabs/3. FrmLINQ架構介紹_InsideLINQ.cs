using LinqLabs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Starter
{
    public partial class FrmLINQ架構介紹_InsideLINQ : Form
    {
        public FrmLINQ架構介紹_InsideLINQ()
        {
            InitializeComponent();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            ArrayList arr = new ArrayList();
            arr.Add(10);
            arr.Add(20);
            //ArrayList為非泛用,所以要使用Cast轉型才可以變成可列舉的陣列
            //因為使用匿名屬性所以型別只能用var
           var q = from a in arr.Cast<int>()
                                        //select a.ToString();
                                        //int沒有屬性就幫他加屬性
                                    select new { N = a, S = a * a, C = a * a * a };

                            //int沒有屬性所以無法被繫結到dataGridView上,string有
                            dataGridView1.DataSource = q.ToList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);

            //找到Products中UnitsInStock最高的前5筆資料
            IEnumerable<NWDataSet.ProductsRow> q = (from p in this.nwDataSet1.Products
                     orderby p.UnitsInStock descending
                     select p).Take(5);

            dataGridView1.DataSource = q.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9 ,10};
            this.listBox1.Items.Add($"Sum = {nums.Sum()}");
            this.listBox1.Items.Add($"Max= {nums.Max()}");
            this.listBox1.Items.Add($"Min = {nums.Min()}");
            this.listBox1.Items.Add($"Avg = {nums.Average()}");
            this.listBox1.Items.Add($"Count = {nums.Count()}");

            //================================
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            this.listBox1.Items.Add($"UnitPrice Max = {nwDataSet1.Products.Max(n => n.UnitPrice):c2}");
            this.listBox1.Items.Add($"UnitPrice Min = {nwDataSet1.Products.Min(n => n.UnitPrice):c2}");

        }
    }
}