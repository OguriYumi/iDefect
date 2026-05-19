namespace iDefect
{
    partial class Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtCsvFile1 = new TextBox();
            btnSelect1 = new Button();
            label1 = new Label();
            label2 = new Label();
            btnSelect2 = new Button();
            txtCsvFile2 = new TextBox();
            gro_overlap = new GroupBox();
            label20 = new Label();
            label19 = new Label();
            label18 = new Label();
            txtXshift1 = new TextBox();
            label17 = new Label();
            txtYoverlap = new TextBox();
            txtXoverlap = new TextBox();
            txtXshift2 = new TextBox();
            label6 = new Label();
            label7 = new Label();
            label4 = new Label();
            label9 = new Label();
            gro_out = new GroupBox();
            label23 = new Label();
            label24 = new Label();
            label22 = new Label();
            label21 = new Label();
            panOpt = new Panel();
            optDesc = new RadioButton();
            optAsc = new RadioButton();
            txtYlimitEnd = new TextBox();
            label15 = new Label();
            txtXlimitEnd = new TextBox();
            label16 = new Label();
            txtYlimitStart = new TextBox();
            label11 = new Label();
            txtXlimitStart = new TextBox();
            label12 = new Label();
            label10 = new Label();
            txt2DCend = new TextBox();
            label5 = new Label();
            txt2DCstart = new TextBox();
            label3 = new Label();
            btnRun = new Button();
            label8 = new Label();
            btnClose = new Button();
            gro_removal = new GroupBox();
            chkDefectKind8 = new CheckBox();
            chkDefectKind7 = new CheckBox();
            chkDefectKind6 = new CheckBox();
            chkDefectKind5 = new CheckBox();
            chkDefectKind4 = new CheckBox();
            chkDefectKind3 = new CheckBox();
            chkDefectKind2 = new CheckBox();
            chkDefectKind1 = new CheckBox();
            label13 = new Label();
            txtShippingFileName = new TextBox();
            label14 = new Label();
            gro_overlap.SuspendLayout();
            gro_out.SuspendLayout();
            panOpt.SuspendLayout();
            gro_removal.SuspendLayout();
            SuspendLayout();
            // 
            // txtCsvFile1
            // 
            txtCsvFile1.ForeColor = SystemColors.GrayText;
            txtCsvFile1.Location = new Point(45, 112);
            txtCsvFile1.Margin = new Padding(4);
            txtCsvFile1.Name = "txtCsvFile1";
            txtCsvFile1.Size = new Size(605, 29);
            txtCsvFile1.TabIndex = 0;
            txtCsvFile1.Text = "CSVファイルを選択してください";
            txtCsvFile1.Enter += txtCsvFile1_Enter;
            txtCsvFile1.Leave += txtCsvFile1_Leave;
            // 
            // btnSelect1
            // 
            btnSelect1.Location = new Point(663, 112);
            btnSelect1.Margin = new Padding(4);
            btnSelect1.Name = "btnSelect1";
            btnSelect1.Size = new Size(70, 32);
            btnSelect1.TabIndex = 1;
            btnSelect1.Text = "選択";
            btnSelect1.UseVisualStyleBackColor = true;
            btnSelect1.Click += btnSelect1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(45, 88);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(74, 21);
            label1.TabIndex = 99;
            label1.Text = "工程１：";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(45, 146);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(74, 21);
            label2.TabIndex = 99;
            label2.Text = "工程２：";
            // 
            // btnSelect2
            // 
            btnSelect2.Location = new Point(663, 170);
            btnSelect2.Margin = new Padding(4);
            btnSelect2.Name = "btnSelect2";
            btnSelect2.Size = new Size(70, 32);
            btnSelect2.TabIndex = 3;
            btnSelect2.Text = "選択";
            btnSelect2.UseVisualStyleBackColor = true;
            btnSelect2.Click += btnSelect2_Click;
            // 
            // txtCsvFile2
            // 
            txtCsvFile2.ForeColor = SystemColors.GrayText;
            txtCsvFile2.Location = new Point(45, 170);
            txtCsvFile2.Margin = new Padding(4);
            txtCsvFile2.Name = "txtCsvFile2";
            txtCsvFile2.Size = new Size(605, 29);
            txtCsvFile2.TabIndex = 2;
            txtCsvFile2.Text = "CSVファイルを選択してください";
            txtCsvFile2.Enter += txtCsvFile2_Enter;
            txtCsvFile2.Leave += txtCsvFile2_Leave;
            // 
            // gro_overlap
            // 
            gro_overlap.Controls.Add(label20);
            gro_overlap.Controls.Add(label19);
            gro_overlap.Controls.Add(label18);
            gro_overlap.Controls.Add(txtXshift1);
            gro_overlap.Controls.Add(label17);
            gro_overlap.Controls.Add(txtYoverlap);
            gro_overlap.Controls.Add(txtXoverlap);
            gro_overlap.Controls.Add(txtXshift2);
            gro_overlap.Controls.Add(label6);
            gro_overlap.Controls.Add(label7);
            gro_overlap.Controls.Add(label4);
            gro_overlap.Controls.Add(label9);
            gro_overlap.Location = new Point(39, 211);
            gro_overlap.Name = "gro_overlap";
            gro_overlap.Size = new Size(790, 60);
            gro_overlap.TabIndex = 4;
            gro_overlap.TabStop = false;
            gro_overlap.Text = "重複判定";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label20.Location = new Point(650, 33);
            label20.Margin = new Padding(4, 0, 4, 0);
            label20.Name = "label20";
            label20.Size = new Size(27, 15);
            label20.TabIndex = 99;
            label20.Text = "mm";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label19.Location = new Point(504, 33);
            label19.Margin = new Padding(4, 0, 4, 0);
            label19.Name = "label19";
            label19.Size = new Size(27, 15);
            label19.TabIndex = 99;
            label19.Text = "mm";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label18.Location = new Point(361, 33);
            label18.Margin = new Padding(4, 0, 4, 0);
            label18.Name = "label18";
            label18.Size = new Size(27, 15);
            label18.TabIndex = 99;
            label18.Text = "mm";
            // 
            // txtXshift1
            // 
            txtXshift1.Location = new Point(103, 24);
            txtXshift1.MaxLength = 7;
            txtXshift1.Name = "txtXshift1";
            txtXshift1.Size = new Size(62, 29);
            txtXshift1.TabIndex = 5;
            txtXshift1.Text = "-9999.9";
            txtXshift1.TextAlign = HorizontalAlignment.Right;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label17.Location = new Point(165, 33);
            label17.Margin = new Padding(4, 0, 4, 0);
            label17.Name = "label17";
            label17.Size = new Size(27, 15);
            label17.TabIndex = 99;
            label17.Text = "mm";
            // 
            // txtYoverlap
            // 
            txtYoverlap.Location = new Point(620, 24);
            txtYoverlap.MaxLength = 2;
            txtYoverlap.Name = "txtYoverlap";
            txtYoverlap.Size = new Size(30, 29);
            txtYoverlap.TabIndex = 8;
            txtYoverlap.Text = "99";
            txtYoverlap.TextAlign = HorizontalAlignment.Right;
            // 
            // txtXoverlap
            // 
            txtXoverlap.Location = new Point(474, 24);
            txtXoverlap.MaxLength = 2;
            txtXoverlap.Name = "txtXoverlap";
            txtXoverlap.Size = new Size(30, 29);
            txtXoverlap.TabIndex = 7;
            txtXoverlap.Text = "99";
            txtXoverlap.TextAlign = HorizontalAlignment.Right;
            // 
            // txtXshift2
            // 
            txtXshift2.Location = new Point(299, 24);
            txtXshift2.MaxLength = 7;
            txtXshift2.Name = "txtXshift2";
            txtXshift2.Size = new Size(62, 29);
            txtXshift2.TabIndex = 6;
            txtXshift2.Text = "-9999.9";
            txtXshift2.TextAlign = HorizontalAlignment.Right;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(555, 27);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(67, 21);
            label6.TabIndex = 99;
            label6.Text = "Y距離：";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(410, 27);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(67, 21);
            label7.TabIndex = 99;
            label7.Text = "X距離：";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 27);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(95, 21);
            label4.TabIndex = 99;
            label4.Text = "Xシフト量1：";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(208, 27);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(95, 21);
            label9.TabIndex = 99;
            label9.Text = "Xシフト量2：";
            // 
            // gro_out
            // 
            gro_out.Controls.Add(label23);
            gro_out.Controls.Add(label24);
            gro_out.Controls.Add(label22);
            gro_out.Controls.Add(label21);
            gro_out.Controls.Add(panOpt);
            gro_out.Controls.Add(txtYlimitEnd);
            gro_out.Controls.Add(label15);
            gro_out.Controls.Add(txtXlimitEnd);
            gro_out.Controls.Add(label16);
            gro_out.Controls.Add(txtYlimitStart);
            gro_out.Controls.Add(label11);
            gro_out.Controls.Add(txtXlimitStart);
            gro_out.Controls.Add(label12);
            gro_out.Controls.Add(label10);
            gro_out.Controls.Add(txt2DCend);
            gro_out.Controls.Add(label5);
            gro_out.Controls.Add(txt2DCstart);
            gro_out.Controls.Add(label3);
            gro_out.Location = new Point(39, 390);
            gro_out.Name = "gro_out";
            gro_out.Size = new Size(790, 100);
            gro_out.TabIndex = 18;
            gro_out.TabStop = false;
            gro_out.Text = "出力加工";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label23.Location = new Point(757, 71);
            label23.Margin = new Padding(4, 0, 4, 0);
            label23.Name = "label23";
            label23.Size = new Size(27, 15);
            label23.TabIndex = 99;
            label23.Text = "mm";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label24.Location = new Point(757, 36);
            label24.Margin = new Padding(4, 0, 4, 0);
            label24.Name = "label24";
            label24.Size = new Size(27, 15);
            label24.TabIndex = 99;
            label24.Text = "mm";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label22.Location = new Point(525, 71);
            label22.Margin = new Padding(4, 0, 4, 0);
            label22.Name = "label22";
            label22.Size = new Size(27, 15);
            label22.TabIndex = 99;
            label22.Text = "mm";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label21.Location = new Point(525, 36);
            label21.Margin = new Padding(4, 0, 4, 0);
            label21.Name = "label21";
            label21.Size = new Size(27, 15);
            label21.TabIndex = 99;
            label21.Text = "mm";
            // 
            // panOpt
            // 
            panOpt.Controls.Add(optDesc);
            panOpt.Controls.Add(optAsc);
            panOpt.Location = new Point(109, 61);
            panOpt.Name = "panOpt";
            panOpt.Size = new Size(160, 32);
            panOpt.TabIndex = 23;
            // 
            // optDesc
            // 
            optDesc.AutoSize = true;
            optDesc.Location = new Point(84, 3);
            optDesc.Name = "optDesc";
            optDesc.Size = new Size(60, 25);
            optDesc.TabIndex = 25;
            optDesc.Text = "降順";
            optDesc.UseVisualStyleBackColor = true;
            optDesc.CheckedChanged += radioButton_CheckedChanged;
            // 
            // optAsc
            // 
            optAsc.AutoSize = true;
            optAsc.Checked = true;
            optAsc.Location = new Point(7, 3);
            optAsc.Name = "optAsc";
            optAsc.Size = new Size(80, 25);
            optAsc.TabIndex = 24;
            optAsc.TabStop = true;
            optAsc.Text = "昇順 ／";
            optAsc.UseVisualStyleBackColor = true;
            optAsc.CheckedChanged += radioButton_CheckedChanged;
            // 
            // txtYlimitEnd
            // 
            txtYlimitEnd.Location = new Point(707, 63);
            txtYlimitEnd.MaxLength = 4;
            txtYlimitEnd.Name = "txtYlimitEnd";
            txtYlimitEnd.Size = new Size(50, 29);
            txtYlimitEnd.TabIndex = 27;
            txtYlimitEnd.Text = "9999";
            txtYlimitEnd.TextAlign = HorizontalAlignment.Right;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(563, 66);
            label15.Margin = new Padding(4, 0, 4, 0);
            label15.Name = "label15";
            label15.Size = new Size(147, 21);
            label15.TabIndex = 99;
            label15.Text = "Y座標限度値終了：";
            // 
            // txtXlimitEnd
            // 
            txtXlimitEnd.Location = new Point(707, 28);
            txtXlimitEnd.MaxLength = 5;
            txtXlimitEnd.Name = "txtXlimitEnd";
            txtXlimitEnd.Size = new Size(50, 29);
            txtXlimitEnd.TabIndex = 22;
            txtXlimitEnd.Text = "-9999";
            txtXlimitEnd.TextAlign = HorizontalAlignment.Right;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(563, 31);
            label16.Margin = new Padding(4, 0, 4, 0);
            label16.Name = "label16";
            label16.Size = new Size(147, 21);
            label16.TabIndex = 99;
            label16.Text = "X座標限度値終了：";
            // 
            // txtYlimitStart
            // 
            txtYlimitStart.Location = new Point(474, 63);
            txtYlimitStart.MaxLength = 4;
            txtYlimitStart.Name = "txtYlimitStart";
            txtYlimitStart.Size = new Size(50, 29);
            txtYlimitStart.TabIndex = 26;
            txtYlimitStart.Text = "9999";
            txtYlimitStart.TextAlign = HorizontalAlignment.Right;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(330, 66);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(147, 21);
            label11.TabIndex = 99;
            label11.Text = "Y座標限度値開始：";
            // 
            // txtXlimitStart
            // 
            txtXlimitStart.Location = new Point(474, 28);
            txtXlimitStart.MaxLength = 5;
            txtXlimitStart.Name = "txtXlimitStart";
            txtXlimitStart.Size = new Size(50, 29);
            txtXlimitStart.TabIndex = 21;
            txtXlimitStart.Text = "-9999";
            txtXlimitStart.TextAlign = HorizontalAlignment.Right;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(330, 31);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(147, 21);
            label12.TabIndex = 99;
            label12.Text = "X座標限度値開始：";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(12, 66);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(106, 21);
            label10.TabIndex = 99;
            label10.Text = "データ並び順：";
            // 
            // txt2DCend
            // 
            txt2DCend.Location = new Point(268, 28);
            txt2DCend.MaxLength = 4;
            txt2DCend.Name = "txt2DCend";
            txt2DCend.Size = new Size(46, 29);
            txt2DCend.TabIndex = 20;
            txt2DCend.Text = "9999";
            txt2DCend.TextAlign = HorizontalAlignment.Right;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(168, 31);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(104, 21);
            label5.TabIndex = 99;
            label5.Text = "2DC標終了：";
            // 
            // txt2DCstart
            // 
            txt2DCstart.Location = new Point(112, 28);
            txt2DCstart.MaxLength = 4;
            txt2DCstart.Name = "txt2DCstart";
            txt2DCstart.Size = new Size(46, 29);
            txt2DCstart.TabIndex = 19;
            txt2DCstart.Text = "9999";
            txt2DCstart.TextAlign = HorizontalAlignment.Right;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 31);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(104, 21);
            label3.TabIndex = 99;
            label3.Text = "2DC標開始：";
            // 
            // btnRun
            // 
            btnRun.Location = new Point(595, 567);
            btnRun.Margin = new Padding(4);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(110, 50);
            btnRun.TabIndex = 29;
            btnRun.Text = "実行";
            btnRun.UseVisualStyleBackColor = true;
            btnRun.Click += btnRun_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 128);
            label8.Location = new Point(39, 33);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(409, 21);
            label8.TabIndex = 99;
            label8.Text = "表面欠点情報をもとに、分離・重複欠点ファイルを出力します。";
            // 
            // btnClose
            // 
            btnClose.Location = new Point(713, 567);
            btnClose.Margin = new Padding(4);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(110, 50);
            btnClose.TabIndex = 30;
            btnClose.Text = "閉じる";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // gro_removal
            // 
            gro_removal.Controls.Add(chkDefectKind8);
            gro_removal.Controls.Add(chkDefectKind7);
            gro_removal.Controls.Add(chkDefectKind6);
            gro_removal.Controls.Add(chkDefectKind5);
            gro_removal.Controls.Add(chkDefectKind4);
            gro_removal.Controls.Add(chkDefectKind3);
            gro_removal.Controls.Add(chkDefectKind2);
            gro_removal.Controls.Add(chkDefectKind1);
            gro_removal.Location = new Point(39, 280);
            gro_removal.Name = "gro_removal";
            gro_removal.Size = new Size(790, 100);
            gro_removal.TabIndex = 9;
            gro_removal.TabStop = false;
            gro_removal.Text = "除去加工";
            // 
            // chkDefectKind8
            // 
            chkDefectKind8.AutoSize = true;
            chkDefectKind8.Enabled = false;
            chkDefectKind8.Location = new Point(581, 64);
            chkDefectKind8.Name = "chkDefectKind8";
            chkDefectKind8.Size = new Size(173, 25);
            chkDefectKind8.TabIndex = 17;
            chkDefectKind8.Text = "XXXXXXXXXXXXXXXX";
            chkDefectKind8.UseVisualStyleBackColor = true;
            // 
            // chkDefectKind7
            // 
            chkDefectKind7.AutoSize = true;
            chkDefectKind7.Enabled = false;
            chkDefectKind7.Location = new Point(393, 64);
            chkDefectKind7.Name = "chkDefectKind7";
            chkDefectKind7.Size = new Size(173, 25);
            chkDefectKind7.TabIndex = 16;
            chkDefectKind7.Text = "XXXXXXXXXXXXXXXX";
            chkDefectKind7.UseVisualStyleBackColor = true;
            // 
            // chkDefectKind6
            // 
            chkDefectKind6.AutoSize = true;
            chkDefectKind6.Enabled = false;
            chkDefectKind6.Location = new Point(205, 64);
            chkDefectKind6.Name = "chkDefectKind6";
            chkDefectKind6.Size = new Size(173, 25);
            chkDefectKind6.TabIndex = 15;
            chkDefectKind6.Text = "XXXXXXXXXXXXXXXX";
            chkDefectKind6.UseVisualStyleBackColor = true;
            // 
            // chkDefectKind5
            // 
            chkDefectKind5.AutoSize = true;
            chkDefectKind5.Location = new Point(17, 64);
            chkDefectKind5.Name = "chkDefectKind5";
            chkDefectKind5.Size = new Size(173, 25);
            chkDefectKind5.TabIndex = 14;
            chkDefectKind5.Text = "XXXXXXXXXXXXXXXX";
            chkDefectKind5.UseVisualStyleBackColor = true;
            // 
            // chkDefectKind4
            // 
            chkDefectKind4.AutoSize = true;
            chkDefectKind4.Location = new Point(581, 33);
            chkDefectKind4.Name = "chkDefectKind4";
            chkDefectKind4.Size = new Size(105, 25);
            chkDefectKind4.TabIndex = 13;
            chkDefectKind4.Text = "欠点テスト3";
            chkDefectKind4.UseVisualStyleBackColor = true;
            // 
            // chkDefectKind3
            // 
            chkDefectKind3.AutoSize = true;
            chkDefectKind3.Location = new Point(393, 33);
            chkDefectKind3.Name = "chkDefectKind3";
            chkDefectKind3.Size = new Size(105, 25);
            chkDefectKind3.TabIndex = 12;
            chkDefectKind3.Text = "欠点テスト2";
            chkDefectKind3.UseVisualStyleBackColor = true;
            // 
            // chkDefectKind2
            // 
            chkDefectKind2.AutoSize = true;
            chkDefectKind2.Location = new Point(205, 33);
            chkDefectKind2.Name = "chkDefectKind2";
            chkDefectKind2.Size = new Size(105, 25);
            chkDefectKind2.TabIndex = 11;
            chkDefectKind2.Text = "欠点テスト1";
            chkDefectKind2.UseVisualStyleBackColor = true;
            // 
            // chkDefectKind1
            // 
            chkDefectKind1.AutoSize = true;
            chkDefectKind1.Location = new Point(17, 33);
            chkDefectKind1.Name = "chkDefectKind1";
            chkDefectKind1.Size = new Size(93, 25);
            chkDefectKind1.TabIndex = 10;
            chkDefectKind1.Text = "欠点種類";
            chkDefectKind1.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(45, 505);
            label13.Margin = new Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new Size(119, 21);
            label13.TabIndex = 99;
            label13.Text = "出荷ファイル名：";
            // 
            // txtShippingFileName
            // 
            txtShippingFileName.ForeColor = SystemColors.GrayText;
            txtShippingFileName.Location = new Point(45, 529);
            txtShippingFileName.Margin = new Padding(4);
            txtShippingFileName.Name = "txtShippingFileName";
            txtShippingFileName.Size = new Size(300, 29);
            txtShippingFileName.TabIndex = 28;
            txtShippingFileName.Text = "出荷ファイル名を入力してください";
            txtShippingFileName.Enter += txtShippingFileName_Enter;
            txtShippingFileName.Leave += txtShippingFileName_Leave;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(345, 535);
            label14.Margin = new Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new Size(35, 21);
            label14.TabIndex = 99;
            label14.Text = ".csv";
            // 
            // Form
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(864, 651);
            Controls.Add(label14);
            Controls.Add(label13);
            Controls.Add(txtShippingFileName);
            Controls.Add(gro_removal);
            Controls.Add(btnClose);
            Controls.Add(label8);
            Controls.Add(btnRun);
            Controls.Add(gro_out);
            Controls.Add(gro_overlap);
            Controls.Add(label2);
            Controls.Add(btnSelect2);
            Controls.Add(txtCsvFile2);
            Controls.Add(label1);
            Controls.Add(btnSelect1);
            Controls.Add(txtCsvFile1);
            Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "iDefect";
            gro_overlap.ResumeLayout(false);
            gro_overlap.PerformLayout();
            gro_out.ResumeLayout(false);
            gro_out.PerformLayout();
            panOpt.ResumeLayout(false);
            panOpt.PerformLayout();
            gro_removal.ResumeLayout(false);
            gro_removal.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtCsvFile1;
        private Button btnSelect1;
        private Label label1;
        private Label label2;
        private Button btnSelect2;
        private TextBox txtCsvFile2;
        private GroupBox gro_overlap;
        private TextBox txtYoverlap;
        private Label label6;
        private TextBox txtXoverlap;
        private Label label7;
        private GroupBox gro_out;
        private TextBox txt2DCstart;
        private Label label3;
        private TextBox txtXshift1;
        private Label label4;
        private TextBox txt2DCend;
        private Label label5;
        private Button btnRun;
        private Label label8;
        private Button btnClose;
        private GroupBox gro_removal;
        private CheckBox chkDefectKind8;
        private CheckBox chkDefectKind7;
        private CheckBox chkDefectKind6;
        private CheckBox chkDefectKind5;
        private CheckBox chkDefectKind4;
        private CheckBox chkDefectKind3;
        private CheckBox chkDefectKind2;
        private CheckBox chkDefectKind1;
        private TextBox txtXshift2;
        private Label label9;
        private TextBox txtYlimitStart;
        private Label label11;
        private TextBox txtXlimitStart;
        private Label label12;
        private Label label10;
        private Label label13;
        private TextBox txtShippingFileName;
        private Label label14;
        private TextBox txtYlimitEnd;
        private Label label15;
        private TextBox txtXlimitEnd;
        private Label label16;
        private Panel panOpt;
        private RadioButton optAsc;
        private RadioButton optDesc;
        private Label label20;
        private Label label19;
        private Label label18;
        private Label label17;
        private Label label23;
        private Label label24;
        private Label label22;
        private Label label21;
    }
}
