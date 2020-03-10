using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challenge.Helper.Set;
using Challenge.Helper;
using Challenge.Classifier;
using System.IO;

namespace Challenge.Program
{
    /// <summary>
    /// this program produces the file described in tomato competition : <see href="http://stackoverflow.com">here</see>
    /// </summary>
    public class ConsoleCompetitionOutput : AbstractProgram
    {
        private TomatoClassifier classifier;
        private string indexName;
        /// <summary>
        /// for other params, <see>AbstractProgram</see>
        /// </summary>
        /// <param name="classifier">The classifier to train and to evalaute</param>
        /// <param name="testIndexName">index files that link each image in the test set</param>
        public ConsoleCompetitionOutput(string tomatoDirPath, 
            string indexFileName, 
            SplitStrategy split, 
            TomatoClassifier classifier,
            string testIndexName) : base(tomatoDirPath, indexFileName, split)
        {
            this.indexName = testIndexName;
            this.classifier = classifier;
        }

        protected override void OnStart(IDictionary<SetType, List<Tomato>> imageSets)
        {
            //traning phase
            var trainSet = TomatoSetHelper.Expand(imageSets[SetType.TRANING]);
            classifier.Train(trainSet.Item1, trainSet.Item2, trainSet.Item3, trainSet.Item4);
            //load test images
            var testImages = new ImageLoaderHelper(base.ComputePath(this.indexName));
            //the classifier predicts the class label for each image in the test set.
            var lines = ExportClassificationResult(classifier, testImages.Images);
            File.WriteAllLines(base.ComputePath("submission" + DateTime.Now.ToFileTime() + ".txt"), lines.ToArray());
            log.LogNewLine("evaluation done, output in submission<timestamp>.txt");
        }
        /*
         * for each prediction, this method create a line following this schema:
         * {image_name} {label} {score}
         */
        private List<string> ExportClassificationResult(TomatoClassifier classifier, List<Tomato> testTomatoes)
        {
            var classifiedResult = new List<string>();
            log.LogNewLine("compute label on test images..");
            foreach (Tomato tomato in testTomatoes)
            {
                var score = 0.0;
                var res = classifier.Classify(tomato.ColoredImage, tomato.GrayImage1, tomato.GrayImage2, out score);
                classifiedResult.Add(tomato.Name + " " + (int)res + " " + score);
            }
            return classifiedResult;
        }
    }
}
