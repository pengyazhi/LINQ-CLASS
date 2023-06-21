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


//entity data model 特色

//1. App.config 連接字串
//2. Package 套件下載, 參考 EntityFramework.dll, EntityFramework.SqlServer.dll
//3. 導覽屬性 關聯
//4. DataSet model 需要處理 DBNull; Entity Model  不需要處理 DBNull (DBNull 會被 ignore)
//5. IQuerable<T> query 執行時會轉成 => T-SQL

namespace Starter
{
    public partial class FrmLinq_To_Entity : Form
    {
        public FrmLinq_To_Entity()
        {
            InitializeComponent();

            this.dbContext.Database.Log = Console.Write;

            Console.Write("xxx");
        }

            NorthwindEntities dbContext = new NorthwindEntities();
        
        private void button1_Click(object sender, EventArgs e)
        {       
            //IQuerable<T> query 執行時會 => T-SQL

            var q = from p in dbContext.Products
                    where p.UnitPrice > 30
                    select p;

            this.dataGridView1.DataSource = q.ToList();


        }

        private void button22_Click(object sender, EventArgs e)
        {
            var q = from p in this.dbContext.Products
                    orderby p.UnitsInStock descending, p.ProductID descending
                    select p;

            this.dataGridView1.DataSource = q.ToList();

            //=================================================
            var q2 = this.dbContext.Products.OrderByDescending(p => p.UnitsInStock)
                                           .ThenByDescending(p => p.ProductID)
                                           .Select(p => new { p.ProductID, p.UnitPrice, p.UnitsInStock, TotalPrice = p.UnitPrice * p.UnitsInStock });

            this.dataGridView2.DataSource = q2.ToList();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            ////============================
            ////自訂 compare logic
            var q3 = dbContext.Products.AsEnumerable().OrderBy(p => p, new MyComparer()).ToList();
            this.dataGridView2.DataSource = q3.ToList();
        }

        class MyComparer : IComparer<Product>
        {
            public int Compare(Product x, Product y)
            {
                if (x.UnitPrice < y.UnitPrice)
                    return -1;
                else if (x.UnitPrice > y.UnitPrice)
                    return 1;
                else
                    return string.Compare(x.ProductName[0].ToString(), y.ProductName[0].ToString(), true);

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //===================================
            //Note: System.NotSupportedException: 'LINQ to Entities 無法辨識方法 'System.String MyKey(System.Nullable`1[System.Decimal])' 方法，而且這個方法無法轉譯成存放區運算式。'
            //Solution => AsEnumerable()

            var q = from p in this.dbContext.Products .AsEnumerable()
                    group p by p.Category.CategoryName into g
                    select new { CategoryName= g.Key, AvgUnitPrice =$"{g.Average(p => p.UnitPrice):c2}"  };

            this.dataGridView1.DataSource = q.ToList();

        }

        private void button2_Click(object sender, EventArgs e)
        {     
            //物件關聯對映（英語：Object Relational Mapping，簡稱ORM，或O / RM，或O / R mapping），

            this.dataGridView1.DataSource =  this.dbContext.Categories.First().Products.ToList();

            MessageBox.Show(this.dbContext.Products.First().Category.CategoryName);

        }

        private void button20_Click(object sender, EventArgs e)
        {
            var q = from c in this.dbContext.Categories
                    join p in this.dbContext.Products
                    on c.CategoryID equals p.Category.CategoryID
                    select new { c.CategoryID, c.CategoryName, p.ProductID, p.UnitPrice, p.UnitsInStock };

            this.dataGridView1.DataSource = q.ToList();

            //====================================

            //lefter outer join (包括 categoryID 9)
            var q2x = from c in this.dbContext.Categories
                      join p in this.dbContext.Products
                      on c.CategoryID equals p.CategoryID into js  // js 是 IEnumerable<Product> => 包括 categoryID 9 的 product
                      select new { c.CategoryID, c.CategoryName, MyAvg = js.Average(p => p.UnitPrice) };

            this.dataGridView2.DataSource = q2x.ToList();

        }

        private void button21_Click(object sender, EventArgs e)
        {
            //inner join
            var q = from c in this.dbContext.Categories
                    from p in c.Products
                    select new { c.CategoryID, c.CategoryName, p.ProductID, p.UnitPrice, p.UnitsInStock };

            this.dataGridView1.DataSource = q.ToList();
            MessageBox.Show("q.count() =" + q.Count());

            //=================================
            this.dbContext.Categories.SelectMany(c => c.Products, (c, p) => new { c.CategoryID, c.CategoryName, p.ProductID, p.UnitPrice, p.UnitsInStock });

            //cross join
            var q2 = from c in this.dbContext.Categories
                     from p in this.dbContext.Products
                     select new { c.CategoryID, c.CategoryName, p.ProductID, p.UnitPrice, p.UnitsInStock };
            MessageBox.Show("q2.count() =" + q2.Count());
            this.dataGridView2.DataSource = q2.ToList();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            var q = from p in this.dbContext.Products
                    select new { p.CategoryID, p.Category.CategoryName, p.ProductID, p.UnitPrice, p.UnitsInStock };

            this.dataGridView2.DataSource = q.ToList();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            bool? pass;

            pass = true;
            pass = false;
            pass = null;
            //pass.Value

            //NOTE
            var q = from o in this.dbContext.Orders
                    group o by o.OrderDate.Value.Year into g
                    select new { g.Key, MyCount= g.Count() };

            this.dataGridView1.DataSource = q.ToList();

            //==============================
            //key - mutiple hierarchy key
            var q2 = from o in this.dbContext.Orders
                    group o by new { o.OrderDate.Value.Year, o.OrderDate.Value.Month } into g
                    select new { g.Key, Count = g.Count() };

            this.dataGridView2.DataSource = q2.ToList();
        }

        private void button55_Click(object sender, EventArgs e)
        {
            //Add Product

            Product product = new Product { ProductName = "Test " + DateTime.Now.ToString(), Discontinued = true };
            this.dbContext.Products.Add(product);


            this.dbContext.SaveChanges();

            this.Read_RefreshDataGridView();

        }

        void Read_RefreshDataGridView()
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.DataSource = this.dbContext.Products.ToList();

        }

        private void button53_Click(object sender, EventArgs e)
        {
            //delete one product
            var product = (from p in this.dbContext.Products
                           where p.ProductName.Contains("Test")
                           select p).FirstOrDefault();

            if (product == null) return;

            this.dbContext.Products.Remove(product);
            this.dbContext.SaveChanges();

            this.Read_RefreshDataGridView();
        }

        private void button56_Click(object sender, EventArgs e)
        {
            //update
            var product = (from p in this.dbContext.Products
                           where p.ProductName.Contains("Test")
                           select p).FirstOrDefault();

            if (product == null) return; //exit method

            product.ProductName = "Test" + product.ProductName;

            this.dbContext.SaveChanges();

            this.Read_RefreshDataGridView();
        }

        private void button57_Click(object sender, EventArgs e)
        {

        }
    }
}
