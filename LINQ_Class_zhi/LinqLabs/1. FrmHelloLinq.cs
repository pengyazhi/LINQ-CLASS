using LinqLabs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Starter
{
    public partial class FrmHelloLinq : Form
    {
        public FrmHelloLinq()
        {
            InitializeComponent();
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("int [] nums --foreach");
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            foreach(int n in nums)
            {
                listBox1.Items.Add(n);
            }
           
            listBox1.Items.Add("int [] nums --IEnumerator");
          
            IEnumerator en = nums.GetEnumerator();
            while(en.MoveNext())
            {
                listBox1.Items.Add(en.Current);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<int> list = new List<int>() { 10, 20, 30, 40, 50, 60, 70, 80 };
            listBox1.Items.Add("List<int> list  --foreach");
            foreach(int n in list)
            {
                listBox1.Items.Add(n);
            }
            listBox1.Items.Add("List<int> list --IEnumerator");

            List<int>.Enumerator en = list.GetEnumerator();
            while (en.MoveNext())
            {
                listBox1.Items.Add(en.Current);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            // 嚴重性程式碼說明專案檔案行隱藏項目狀態錯誤  CS1579 因為 'int' 不包含 'GetEnumerator' 的公用執行個體或延伸模組定義，所以 foreach 陳述式無法在型別 'int' 的變數上運作 LinqLabs    C: \Users\User\Desktop\LinqLabs(StartUp)\LinqLabs\1.FrmHelloLinq.cs    54  作用中

            //            int w = 100;
            //            foreach(int n in w) {}
            listBox1.Items.Add("字串 -- foreach迴圈");
            string sz = "ascdfsf";
            foreach(char c in sz)
            {
                listBox1.Items.Add(c);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("LINQ -- foreach 取偶數");
            
            //Step1 : define source
            int[] nums = { 1,2,3,4,5,6,7,8,9,10};
           
            //Step2 : define query
            IEnumerable<int> q = from n in nums 
                                                       where n % 2 == 0 && n>5 && n <10  
                                                       select n;
            
            //Step3 : execute query
            foreach(int n in q)
            {
                listBox1.Items.Add(n);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("LINQ --IsEven() 取偶數");

            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //執行順序是先跑foreach迴圈 到"in q"時才會去IsEven()判斷n是不是偶數
            IEnumerable<int> q = from n in nums
                                 where IsEven(n)
                                 select n;

            foreach (int n in q)
            {
                listBox1.Items.Add(n);
            }
        }

        private bool IsEven(int n)
        {
            return (n % 2 == 0);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("LINQ --Point   IEnumerable可投射任意型別");

            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            
            IEnumerable<Point> q = from n in nums
                                 where IsEven(n)
                                 select new Point(n,n*n);

            foreach (Point n in q)
            {
                listBox1.Items.Add(n);
            }

            List<Point> list = q.ToList();
            
            dataGridView1.DataSource  = list;
            
            chart1.DataSource = list;

            //以下設置chart1的第一筆Series
            
            //Point的兩個點的屬性名稱為X,Y
            //public int X{ get{return x;}set {x = value;}}
            //public int Y{get {return y; }set{y = value;}}
            chart1.Series[0].XValueMember = "X";
            chart1.Series[0].YValueMembers = "Y";
            chart1.Series[0].Color = Color.SeaGreen;
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] sz = { "apple", "Apple", "Apple123", "xxxApple","ddddd" };

            IEnumerable<string> strings = from s in sz
                                          //先把全部字串都變小寫,在尋找包含"apple"字串的,且字串長度大於5
                                          where s.ToLower().Contains("apple") &&  s.Length >5
                                          select s;
            foreach(string s in strings)
            {
                listBox1.Items.Add(s);
            }
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
           
            string x = textBox1.Text;
            IEnumerable<NWDataSet.ProductsRow> q = from p in this.nwDataSet1.Products
                    where !p.IsUnitPriceNull() && p.UnitPrice > 30 && p.UnitPrice < 100 && p.ProductName.StartsWith($"{x}")
                    select p;
            

          dataGridView1.DataSource = q.ToList();
           
        }

        private void button10_Click(object sender, EventArgs e)
        {
           IEnumerable<NWDataSet.OrdersRow> d = from o in this.nwDataSet1.Orders
                    where o.OrderDate.Year == 1997
                    select o;
            this.bindingSource1.DataSource = d.ToList();
            dataGridView1.DataSource = this.bindingSource1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            //nums.Where().Select()
        }

        private void button11_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(@"C:\Windows");
            FileInfo[] files = directoryInfo.GetFiles();
            
            dataGridView1.DataSource = files;   
        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button49_Click(object sender, EventArgs e)
        {

        }
    }
}
