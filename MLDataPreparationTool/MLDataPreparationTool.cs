using MLDataPreparation.Dll;
using MLDataPreparation.Tool;
using System;
using System.IO;
using System.Windows.Forms;
using DataProcessing.MLBasicTypes;
using DataProcessing.MLData;

namespace MLDataPreparationTool
{
    public partial class MLDataPreparationTool : Form
    {
        public Func<string> GetExperimentData { get; set; }
        public Func<MLDataSet> GetDataSet { get; set; }

        public MLDataPreparationTool()
        {
            InitializeComponent();
            optionsPanel1.button1.Click += ExportToML_Click;
        }

        private void ExportToML_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "All files (*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //create full dataset with all columns and rows
                    var options = optionsPanel1.GetOptions();
                    var fulldata = experimentPanel1.GetDataSet();

                    ExportData.MakeDataSets(dlg.FileName, options, fulldata);

                   MessageBox.Show("The data is exported successfully!");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }



        

        private void ImportData_Click(object sender, EventArgs e)
        {
            ImportExperimentalData dlg = new ImportExperimentalData();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (dlg.Data == null)
                    return;
                experimentPanel1.FillDataGrid(dlg.Header, dlg.Data);
            }
        }
        /// <summary>
        /// About Tool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new AboutBox();
            dlg.ShowDialog();
        }
    }
}
