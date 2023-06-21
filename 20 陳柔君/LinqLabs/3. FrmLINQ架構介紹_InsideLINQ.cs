using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LinqLabs
{
    public partial class FrmLINQ架構介紹_InsideLINQ : Form
    {
        public FrmLINQ架構介紹_InsideLINQ()
        {
            InitializeComponent();
        }

        private void btnArrayList_Click(object sender, EventArgs e)
        {
            // 查詢非泛用集合
            ArrayList arr = new ArrayList();
            arr.Add(2);
            arr.Add(10);
            arr.Add(100);

            //IEnumerable<int> q = from n in arr.Cast<int>()
            //        where n > 3
            //        select n;

            // dataGridView1.DataSource = q.ToList();
            // int沒有屬性，繫結到DataGridView沒有資料

            // 匿名型別設屬性
            var q = from n in arr.Cast<int>()
                    where n > 3
                    select new
                    {
                        Int = n,
                        Square = n * n,
                        Cube = n * n * n
                    };

            dataGridView1.DataSource = q.ToList();
        }

        private void btnMixQuery_Click(object sender, EventArgs e)
        {
            // 運算式混和方法查詢
            // 取庫存最高的五筆資料
            productsTableAdapter1.Fill(nwDataSet1.Products);
            var q = (from n in nwDataSet1.Products
                    orderby n.UnitsInStock descending
                    select n).Take(5);

            dataGridView1 .DataSource = q.ToList();
        }

        private void btnAggregation_Click(object sender, EventArgs e)
        {
            // 立即執行的查詢指令
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            listBox1.Items.Add("Sum: " +nums.Sum());
            listBox1.Items.Add("Max: " + nums.Max());
            listBox1.Items.Add("Min: " + nums.Min());
            listBox1.Items.Add("Avg: " + nums.Average());
            listBox1.Items.Add("Count: " + nums.Count());
            listBox1.Items.Add("=============");
            //===================
            productsTableAdapter1.Fill(nwDataSet1.Products);
            listBox1.Items.Add("Max Stock: "+nwDataSet1.Products.Max(n => n.UnitsInStock));
            listBox1.Items.Add("Min Stock: " + nwDataSet1.Products.Min(n => n.UnitsInStock));
        }
    }
}