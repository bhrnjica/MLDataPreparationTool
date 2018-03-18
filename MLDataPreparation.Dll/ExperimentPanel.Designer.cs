﻿namespace MLDataPreparation.Dll
{
    partial class ExperimentPanel
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
            //this.btnLoadTesting.Click -= btnLoadTrainig_Click;
            //this.btnLoadTesting.Click -= btnLoadTesting_Click;
            //this.btnSetToGP.Click -= btnLoadTesting_Click;

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.randomoizeDataSet = new System.Windows.Forms.CheckBox();
            this.numberRadio = new System.Windows.Forms.RadioButton();
            this.presentigeRadio = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.numCtrlNumForTest = new System.Windows.Forms.NumericUpDown();
            this.listView1 = new System.Windows.Forms.ListView();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCtrlNumForTest)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.listView1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(690, 541);
            this.tableLayoutPanel1.TabIndex = 28;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.randomoizeDataSet);
            this.groupBox5.Controls.Add(this.numberRadio);
            this.groupBox5.Controls.Add(this.presentigeRadio);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.numCtrlNumForTest);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox5.Location = new System.Drawing.Point(3, 474);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(320, 64);
            this.groupBox5.TabIndex = 26;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Testing data set";
            // 
            // randomoizeDataSet
            // 
            this.randomoizeDataSet.AutoSize = true;
            this.randomoizeDataSet.Location = new System.Drawing.Point(15, 44);
            this.randomoizeDataSet.Name = "randomoizeDataSet";
            this.randomoizeDataSet.Size = new System.Drawing.Size(120, 17);
            this.randomoizeDataSet.TabIndex = 18;
            this.randomoizeDataSet.Text = "Randomize data set";
            this.randomoizeDataSet.UseVisualStyleBackColor = true;
            // 
            // numberRadio
            // 
            this.numberRadio.AutoSize = true;
            this.numberRadio.Checked = true;
            this.numberRadio.Location = new System.Drawing.Point(156, 17);
            this.numberRadio.Margin = new System.Windows.Forms.Padding(2);
            this.numberRadio.Name = "numberRadio";
            this.numberRadio.Size = new System.Drawing.Size(144, 17);
            this.numberRadio.TabIndex = 17;
            this.numberRadio.TabStop = true;
            this.numberRadio.Text = "# for testing. (0-n/2 rows)";
            this.numberRadio.UseVisualStyleBackColor = true;
            // 
            // presentigeRadio
            // 
            this.presentigeRadio.AutoSize = true;
            this.presentigeRadio.Location = new System.Drawing.Point(156, 38);
            this.presentigeRadio.Margin = new System.Windows.Forms.Padding(2);
            this.presentigeRadio.Name = "presentigeRadio";
            this.presentigeRadio.Size = new System.Drawing.Size(123, 17);
            this.presentigeRadio.TabIndex = 16;
            this.presentigeRadio.Text = "% for testing. (0-50%)";
            this.presentigeRadio.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 21);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Select last :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // numCtrlNumForTest
            // 
            this.numCtrlNumForTest.Location = new System.Drawing.Point(74, 19);
            this.numCtrlNumForTest.Margin = new System.Windows.Forms.Padding(2);
            this.numCtrlNumForTest.Name = "numCtrlNumForTest";
            this.numCtrlNumForTest.Size = new System.Drawing.Size(78, 20);
            this.numCtrlNumForTest.TabIndex = 14;
            this.numCtrlNumForTest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(684, 465);
            this.listView1.TabIndex = 25;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            this.listView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDown);
            // 
            // ExperimentPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ExperimentPanel";
            this.Size = new System.Drawing.Size(690, 541);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCtrlNumForTest)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton numberRadio;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown numCtrlNumForTest;
        public System.Windows.Forms.CheckBox randomoizeDataSet;
        public System.Windows.Forms.RadioButton presentigeRadio;
    }
}
