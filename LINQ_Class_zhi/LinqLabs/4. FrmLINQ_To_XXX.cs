using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Starter
{
    public partial class FrmLINQ_To_XXX : Form
    {
        public FrmLINQ_To_XXX()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            IEnumerable<IGrouping<string,int>> q = from n in nums
                                                       //group n by後面是"Key"屬性的名稱
                                                   group n by n % 2 == 0?"偶數":"奇數";
            dataGridView1.DataSource = q.ToList();

            //================================
            //在TreeView產生結點
            //先產生大的結點(Key的value)
            foreach(var group in q)
            {
                TreeNode node= this.treeView1.Nodes.Add(group.Key);
                foreach(var item in group)
                { 
                    //再產生大結點下的結點
                    node.Nodes.Add(item.ToString());
                }
            }

            //ListView
            foreach (var group in q)
            {
                //先設定好ListViewGroup的大群及名稱
                ListViewGroup lvg = this.listView1.Groups.Add(group.Key, group.Key);
                foreach (var item in group)
                {
                    //將item放到ListViewGroup的group
                    this.listView1.Items.Add(item.ToString()).Group = lvg;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int[] nums = {1,2, 3, 4,4,5,6,7,8,9,10,11,15,16};
            var q = from n in nums
                    //將group by 的結果放到g
                    group n by n % 2 == 0 ? "偶數" : "奇數" into g
                    select new { MyKey = g.Key, MyCount = g.Count(),MyAvg = g.Average(),MyGroup = g };
            dataGridView1.DataSource = q.ToList();

            //treeView
            foreach (var group in q)
            {
                string s = $"{group.MyKey} ({group.MyCount})";
                TreeNode node = this.treeView1.Nodes.Add(s);
                foreach (var item in group.MyGroup)
                {
                    //再產生大結點下的結點
                    node.Nodes.Add(item.ToString());
                }
            }
            //ListView
            foreach (var group in q)
            {
                //先設定好ListViewGroup的大群及名稱
                string header = $"{group.MyKey} ({group.MyCount})";
                ListViewGroup lvg = this.listView1.Groups.Add(group.MyKey, header);
                foreach (var item in group.MyGroup)
                {
                    //將item放到ListViewGroup的group
                    this.listView1.Items.Add(item.ToString()).Group = lvg;
                }
            }
            //Chart
            chart1.DataSource = q.ToList();
            chart1.Series[0].XValueMember = "MyKey";
            chart1.Series[0].YValueMembers = "MyCount";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            chart1.Series[1].XValueMember = "MyKey";
            chart1.Series[1].YValueMembers = "MyAvg";
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 4, 5, 6, 7, 8, 9, 10, 11, 15, 16 };
            var q = from n in nums
                        //將group by 的結果放到g
                    group n by MyKey(n) into g
                    select new { MyKey = g.Key, MyCount = g.Count(), MyAvg = g.Average(), MyGroup = g };
            dataGridView1.DataSource = q.ToList();
            //treeView
            foreach (var group in q)
            {
                string s = $"{group.MyKey} ({group.MyCount})";
                TreeNode node = this.treeView1.Nodes.Add(s);
                foreach (var item in group.MyGroup)
                {
                    //再產生大結點下的結點
                    node.Nodes.Add(item.ToString());
                }
            }
            //ListView
            foreach (var group in q)
            {
                //先設定好ListViewGroup的大群及名稱
                string header = $"{group.MyKey} ({group.MyCount})";
                ListViewGroup lvg = this.listView1.Groups.Add(group.MyKey, header);
                foreach (var item in group.MyGroup)
                {
                    //將item放到ListViewGroup的group
                    this.listView1.Items.Add(item.ToString()).Group = lvg;
                }
            }
            //Chart
            chart1.DataSource = q.ToList();
            chart1.Series[0].XValueMember = "MyKey";
            chart1.Series[0].YValueMembers = "MyCount";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            chart1.Series[1].XValueMember = "MyKey";
            chart1.Series[1].YValueMembers = "MyAvg";
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
        }

        private string MyKey(int n)
        {
            if (n < 5)
            {
                return "小";
            }else if (n < 10) 
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
            DirectoryInfo directoryInfo = new DirectoryInfo(@"c:\windows");
            FileInfo [] files = directoryInfo.GetFiles();
            var q = from item in files
                    let ex = item.Extension
                    group item by ex into g
                    orderby g.Count() descending
                    select new { MyKey = g.Key  ,MyCount = g.Count() };
            dataGridView1.DataSource = q.ToList();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
            var q = from p in this.nwDataSet1.Orders
                        //將p.OrderDate.Year暫存到let y 變數
                    let y = p.OrderDate.Year
                    //使用變數名稱
                    group p by y into g
                    select new { Year = g.Key, OrderCount = g.Count(),g };
            dataGridView1.DataSource = q.ToList();

            //foreach (var year in q)
            //{
            //    string s = $"{year.Year}({year.OrderCount})";
            //    TreeNode tn = 
            //    foreach (var item in year.g)
            //    {

            //    }
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(@"c:\windows");
            FileInfo[] files = directoryInfo.GetFiles();
            var q = from item in files
                    let ex = item.Extension
                    where ex ==".log"
                    select item;
            dataGridView1.DataSource = q.ToList();
            MessageBox.Show(q.Count().ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = "This is a pen,this is an apple,this is a book.";
            char[] chars = { ' ', '.', '?',',' };
            string[] words = s.Split(chars);

            var q = from w in words
                    where w != ""
                    //所有的key都會變大寫,就不會有This跟this的差別
                    group w by w.ToUpper() into g
                    select new {String = g.Key , Count = g.Count() };
            dataGridView1.DataSource = q.ToList();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            int[] nums1 = { 45, 88, 92, 44, 66, 34, 112, 57 };
            int[] nums2 = { 66, 88, 74, 16, 22, 39, 112, 34 };
            //集合運算子 Distinct / Union / Intersect / Except
            var q = nums1.Intersect(nums2);
            q = nums1.Union(nums2);
            q = nums1.Distinct();
           
            //切割運算子 Take / Skip
            q = nums1.Take(2);

            //數量詞作業 : Any / All / Contains
            bool result = nums1.Any(n => n > 50);
            result = nums2.Any(n => n > 1000);
            result = nums1.All(n => n > 1);
            result = nums2.All(n => n > 1000);

            //單一元素運算子 :
            //First / Last / Single / ElementAt  
            //FirstOrDefault / LastOrDefault / SingleOrDefault / ElementAtOrDefault
            int n1 = nums1.First();
            n1 = nums1.ElementAt(2);
            n1 = nums1.ElementAtOrDefault(20);

            //n1 = nums1.ElementAt(12);
            n1 = nums1.ElementAtOrDefault(12); 

            //=============================
            //產生作業 : Generation – Range / Repeat / Empty DefaultIfEmpty
            //RangeTest();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            this.categoriesTableAdapter1.Fill(this.nwDataSet1.Categories);
            //var q = from p in this.nwDataSet1.Products
            //        group p by p.CategoryID into g
            //        select new { CategoryID = g.Key , Avg = g.Average(n=>n.UnitPrice)};

            //join 太T-SQL化的查詢
            var q = from c in this.nwDataSet1.Categories
                    join p in this.nwDataSet1.Products
                    on c.CategoryID equals p.CategoryID
                    group p by c.CategoryName into g
                    select new { CatgoryName = g.Key,Avg =  $"{g.Average(n=>n.UnitPrice):c2}" };
            
            dataGridView1.DataSource = q.ToList();
        }
    }
}
