using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challenge.Helper.Set;
using Challenge.Classifier;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Diagnostics;

namespace Challenge.Program
{
    using TomatoAdapted = Tuple<Image<Bgr, byte>, Image<Gray, byte>, Image<Gray, byte>, int>;
    /// <summary>
    /// this program helps to give a performance report for a set of tomato classifier.
    /// </summary>
    public class ConsolePerformanceEvaluator : AbstractProgram
    {
        private Stopwatch stopWatch = new Stopwatch(); //used to eval time perfomance in term of traning and predictions per seconds
        private List<TomatoClassifier> classifiers;
        /// <summary>
        /// For other params, <see>AbstractProgram</see>
        /// </summary>
        /// <param name="classifiers">The classifiers tested by the programs</param>
        public ConsolePerformanceEvaluator(string tomatoDirPath, 
            string indexFileName, 
            SplitStrategy split, 
            List<TomatoClassifier> classifiers) : base(tomatoDirPath, indexFileName, split)
        {
            this.classifiers = classifiers;
        }

        protected override void OnStart(IDictionary<SetType, List<Tomato>> imageSets)
        {
            var train = TomatoSetHelper.Expand(imageSets[SetType.TRANING]);
            var i = 0;
            foreach(TomatoClassifier classifier in classifiers)
            {
                log.LogNewLine("Classifier : " + i++);
                log.LogNewLine("___________________");
                stopWatch.Start(); 
                classifier.Train(train.Item1, train.Item2, train.Item3, train.Item4);
                stopWatch.Stop();
                log.LogNewLine("train time : " + stopWatch.Elapsed.TotalSeconds + " seconds");
                EvalPerformance(classifier, imageSets[SetType.VALIDATION]);
                log.LogNewLine("___________________");
                
            }
            Console.ReadLine();
        }
        /*
         * this method eval the classifier performance in the validation set.
         * the metrics used are:
         *  - precision;
         *  - recall;
         *  - accuracy;
         *  - confusion matrix.
         */
        private void EvalPerformance(TomatoClassifier classifier, List<Tomato> tomatoes)
        {
            var falsePositive = 0.0;
            var falseNegative = 0.0;
            var truePositive = 0.0;
            var trueNegative = 0.0;
            log.LogNewLine("eval...");
            stopWatch.Restart();
            foreach (Tomato tomato in tomatoes)
            {
                log.Log(".");
                double score = 0;
                var predicted = classifier.Classify(tomato.ColoredImage, tomato.GrayImage1, tomato.GrayImage2, out score);
                if(predicted == (int)tomato.Type)
                {
                    var updated = predicted == (int)(TomatoType.BAD) ? trueNegative++ : truePositive++;
                } else
                {
                    var updated = predicted == (int)(TomatoType.BAD) ? falseNegative++ : falsePositive++;
                }
            }
            stopWatch.Stop();

            log.LogNewLine("");
            log.LogNewLine("predict " + tomatoes.Count / stopWatch.Elapsed.Seconds + " tomatoes per second");
            log.LogNewLine("ends..");
            log.LogNewLine("precision : " + truePositive / (truePositive + falsePositive));
            log.LogNewLine("recall : " + truePositive / (truePositive + falseNegative));
            log.LogNewLine("accuracy : " + (truePositive + trueNegative) / tomatoes.Count);
            log.LogNewLine("-- CONFUSION MATRIX --");
            log.LogNewLine("BAD\t" + trueNegative + "\t" + falseNegative);
            log.LogNewLine("OK \t" + falsePositive + "\t" + truePositive);
            log.LogNewLine("   \tBAD\tOK");
            log.LogNewLine("...");
        }
    }
}
