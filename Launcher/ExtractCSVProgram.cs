using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challenge.Helper.Set;
using Challenge.Helper;
using Challenge.Classifier.Extractor;

namespace Challenge.Program
{
    /// <summary>
    /// This program extract feature from tomato images. The program saves the features extracted in a csv file.
    /// After computations, the result of programs is a pair of file called Train.csv and Validation.csv.
    /// The features extracted from the traning set are stored in Train.csv, in Validation.csv the other features extracted.
    /// </summary>
    public class ExtractCSVProgram : AbstractProgram
    {
        private TomatoDescriptorExtractor extractor;
        /// <summary>
        /// for other params, <see>AbstractProgram</see>
        /// </summary>
        public ExtractCSVProgram(string tomatoDirPath, 
            string indexFileName, 
            SplitStrategy split, 
            TomatoDescriptorExtractor extractor) : base(tomatoDirPath, indexFileName, split)
        {
            this.extractor = extractor;
        }

        protected override void OnStart(IDictionary<SetType, List<Tomato>> imageSets)
        {
            var mappedTrain = TomatoSetHelper.Expand(imageSets[SetType.TRANING]);
            var mappedVal = TomatoSetHelper.Expand(imageSets[SetType.VALIDATION]);
            log.LogNewLine("Extract feature on train...");
            var train = extractor.ExtractDescriptors(mappedTrain.Item1, mappedTrain.Item2, mappedTrain.Item3, mappedTrain.Item4);
            log.LogNewLine("");
            log.LogNewLine("Extract feature on validation...");
            var validation = extractor.ExtractDescriptors(mappedVal.Item1, mappedVal.Item2, mappedVal.Item3, mappedVal.Item4);
            WriteCsvOn(train, "Train.csv");
            WriteCsvOn(validation, "Validation.csv");
            
        }
        private void WriteCsvOn(List<TomatoDescriptor> descriptors, string fileName)
        {
            var csv = new CSVFeatureOutput(base.ComputePath(fileName));
            csv.Clear();
            descriptors.ForEach(element => csv.Append(element.Features, (int)element.Label));
            csv.WriteOnFile();
        }
    }
}
