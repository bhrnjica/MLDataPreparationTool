using GPdotNet.MLBasicTypes;
using MLDataPreparation.Dll;
using System;
using System.IO;
using System.Windows.Forms;

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
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                MakeDataSets(dlg.FileName);
            }
        }

        private void MakeDataSets(string fileName)
        {
            try
            {
                //create full dataset with all columns and rows
                var options = optionsPanel1.GetOptions();
                var fulldata = experimentPanel2.GetDataSet();
                //get dataset based on options 
                var ds = fulldata.GetDataSet(options.randomize);
                ds.TestRows = options.testRows;
                ds.IsPrecentige = options.prec;
                //create experiment based created dataset
                var exp = new GPdotNet.MLData.Experiment(ds);

                //saving processed data in to file
                var dirPath = Path.GetDirectoryName(fileName);
                var name = Path.GetFileNameWithoutExtension(fileName);
                var ext = Path.GetExtension(fileName);
                var trainPath = Path.Combine(dirPath, name+"_train" + ext);
                var testPath = Path.Combine(dirPath, name + "_test" + ext);

                exp.WriteToFile(trainPath, false, options.delimiter, options.cntk);
                if(options.testRows > 0)
                    exp.WriteToFile(testPath, true, options.delimiter,options.cntk);

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
                experimentPanel2.FillDataGrid(dlg.Header, dlg.Data);
            }
        }

    }
}
