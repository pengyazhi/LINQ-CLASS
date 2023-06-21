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
    public partial class FrmHomeWork02 : Form
    {
        public FrmHomeWork02()
        {
            InitializeComponent();

            productPhotoTableAdapter1.Fill(awDataSet1.ProductPhoto);
            LoadYearToCmbBox();
            LoadPeriodToCalender();
            cmbBoxQuarterly.SelectedIndex = 0;
        }

        void LoadYearToCmbBox()
        {
            IEnumerable<int> q = (from n in awDataSet1.ProductPhoto
                                 orderby n.ModifiedDate
                                 select n.ModifiedDate.Year).Distinct();

            foreach (var n in q)
            {
                cmbBoxYear.Items.Add(n);
            }

            cmbBoxYear.SelectedIndex = 0;
        }

        void LoadPeriodToCalender()
        {
            DateTime qMin = awDataSet1.ProductPhoto.Select(x => x.ModifiedDate).Min();
            DateTime qMax = awDataSet1.ProductPhoto.Select(x => x.ModifiedDate).Max();
            dateTimePickerBegin.MinDate = dateTimePickerEnd.MinDate = qMin;
            dateTimePickerBegin.MaxDate =dateTimePickerEnd.MaxDate =  qMax;

            dateTimePickerBegin.Value = qMin;
            dateTimePickerEnd.Value = qMax;
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            dataGridViewMaster.DataSource = awDataSet1.ProductPhoto;
        }

        private void dataGridViewMaster_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridViewMaster.Rows[e.RowIndex];

            if (e.ColumnIndex == 1)
            {
                byte[] bytes = (byte[])row.Cells["ThumbNailPhoto"].Value;
                MemoryStream ms = new MemoryStream(bytes);
                picBoxDetails.Image = Image.FromStream(ms);
            }
            else
            {
                byte[] bytes = (byte[])row.Cells["LargePhoto"].Value;
                MemoryStream ms = new MemoryStream(bytes);
                picBoxDetails.Image = Image.FromStream(ms);
            }
        }

        private void btnPeriod_Click(object sender, EventArgs e)
        {
            var q = awDataSet1.ProductPhoto.Where(n => n.ModifiedDate < dateTimePickerEnd.Value && n.ModifiedDate > dateTimePickerBegin.Value);

            dataGridViewMaster.DataSource = q.ToList();
        }

        private void btnYear_Click(object sender, EventArgs e)
        {
            var q = awDataSet1.ProductPhoto.Where(n => n.ModifiedDate.Year == (int)cmbBoxYear.SelectedItem);
            dataGridViewMaster.DataSource = q.ToList();
        }

        private void btnQuarterly_Click(object sender, EventArgs e)
        {
            var q = awDataSet1.ProductPhoto.Where(n =>
                                                           n.ModifiedDate.Year == (int)cmbBoxYear.SelectedItem &&
                                                           WhichQuarterly(n.ModifiedDate) == cmbBoxQuarterly.SelectedItem.ToString());

            dataGridViewMaster.DataSource = q.ToList();

            MessageBox.Show($"{cmbBoxYear.SelectedItem}年{cmbBoxQuarterly.SelectedItem}\n" +
                $"共{q.Count()}筆");
        }

        string WhichQuarterly(DateTime date)
        {
            if (date.Month == 1 || date.Month == 2 || date.Month == 3)
            {
                return "第一季";
            }
            else if (date.Month == 4 || date.Month == 5 || date.Month == 6)
            {
                return "第二季";
            }
            else if (date.Month == 7 || date.Month == 8 || date.Month == 9)
            {
                return "第三季";
            }
            else
            {
                return "第四季";
            }
        }

    }
}
