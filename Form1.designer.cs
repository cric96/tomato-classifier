namespace _01_TextureClassification
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabPageTest = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.imageBoxTestCoOccMatrix = new Emgu.CV.UI.ImageBox();
            this.imageBoxTestInputImage = new Emgu.CV.UI.ImageBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.imageBoxCM_Wood = new Emgu.CV.UI.ImageBox();
            this.imageBoxCM_River = new Emgu.CV.UI.ImageBox();
            this.imageBoxCM_Houses = new Emgu.CV.UI.ImageBox();
            this.imageBoxCM_Fields = new Emgu.CV.UI.ImageBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBoxTestImagePath = new System.Windows.Forms.TextBox();
            this.buttonLoadTestImage = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.imageBoxCM_3 = new Emgu.CV.UI.ImageBox();
            this.imageBoxCM_2 = new Emgu.CV.UI.ImageBox();
            this.imageBoxCM_1 = new Emgu.CV.UI.ImageBox();
            this.imageBoxCM_0 = new Emgu.CV.UI.ImageBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxIdxFilePath = new System.Windows.Forms.TextBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.buttonTraining = new System.Windows.Forms.Button();
            this.tabControlTraining = new System.Windows.Forms.TabControl();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownK = new System.Windows.Forms.NumericUpDown();
            this.tabPageTest.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxTestCoOccMatrix)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxTestInputImage)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCM_Wood)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCM_River)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCM_Houses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCM_Fields)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCM_3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCM_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCM_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCM_0)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabControlTraining.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownK)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPageTest
            // 
            this.tabPageTest.Controls.Add(this.tableLayoutPanel3);
            this.tabPageTest.Location = new System.Drawing.Point(4, 22);
            this.tabPageTest.Name = "tabPageTest";
            this.tabPageTest.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTest.Size = new System.Drawing.Size(976, 723);
            this.tabPageTest.TabIndex = 1;
            this.tabPageTest.Text = "Test";
            this.tabPageTest.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 687F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(970, 717);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.imageBoxTestCoOccMatrix, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.imageBoxTestInputImage, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 33);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(479, 681);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // imageBoxTestCoOccMatrix
            // 
            this.imageBoxTestCoOccMatrix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBoxTestCoOccMatrix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxTestCoOccMatrix.Location = new System.Drawing.Point(3, 343);
            this.imageBoxTestCoOccMatrix.Name = "imageBoxTestCoOccMatrix";
            this.imageBoxTestCoOccMatrix.Size = new System.Drawing.Size(473, 335);
            this.imageBoxTestCoOccMatrix.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBoxTestCoOccMatrix.TabIndex = 3;
            this.imageBoxTestCoOccMatrix.TabStop = false;
            // 
            // imageBoxTestInputImage
            // 
            this.imageBoxTestInputImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBoxTestInputImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxTestInputImage.Location = new System.Drawing.Point(3, 3);
            this.imageBoxTestInputImage.Name = "imageBoxTestInputImage";
            this.imageBoxTestInputImage.Size = new System.Drawing.Size(473, 334);
            this.imageBoxTestInputImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBoxTestInputImage.TabIndex = 2;
            this.imageBoxTestInputImage.TabStop = false;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.70564F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.29436F));
            this.tableLayoutPanel5.Controls.Add(this.imageBoxCM_Wood, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.imageBoxCM_River, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.imageBoxCM_Houses, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.imageBoxCM_Fields, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label2, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.label3, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.label4, 1, 3);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(488, 33);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 4;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(479, 681);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // imageBoxCM_Wood
            // 
            this.imageBoxCM_Wood.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBoxCM_Wood.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxCM_Wood.Location = new System.Drawing.Point(3, 513);
            this.imageBoxCM_Wood.Name = "imageBoxCM_Wood";
            this.imageBoxCM_Wood.Size = new System.Drawing.Size(371, 165);
            this.imageBoxCM_Wood.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBoxCM_Wood.TabIndex = 7;
            this.imageBoxCM_Wood.TabStop = false;
            // 
            // imageBoxCM_River
            // 
            this.imageBoxCM_River.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBoxCM_River.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxCM_River.Location = new System.Drawing.Point(3, 343);
            this.imageBoxCM_River.Name = "imageBoxCM_River";
            this.imageBoxCM_River.Size = new System.Drawing.Size(371, 164);
            this.imageBoxCM_River.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBoxCM_River.TabIndex = 6;
            this.imageBoxCM_River.TabStop = false;
            // 
            // imageBoxCM_Houses
            // 
            this.imageBoxCM_Houses.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBoxCM_Houses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxCM_Houses.Location = new System.Drawing.Point(3, 173);
            this.imageBoxCM_Houses.Name = "imageBoxCM_Houses";
            this.imageBoxCM_Houses.Size = new System.Drawing.Size(371, 164);
            this.imageBoxCM_Houses.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBoxCM_Houses.TabIndex = 5;
            this.imageBoxCM_Houses.TabStop = false;
            // 
            // imageBoxCM_Fields
            // 
            this.imageBoxCM_Fields.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBoxCM_Fields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxCM_Fields.Location = new System.Drawing.Point(3, 3);
            this.imageBoxCM_Fields.Name = "imageBoxCM_Fields";
            this.imageBoxCM_Fields.Size = new System.Drawing.Size(371, 164);
            this.imageBoxCM_Fields.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBoxCM_Fields.TabIndex = 4;
            this.imageBoxCM_Fields.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(402, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fields";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(397, 247);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Houses";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(405, 417);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "River";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(403, 587);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Wood";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBoxTestImagePath);
            this.panel2.Controls.Add(this.buttonLoadTestImage);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(479, 24);
            this.panel2.TabIndex = 2;
            // 
            // textBoxTestImagePath
            // 
            this.textBoxTestImagePath.Location = new System.Drawing.Point(81, 1);
            this.textBoxTestImagePath.Name = "textBoxTestImagePath";
            this.textBoxTestImagePath.Size = new System.Drawing.Size(379, 20);
            this.textBoxTestImagePath.TabIndex = 1;
            // 
            // buttonLoadTestImage
            // 
            this.buttonLoadTestImage.Location = new System.Drawing.Point(0, 1);
            this.buttonLoadTestImage.Name = "buttonLoadTestImage";
            this.buttonLoadTestImage.Size = new System.Drawing.Size(75, 23);
            this.buttonLoadTestImage.TabIndex = 0;
            this.buttonLoadTestImage.Text = "Browse...";
            this.buttonLoadTestImage.UseVisualStyleBackColor = true;
            this.buttonLoadTestImage.Click += new System.EventHandler(this.buttonLoadTestImage_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(976, 723);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Training";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.077626F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.92237F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(970, 717);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.imageBoxCM_3, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.imageBoxCM_2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.imageBoxCM_1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.imageBoxCM_0, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 53);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(964, 661);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // imageBoxCM_3
            // 
            this.imageBoxCM_3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBoxCM_3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxCM_3.Location = new System.Drawing.Point(485, 333);
            this.imageBoxCM_3.Name = "imageBoxCM_3";
            this.imageBoxCM_3.Size = new System.Drawing.Size(476, 325);
            this.imageBoxCM_3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBoxCM_3.TabIndex = 5;
            this.imageBoxCM_3.TabStop = false;
            // 
            // imageBoxCM_2
            // 
            this.imageBoxCM_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBoxCM_2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxCM_2.Location = new System.Drawing.Point(3, 333);
            this.imageBoxCM_2.Name = "imageBoxCM_2";
            this.imageBoxCM_2.Size = new System.Drawing.Size(476, 325);
            this.imageBoxCM_2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBoxCM_2.TabIndex = 4;
            this.imageBoxCM_2.TabStop = false;
            // 
            // imageBoxCM_1
            // 
            this.imageBoxCM_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBoxCM_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxCM_1.Location = new System.Drawing.Point(485, 3);
            this.imageBoxCM_1.Name = "imageBoxCM_1";
            this.imageBoxCM_1.Size = new System.Drawing.Size(476, 324);
            this.imageBoxCM_1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBoxCM_1.TabIndex = 3;
            this.imageBoxCM_1.TabStop = false;
            // 
            // imageBoxCM_0
            // 
            this.imageBoxCM_0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBoxCM_0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBoxCM_0.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.PanAndZoom;
            this.imageBoxCM_0.Location = new System.Drawing.Point(3, 3);
            this.imageBoxCM_0.Name = "imageBoxCM_0";
            this.imageBoxCM_0.Size = new System.Drawing.Size(476, 324);
            this.imageBoxCM_0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBoxCM_0.TabIndex = 2;
            this.imageBoxCM_0.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.numericUpDownK);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.textBoxIdxFilePath);
            this.panel1.Controls.Add(this.buttonBrowse);
            this.panel1.Controls.Add(this.buttonTraining);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(964, 44);
            this.panel1.TabIndex = 1;
            // 
            // textBoxIdxFilePath
            // 
            this.textBoxIdxFilePath.Location = new System.Drawing.Point(20, 15);
            this.textBoxIdxFilePath.Name = "textBoxIdxFilePath";
            this.textBoxIdxFilePath.ReadOnly = true;
            this.textBoxIdxFilePath.Size = new System.Drawing.Size(642, 20);
            this.textBoxIdxFilePath.TabIndex = 2;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(668, 13);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowse.TabIndex = 1;
            this.buttonBrowse.Text = "Browse...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // buttonTraining
            // 
            this.buttonTraining.Location = new System.Drawing.Point(858, 13);
            this.buttonTraining.Name = "buttonTraining";
            this.buttonTraining.Size = new System.Drawing.Size(75, 23);
            this.buttonTraining.TabIndex = 0;
            this.buttonTraining.Text = "Training";
            this.buttonTraining.UseVisualStyleBackColor = true;
            this.buttonTraining.Click += new System.EventHandler(this.buttonTraining_Click);
            // 
            // tabControlTraining
            // 
            this.tabControlTraining.Controls.Add(this.tabPage1);
            this.tabControlTraining.Controls.Add(this.tabPageTest);
            this.tabControlTraining.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlTraining.Location = new System.Drawing.Point(0, 0);
            this.tabControlTraining.Name = "tabControlTraining";
            this.tabControlTraining.SelectedIndex = 0;
            this.tabControlTraining.Size = new System.Drawing.Size(984, 749);
            this.tabControlTraining.TabIndex = 0;
            this.tabControlTraining.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControlTraining_Selected);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(773, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "K:";
            // 
            // numericUpDownK
            // 
            this.numericUpDownK.Location = new System.Drawing.Point(796, 14);
            this.numericUpDownK.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownK.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownK.Name = "numericUpDownK";
            this.numericUpDownK.Size = new System.Drawing.Size(56, 20);
            this.numericUpDownK.TabIndex = 4;
            this.numericUpDownK.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 749);
            this.Controls.Add(this.tabControlTraining);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabPageTest.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxTestCoOccMatrix)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxTestInputImage)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCM_Wood)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCM_River)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCM_Houses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCM_Fields)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCM_3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCM_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCM_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCM_0)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControlTraining.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownK)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPageTest;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private Emgu.CV.UI.ImageBox imageBoxTestCoOccMatrix;
        private Emgu.CV.UI.ImageBox imageBoxTestInputImage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private Emgu.CV.UI.ImageBox imageBoxCM_Wood;
        private Emgu.CV.UI.ImageBox imageBoxCM_River;
        private Emgu.CV.UI.ImageBox imageBoxCM_Houses;
        private Emgu.CV.UI.ImageBox imageBoxCM_Fields;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBoxTestImagePath;
        private System.Windows.Forms.Button buttonLoadTestImage;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Emgu.CV.UI.ImageBox imageBoxCM_3;
        private Emgu.CV.UI.ImageBox imageBoxCM_2;
        private Emgu.CV.UI.ImageBox imageBoxCM_1;
        private Emgu.CV.UI.ImageBox imageBoxCM_0;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBoxIdxFilePath;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Button buttonTraining;
        private System.Windows.Forms.TabControl tabControlTraining;
        private System.Windows.Forms.NumericUpDown numericUpDownK;
        private System.Windows.Forms.Label label5;
    }
}

