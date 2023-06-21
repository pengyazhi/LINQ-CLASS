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

namespace Starter
{
    public partial class FrmLinq_To_Entity : Form
    {
        public FrmLinq_To_Entity()
        {
            InitializeComponent();
            this.dbContext.Database.Log = Console.Write;
            
        }
        NorthwindEntities dbContext = new NorthwindEntities();
        private void button1_Click(object sender, EventArgs e)
        {
            //IQueryable 會產生T-SQL的查詢指令
            //IQuerable<T> query 執行時會 => T-SQL
            var q = from p in dbContext.Products
                    where p.UnitPrice > 30
                    select p;

            dataGridView1.DataSource = q.ToList();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            var q = from p in dbContext.Products
                        //由大到小排序UnitPrice,如果一樣則用ProductID由大到小排序
                    orderby p.UnitPrice descending, p.ProductID descending
                    select p;
            dataGridView1.DataSource= q.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.dbContext.Categories.First().Products.ToList();

            MessageBox.Show(this.dbContext.Products.First().Category.CategoryName);
            this.dataGridView1.DataSource = this.dbContext.Categories.First().Products.ToList();

            MessageBox.Show(this.dbContext.Products.First().Category.CategoryName);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //在query中加入AsEnumerable()可以讓C#指翻譯到該位置
            //因為有些語法無法翻譯成TSQL指令,會產生錯誤
            var q = from p in this.dbContext.Products.AsEnumerable()
                    group p by p.Category.CategoryName into g
                    select new {  CategoryName = g.Key , Average = $"{g.Average(n=>n.UnitPrice):c2}"};

            dataGridView1.DataSource = q.ToList();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            var q = from d in this.dbContext.Orders
                   group d by d.OrderDate.Value.Year into g
                    select new {Year = g.Key , Count = g.Count() };
            dataGridView1.DataSource = q.ToList();
        }

        private void button55_Click(object sender, EventArgs e)
        {
            //Insert to Products
            //也可以把新增的物件拉出來寫 
            //Product prod = new Product { ProductName = "dddd", Discontinued = true };
            //this.dbContext.Products.Add(prod);
            this.dbContext.Products.Add( new Product  { ProductName="dddd",Discontinued = true});
            dbContext.SaveChanges();

           
        }
    }
}
