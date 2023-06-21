using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs.HomeWork
{
    public partial class FrmHomeWork01 : Form
    {
        public FrmHomeWork01()
        {
            InitializeComponent();

            ordersTableAdapter1.Fill(nwDataSet1.Orders);
            order_DetailsTableAdapter1.Fill(nwDataSet1.Order_Details);
            productsTableAdapter1.Fill(nwDataSet1.Products);

            LoadYearToCmbBox();
        }

        private void LoadYearToCmbBox()
        {
            IEnumerable<NWDataSet.OrdersRow> q = from row in nwDataSet1.Orders
                    orderby row.OrderDate
                    select row;

            //q.Distinct().ToList();

            foreach (NWDataSet.OrdersRow row in q)
            {
                if (!cmbBoxYear.Items.Contains(row.OrderDate.Year))
                    cmbBoxYear.Items.Add(row.OrderDate.Year);
            }
            cmbBoxYear.SelectedIndex = 0;
        }

        private void btnFileLog_Click(object sender, EventArgs e)
        {
            lblMaster.Text = "C:\\Windows";
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"C:\Windows");
            FileInfo[] files = dir.GetFiles();

            var q = from f in files
                    where f.Extension == ".log"
                    select f;

            dataGridViewOrders.DataSource = q.ToList();
        }
        private void btnFile2019_Click(object sender, EventArgs e)
        {
            lblMaster.Text = "C:\\Windows";
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"C:\Windows");
            FileInfo[] files = dir.GetFiles();

            var q = from f in files
                    where f.CreationTime.Year == 2019
                    select f;

            dataGridViewOrders.DataSource = q.ToList();
        }
        private void btnFileLarge_Click(object sender, EventArgs e)
        {
            lblMaster.Text = "C:\\Windows";
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"C:\Windows");
            FileInfo[] files = dir.GetFiles();

            var q = from f in files
                    where f.Length > 1000000
                    orderby f.Length
                    select f;

            dataGridViewOrders.DataSource = q.ToList();
        }



        private void btnOrderDetail_Click(object sender, EventArgs e)
        {
            lblMaster.Text = "訂單";
            if (cmbBoxYear.SelectedIndex == 0)
            {
                return;
            }

            var q = from row in nwDataSet1.Orders
                    where !row.IsShipRegionNull()
                    && row.OrderDate.Year == (int)cmbBoxYear.SelectedItem
                    select row;

            bindingSource1.DataSource = q.ToList();
            dataGridViewOrders.DataSource = bindingSource1;
        }

        private void btnShowOrders_Click(object sender, EventArgs e)
        {
            lblMaster.Text = "訂單";
            bindingSource1.DataSource = nwDataSet1.Orders;
            dataGridViewOrders.DataSource = bindingSource1;
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            lblMaster.Text = "訂單";
            NWDataSet.OrdersRow row = bindingSource1.Current as NWDataSet.OrdersRow;
            
            if (row != null)
            {
            var q = from od in nwDataSet1.Order_Details
                    where od.OrderID == row.OrderID
                    select od;

            dataGridViewOrderDetails.DataSource = q.ToList();
            }

        }

        private void cmbBoxYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMaster.Text = "訂單";
            if (cmbBoxYear.SelectedIndex == 0)
            {
                return;
            }

            var q = from row in nwDataSet1.Orders
                    where !row.IsShipRegionNull()
                    && row.OrderDate.Year == (int)cmbBoxYear.SelectedItem
                    select row;

            bindingSource1.DataSource = q.ToList();
            dataGridViewOrders.DataSource = bindingSource1;
        }

        int potision = 0;
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (potision < 0)
            {
                potision = 0;
            }
            else
            {
                potision--;
            }

            var q = from row in nwDataSet1.Products
                    where !row.IsSupplierIDNull()
                    orderby row.ProductID
                    select row;

            if (int.TryParse(txtPage.Text, out int num))
            {
                dataGridViewOrderDetails.DataSource = q.Skip(num * potision).Take(num).ToList();
            }
            else
            {
                MessageBox.Show("請輸入數值");
                txtPage.Focus();
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            var q = from row in nwDataSet1.Products
                    where !row.IsSupplierIDNull()
                    orderby row.ProductID
                    select row;


            if (int.TryParse(txtPage.Text, out int num))
            {
                if (potision>=(q.Count()/num))
                {
                    potision = q.Count() / num;
                }
                else
                {
                    potision++;
                }
                dataGridViewOrderDetails.DataSource = q.Skip(num * potision).Take(num).ToList();
            }
            else
            {
                MessageBox.Show("請輸入數值");
                txtPage.Focus();
            }
        }
    }
}
