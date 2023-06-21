using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.LinkLabel;

namespace LinqLabs
{
    //public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate);

    //1. 泛型 (泛用方法)                                                                         (ex.  void SwapAnyType<T>(ref T a, ref T b)
    //2. 委派參數 Lambda Expression (匿名方法簡潔版)               (ex.  MyWhere(nums, n => n %2==0);
    //3. Iterator                                                                                      (ex.  MyIterator)
    //4. 擴充方法                                                                                     (ex.  MyStringExtend.WordCount(s);
    public partial class FrmLangForLINQ : Form
    {
        public FrmLangForLINQ()
        {
            InitializeComponent();

        }

        private void btnSwapInt_Click(object sender, EventArgs e)
        {
            int n1 = 100;
            int n2 = 200;

            MessageBox.Show(n1 + "," + n2);
           Swap(ref n1, ref n2);
            MessageBox.Show(n1 + "," + n2);

            //============

            string s1 = "aaa";
            string s2 = "bbb";

            MessageBox.Show(s1 + "," + s2);
            Swap(ref s1, ref s2);
            MessageBox.Show(s1 + "," + s2);
        }

        void Swap(ref int n1, ref int n2) 
        //傳址：在方法中運算，改變參數記憶體參考的位置
        {
            int temp = n1;
            n1 = n2;
            n2 = temp;
        }

        void Swap(ref string n1, ref string n2)
        {
            string temp = n1;
            n1 = n2;
            n2 = temp;
        }

        void Swap(ref Point n1, ref Point n2)
        {
            Point temp = n1;
            n1 = n2;
            n2 = temp;
        }

        // 要為了不同型別多載方法...
        // C# 1.0的作法
        void SwapObject(ref Object n1, ref Object n2)
        {
            Object temp = n1;
            n1 = n2;
            n2 = temp;
        }

        private void btnSwapObj_Click(object sender, EventArgs e)
        {
            // 傳入傳出都需要轉型，耗效能～
            int n1 = 100;
            int n2 = 200;

            MessageBox.Show(n1 + "," + n2);
            //SwapObject()
            MessageBox.Show(n1 + "," + n2);
        }

        // C# 2.0：泛型<T>
        void SwapAnyType<T>(ref T n1, ref T n2)
        {
            T temp = n1;
            n1 = n2;
            n2 = temp;
        }

        private void btnSwapGeneric_Click(object sender, EventArgs e)
        {
            int n1 = 100;
            int n2 = 200;

            MessageBox.Show(n1 + "," + n2);
            SwapAnyType<int>(ref n1, ref n2);
            MessageBox.Show(n1 + "," + n2);

            //============

            string s1 = "aaa";
            string s2 = "bbb";

            MessageBox.Show(s1 + "," + s2);
            SwapAnyType(ref s1, ref s2); // 由系統推斷型別：可省略<>
            MessageBox.Show(s1 + "," + s2);
        }

        private void btnDelegate_Click(object sender, EventArgs e)
        {
            //錯誤  CS0123  'aaa' 沒有任何多載符合委派 'EventHandler'
            //this.buttonX.Click += aaa;

            this.buttonX.Click += bbb;
            this.buttonX.Click += ccc;
            //=============
            //C#2.0 匿名方法
            this.buttonX.Click += delegate (object sender1, EventArgs e1)
                                                        {
                                                                MessageBox.Show("C#2.0 匿名方法");
                                                        };
            //C#3.0
            this.buttonX.Click += (object sender1, EventArgs e1) =>
                                                        {
                                                            MessageBox.Show("C#3.0 Lambda");
                                                        };
        }

        void aaa()
        {
        }
        void bbb(object sender, EventArgs e)
        {
            MessageBox.Show("bbb");
        }
        void ccc(object sender, EventArgs e)
        {
            MessageBox.Show("ccc");
        }

        bool Test(int n)
        {
            return n > 5;
        }
        bool IsEven(int n)
        {
            return (n % 2 == 0);
        }

        // 委派三步驟
        // Step1: Create Delegate Class 宣告委派型別（定義比照方法的型別、參數）
        // Step2: Create Delegate Object 宣告委派物件，指向方法
        // Step3: Call Method 呼叫方法

        // Step1
        public delegate bool MyDelegate(int n);

        private void btnCallTest_Click(object sender, EventArgs e)
        {
            bool result = Test(6);
            MessageBox.Show("result: " +  result);

            //==========

            //C#1.0 具名方法
            // Step2
            MyDelegate delegateObj = new MyDelegate(Test);
            // Step3
            result = delegateObj(4);
            MessageBox.Show("result: " + result);

            //==========

            delegateObj = IsEven; // new MyDelegate(IsEven)
            result = delegateObj(5); //delegateObj.Invoke(5)
            MessageBox.Show("result: " + result);

            //==========

            //C#2.0 匿名方法：適合一次性使用的方法
            delegateObj = delegate (int n)
                                                {
                                                    return n > 5;
                                                };
            result = delegateObj(10);
            MessageBox.Show("C#2.0 匿名方法\nresult: " + result);

            //==========

            // C#3.0 Lambda Expression ： 簡潔版匿名方法
            delegateObj = n => n > 5;
            result = delegateObj(10);
            MessageBox.Show("C#3.0 Lambda Expression\nresult: " + result);
        }

        List<int> MyWhere(int[] nums, MyDelegate delegateObj)
        {
            List<int> list = new List<int>();
            foreach (int n in nums)
            {
                if (delegateObj(n))
                {
                    list.Add(n);
                }
            }
            return list;
        }

        IEnumerable<int> MyIterator(int[] nums, MyDelegate delegateObj)
        {
            foreach (int n in nums)
            {
                if (delegateObj(n))
                {
                    yield return n;
                }
            }
        }

        private void btnMyWhere_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            List<int> listTest = MyWhere(nums, Test); //匿名方法：MyWhere(nums, n => n>5)

            List<int> evenList = MyWhere(nums, n => n%2==0);  //具名方法：MyWhere(nums, IsEven)
            List<int> oddList = MyWhere(nums, n => n%2 == 1);

            foreach (int n in listTest)
            {
                listBox1.Items.Add(n);
            }

            foreach (int n in evenList)
            {
                listBox2.Items.Add(n);
            }
        }

        private void btnMyIterator_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            IEnumerable<int> q = MyIterator(nums, n => n%2==0); //僅定義

            foreach (int n in q) //直到這行才會執行方法
            {
                listBox1.Items.Add(n);
            }
        }

        private void btnLinq_Click(object sender, EventArgs e)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //IEnumerable<int> q = nums.Where<int>(n => n > 5);
            IEnumerable<Point> q = nums.Where<int>(n => n>5).Select(n => new Point(n,n*n));

            foreach (Point n in q)
            {
                listBox1.Items.Add(n);
            }

            dataGridView1.DataSource = q.ToList();

            //=============

            string[] words = { "aaa", "bbbbbb", "cccc", "d" };

            IEnumerable<string> q2 = words.Where(n => n.Length > 3);

            foreach (string word in q2)
            {
                listBox2.Items.Add(word);
            }
        }

        private void btnVar_Click(object sender, EventArgs e)
        {
            //var 能依照值推斷型別，編譯時直接可以使用所屬型別的方法
            var n = 100;

            var s = "aaa";
            s.ToLower();

            var p = new Point(100, 200);
            listBox1.Items.Add(p.X);
        }

        private void btnInitializer_Click(object sender, EventArgs e)
        {
            Font f = new Font("Times", 15); 
            // 當一個類別有許多參數，建構子方法就會有很多排列組合(多載)

            //建構子方法
            MyPoint p00 =new MyPoint(); 
            MyPoint p01 =new MyPoint(100);
            MyPoint p02 = new MyPoint(10, 10);

            List<MyPoint> list = new List<MyPoint>();
            list.Add(p00);
            list.Add(p01);
            list.Add(p02);
            //物件初始化
            list.Add(new MyPoint { field1 = "xxx", P1 = 1111, X=333 });
            list.Add(new MyPoint { field2 = "yyyyy", X = 11, Y = 22 });

            dataGridView1.DataSource =list; // 類別變數無法繫結，只會出現屬性

            //============

            List<MyPoint> list2 = new List<MyPoint>()
            {
                new MyPoint{X=9,Y=99},
                new MyPoint{P1=123}
            };
            dataGridView2.DataSource = list2;
        }

        private void btnAnonymousType_Click(object sender, EventArgs e)
        {
            // 匿名型別
            var pt1 = new { P1 = 100, P2 = 200 };
            var pt2 = new { P1 = 200, P2 = 300 };

            listBox1.Items.Add(pt1.GetType());
            listBox1.Items.Add(pt2.GetType());

            var pt3 = new {X=1,Y=2,Z=3};
            listBox1.Items.Add(pt3.GetType());

            //====================
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //var q = from n in nums
            //        where n > 5
            //        select new { N = n, Square = n*n, Cube = n*n*n };

            var q = nums.Where(n => n > 5).Select(n => new { N = n, Square = n * n, Cube = n * n * n });

            dataGridView1.DataSource = q.ToList();

            //==================
            productsTableAdapter1.Fill(nwDataSet1.Products);

            //var q2 = from p in nwDataSet1.Products
            //         where p.UnitPrice > 30
            //         select new
            //         {
            //             ID = p.ProductID,
            //             p.ProductName,
            //             p.UnitPrice,
            //             p.UnitsInStock,
            //             TotalPrice = $"{p.UnitPrice * p.UnitsInStock:c2}"
            //         };

            var q2 = nwDataSet1.Products.Where(p => p.UnitPrice > 30)
                .Select(p => new
                {
                    ID = p.ProductID,
                    p.ProductName, //當欄位值=欄位名稱，可給屬性名稱
                    p.UnitPrice,
                    p.UnitsInStock,
                    TotalPrice = $"{p.UnitPrice * p.UnitsInStock:c2}"
                });

            dataGridView2.DataSource = q2.ToList();
        }

        private void btnExtensionMethods_Click(object sender, EventArgs e)
        {
            string s = "abcde";
            int count = s.WordCount();
            MessageBox.Show("count: " + count);

            //============

            string s1 = "123456789";
            count = s1.WordCount();
            //count = MyStringExtend.WordCount(s1);
            MessageBox.Show("count: " + count);

            //============

            char ch = s1.Chars(2);
            MessageBox.Show("ch: " + ch);
        }
    }

    class MyPoint
    {
        public MyPoint()
        {

        }
        public MyPoint(int p1)
        {
            this.P1 = p1;
        }
        public MyPoint(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public string field1, field2;

        private int m_P1; // 屬性背後的私有變數
        public int P1
        {
            get
            {
                return m_P1;
            }
            set
            {
                m_P1 = value;
            }
        }

        public int X
        {
            get; set;
        }

        public int Y
        {
            get; set;
        }
    }


}

//public class MyString : String
//{
//    錯誤 CS0509	'MyString': 無法衍生自密封類型 'string'
//}

public static class MyStringExtend //靜態類別
{
    public static int WordCount(this string s) //靜態方法，型別+this
    {
        return s.Length;
    }

    public static char Chars(this string s, int index) 
    {
        return s[index];
    }
}