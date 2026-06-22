namespace SistemPendataanHewan
{
    partial class Form5
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtIDHewan = new System.Windows.Forms.TextBox();
            this.txtNamaHewan = new System.Windows.Forms.TextBox();
            this.txtUmur = new System.Windows.Forms.TextBox();
            this.txtWarna = new System.Windows.Forms.TextBox();
            this.cmbJenisKelamin = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.txtIDPemilik = new System.Windows.Forms.TextBox();
            this.cmbJenisHewan = new System.Windows.Forms.ComboBox();
            this.cmbRas = new System.Windows.Forms.ComboBox();
            this.btnImportExcel = new System.Windows.Forms.Button();
            this.btnImportDatabase = new System.Windows.Forms.Button();
            this.picFoto = new System.Windows.Forms.PictureBox();
            this.btnUploadFoto = new System.Windows.Forms.Button();
            this.btnHapusFoto = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFoto)).BeginInit();
            this.SuspendLayout();

            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label1.Location = new System.Drawing.Point(30, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID Hewan";

            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(30, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "ID Pemilik";

            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label3.Location = new System.Drawing.Point(30, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nama Hewan";

            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label4.Location = new System.Drawing.Point(30, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 23);
            this.label4.TabIndex = 3;
            this.label4.Text = "Jenis Hewan";

            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label5.Location = new System.Drawing.Point(30, 208);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 23);
            this.label5.TabIndex = 4;
            this.label5.Text = "Ras";

            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(30, 248);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 23);
            this.label6.TabIndex = 5;
            this.label6.Text = "Jenis Kelamin";

            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label7.Location = new System.Drawing.Point(30, 288);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 23);
            this.label7.TabIndex = 6;
            this.label7.Text = "Umur";

            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label8.Location = new System.Drawing.Point(30, 328);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 23);
            this.label8.TabIndex = 7;
            this.label8.Text = "Warna";

            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label9.Location = new System.Drawing.Point(525, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 23);
            this.label9.TabIndex = 8;
            this.label9.Text = "Foto";

            this.txtIDHewan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIDHewan.Location = new System.Drawing.Point(170, 46);
            this.txtIDHewan.Name = "txtIDHewan";
            this.txtIDHewan.ReadOnly = true;
            this.txtIDHewan.Size = new System.Drawing.Size(280, 29);
            this.txtIDHewan.TabIndex = 9;

            this.txtNamaHewan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNamaHewan.Location = new System.Drawing.Point(170, 126);
            this.txtNamaHewan.Name = "txtNamaHewan";
            this.txtNamaHewan.Size = new System.Drawing.Size(280, 29);
            this.txtNamaHewan.TabIndex = 11;

            this.txtUmur.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUmur.Location = new System.Drawing.Point(170, 286);
            this.txtUmur.Name = "txtUmur";
            this.txtUmur.Size = new System.Drawing.Size(280, 29);
            this.txtUmur.TabIndex = 15;

            this.txtWarna.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWarna.Location = new System.Drawing.Point(170, 326);
            this.txtWarna.Name = "txtWarna";
            this.txtWarna.Size = new System.Drawing.Size(280, 29);
            this.txtWarna.TabIndex = 16;

            this.cmbJenisKelamin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJenisKelamin.FormattingEnabled = true;
            this.cmbJenisKelamin.Location = new System.Drawing.Point(170, 245);
            this.cmbJenisKelamin.Name = "cmbJenisKelamin";
            this.cmbJenisKelamin.Size = new System.Drawing.Size(280, 29);
            this.cmbJenisKelamin.TabIndex = 14;

            this.btnConnect.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnConnect.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnConnect.Location = new System.Drawing.Point(470, 86);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(140, 35);
            this.btnConnect.TabIndex = 18;
            this.btnConnect.Text = "Membuka Koneksi";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);

            this.btnLoad.BackColor = System.Drawing.Color.PeachPuff;
            this.btnLoad.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnLoad.Location = new System.Drawing.Point(470, 126);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(140, 35);
            this.btnLoad.TabIndex = 19;
            this.btnLoad.Text = "Menampilkan Data";
            this.btnLoad.UseVisualStyleBackColor = false;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);

            this.btnInsert.BackColor = System.Drawing.Color.LightGreen;
            this.btnInsert.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnInsert.Location = new System.Drawing.Point(470, 166);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(140, 35);
            this.btnInsert.TabIndex = 20;
            this.btnInsert.Text = "Menambah Data";
            this.btnInsert.UseVisualStyleBackColor = false;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);

            this.btnUpdate.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.Location = new System.Drawing.Point(470, 206);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(140, 35);
            this.btnUpdate.TabIndex = 21;
            this.btnUpdate.Text = "Mengubah Data";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);

            this.btnDelete.BackColor = System.Drawing.Color.LightCoral;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnDelete.Location = new System.Drawing.Point(470, 246);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(140, 35);
            this.btnDelete.TabIndex = 22;
            this.btnDelete.Text = "Menghapus Data";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(30, 380);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(740, 190);
            this.dataGridView1.TabIndex = 27;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);

            this.bindingNavigator1.AddNewItem = null;
            this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator1.DeleteItem = null;
            this.bindingNavigator1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem});
            this.bindingNavigator1.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator1.Size = new System.Drawing.Size(790, 27);
            this.bindingNavigator1.TabIndex = 28;
            this.bindingNavigator1.Text = "bindingNavigator1";

            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(45, 24);
            this.bindingNavigatorCountItem.Text = "of {0}";

            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(29, 24);

            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(29, 24);

            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 27);

            this.bindingNavigatorPositionItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(100, 27);
            this.bindingNavigatorPositionItem.Text = "0";

            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 27);

            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(29, 24);

            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(29, 24);

            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 27);

            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(29, 24);

            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(29, 24);

            this.txtIDPemilik.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIDPemilik.Location = new System.Drawing.Point(170, 86);
            this.txtIDPemilik.Name = "txtIDPemilik";
            this.txtIDPemilik.Size = new System.Drawing.Size(280, 29);
            this.txtIDPemilik.TabIndex = 10;
            this.txtIDPemilik.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HanyaAngka_KeyPress);

            this.cmbJenisHewan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJenisHewan.FormattingEnabled = true;
            this.cmbJenisHewan.Location = new System.Drawing.Point(170, 166);
            this.cmbJenisHewan.Name = "cmbJenisHewan";
            this.cmbJenisHewan.Size = new System.Drawing.Size(280, 29);
            this.cmbJenisHewan.TabIndex = 12;

            this.cmbRas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRas.FormattingEnabled = true;
            this.cmbRas.Location = new System.Drawing.Point(170, 205);
            this.cmbRas.Name = "cmbRas";
            this.cmbRas.Size = new System.Drawing.Size(280, 29);
            this.cmbRas.TabIndex = 13;

            this.btnImportExcel.BackColor = System.Drawing.Color.LightYellow;
            this.btnImportExcel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnImportExcel.Location = new System.Drawing.Point(470, 286);
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.Size = new System.Drawing.Size(140, 35);
            this.btnImportExcel.TabIndex = 23;
            this.btnImportExcel.Text = "Import Excel";
            this.btnImportExcel.UseVisualStyleBackColor = false;
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);

            this.btnImportDatabase.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.btnImportDatabase.Enabled = false;
            this.btnImportDatabase.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnImportDatabase.Location = new System.Drawing.Point(616, 286);
            this.btnImportDatabase.Name = "btnImportDatabase";
            this.btnImportDatabase.Size = new System.Drawing.Size(140, 35);
            this.btnImportDatabase.TabIndex = 24;
            this.btnImportDatabase.Text = "Import to DB";
            this.btnImportDatabase.UseVisualStyleBackColor = false;
            this.btnImportDatabase.Click += new System.EventHandler(this.btnImportDatabase_Click);

            this.picFoto.BackColor = System.Drawing.Color.WhiteSmoke;
            this.picFoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picFoto.Location = new System.Drawing.Point(616, 128);
            this.picFoto.Name = "picFoto";
            this.picFoto.Size = new System.Drawing.Size(140, 140);
            this.picFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picFoto.TabIndex = 17;
            this.picFoto.TabStop = false;
            this.picFoto.Click += new System.EventHandler(this.picFoto_Click);

            this.btnUploadFoto.BackColor = System.Drawing.Color.LightBlue;
            this.btnUploadFoto.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnUploadFoto.Location = new System.Drawing.Point(616, 48);
            this.btnUploadFoto.Name = "btnUploadFoto";
            this.btnUploadFoto.Size = new System.Drawing.Size(140, 35);
            this.btnUploadFoto.TabIndex = 25;
            this.btnUploadFoto.Text = "Upload Foto";
            this.btnUploadFoto.UseVisualStyleBackColor = false;
            this.btnUploadFoto.Click += new System.EventHandler(this.btnUploadFoto_Click);

            this.btnHapusFoto.BackColor = System.Drawing.Color.LightCoral;
            this.btnHapusFoto.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnHapusFoto.Location = new System.Drawing.Point(616, 88);
            this.btnHapusFoto.Name = "btnHapusFoto";
            this.btnHapusFoto.Size = new System.Drawing.Size(140, 35);
            this.btnHapusFoto.TabIndex = 26;
            this.btnHapusFoto.Text = "Hapus Foto";
            this.btnHapusFoto.UseVisualStyleBackColor = false;
            this.btnHapusFoto.Click += new System.EventHandler(this.btnHapusFoto_Click);

            this.openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            this.openFileDialog1.Title = "Pilih Foto Hewan";

            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(790, 590);
            this.Controls.Add(this.bindingNavigator1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnHapusFoto);
            this.Controls.Add(this.btnUploadFoto);
            this.Controls.Add(this.picFoto);
            this.Controls.Add(this.btnImportDatabase);
            this.Controls.Add(this.btnImportExcel);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.cmbJenisKelamin);
            this.Controls.Add(this.cmbJenisHewan);
            this.Controls.Add(this.cmbRas);
            this.Controls.Add(this.txtWarna);
            this.Controls.Add(this.txtUmur);
            this.Controls.Add(this.txtNamaHewan);
            this.Controls.Add(this.txtIDPemilik);
            this.Controls.Add(this.txtIDHewan);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Name = "Form5";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form Data Hewan";
            this.Load += new System.EventHandler(this.Form5_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFoto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtIDHewan;
        private System.Windows.Forms.TextBox txtNamaHewan;
        private System.Windows.Forms.TextBox txtUmur;
        private System.Windows.Forms.TextBox txtWarna;
        private System.Windows.Forms.ComboBox cmbJenisKelamin;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.TextBox txtIDPemilik;
        private System.Windows.Forms.ComboBox cmbJenisHewan;
        private System.Windows.Forms.ComboBox cmbRas;
        private System.Windows.Forms.Button btnImportExcel;
        private System.Windows.Forms.Button btnImportDatabase;
        private System.Windows.Forms.PictureBox picFoto;
        private System.Windows.Forms.Button btnUploadFoto;
        private System.Windows.Forms.Button btnHapusFoto;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}