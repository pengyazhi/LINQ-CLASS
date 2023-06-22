using LinqLabs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace MyHomeWork
{
    public partial class Frm作業_4 : Form
    {
        public Frm作業_4()
        {
            InitializeComponent();
        }

        private void classifyNums_Click(object sender, EventArgs e)
        {
            this.treeView1.Nodes.Clear();
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            ClassifyNum(nums);
        }
        //for classifyNums_Click 分類數字
        private void ClassifyNum(int[] nums)
        {
            
            List<string> categories = new List<string>{"小","中","大"};
            foreach (string c in categories)
            {
                TreeNode node = treeView1.Nodes.Add(c);
                foreach (int n in nums)
                {
                    if(c == "小" &&  n <= 5)
                    {
                        node.Nodes.Add(n.ToString());
                    }
                    else if (c=="中" && n>5 && n <= 10)
                    {
                        node.Nodes.Add(n.ToString());
                    }
                    else if(c== "大" && n>10)
                    {
                        node.Nodes.Add(n.ToString());
                    }
                }
                
            }

        }
        
        //載入C槽檔案
        FileInfo[] files;
        DirectoryInfo dir;
        void LoadFiles()
        {
            dir = new DirectoryInfo(@"c:\windows");
            files = dir.GetFiles();
        }

        //清空treeView & listView
        void Clear()
        {
            treeView1.Nodes.Clear();
            listView1.Clear();
        }
        private void btnFileSize_Click(object sender, EventArgs e)
        {
            Clear();
            LoadFiles();
            var q = from f in files
                    orderby f.Length descending
                    group f by FileLength(f) into g
                    select new {fileSzie = g.Key,Count = g.Count(), file = g};
            dataGridView1.DataSource = q.ToList();

            
            //TreeView
            foreach(var f in q)
            {
                string header = $"{f.fileSzie}({f.Count})";
                TreeNode parentNode = this.treeView1.Nodes.Add(header);
                foreach(var item in f.file)
                {
                    parentNode.Nodes.Add(item.ToString());
                }
            }
            
            //ListView
            foreach(var f in q)
            {
                //先設定好ListViewGroup的大群及名稱
                string header = $"{f.fileSzie}({f.Count})";
                ListViewGroup lvg = listView1.Groups.Add(f.fileSzie, header);
                foreach(var item in f.file)
                {
                    //將item指派到給lvg這個Group
                    this.listView1.Items.Add(item.ToString()).Group = lvg;
                }
            }

         
        }

        private string FileLength(FileInfo f)
        {
            if (f.Length < 1000)
            {
                return "小檔案";
            }
            else if (f.Length < 5000)
            {
                return "中檔案";
            }
            else
            {
                return "大檔案";
            }
        }

        private void btnFileYear_Click(object sender, EventArgs e)
        {
            Clear();
            LoadFiles();
            var q = from f in files
                    orderby f.CreationTime descending
            group f by f.CreationTime.Year into g
                    select new { Year = g.Key, Count = g.Count(), file =g };
            dataGridView1.DataSource = q.ToList();

            //TreeView
            foreach(var f in q)
            {
                string header = $"{f.Year}({f.Count})";
                TreeNode parentNode = this.treeView1.Nodes.Add(header);
                foreach(var item in f.file)
                {
                    parentNode.Nodes.Add(item.ToString());
                }
            }
            //ListView
            foreach(var f in q)
            {
                string header = $"{f.Year}({f.Count})";
                ListViewGroup lvg= this.listView1.Groups.Add(f.Year.ToString(),header);
                foreach (var item in f.file)
                {
                    this.listView1.Items.Add(item.ToString()).Group = lvg;
                }
            }
        }
       
        //LINQ to Northwind Entity
        //LINQ的資料來源
       NorthwindEntities dbContext = new NorthwindEntities();
        private void btnNWProdPrice_Click(object sender, EventArgs e)
        {
            var q = dbContext.Products.OrderBy(n => n.UnitPrice).AsEnumerable().
                GroupBy(n => PriceRange(n)).Select(n => new {PriceLevel = n.Key,Count = n.Count(), ProductName= n });
            dataGridView1.DataSource = q.ToList();
            //treeView
            foreach(var f in q)
            {
                string header = $"{f.PriceLevel}({f.Count})";
                TreeNode node = this.treeView1.Nodes.Add(header);
                foreach(var item in f.ProductName)
                {
                    node.Nodes.Add(item.ProductName);
                }
                
            }
            //listView
        }

        private string PriceRange(Product n)
        {
            var sortNums = dbContext.Products.OrderBy(x => x.UnitPrice).ToList();
            int range = Convert.ToInt32(sortNums.Count()) / 3;
            decimal? productUnitPrice = n.UnitPrice;
            if (productUnitPrice < sortNums[range].UnitPrice)
            {
                return "LowPrice";
            }
            else if (productUnitPrice < sortNums[range].UnitPrice*2)
            {
                return "MiddlePrice";
            }
            else
            {
                return "HighPrice";
            }
        }
    }
}
