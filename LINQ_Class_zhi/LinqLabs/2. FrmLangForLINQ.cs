using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace Starter
{
    public partial class FrmLangForLINQ : Form
    {
        //Notes: LINQ 主要參考 
        //組件 System.Core.dll,
        //namespace {}  System.Linq
        //public static class Enumerable
        //
        //public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate);

        //1. 泛型 (泛用方法)                                                                         (ex.  void SwapAnyType<T>(ref T a, ref T b)
        //2. 委派參數 Lambda Expression (匿名方法簡潔版)               (ex.  MyWhere(nums, n => n %2==0);
        //3. Iterator                                                                                      (ex.  MyIterator)
        //4. 擴充方法                                                                                     (ex.  MyStringExtend.WordCount(s);
        public FrmLangForLINQ()
        {
            InitializeComponent();
           
        }
        int num1 = 100;
        int num2 = 200;
        string s1 = "aaa";
        string s2 = "bbb";
        Point p1 = new Point(10, 10);
        Point p2 = new Point(20, 20);
        private void button4_Click(object sender, EventArgs e)
        {
           MessageBox.Show($"num1= {num1} , num2 = {num2}");
            Swap(ref num1,ref num2);
            MessageBox.Show($"num1= {num1} , num2 = {num2}");

            MessageBox.Show($"s1 = {s1},s2 ={s2}");
            Swap(ref s1,ref s2);
            MessageBox.Show($"s1 = {s1},s2 ={s2}");

            
            MessageBox.Show($"p1 = {p1}, p2 = {p2}");
            Swap(ref p1, ref p2);
            MessageBox.Show($"p1 = {p1}, p2 = {p2}");
        }
        //ref傳址
        //使用多載會有太多相同名稱的方法
        private void Swap(ref int num1, ref int num2)
        {
            int temp = num2;
            num2 = num1;
            num1 = temp;
        }
        private void Swap(ref string s1, ref string s2)
        {
            string temp = s2;
            s2 = s1;
            s1 = temp;
        }
        private void Swap(ref Point p1, ref Point p2)
        {
            Point temp = p2;
            p2 = p1;
            p1 = temp;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"num1= {num1} , num2 = {num2}");
            //用object有轉型的問題
            //SwapObject(ref num1, ref num2);
            MessageBox.Show($"num1= {num1} , num2 = {num2}");

            MessageBox.Show($"s1 = {s1},s2 ={s2}");
            Swap(ref s1, ref s2);
            MessageBox.Show($"s1 = {s1},s2 ={s2}");


            MessageBox.Show($"p1 = {p1}, p2 = {p2}");
            Swap(ref p1, ref p2);
            MessageBox.Show($"p1 = {p1}, p2 = {p2}");
        }

        private void SwapObject(ref object o1, ref object o2)
        {
            object temp = o2;
            o2 = o1;
            o1 = temp;
        }

        private void button7_Click(object sender, EventArgs e)
        {
           
            MessageBox.Show($"num1= {num1} , num2 = {num2}");
            //<T>在call方法時可以省略
            SwapAnyType(ref num1,ref num2);
            MessageBox.Show($"num1= {num1} , num2 = {num2}");

           MessageBox.Show($"s1 = {s1},s2 ={s2}");
            SwapAnyType(ref s1, ref s2);
            MessageBox.Show($"s1 = {s1},s2 ={s2}");

            MessageBox.Show($"p1 = {p1}, p2 = {p2}");
            SwapAnyType(ref p1, ref p2);
            MessageBox.Show($"p1 = {p1}, p2 = {p2}");
        }

        //使用static就不用new去實作
        public static void SwapAnyType<T>(ref T i1, ref T i2)
        {
            T temp = i2;
            i2 = i1;
            i1 = temp;
            //要判斷點進來的實體用this
        }

        //先按了button2後才註冊buttonX的click事件
        private void button2_Click(object sender, EventArgs e)
        {
            //嚴重性 程式碼 說明 專案  檔案 行   隱藏項目狀態
            //錯誤  CS0123  'aaa' 沒有任何多載符合委派 'EventHandler'
            //this.buttonX.Click += aaa;
            this.buttonX.Click += bbb;
            this.buttonX.Click += ccc;

            //=================================
            //C#2.0匿名方法
            this.buttonX.Click += delegate (object sender1, EventArgs e1) { MessageBox.Show("C#2.0匿名方法"); };
            
            //=================================
            //C#3.0
            this.buttonX.Click += (object sender2, EventArgs e2) => MessageBox.Show("C#3.0匿名方法");
        }
        //private void aaa()
        //{
        //    //throw new NotImplementedException();
        //}

        private void ccc(object sender, EventArgs e)
        {
            MessageBox.Show("ccc");
        }

        private void bbb(object sender, EventArgs e)
        {
            MessageBox.Show("bbb");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bool r = Test(6);
            MessageBox.Show($"6 , result is greater than 5= {r}");
            
            //==============================
            //C#1.0具名方法 (Test,isEven...)
            MyDelegate myDelegate = new MyDelegate(Test);
            r = myDelegate(4);
            MessageBox.Show($"4 , delegate result is greater than 5 = {r}");
            //===============================
            myDelegate = new MyDelegate(isEven);
            r = myDelegate(7);
            MessageBox.Show($"7 , delegate result is even = {r}");

            //=================================
            //C#2.0匿名方法
            myDelegate = delegate (int n)
            {
                return n > 5;
            };
            r = myDelegate(10);
            MessageBox.Show($"10 , delegate result is greater than 5 = {r}");

            //=================================
            //C#3.0匿名方法簡潔版 Lambda expression
            myDelegate = n => n> 5;
            r = myDelegate(3);
            MessageBox.Show($"3 , delegate( Lambda expression) result is greater than 5 = {r}");
        
        
        }
        //step1: create delegate class
        //step2: create delegate object
        //step3: call method

        //s1
        public delegate bool MyDelegate(int n); 
        
        private bool Test(int n )
        {
            return n> 5;
        }

       
        bool isEven (int n)
        {
            return n % 2 == 0;
        }

        //此方法回傳一個 List<int>,帶進的參數為int[]nums,MyDelegate
        List<int> MyWhere(int[]nums,MyDelegate myDelegate)
        {
            List<int> list = new List<int>();
            foreach (int n in nums)
            {
                if (myDelegate(n))
                {
                    list.Add(n);
                }
            }
            return list;
        }

        IEnumerable<int> MyIterator(int[] nums, MyDelegate myDelegate)
        {
            
            foreach (int n in nums)
            {
                if (myDelegate(n))
                {
                    yield return n;
                }
            }
            
        }


        private void button10_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            //具名方法
            List<int > list = MyWhere(nums, Test);

            //匿名方法
            List<int> evenList = MyWhere(nums, n=>n%2 == 0);
            foreach(int n in evenList)
            {
                listBox1.Items.Add(n);
            }
            List<int> oddList = MyWhere(nums, n => n%2 ==1);
            foreach (int n in oddList)
            {
                listBox2.Items.Add(n);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //query source
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

            //query  define
            IEnumerable<int> q = MyIterator(nums,n => n%2 == 0);
            //IEnumerable<int> q = nums.Where(n =>  n%2 == 0);
            //exceute query
            foreach (int n in q)
            {
                listBox1.Items.Add(n);
            }


            //=========================
            string[] words = { "aaa","dddd","ff","eeeeeee","errtrt" };

            IEnumerable<string> w = words.Where(s => s.Length > 3);

            foreach (string s in w)
            {
                listBox2.Items.Add(s);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

            IEnumerable<int> q = nums.Where(n => n % 2 == 0);

            foreach (int n in q)
            {
                listBox1.Items.Add(n);
            }

            //=========================
            string[] words = { "aaa", "dddd", "ff", "eeeeeee", "errtrt" };

            IEnumerable<string> w = words.Where(s => s.Length > 3);

            foreach (string s in w)
            {
                listBox2.Items.Add(s);
            }

            //=========================
           

            IEnumerable<Point> r = nums.Where(n => n > 5).Select( n => new Point(n, n*n));

            dataGridView1.DataSource = r.ToList();
            
        }

        private void button45_Click(object sender, EventArgs e)
        {
            var n = 100;
            listBox1.Items.Add(n);

            //var n1 = 100;

            //var s = "abc";
            //s = s.ToUpper();

            //var p = new Point(100, 199);
            //this.listBox1.Items.Add(p.X);
            var p = new Point(50, 260);
            this.listBox1.Items.Add(p.X);
        }

        private void button41_Click(object sender, EventArgs e)
        {
            MyPoint pt0= new MyPoint();
            MyPoint pt1 = new MyPoint(100);
            MyPoint pt2 = new MyPoint(20,80);

            List<MyPoint> list = new List<MyPoint>();
            list.Add(pt0);
            list.Add(pt1);
            list.Add(pt2);
            list.Add(new MyPoint { P1 = 222, X = 50, Y = 80 });
            //*欄位(類別變數)不能被繫結,所以datagridview上看不到*
            list.Add(new MyPoint { P1 = 222, X = 50, Y = 80, P2 = 2000, Field1 ="aaa"}) ;
            dataGridView1.DataSource = list;
        }

        private void button43_Click(object sender, EventArgs e)
        {
            var pt1 = new { P1 = 111, P2 = 222, P3 = 333 };
            var pt2 = new { P1 = 222, P2 = 444, P3 = 666 };
            
            var pt3 = new { T = "aaa", R = "bbb", Y = "CCC" };
            //listBox1.Items.Add(pt1.GetType());
            //listBox1.Items.Add(pt2.GetType());
            //listBox1.Items.Add(pt3.GetType());

            // ================================
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //var q = from n in nums
            //        where n > 5
            //        select new { N = n, S = n * n, C = n * n * n };

            var q = nums.Where(n => n > 5).Select(n => new { N = n, S = n * n, C = n * n * n });

            //dataGridView1.DataSource = q.ToList();
            // ================================
            //SQL為資料來源
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);

            var r = from p in this.nwDataSet1.Products
                    where p.UnitPrice > 30
                    select new
                    {
                        ID = p.ProductID,
                        Name = p.ProductName,
                        UnitPrice = $"{p.UnitPrice:c2}",
                        p.UnitsInStock,
                        Toatl = $"{p.UnitPrice * p.UnitsInStock:c2}"
                    };
            dataGridView2.DataSource = r.ToList();
        }

        private void button40_Click(object sender, EventArgs e)
        {

        }

        private void button32_Click(object sender, EventArgs e)
        {
            string s = "dadfgr";

            //使用s.WordCount();更像是字串本身就有WordCount的擴充方法
            int count = s.WordCount(); // ==MyStringExtend.WordCount(s); 一般使用靜態類別的方法
            MessageBox.Show($"Count : {count}");

            s = "ggggggg";
            count = s.WordCount();
            MessageBox.Show($"Count : {count}");

            //=============================================
            string s1 = "wqefdsdsf";
            //使用字串的擴充方法Char來找出字串帶入的index的位置
            char ch = s1.Char(4);
            MessageBox.Show($"ch : {ch}");
        }
    }
}
public class MyPoint
{
    public int X {get; set;}
    public int Y { get; set; }

    public string Field1 = "aaa", Field2 = "bbb";
  
    private int m_p1;

   
    public int P2
    {
        get { return m_p1; }
        set { m_p1 = value; }
    }
    public int P1
    {
        get { return m_p1; }
        set { m_p1 = value; }
    }
    public MyPoint()
    {
     }
    public MyPoint(int p1 )
    {
        P1 = p1;
    }
    public MyPoint(int x , int y)
    {
        X = x;
        Y = y;
    }

}

//嚴重性 程式碼	說明	專案	檔案	行	隱藏項目狀態
//錯誤	CS0509	'MyString': 無法衍生自密封類型 'string'	LinqLabs
//使用繼承會有兩個錯誤:
//1.父類別為sealed無法被繼承
//2.使用的型別不一致
//public class MyString:String
//{

//}

//擴充方法的條件:
//1.必須是public 且 static 
//2.方法帶進的參數前面要加this
public static class MyStringExtend
{
    public static int WordCount(this string s)
    {
        return s.Length;
    }
    public static char Char(this string s,int index)
    {
        return s[index];
    }
}
