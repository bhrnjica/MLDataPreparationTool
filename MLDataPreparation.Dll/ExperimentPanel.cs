//////////////////////////////////////////////////////////////////////////////////////////
// GPdotNET - Genetic Programming Tool                                                  //
// Copyright 2006-2017 Bahrudin Hrnjica                                                 //
//                                                                                      //
// This code is free software under the GNU Library General Public License (LGPL)       //
// See license section of  https://github.com/bhrnjica/gpdotnet/blob/master/license.md  //
//                                                                                      //
// Bahrudin Hrnjica                                                                     //
// bhrnjica@hotmail.com                                                                 //
// Bihac,Bosnia and Herzegovina                                                         //
// http://bhrnjica.wordpress.com                                                        //
//////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using DataProcessing.MLData;
using DataProcessing.MLBasicTypes;

namespace MLDataPreparation.Dll
{

    /// <summary>
    /// Main panel for loading and defining experimental dataA
    /// </summary>
    public partial class ExperimentPanel : UserControl
    {

        #region Ctor and Fields
        //listview items
        private ListViewItem li;
        private int X = 0;
        private int Y = 0;
        private int subItemSelected = 0;
        private System.Windows.Forms.ComboBox cmbBox = new System.Windows.Forms.ComboBox();
        private System.Windows.Forms.ComboBox cmbBox1 = new System.Windows.Forms.ComboBox();
        private System.Windows.Forms.ComboBox cmbBox11 = new System.Windows.Forms.ComboBox();
        private System.Windows.Forms.ComboBox cmbBox2 = new System.Windows.Forms.ComboBox();
        private System.Windows.Forms.ComboBox cmbBox3 = new System.Windows.Forms.ComboBox();
        private string[][] m_strData; //loaded string of data

        public ExperimentPanel()
        {
            InitializeComponent();
          
            //first row combobox
            cmbBox.Items.Add(ColumnType.Numeric.Description());
            cmbBox.Items.Add(ColumnType.Binary.Description());
            cmbBox.Items.Add(ColumnType.Category.Description());
            cmbBox.Items.Add(ColumnType.Unknown.Description());
          
            cmbBox.Size = new System.Drawing.Size(0, 0);
            cmbBox.Location = new System.Drawing.Point(0, 0);
            this.listView1.Controls.AddRange(new System.Windows.Forms.Control[] { this.cmbBox });
            cmbBox.SelectedIndexChanged += new System.EventHandler(this.CmbSelected);
            cmbBox.LostFocus += new System.EventHandler(this.CmbFocusOver);
            cmbBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbKeyPress);
            cmbBox.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBox.Hide();

            //second row combobox
            cmbBox1.Items.Add(ParameterType.Ignored.Description());
            cmbBox1.Items.Add(ParameterType.Input.Description());
            cmbBox1.Items.Add(ParameterType.Output.Description());

            cmbBox1.Size = new System.Drawing.Size(0, 0);
            cmbBox1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Controls.AddRange(new System.Windows.Forms.Control[] { this.cmbBox1 });
            cmbBox1.SelectedIndexChanged += new System.EventHandler(this.CmbSelected);
            cmbBox1.LostFocus += new System.EventHandler(this.CmbFocusOver);
            cmbBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbKeyPress);
            cmbBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBox1.Hide();

            //cat encoding is filled on demand

            cmbBox11.Size = new System.Drawing.Size(0, 0);
            cmbBox11.Location = new System.Drawing.Point(0, 0);
            this.listView1.Controls.AddRange(new System.Windows.Forms.Control[] { this.cmbBox11 });
            cmbBox11.SelectedIndexChanged += new System.EventHandler(this.CmbSelected);
            cmbBox11.LostFocus += new System.EventHandler(this.CmbFocusOver);
            cmbBox11.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbKeyPress);
            cmbBox11.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBox11.Hide();

            //third row combobox
            cmbBox2.Items.Add(Scaling.None.Description());
            cmbBox2.Items.Add(Scaling.MinMax.Description());
            cmbBox2.Items.Add(Scaling.Gauss.Description());

            cmbBox2.Size = new System.Drawing.Size(0, 0);
            cmbBox2.Location = new System.Drawing.Point(0, 0);
            this.listView1.Controls.AddRange(new System.Windows.Forms.Control[] { this.cmbBox2 });
            cmbBox2.SelectedIndexChanged += new System.EventHandler(this.CmbSelected);
            cmbBox2.LostFocus += new System.EventHandler(this.CmbFocusOver);
            cmbBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbKeyPress);
            cmbBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBox2.Hide();

            //forth row combo box
            cmbBox3.Items.Add(MissingValue.Ignore.Description());
            cmbBox3.Items.Add(MissingValue.Average.Description());
            cmbBox3.Items.Add(MissingValue.Max.Description());
            cmbBox3.Items.Add(MissingValue.Min.Description());
            cmbBox3.Items.Add(MissingValue.Mode.Description());
            cmbBox3.Items.Add(MissingValue.Random.Description());
            

            cmbBox3.Size = new System.Drawing.Size(0, 0);
            cmbBox3.Location = new System.Drawing.Point(0, 0);
            this.listView1.Controls.AddRange(new System.Windows.Forms.Control[] { this.cmbBox3 });
            cmbBox3.SelectedIndexChanged += new System.EventHandler(this.CmbSelected);
            cmbBox3.LostFocus += new System.EventHandler(this.CmbFocusOver);
            cmbBox3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbKeyPress);
            cmbBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBox3.Hide();
        }

       
        //implementation Header control 
        #region Cell ComboBox Events
        private void CmbKeyPress(object sender, KeyPressEventArgs e)
        {
            var combo = sender as ComboBox;

            if (e.KeyChar == 13 || e.KeyChar == 27)
            {
                combo.Hide();
            }
        }

        private void CmbFocusOver(object sender, EventArgs e)
        {
            var combo = sender as ComboBox;
            combo.Hide();
        }

        private void CmbSelected(object sender, EventArgs e)
        {
            var combo = sender as ComboBox;

            int sel = combo.SelectedIndex;
            if (sel >= 0)
            {
                string itemSel = combo.Items[sel].ToString();
                li.SubItems[subItemSelected].Text = itemSel;
                if(itemSel.Equals(ColumnType.Category.Description(),StringComparison.OrdinalIgnoreCase))
                {
                    //
                    addCategoryEncoding();

                    listView1.Items[2].SubItems[subItemSelected].Text = CategoryEncoding.Level.Description();
                }
                else if(itemSel.Equals(ColumnType.Binary.Description(), StringComparison.OrdinalIgnoreCase))
                {
                    //
                    addBinaryEncoding();

                    listView1.Items[2].SubItems[subItemSelected].Text = CategoryEncoding.Binary1.Description();
                }
                else if (itemSel.Equals(ColumnType.Numeric.Description(), StringComparison.OrdinalIgnoreCase) 
                    || itemSel.Equals(ColumnType.Unknown.Description(), StringComparison.OrdinalIgnoreCase))
                {
                    addNoEncoding();

                    listView1.Items[2].SubItems[subItemSelected].Text = CategoryEncoding.None.Description(); 
                }

            }
        }
        #endregion

        #endregion

        #region Private Methods
        /// <summary>
        /// Fill Table with data 
        /// </summary>
        /// <param name="header"></param>
        /// <param name="data"></param>
        public void FillDataGrid(string[] header, string[][] data)
        {
            m_strData = data;
            //clear the list first
            listView1.Clear();
            listView1.GridLines = true;
            listView1.HideSelection = false;
            if (data == null)
                return;
            int numCol = data[0].Length;
            int numRow = data.Length;
            //
            setDefaultColumns(header, numCol);

            //insert data
            setData(data);
        }

       

        /// <summary>
        /// Handling double mouse click for changing MetaData info of the loaded data columns
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = listView1.HitTest(X, Y);
            var row = info.Item.Index;
            var col = info.Item.SubItems.IndexOf(info.SubItem);

            li = info.Item;
            subItemSelected = col;
            //only first and second Row process the mouse input 
            if (li == null || row > 5|| row < 1 || col < 1)
                return;

            ComboBox combo = null;
            if (row == 1)
                combo = cmbBox;
            else if (row == 2)
            {
                

                if (listView1.Items[1].SubItems[subItemSelected].Text.Equals(ColumnType.Binary.Description(), StringComparison.OrdinalIgnoreCase))
                {

                    addBinaryEncoding();
                }
                else if (listView1.Items[1].SubItems[subItemSelected].Text.Equals(ColumnType.Category.Description(), StringComparison.OrdinalIgnoreCase))
                {
                    addCategoryEncoding();
                    
                }

                if (listView1.Items[1].SubItems[subItemSelected].Text.Equals(ColumnType.Category.Description(), StringComparison.OrdinalIgnoreCase) ||
                    listView1.Items[1].SubItems[subItemSelected].Text.Equals(ColumnType.Binary.Description(), StringComparison.OrdinalIgnoreCase))
                    combo = cmbBox11;
            }
            else if (row == 3)
            {
                combo = cmbBox1;
            }

            else if (row == 4)
                combo = cmbBox2;
            else
                combo = cmbBox3;

            var subItm = li.SubItems[col];
            if(combo!=null)
            {
                combo.Bounds = subItm.Bounds;
                combo.Show();
                combo.Text = subItm.Text;
                combo.SelectAll();
                combo.Focus();
            }
            
        }
        private void addNoEncoding()
        {
            cmbBox11.Items.Clear();
            cmbBox11.Items.Add(CategoryEncoding.None.Description());
        }

        private void addBinaryEncoding()
        {
            cmbBox11.Items.Clear();
            cmbBox11.Items.Add(CategoryEncoding.None.Description());
            cmbBox11.Items.Add(CategoryEncoding.Binary1.Description());
            cmbBox11.Items.Add(CategoryEncoding.Binary2.Description());
        }

        private void addCategoryEncoding()
        {
            cmbBox11.Items.Clear();
            cmbBox11.Items.Add(CategoryEncoding.None.Description());
            cmbBox11.Items.Add(CategoryEncoding.Level.Description());
            cmbBox11.Items.Add(CategoryEncoding.OneHot.Description());
            cmbBox11.Items.Add(CategoryEncoding.Dummy1.Description());
            cmbBox11.Items.Add(CategoryEncoding.Dummy2.Description());
        }

        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {

            X = e.X;
            Y = e.Y;

            // Console.WriteLine(string.Format("Row={0}:Col={1} val='{2}'", row, col, value));
        }
        /// <summary>
        /// Fill ListView with proper columns
        /// </summary>
        /// <param name="cols"></param>
        private void setColumn(List<MetaColumn> cols )
        {
            //clear the list first
            listView1.Clear();
            listView1.GridLines = true;
            listView1.HideSelection = false;

            int numCol = cols.Count;
            //int numRow = 5;

            ColumnHeader colHeader = null;
            colHeader = new ColumnHeader();
            colHeader.Text = " ";
            colHeader.Width = 120;
            listView1.Columns.Add(colHeader);
            //
            for (int i = 0; i < numCol; i++)
            {
                colHeader = new ColumnHeader();
                colHeader.Text = cols[i].Name;
                colHeader.Width = 100;
                colHeader.TextAlign = HorizontalAlignment.Center;

                listView1.Columns.Add(colHeader);
            }
            //first row is going to represent column names
            ListViewItem LVI = listView1.Items.Add("Column name:");
            for (int i = 0; i < numCol; i++)
            {
                LVI.SubItems.Add(cols[i].Name);
                LVI.BackColor = SystemColors.MenuHighlight;

            }

            //second row is going to represent the type of each column (input parameter, output variable)
            LVI = listView1.Items.Add(MetaData.CType.Description());
            for (int i = 0; i < numCol; i++)
            {
                //LVI.SubItems.Add("numeric");
                LVI.SubItems.Add(cols[i].Type);
                 LVI.BackColor = SystemColors.GradientActiveCaption;
            }
            //third row is going to represent the type of each column (input parameter, output variable)
            LVI = listView1.Items.Add(MetaData.Encoding.Description());
            for (int i = 0; i < numCol; i++)
            {
                LVI.SubItems.Add(cols[i].Encoding);
                LVI.BackColor = SystemColors.GradientActiveCaption;

            }
            //second row is going to represent is the column input, output or ignored column
            LVI = listView1.Items.Add(MetaData.Variable.Description());
            for (int i = 0; i < numCol; i++)
            {
                //if (i + 1 >= numCol)
                //    LVI.SubItems.Add("output");
                //else
                //    LVI.SubItems.Add("input");
                LVI.SubItems.Add(cols[i].Param);
                LVI.BackColor = SystemColors.GradientActiveCaption;
            }

            //third row is going to represent is the normalization for column
            LVI = listView1.Items.Add(MetaData.Scaling.Description());
            for (int i = 0; i < numCol; i++)
            {
                //LVI.SubItems.Add("MinMax");
                LVI.SubItems.Add(cols[i].Scale);
                LVI.BackColor = SystemColors.GradientActiveCaption;
            }

            //forth row is going to represent missing values action
            LVI = listView1.Items.Add(MetaData.MissingValue.Description());
            for (int i = 0; i < numCol; i++)
            {
                //LVI.SubItems.Add("Ignore");
                LVI.SubItems.Add(cols[i].MissingValue);
                LVI.BackColor = SystemColors.GradientActiveCaption;
            }
        }

        /// <summary>
        /// Set default columns
        /// </summary>
        /// <param name="header"></param>
        /// <param name="numCol"></param>
        private void setDefaultColumns(string[] header, int numCol)
        {
            var cols = new List<MetaColumn>();
            for(int i=0; i<numCol; i++)
            {
                var mc = new MetaColumn();
                mc.Encoding = CategoryEncoding.None.ToString();
                mc.Id = i;
                mc.Index = i;
                mc.MissingValue = MissingValue.Ignore.ToString();
                mc.Scale = Scaling.None.ToString();
                mc.Type = ColumnType.Numeric.ToString();


                if (header == null)
                {
                    if (i + 1 == numCol)
                    {
                        mc.Name = "y";
                        mc.Param = ParameterType.Output.ToString();
                    }

                    else
                    {
                        mc.Name = "x" + (i + 1).ToString();
                        mc.Param = ParameterType.Output.ToString();
                    }

                }
                else
                {
                    mc.Name = header[i];
                    if (i + 1 == numCol)
                        mc.Param = ParameterType.Output.ToString();
                    else
                        mc.Name = "x" + (i + 1).ToString();
                }
                   

            }



            ///
            ColumnHeader colHeader = null;
            colHeader = new ColumnHeader();
            colHeader.Text = " ";
            colHeader.Width = 150;
            listView1.Columns.Add(colHeader);
            //
            for (int i = 0; i < numCol; i++)
            {
                colHeader = new ColumnHeader();

                if (header == null)
                {
                    if (i + 1 == numCol)
                        colHeader.Text = "y";
                    else
                        colHeader.Text = "x" + (i + 1).ToString();
                }
                else
                    colHeader.Text = header[i];


                colHeader.Width = 100;
                colHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

                listView1.Columns.Add(colHeader);
            }
            //first row is going to represent column names
            ListViewItem LVI = listView1.Items.Add(MetaData.Name.Description());
            for (int i = 0; i < numCol; i++)
            {
                if (header == null)
                {
                    if (i + 1 == numCol)
                        LVI.SubItems.Add("y");
                    else
                        LVI.SubItems.Add("x" + (i + 1).ToString());
                }
                else
                    LVI.SubItems.Add(header[i]);


                LVI.BackColor = System.Drawing.SystemColors.MenuHighlight;

            }

            //second row is going to represent the type of each column (input parameter, output variable)
            LVI = listView1.Items.Add(MetaData.CType.Description());
            for (int i = 0; i < numCol; i++)
            {
                LVI.SubItems.Add(ColumnType.Numeric.Description());
                LVI.BackColor = SystemColors.GradientActiveCaption;
            }

            //third row is going to represent the type of each column (input parameter, output variable)
            LVI = listView1.Items.Add(MetaData.Encoding.Description());
            for (int i = 0; i < numCol; i++)
            {
                LVI.SubItems.Add(CategoryEncoding.None.Description());
                LVI.BackColor = SystemColors.GradientActiveCaption;

            }

            //fourth row is going to represent is the column input, output or ignored column
            LVI = listView1.Items.Add(MetaData.Variable.Description());
            for (int i = 0; i < numCol; i++)
            {
                if (i + 1 >= numCol)
                    LVI.SubItems.Add(ParameterType.Output.Description());
                else
                    LVI.SubItems.Add(ParameterType.Input.Description());

                LVI.BackColor = SystemColors.GradientActiveCaption;
            }

            //fifth row is going to represent is the normalization for column
            LVI = listView1.Items.Add(MetaData.Scaling.Description());
            for (int i = 0; i < numCol; i++)
            {
                LVI.SubItems.Add(Scaling.None.Description());

                LVI.BackColor = SystemColors.GradientActiveCaption;
            }

            //sixth row is going to represent missing values action
            LVI = listView1.Items.Add(MetaData.MissingValue.Description());
            for (int i = 0; i < numCol; i++)
            {
                LVI.SubItems.Add(MissingValue.Ignore.Description());

                LVI.BackColor = SystemColors.GradientActiveCaption;
            }
        }
        
        
        private void setData(string[][] data)
        {
            int numCol = data[0].Length;
            int numRow = data.Length;
            ListViewItem LVI = null;
            //insert data
            for (int j = 0; j < numRow; j++)
            {
                LVI = listView1.Items.Add((j + 1).ToString());
                LVI.UseItemStyleForSubItems = false;
                for (int i = 0; i < numCol; i++)
                {
                    if (ColumnData.m_missingSymbols.Contains(data[j][i]))
                    {
                        System.Windows.Forms.ListViewItem.ListViewSubItem itm = new ListViewItem.ListViewSubItem();
                        itm.ForeColor = Color.Red;
                        itm.Text = data[j][i];
                        LVI.SubItems.Add(itm);
                    }

                    else
                        LVI.SubItems.Add(data[j][i].ToString());
                }

            }
            m_strData = data;
            return;
        }
        private MetaColumn[] ParseHeader(bool omitIgnored = false)
        {

            var lst = new List<MetaColumn>();

            if (listView1.Items.Count == 0)
                return null;
            //f name of the columns
            var firstRow = listView1.Items[0];
            var secondRow = listView1.Items[1];
            var thirdRow = listView1.Items[2];
            var forthRow = listView1.Items[3];
            var fifthRow = listView1.Items[4];
            var sixthRow = listView1.Items[5];

            for (int i = 1; i < firstRow.SubItems.Count; i++)
            {
                int colIndex = i-1;
                string colName = firstRow.SubItems[i].Text;
                string colType = secondRow.SubItems[i].Text;
                string colEncoding = thirdRow.SubItems[i].Text;
                string paramType = forthRow.SubItems[i].Text;
                string normType = fifthRow.SubItems[i].Text;
                string missingValue = sixthRow.SubItems[i].Text;

                var col = new MetaColumn() {Id=colIndex, Index = colIndex, Name = colName, Encoding = colEncoding, Type = colType, Param = paramType, Scale = normType, MissingValue = missingValue };
                if (!col.IsIngored || !omitIgnored)
                    lst.Add(col);
            }


            return lst.ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="omitIgnored"></param>
        /// <returns></returns>
        private string[][] ParseData(MetaColumn[] metaCols)
        {
            int metadataRows = 6;
            int metadataColumns = 1;
            string[][] data = new string[listView1.Items.Count - metadataRows][];
            for (int k = 0; k < listView1.Items.Count - 6; k++)
            {
                var i = k + metadataRows;
                var row = listView1.Items[i];

                //calculate number of columns
                int col = metaCols.Length;
                data[k] = new string[col];
                for (int j = 0; j < metaCols.Length; j++)
                {
                    data[k][j] = row.SubItems[metaCols[j].Index+metadataColumns].Text;
                }
            }


            return data;
        }
        #endregion

        #region Public Members
        /// <summary>
        /// When the Projects model is empty we should be able to reset previous state
        /// </summary>
        public void ResetExperimentalPanel()
        {
            listView1.Clear();
            //numCtrlNumForTest.Value = 0;
            //numberRadio.Checked = true;
        }

        public void SetDataSet(MLDataSet dataSet)
        {
            setColumn(dataSet.MetaData.ToList());
            setData(dataSet.Data);
            numCtrlNumForTest.Value = dataSet.TestRows;
            numberRadio.Checked = !dataSet.IsPrecentige;
            presentigeRadio.Checked = dataSet.IsPrecentige;
        }
        public MLDataSet GetDataSet(bool omitIgnored = false)
        {
            try
            {
                var data1 = new MLDataSet();
                //
                data1.MetaData = ParseHeader(omitIgnored);

                data1.TestRows = (int)numCtrlNumForTest.Value;
                data1.IsPrecentige = !numberRadio.Checked;

                var strData = ParseData(data1.MetaData);

                //
                data1.Data = strData;
                return data1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


    }



}
