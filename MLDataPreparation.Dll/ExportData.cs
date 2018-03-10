using DataProcessing.MLBasicTypes;
using DataProcessing.MLData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLDataPreparation.Dll
{
    public static class ExportData
    {
        public static bool MakeDataSets (string fileName, (bool cntk, bool randomize, char delimiter, int testRows, bool precentige) options, MLDataSet fulldata)
        {
            try
            {

                //get dataset based on options 
                var ds = fulldata.GetDataSet(options.randomize);
                ds.TestRows = options.testRows;
                ds.IsPrecentige = options.precentige;
                //create experiment based created dataset
                var exp = new Experiment(ds);

                //saving processed data in to file
                var dirPath = Path.GetDirectoryName(fileName);
                var name = Path.GetFileNameWithoutExtension(fileName);
                var ext = Path.GetExtension(fileName);
                var trainPath = Path.Combine(dirPath, name + "_train" + ext);
                var testPath = Path.Combine(dirPath, name + "_test" + ext);

                exp.WriteToFile(trainPath, false, options.delimiter, options.cntk);
                if (options.testRows > 0)
                    exp.WriteToFile(testPath, true, options.delimiter, options.cntk);

                return true;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
