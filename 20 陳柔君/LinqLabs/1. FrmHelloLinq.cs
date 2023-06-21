using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace LinqLabs
{
    public partial class FrmHelloLinq : Form
    {
        public FrmHelloLinq()
        {
            InitializeComponent();
            productsTableAdapter1.Fill(nwDataSet1.Products);
            ordersTableAdapter1.Fill(nwDataSet1.Orders);
        }

        private void btnEnumInt_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4,5 };

            // 語法糖
            foreach (int n in nums)
            {
                listBoxShow.Items.Add(n);
            }

            listBoxShow.Items.Add("================");

            //public interface IEnumerator<T>
            //支援泛型集合上的簡單反覆運算。

            // 陣列物件實作IEnumerator<T>所產生的運算方法
            System.Collections.IEnumerator en = nums.GetEnumerator();
            while (en.MoveNext())
            {
                listBoxShow.Items.Add(en.Current);
            }
        }

        private void btnEnumList_Click(object sender, EventArgs e)
        {
            List<int> list = new List<int>() { 10, 20, 30, 40, 50 };
            foreach (int n in list)
            {
                listBoxShow.Items.Add(n);
            }

            listBoxShow.Items.Add("================");

            List<int>.Enumerator en = list.GetEnumerator();

            while (en.MoveNext())
            {
                listBoxShow.Items.Add(en.Current);
            }
        }

        private void btnEnumString_Click(object sender, EventArgs e)
        {            
            // 錯誤 CS1579
            // 因為 'int' 不包含 'GetEnumerator' 的公用執行個體或延伸模組定義
            // 所以 foreach 陳述式無法在型別 'int' 的變數上運作
            //int m = 100;
            //foreach ( int n in m)
            {
            }

            // string也是一種字元的集合
            foreach (char s in "abcd")
            {
                listBoxShow.Items.Add(s);
            }
        }

        private void btnInt_Click(object sender, EventArgs e)
        {
            //Step1: Define Source
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //Step2: Define Query
            //不確定變數型態時 >> var
            //q 是一個  Iterator 物件, 陣列集合也是一個  Iterator 物件
            IEnumerable<int> q = from n in nums
                    where n %2 == 0 && (n>=5 && n<=10)
                    select n;

            //Step3: Excute Query
            //執行 iterator - 逐一查看集合的item
            foreach (int n in q)
            {
                listBoxShow.Items.Add(n);
            }
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // 此步驟僅定義q
            IEnumerable<int> q = from n in nums
                                 where isEven(n)
                                 select n;

            // 直到此步驟才會進入方法驗證n
            foreach (int n in q)
            {
                listBoxShow.Items.Add(n);
            }
        }

        private bool isEven(int n)
        {
            return n % 2 == 0;

            //if (n%2 == 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }

        private void btnAnyType_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // q 是一個  Iterator 物件，投射出的結果可以是任意型別  IEnumerable<T>
            IEnumerable<Point> q = from n in nums
                                 where isEven(n)
                                 select new Point(n, n*2);

            foreach (Point n in q)
            {
                listBoxShow.Items.Add(n);
            }

            // Excute Query ：q.ToXXX()
            List<Point> list = q.ToList();  // 背後運算 foreach( n in q) { list.add(n)}...
            
            dataGridView1.DataSource = list;

            // 將結果繫結在圖表上
            chart1.DataSource = list;
            chart1.Series[0].XValueMember = "X"; // X軸成員( X為Point的屬性)
            chart1.Series[0].YValueMembers = "Y"; // Y軸成員
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chart1.Series[0].Name = "Point";
            chart1.Series[0].Color = Color.Red;
        }

        private void btnString_Click(object sender, EventArgs e)
        {
            string[] words = { "xxx", "Apple", "xxxApple", "pineapple", "yyy", "yyyApple" };

            IEnumerable<string> q = from w in words
                                        //where w.Contains("Apple") 區分大小寫的搜尋
                                        where w.ToLower().Contains("apple") && w.Length > 5 //不區分大小寫，且字數>5
                                        select w;

            foreach (string w in q)
            {
                listBoxShow.Items.Add(w);
            }
        }

        private void btnNWProducts_Click(object sender, EventArgs e)
        {
            // 從資料庫抓資料
            // NWDataSet.ProductsRow >> 比DataRow強壯的型別，欄位名稱直接是屬性
            IEnumerable<NWDataSet.ProductsRow> p = from row in nwDataSet1.Products
                                                   //where !row.IsUnitPriceNull() 預防空值的判斷
                                                   where (row.UnitPrice > 30 && row.UnitPrice < 100) 
                                                   && row.ProductName.StartsWith("S") //產品名稱為S開頭(區分大小寫)
                                                   orderby row.ProductName
                                                   select row;

            dataGridView1.DataSource = p.ToList();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            IEnumerable<NWDataSet.OrdersRow> p = from row in nwDataSet1.Orders
                                                   where !row.IsOrderDateNull()
                                                   && (row.OrderDate.Year == 1997)
                                                   select row;

            dataGridView1.DataSource = p.ToList();
        }

        private void btnRef_Click(object sender, EventArgs e)
        {
            // 參考組件
            // System.Core
            // 命名空間
            // using System.Linq;
            // 類型
            // public static class Enumerable
        }

        private void btnAdvaned_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5 };

            //進階
            //var q = nums.Where(delegateObj).Select(delegateObj);
        }
    }
}
