namespace LinqLabs
{
    partial class FrmHelloLinq
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btnInt = new System.Windows.Forms.Button();
            this.btnString = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxShow = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnAdvaned = new System.Windows.Forms.Button();
            this.btnRef = new System.Windows.Forms.Button();
            this.btnEnumInt = new System.Windows.Forms.Button();
            this.btnEnumList = new System.Windows.Forms.Button();
            this.btnEnumString = new System.Windows.Forms.Button();
            this.btnDebug = new System.Windows.Forms.Button();
            this.btnAnyType = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnNWProducts = new System.Windows.Forms.Button();
            this.nwDataSet1 = new LinqLabs.NWDataSet();
            this.productsTableAdapter1 = new LinqLabs.NWDataSetTableAdapters.ProductsTableAdapter();
            this.btnOrders = new System.Windows.Forms.Button();
            this.ordersTableAdapter1 = new LinqLabs.NWDataSetTableAdapters.OrdersTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nwDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnInt
            // 
            this.btnInt.BackColor = System.Drawing.SystemColors.Control;
            this.btnInt.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnInt.ForeColor = System.Drawing.Color.Black;
            this.btnInt.Location = new System.Drawing.Point(166, 154);
            this.btnInt.Margin = new System.Windows.Forms.Padding(5);
            this.btnInt.Name = "btnInt";
            this.btnInt.Size = new System.Drawing.Size(362, 59);
            this.btnInt.TabIndex = 25;
            this.btnInt.Text = "a taste of Linq - int[]";
            this.btnInt.UseVisualStyleBackColor = false;
            this.btnInt.Click += new System.EventHandler(this.btnInt_Click);
            // 
            // btnString
            // 
            this.btnString.BackColor = System.Drawing.SystemColors.Control;
            this.btnString.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnString.ForeColor = System.Drawing.Color.Black;
            this.btnString.Location = new System.Drawing.Point(168, 361);
            this.btnString.Margin = new System.Windows.Forms.Padding(5);
            this.btnString.Name = "btnString";
            this.btnString.Size = new System.Drawing.Size(362, 59);
            this.btnString.TabIndex = 24;
            this.btnString.Text = "a taste of Linq - string[]";
            this.btnString.UseVisualStyleBackColor = false;
            this.btnString.Click += new System.EventHandler(this.btnString_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(165, 93);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(433, 100);
            this.label1.TabIndex = 23;
            this.label1.Text = "What is Linq \r\n\r\n統一資料存取 - 徹底融入語言";
            // 
            // listBoxShow
            // 
            this.listBoxShow.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxShow.FormattingEnabled = true;
            this.listBoxShow.ItemHeight = 19;
            this.listBoxShow.Location = new System.Drawing.Point(635, 155);
            this.listBoxShow.Margin = new System.Windows.Forms.Padding(5);
            this.listBoxShow.Name = "listBoxShow";
            this.listBoxShow.Size = new System.Drawing.Size(515, 118);
            this.listBoxShow.TabIndex = 26;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(163, 565);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 15);
            this.label9.TabIndex = 59;
            this.label9.Text = "Demo :";
            // 
            // btnAdvaned
            // 
            this.btnAdvaned.BackColor = System.Drawing.Color.Blue;
            this.btnAdvaned.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnAdvaned.ForeColor = System.Drawing.Color.White;
            this.btnAdvaned.Location = new System.Drawing.Point(168, 598);
            this.btnAdvaned.Margin = new System.Windows.Forms.Padding(5);
            this.btnAdvaned.Name = "btnAdvaned";
            this.btnAdvaned.Size = new System.Drawing.Size(313, 56);
            this.btnAdvaned.TabIndex = 60;
            this.btnAdvaned.Text = "Advanced";
            this.btnAdvaned.UseVisualStyleBackColor = false;
            this.btnAdvaned.Click += new System.EventHandler(this.btnAdvaned_Click);
            // 
            // btnRef
            // 
            this.btnRef.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnRef.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnRef.Location = new System.Drawing.Point(166, 35);
            this.btnRef.Margin = new System.Windows.Forms.Padding(5);
            this.btnRef.Name = "btnRef";
            this.btnRef.Size = new System.Drawing.Size(159, 42);
            this.btnRef.TabIndex = 61;
            this.btnRef.Text = "參考 / 匯入";
            this.btnRef.UseVisualStyleBackColor = false;
            this.btnRef.Click += new System.EventHandler(this.btnRef_Click);
            // 
            // btnEnumInt
            // 
            this.btnEnumInt.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnEnumInt.Location = new System.Drawing.Point(635, 44);
            this.btnEnumInt.Name = "btnEnumInt";
            this.btnEnumInt.Size = new System.Drawing.Size(195, 42);
            this.btnEnumInt.TabIndex = 62;
            this.btnEnumInt.Text = "IEnumerable<T> - int[]";
            this.btnEnumInt.UseVisualStyleBackColor = true;
            this.btnEnumInt.Click += new System.EventHandler(this.btnEnumInt_Click);
            // 
            // btnEnumList
            // 
            this.btnEnumList.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnEnumList.Location = new System.Drawing.Point(635, 105);
            this.btnEnumList.Name = "btnEnumList";
            this.btnEnumList.Size = new System.Drawing.Size(195, 42);
            this.btnEnumList.TabIndex = 63;
            this.btnEnumList.Text = "IEnumerable<T> - List<T>";
            this.btnEnumList.UseVisualStyleBackColor = true;
            this.btnEnumList.Click += new System.EventHandler(this.btnEnumList_Click);
            // 
            // btnEnumString
            // 
            this.btnEnumString.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnEnumString.Location = new System.Drawing.Point(872, 44);
            this.btnEnumString.Name = "btnEnumString";
            this.btnEnumString.Size = new System.Drawing.Size(219, 42);
            this.btnEnumString.TabIndex = 64;
            this.btnEnumString.Text = "IEnumerable<T> - string";
            this.btnEnumString.UseVisualStyleBackColor = true;
            this.btnEnumString.Click += new System.EventHandler(this.btnEnumString_Click);
            // 
            // btnDebug
            // 
            this.btnDebug.BackColor = System.Drawing.SystemColors.Control;
            this.btnDebug.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnDebug.ForeColor = System.Drawing.Color.Black;
            this.btnDebug.Location = new System.Drawing.Point(166, 223);
            this.btnDebug.Margin = new System.Windows.Forms.Padding(5);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(362, 59);
            this.btnDebug.TabIndex = 65;
            this.btnDebug.Text = "a taste of Linq - int[]\r\nIsEven(int) Debug";
            this.btnDebug.UseVisualStyleBackColor = false;
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // btnAnyType
            // 
            this.btnAnyType.BackColor = System.Drawing.SystemColors.Control;
            this.btnAnyType.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnAnyType.ForeColor = System.Drawing.Color.Black;
            this.btnAnyType.Location = new System.Drawing.Point(168, 292);
            this.btnAnyType.Margin = new System.Windows.Forms.Padding(5);
            this.btnAnyType.Name = "btnAnyType";
            this.btnAnyType.Size = new System.Drawing.Size(362, 59);
            this.btnAnyType.TabIndex = 66;
            this.btnAnyType.Text = "a taste of Linq - int[]\r\nIEnumerable<T>";
            this.btnAnyType.UseVisualStyleBackColor = false;
            this.btnAnyType.Click += new System.EventHandler(this.btnAnyType_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(635, 281);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(515, 193);
            this.dataGridView1.TabIndex = 67;
            // 
            // chart1
            // 
            chartArea3.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chart1.Legends.Add(legend3);
            this.chart1.Location = new System.Drawing.Point(635, 480);
            this.chart1.Name = "chart1";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chart1.Series.Add(series3);
            this.chart1.Size = new System.Drawing.Size(353, 174);
            this.chart1.TabIndex = 68;
            this.chart1.Text = "chart1";
            // 
            // btnNWProducts
            // 
            this.btnNWProducts.BackColor = System.Drawing.SystemColors.Info;
            this.btnNWProducts.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnNWProducts.ForeColor = System.Drawing.Color.Black;
            this.btnNWProducts.Location = new System.Drawing.Point(168, 430);
            this.btnNWProducts.Margin = new System.Windows.Forms.Padding(5);
            this.btnNWProducts.Name = "btnNWProducts";
            this.btnNWProducts.Size = new System.Drawing.Size(362, 59);
            this.btnNWProducts.TabIndex = 69;
            this.btnNWProducts.Text = "a taste of Linq - NW Products";
            this.btnNWProducts.UseVisualStyleBackColor = false;
            this.btnNWProducts.Click += new System.EventHandler(this.btnNWProducts_Click);
            // 
            // nwDataSet1
            // 
            this.nwDataSet1.DataSetName = "NWDataSet";
            this.nwDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // productsTableAdapter1
            // 
            this.productsTableAdapter1.ClearBeforeFill = true;
            // 
            // btnOrders
            // 
            this.btnOrders.BackColor = System.Drawing.SystemColors.Info;
            this.btnOrders.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnOrders.ForeColor = System.Drawing.Color.Black;
            this.btnOrders.Location = new System.Drawing.Point(168, 499);
            this.btnOrders.Margin = new System.Windows.Forms.Padding(5);
            this.btnOrders.Name = "btnOrders";
            this.btnOrders.Size = new System.Drawing.Size(362, 59);
            this.btnOrders.TabIndex = 70;
            this.btnOrders.Text = "a taste of Linq - NW Orders";
            this.btnOrders.UseVisualStyleBackColor = false;
            this.btnOrders.Click += new System.EventHandler(this.btnOrders_Click);
            // 
            // ordersTableAdapter1
            // 
            this.ordersTableAdapter1.ClearBeforeFill = true;
            // 
            // FrmHelloLinq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1162, 668);
            this.Controls.Add(this.btnOrders);
            this.Controls.Add(this.btnNWProducts);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnAnyType);
            this.Controls.Add(this.btnDebug);
            this.Controls.Add(this.btnEnumString);
            this.Controls.Add(this.btnEnumList);
            this.Controls.Add(this.btnEnumInt);
            this.Controls.Add(this.btnRef);
            this.Controls.Add(this.btnAdvaned);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.listBoxShow);
            this.Controls.Add(this.btnInt);
            this.Controls.Add(this.btnString);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmHelloLinq";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nwDataSet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInt;
        private System.Windows.Forms.Button btnString;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxShow;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnAdvaned;
        private System.Windows.Forms.Button btnRef;
        private System.Windows.Forms.Button btnEnumInt;
        private System.Windows.Forms.Button btnEnumList;
        private System.Windows.Forms.Button btnEnumString;
        private System.Windows.Forms.Button btnDebug;
        private System.Windows.Forms.Button btnAnyType;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button btnNWProducts;
        private NWDataSet nwDataSet1;
        private NWDataSetTableAdapters.ProductsTableAdapter productsTableAdapter1;
        private System.Windows.Forms.Button btnOrders;
        private NWDataSetTableAdapters.OrdersTableAdapter ordersTableAdapter1;
    }
}

