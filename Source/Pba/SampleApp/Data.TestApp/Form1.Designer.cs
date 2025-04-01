namespace SIT.Components.Data.TestApp {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing ) {
            if( disposing && ( components != null ) ) {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.firstnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.streetDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zIPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.view1BindingSource = new System.Windows.Forms.BindingSource( this.components );
            this.sIT_TESTDataSet = new SIT.Components.Data.TestApp.SIT_TESTDataSet();
            this.button2 = new System.Windows.Forms.Button();
            this.view_1TableAdapter = new SIT.Components.Data.TestApp.SIT_TESTDataSetTableAdapters.View_1TableAdapter();
            this.button3 = new System.Windows.Forms.Button();
            ( (System.ComponentModel.ISupportInitialize)( this.dataGridView1 ) ).BeginInit();
            ( (System.ComponentModel.ISupportInitialize)( this.view1BindingSource ) ).BeginInit();
            ( (System.ComponentModel.ISupportInitialize)( this.sIT_TESTDataSet ) ).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point( 12, 12 );
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size( 75, 23 );
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler( this.button1_Click );
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange( new System.Windows.Forms.DataGridViewColumn[] {
            this.firstnameDataGridViewTextBoxColumn,
            this.lastnameDataGridViewTextBoxColumn,
            this.streetDataGridViewTextBoxColumn,
            this.zIPDataGridViewTextBoxColumn,
            this.cityDataGridViewTextBoxColumn} );
            this.dataGridView1.DataSource = this.view1BindingSource;
            this.dataGridView1.Location = new System.Drawing.Point( 12, 41 );
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size( 628, 273 );
            this.dataGridView1.TabIndex = 1;
            // 
            // firstnameDataGridViewTextBoxColumn
            // 
            this.firstnameDataGridViewTextBoxColumn.DataPropertyName = "Firstname";
            this.firstnameDataGridViewTextBoxColumn.HeaderText = "Firstname";
            this.firstnameDataGridViewTextBoxColumn.Name = "firstnameDataGridViewTextBoxColumn";
            // 
            // lastnameDataGridViewTextBoxColumn
            // 
            this.lastnameDataGridViewTextBoxColumn.DataPropertyName = "Lastname";
            this.lastnameDataGridViewTextBoxColumn.HeaderText = "Lastname";
            this.lastnameDataGridViewTextBoxColumn.Name = "lastnameDataGridViewTextBoxColumn";
            // 
            // streetDataGridViewTextBoxColumn
            // 
            this.streetDataGridViewTextBoxColumn.DataPropertyName = "Street";
            this.streetDataGridViewTextBoxColumn.HeaderText = "Street";
            this.streetDataGridViewTextBoxColumn.Name = "streetDataGridViewTextBoxColumn";
            // 
            // zIPDataGridViewTextBoxColumn
            // 
            this.zIPDataGridViewTextBoxColumn.DataPropertyName = "ZIP";
            this.zIPDataGridViewTextBoxColumn.HeaderText = "ZIP";
            this.zIPDataGridViewTextBoxColumn.Name = "zIPDataGridViewTextBoxColumn";
            // 
            // cityDataGridViewTextBoxColumn
            // 
            this.cityDataGridViewTextBoxColumn.DataPropertyName = "City";
            this.cityDataGridViewTextBoxColumn.HeaderText = "City";
            this.cityDataGridViewTextBoxColumn.Name = "cityDataGridViewTextBoxColumn";
            // 
            // view1BindingSource
            // 
            this.view1BindingSource.DataMember = "View_1";
            this.view1BindingSource.DataSource = this.sIT_TESTDataSet;
            // 
            // sIT_TESTDataSet
            // 
            this.sIT_TESTDataSet.DataSetName = "SIT_TESTDataSet";
            this.sIT_TESTDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point( 93, 12 );
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size( 75, 23 );
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler( this.button2_Click );
            // 
            // view_1TableAdapter
            // 
            this.view_1TableAdapter.ClearBeforeFill = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point( 238, 12 );
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size( 75, 23 );
            this.button3.TabIndex = 3;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler( this.button3_Click );
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 652, 326 );
            this.Controls.Add( this.button3 );
            this.Controls.Add( this.button2 );
            this.Controls.Add( this.dataGridView1 );
            this.Controls.Add( this.button1 );
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler( this.Form1_Load );
            ( (System.ComponentModel.ISupportInitialize)( this.dataGridView1 ) ).EndInit();
            ( (System.ComponentModel.ISupportInitialize)( this.view1BindingSource ) ).EndInit();
            ( (System.ComponentModel.ISupportInitialize)( this.sIT_TESTDataSet ) ).EndInit();
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button2;
        private SIT_TESTDataSet sIT_TESTDataSet;
        private System.Windows.Forms.BindingSource view1BindingSource;
        private SIT.Components.Data.TestApp.SIT_TESTDataSetTableAdapters.View_1TableAdapter view_1TableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn firstnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn streetDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn zIPDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cityDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button button3;
    }
}

