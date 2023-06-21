﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Xml.Linq;

namespace Starter
{
    public partial class FrmLINQ_To_XXX : Form
    {
        public FrmLINQ_To_XXX()
        {
            InitializeComponent();

            this.categoriesTableAdapter1.Fill(this.nwDataSet1.Categories);
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10,11,13 };

            IEnumerable<IGrouping<string, int>> q = from n in nums
                                                                                 //   group n by (n % 2);
                                                                             group n by n % 2 == 0 ? "偶數" : "奇數";

           this.dataGridView1.DataSource =  q.ToList();

            //=========================
            //treeview
            foreach (var group in q)
            {
                TreeNode node =   this.treeView1.Nodes.Add(group.Key);
             
                foreach (var item in group)
                {
                    node.Nodes.Add(item.ToString());
                }
            }
            //listview
            foreach (var group in q)
            {
                ListViewGroup lvg =  this.listView1.Groups.Add(group.Key, group.Key);   //this.listView1.Groups.Contains("key1")
               
                foreach (var item in group)
                {
                    this.listView1.Items.Add(item.ToString()).Group = lvg;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //split- Apply Aggregation-combine
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13 };

            var q = from n in nums
                        //   group n by (n % 2);
                    group n by n % 2 == 0 ? "偶數" : "奇數" into g
                    select new { MyKey = g.Key, MyCount = g.Count() , MyAvg=g.Average(),  MyGroup=g};


            this.dataGridView1.DataSource = q.ToList();

            //============================

            //treeview
            foreach (var group in q)
            {
                string s = $"{group.MyKey} ({group.MyCount})";
                TreeNode node = this.treeView1.Nodes.Add(s);

                foreach (var item in group.MyGroup)
                {
                    node.Nodes.Add(item.ToString());
                }
            }
            //listview
            foreach (var group in q)
            {
                string header = $"{group.MyKey} ({group.MyCount})";
                ListViewGroup lvg = this.listView1.Groups.Add(group.MyKey, header);   //this.listView1.Groups.Contains("key1")

                foreach (var item in group.MyGroup)
                {
                    this.listView1.Items.Add(item.ToString()).Group = lvg;
                }
            }

            //chart 
            this.chart1.DataSource= q.ToList();
            this.chart1.Series[0].XValueMember = "MyKey";
            this.chart1.Series[0].YValueMembers = "MyCount";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            this.chart1.Series[1].XValueMember = "MyKey";
            this.chart1.Series[1].YValueMembers = "MyAvg";
            this.chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13 };

            var q = from n in nums
                    group n by  MyKey(n) into g
                    select new { MyKey = g.Key, MyCount = g.Count(), MyAvg = g.Average(), MyGroup = g };


            this.dataGridView1.DataSource = q.ToList();


            //treeview
            foreach (var group in q)
            {
                string s = $"{group.MyKey} ({group.MyCount})";
                TreeNode node = this.treeView1.Nodes.Add(s);

                foreach (var item in group.MyGroup)
                {
                    node.Nodes.Add(item.ToString());
                }
            }
            //listview
            foreach (var group in q)
            {
                string header = $"{group.MyKey} ({group.MyCount})";
                ListViewGroup lvg = this.listView1.Groups.Add(group.MyKey, header);   //this.listView1.Groups.Contains("key1")

                foreach (var item in group.MyGroup)
                {
                    this.listView1.Items.Add(item.ToString()).Group = lvg;
                }
            }

            //chart 
            this.chart1.DataSource = q.ToList();
            this.chart1.Series[0].XValueMember = "MyKey";
            this.chart1.Series[0].YValueMembers = "MyCount";
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            this.chart1.Series[1].XValueMember = "MyKey";
            this.chart1.Series[1].YValueMembers = "MyAvg";
            this.chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

        }

        private  string MyKey(int n)
        {
            if (n<5)
            {
                return "小";
            }
            else if (n<10)
            {
                return "中";
            }
            else
            {
                return "大";
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(@"c:\windows");
            FileInfo[] files = dir.GetFiles();

            var q = from f in files
                    group f by f.Extension into g
                    orderby  g.Count() descending
                    select new { Extension = g.Key, MyCount = g.Count() };

            this.dataGridView1.DataSource = q.ToList(); //files;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);

            var q = from o in this.nwDataSet1.Orders
                    group o by o.OrderDate.Year into g
                    select new {年= g.Key, 筆數= g.Count() };

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(@"c:\windows");
            FileInfo[] files = dir.GetFiles();

            var q = from f in files
                    let s=f.Extension
                    where s == ".log"
                    select f;

            this.dataGridView1.DataSource = q.ToList();
            MessageBox.Show(".log ...." + q.Count());

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = "This is a pen. this is an apple. this is      a book";

            char[] chars = { ',', '?', '.', ' ' };
            string[] words = s.Split(chars);

            var q = from w in words
                    where w != ""
                    group w by w.ToUpper() into g
                    select new { g.Key, MyCount = g.Count() };

            this.dataGridView1.DataSource = q.ToList();


        }

        private void button15_Click(object sender, EventArgs e)
        {
            int[] nums1 = { 2, 4, 56, 72, 11, 2 };
            int[] nums2 = { 2,55, 88, 99,  11 , 66};
            
            //集合運算子 Distinct / Union / Intersect / Except
            IEnumerable<int> q = nums1.Intersect(nums2);
            q = nums1.Union(nums2);
            q = nums1.Distinct();

            //切割運算子 Take / Skip
            q = nums1.Take(2);

            //數量詞作業 : Any / All / Contains
            bool result = nums1.Any(n => n > 50);
            result = nums1.Any(n => n > 100);
            result = nums1.All(n => n > 1);

            //單一元素運算子 :
            //First / Last / Single / ElementAt  
            //FirstOrDefault / LastOrDefault / SingleOrDefault / ElementAtOrDefault
            int n1 = nums1.First();
            n1= nums1.ElementAt(2);

            //n1 = nums1.ElementAt(12);
            n1 = nums1.ElementAtOrDefault(12);

            //=============================
            //產生作業 : Generation – Range / Repeat / Empty DefaultIfEmpty
            RangeTest();
        }


        #region Demo
        void RangeTest()
        {
            IEnumerable<int> q1 = Enumerable.Repeat(60, 100);

            this.dataGridView1.DataSource = q1.Select(n => new { n }).ToList();

            //return;

            //===================================
            var source = Enumerable.Range(1, 10000000);
            System.Diagnostics.Stopwatch watcher = new System.Diagnostics.Stopwatch();
            watcher.Start();
            // PLINQ  AsParallel() 
            //什麼是平行查詢？
            //主要差別在於 PLINQ 會嘗試充分運用系統上的所有處理器。 
            //它的作法是將資料來源分割成多個區段，然後以平行方式，以個別的背景工作執行緒在多個處理器上對每個區段執行查詢。 
            //在許多情況下，平行執行可讓查詢速度快許多。
            var q2 = from n in source.AsParallel()
                     where n % 2 == 0
                     orderby n
                     select new { N = n };
            this.dataGridView1.DataSource = q2.ToList();

            watcher.Stop();
            double seconds = watcher.Elapsed.TotalSeconds;
            MessageBox.Show("seconds =" + seconds);
        }
        private void button8_Click(object sender, EventArgs e)
        {
            var q = from p in this.nwDataSet1.Products
                    select new XElement("Product", new XElement("ProductName", p.ProductName), new XElement("UnitPrice", p.UnitPrice));


            XElement doc = new XElement("Products", q);
            doc.Save("Products.xml");
            Process.Start("Products.xml");
        }
        private void button13_Click(object sender, EventArgs e)
        {
            XElement doc;
            doc = XElement.Load("Products.xml");


            //xml 文件 轉物件

            var q = from element in doc.Elements("Product")
                    select new
                    {
                        ProductName = element.Element("ProductName").Value,
                        UnitPrice = element.Element("UnitPrice").Value
                    };
            this.dataGridView1.DataSource = q.ToList();
        }
        int page = 0;
        private void button9_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.nwDataSet1.Products.Skip(10 * page).Take(10).ToList();

            page += 1;
        }
        #endregion

        private void button10_Click(object sender, EventArgs e)
        {
            this.categoriesTableAdapter1.Fill(this.nwDataSet1.Categories);
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);

            //var q = from p in this.nwDataSet1.Products
            //        where !p.IsCategoryIDNull()
            //        group p by p.CategoryID into g
            //        select new { CategoryID = g.Key, AvgUnitPrice = g.Average(p => p.UnitPrice) };

            //太 T-SQL
            var q = from c in this.nwDataSet1.Categories
                    join p in this.nwDataSet1.Products
                    on c.CategoryID equals p.CategoryID
                    //select new {c.CategoryName, p.ProductName, p.UnitPrice, p.CategoryID}
                    group p by c.CategoryName into g
                    select new { CategoryName = g.Key, AvgUnitPrice = g.Average(p => p.UnitPrice) };

            this.dataGridView1.DataSource = q.ToList();

           
        }
    }
}
 