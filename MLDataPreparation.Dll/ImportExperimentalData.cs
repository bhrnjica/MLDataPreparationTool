﻿//////////////////////////////////////////////////////////////////////////////////////////
// GPdotNET - Genetic Programming Tool                                                  //
// Copyright 2006-2018 Bahrudin Hrnjica                                                 //
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace MLDataPreparation.Dll
{ 
    public partial class ImportExperimentalData : Form
    {
        private string originData = "";
        public ImportExperimentalData()
        {
            InitializeComponent();

           
        }

        //Import file
        private void button1_Click(object sender, EventArgs e)
        {
            var strFile = GetFileFromOpenDialog("","");
            if (strFile == null)
                return;

            textBox1.Text = strFile;

            var data = string.Join(Environment.NewLine, File.ReadAllLines(strFile).Where(l => !l.StartsWith("@") && !l.StartsWith("#") && !l.StartsWith("!")));
            originData = data;
            textBox3.Text = data;
            ProcesData();
            
            if (!string.IsNullOrEmpty(data))
                button2.Enabled = true;
        }

        public static string GetFileFromOpenDialog(string fileDescription = "All files ", string extension = "*.*")
        {
#if WINDOWS_APP
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".*");
            //openPicker.FileTypeFilter.Add(".jpeg");
            //openPicker.FileTypeFilter.Add(".png");

            var tsk = openPicker.PickSingleFileAsync();
            return tsk.GetAwaiter().GetResult().Name;
#else
            OpenFileDialog dlg = new OpenFileDialog();
            //if (string.IsNullOrEmpty(extension))
            //    dlg.Filter = "Plain text files (*.csv;*.txt;*.dat,*.*;)|*.csv;*.txt;*.dat |All files (*.*)|*.*";
            //else
            dlg.Filter = string.Format("All files (*.*)|*.*");
            //
            if (dlg.ShowDialog() == DialogResult.OK)
                return dlg.FileName;
            else
                return null;
#endif
        }

        private void ProcesData()
        {
            var data = originData;
            if (string.IsNullOrEmpty(data))
                return;

            if (checkBox2.Checked)
                data = data.Replace(";", "\t|\t");
            if (checkBox3.Checked)
                data = data.Replace(",", "\t|\t");
            if (checkBox4.Checked)
                data = data.Replace(" ", "\t|\t");
            if (checkBox6.Checked)
                data = data.Replace("\t", "\t|\t");
            if (checkBox5.Checked)
            {
                if (!string.IsNullOrEmpty(textBox2.Text))
                    data = data.Replace(textBox2.Text[0], '|');
            }

            //if header is present separate data with horizontal line
            if (checkBox1.Checked)
            {
                var index = data.IndexOf(Environment.NewLine);
                var index2 = data.IndexOf(Environment.NewLine, index + 1);
                int counter=0;
                while(counter<index2-index)
                {
                    data=data.Insert(index,"-");
                    counter++;
                }
                data = data.Insert(index, Environment.NewLine);
            }
            

            textBox3.Text = data;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox3.Text))
                {
                    MessageBox.Show("No file is selected!");
                    return;
                }


                var colDelimiter = GetColumDelimiter();
                //define the row
                string[] rows = originData.Split(Environment.NewLine.ToArray(), StringSplitOptions.RemoveEmptyEntries);

                prepareData(rows, colDelimiter, checkBox1.Checked, radioButton1.Checked);
            }
            catch (Exception ex)
            {

                reportException(ex);
            }
            
        }

        private void reportException(Exception ex)
        {
            MessageBox.Show(ex.Message, "GPdotNET");
        }

        public string[] Header { get; set; }
        public string[][] Data { get; set; }
        /// <summary>
        /// Initialize Projects with string data, with specific formating 
        /// </summary>
        /// <param name="strData"></param>
        /// <param name="columDelimiter"></param>
        /// <param name="isFirstRowHeader"></param>
        /// <param name="isFloatingPoint"></param>
        public void prepareData(string[] rowData, char[] columDelimiter, bool isFirstRowHeader, bool isFloatingPoint = true)
        {
            
            //Define the columns
            var colCount = rowData[0].Split(columDelimiter).Count();
            var rowCount = rowData.Length;
            int headerCount = 0;
            ///
            Header = null;
            if (isFirstRowHeader)
                headerCount++;

            Data = new string[rowCount - headerCount][];

            //
            for (int i = 0; i < rowCount; i++)
            {
                var row = rowData[i].Split(columDelimiter);

                //column creation for each row
                if (i == 0 && isFirstRowHeader)
                    Header = new string[colCount];
                else
                    Data[i - headerCount] = new string[colCount];

                if (row.Length != colCount)
                {

                    Data = null;
                    throw new Exception("Data is not consistent.");
                }
                //column enumeration
                for (int j = 0; j < colCount; j++)
                {
                    //value format
                    var value = "";
                    if (string.IsNullOrEmpty(row[j]))
                        value = "n/a";
                    else
                        value = row[j];

                    //
                    if (i == 0 && isFirstRowHeader)
                        Header[j] = value;
                    else
                        Data[i - headerCount][j] = value;


                }
            }


        }

        private char[] GetColumDelimiter()
        {
            var col = new List<char>();

            if (checkBox2.Checked)
                col.Add(';');
            if (checkBox3.Checked)
                col.Add(',');
            if (checkBox4.Checked)
                col.Add(' ');
            if (checkBox6.Checked)
                col.Add('\t');
            if (checkBox5.Checked)
                col.Add(textBox2.Text[0]);

            return col.ToArray();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var ch = sender as CheckBox;
            if (ch.Name == "checkBox5")
            {
                if (ch.Checked)
                    textBox2.Enabled = true;
                else
                {
                    textBox2.Text = "";
                    textBox2.Enabled = false;
                }
            }
            ProcesData();
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox7.Checked)
            {
                button2.Enabled = false;
                button4.Enabled = true;
                label1.Enabled = true;
                numCtrlNumForTest.Enabled = true;
            }
            else
            {
                button2.Enabled = true;
                button4.Enabled = false;
                label1.Enabled = false;
                numCtrlNumForTest.Enabled = false;
            }

        }

        //import data as time series
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox3.Text))
                {
                    MessageBox.Show("No file is selected!");
                    return;
                }


                var colDelimiter = GetColumDelimiter();

                if(numCtrlNumForTest.Value < 1 && numCtrlNumForTest.Value > originData.Length)
                {
                    MessageBox.Show("Invalid number of time leg. PLease specify the time leg between 1 and row number.");
                    return;
                }

                if (string.IsNullOrEmpty(originData))
                    return;

                //
                //define the row
                string[] tdata = originData.Split(Environment.NewLine.ToArray(), StringSplitOptions.RemoveEmptyEntries);
                //transform the time series into data frame
                string[] prepData = prepareTimeSeriesData(tdata, (int)numCtrlNumForTest.Value);

                //prepare data for loading
                prepareData(prepData, new char[] {';'}, checkBox1.Checked, radioButton1.Checked);
            }
            catch (Exception ex)
            {

                reportException(ex);
            }
        }

        /// <summary>
        /// Transforms the string of time series into data frame string 
        /// </summary>
        /// <param name="tdata"></param>
        /// <param name="legTime"></param>
        /// <returns></returns>
        private string[] prepareTimeSeriesData(string[] tdata,int legTime)
        {
            //split data on for feature and label datasets
            var a = new string[tdata.Length - legTime];
            var b = new string[tdata.Length - legTime];

            for (int l = 0; l < tdata.Length; l++)
            {
                //
                if (l < tdata.Length - legTime)
                    a[l] = tdata[l];

                //
                if (l >= legTime)
                    b[l - legTime] = tdata[l];
            }

            //make arrays of data
            var strDataFrame = new List<string>();
            //
            for (int i = 0; i < tdata.Length - legTime; i++)
            {
                //features
                var row = new string[legTime+1];
                int j = 0;
                for (j = 0; j < legTime; j++)
                    row[j] = tdata[i + j];

                //label column at the end of the list
                row[j] = tdata[i + j];

                //create features row
                strDataFrame.Add(string.Join(";",row));
            }

            return strDataFrame.ToArray();
        }
    }
}
