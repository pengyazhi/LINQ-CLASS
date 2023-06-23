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
            Clear();
            var q = dbContext.Products.OrderBy(n => n.UnitPrice).AsEnumerable().
                GroupBy(n => PriceRange(n)).Select(n => new 
                                                                                    {
                                                                                        PriceLevel = n.Key,
                                                                                        Count = n.Count(),
                                                                                        ProductName= n
                                                                                    }
                                                                                );
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
            foreach(var f in q)
            {
                string header = $"{f.PriceLevel}({f.Count})";
                ListViewGroup lvg = this.listView1.Groups.Add(f.PriceLevel, header);
                foreach(var item in f.ProductName) 
                {
                    this.listView1.Items.Add(item.ProductName).Group = lvg;
                }
            }
        }

        private string PriceRange(Product n)
        {
            //將Products的UnitPrice先由小到大排序
            var sortNums = dbContext.Products.OrderBy(x => x.UnitPrice).ToList();
            //將資料分成三等份,前33%為LowPrice,中間33%MiddlePrice,剩下的為HighPrice
            int range = Convert.ToInt32(sortNums.Count()) / 3;
            //decimal? productUnitPrice = n.UnitPrice;
            if (n.UnitPrice < sortNums[range].UnitPrice)
            {
                return "LowPrice";
            }
            else if (n.UnitPrice < sortNums[range].UnitPrice*2)
            {
                return "MiddlePrice";
            }
            else
            {
                return "HighPrice";
            }
        }

        private void btnOrderGroupByYear_Click(object sender, EventArgs e)
        {
            Clear();
            var q = dbContext.Orders.GroupBy(n => n.OrderDate.Value.Year.ToString()).
                Select(n => new
                {
                    Year = n.Key,
                    Count = n.Count(),
                    n
                });
            dataGridView1.DataSource = q.ToList();
            //treeView
            foreach (var y in q)
            {
                string header = $"{y.Year}({y.Count})";
                TreeNode node = this.treeView1.Nodes.Add(header);
                foreach (var item in y.n)
                {
                    node.Nodes.Add(item.OrderDate.ToString());
                }

            }
            //listView
            foreach (var y in q)
            {
                string header = $"{y.Year}({y.Count})";
                ListViewGroup lvg = this.listView1.Groups.Add(y.Year, header);
                foreach (var item in y.n)
                {
                    this.listView1.Items.Add(item.OrderDate.ToString()).Group = lvg;
                }
            }
        }

        private void btnOrderGroupByYM_Click(object sender, EventArgs e)
        {
            Clear();
            var q = from ym in dbContext.Orders
                    group ym by
                    new
                    {
                        ym.OrderDate.Value.Year,
                        ym.OrderDate.Value.Month
                    } into g
                    select new { Year_Month = g.Key, Count = g.Count(), Result = g };
            dataGridView1.DataSource = q.ToList();
            //treeView
            foreach (var ym in q)
            {
                string header = $"{ym.Year_Month}({ym.Count})";
                TreeNode node = this.treeView1.Nodes.Add(header);
                foreach (var item in ym.Result)
                {
                    node.Nodes.Add(item.OrderDate.ToString());
                }

            }
            //listView
            foreach (var ym in q)
            {
                string header = $"{ym.Year_Month}({ym.Count})";
                ListViewGroup lvg = this.listView1.Groups.Add(ym.Year_Month.ToString(), header);
                foreach (var item in ym.Result)
                {
                    this.listView1.Items.Add(item.OrderDate.ToString()).Group = lvg;
                }
            }
        }

        private void btnRevenue_Click(object sender, EventArgs e)
        {
            var q = from o in dbContext.Orders.AsEnumerable()
                    from od in dbContext.Order_Details
                    where o.OrderID == od.OrderID
                    group new
                    {
                        o.OrderID,
                        o.CustomerID,
                        Revenue = od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount)
                    } by od.OrderID into g
                    select new
                    {
                        OrderID = g.Key,
                        CustomerID = g.Select(n => n.CustomerID),
                        Total_Revenue = $"{g.Sum(n => n.Revenue):c2}",
                        Count = g.Count()
                    };
            dataGridView1.DataSource = q.ToList();
            //treeView 
            foreach(var group in q)
            {
                string header = $"OrderID : {group.OrderID}(訂單數 : {group.Count})";
                TreeNode node = treeView1.Nodes.Add(header) ;
                node.Nodes.Add($"Total_Revenue : {group.Total_Revenue}");
            }
            //listView 
            foreach (var group in q)
            {
                string header = $"OrderID : {group.OrderID}(訂單數 : {group.Count})";
                ListViewGroup lvg = listView1.Groups.Add(group.OrderID.ToString(), header);
                listView1.Items.Add(group.Total_Revenue.ToString()).Group = lvg;
            }
        }

        private void btnTOP5Sales_Click(object sender, EventArgs e)
        {
            var q = from o in dbContext.Orders.AsEnumerable()
                    from od in dbContext.Order_Details
                    where o.OrderID == od.OrderID
                    group new
                    {
                        o.EmployeeID,
                        Revenue = od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount)
                    } by o.EmployeeID into g
                    orderby g.Sum(n => n.Revenue) descending
                    select new
                    {
                        EmployeeID = g.Key,
                        Total_Revenue = $"{g.Sum(n => n.Revenue):c2}",
                        Count = g.Count()
                    };
                    
            
            dataGridView1.DataSource = q.Take(5).ToList();
        }

        private void btnNWProdUnitPriceTOP5_Click(object sender, EventArgs e)
        {
            var q = from p in dbContext.Products.AsEnumerable()
                    from c in dbContext.Categories
                    where p.CategoryID == c.CategoryID
                    orderby p.UnitPrice descending
                    select new
                    {
                        c.CategoryName,
                        p.ProductName,
                        p.UnitPrice
                    };
            dataGridView1.DataSource = q.Take(5).ToList();
        }

        private void btnAnyUnitPrice_Click(object sender, EventArgs e)
        {
            bool isUnitPriceHigherThan300 = dbContext.Products.AsEnumerable().Any(n => n.UnitPrice > 300);
            
            MessageBox.Show($"NW產品是否有任何一筆產品單價大於300? {isUnitPriceHigherThan300}");
                    
        }
    }
}
