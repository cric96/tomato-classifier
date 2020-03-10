using System;
using Challenge.Helper;
using Challenge.Helper.Set;
using Challenge.Classifier;
using System.Windows.Forms;
using System.Collections.Generic;
using Challenge.View;
using System.Drawing;
using Challenge;
using System.IO;
using Challenge.Classifier.Extractor;

namespace Challenge.Program
{
    /// <summary>
    /// Static class used to launch one of the programs used to test the TomatoClassifier
    /// </summary>
    static class Launcher
    {
        //load image using set description
        const string tomatoDir = @"C:\Users\gianluca.aguzzi\Desktop\Tomatos\";
        static readonly ILogger log = Logger.OnConsole();
        static void Main() {
            //GUI();
            EvalPerfomance();
            //ExportFeatureOnCsv();
        }
        static void ExportFeatureOnCsv()
        {
            var extractor = new LBPExtractor(false);
            Logger.Enabled = true;
            var program = new ExtractCSVProgram(
                tomatoDir,
                "TrainingSet.txt",
                loader => TomatoSetHelper.SplitExact(100, 100, loader),
                extractor
            );
            program.Start();
        }
        static void EvalPerfomance()
        {
            var classifier = standardClassifier();
            Logger.Enabled = true;
            var program = new ConsolePerformanceEvaluator(
                tomatoDir,
                "TrainingSet.txt",
                loader => TomatoSetHelper.SplitExact(300, 300, loader),
                new List<TomatoClassifier>(new TomatoClassifier[] { classifier })
            );
            program.Start();
        }

        static void CompetiotionOutput()
        {
            var classifier = standardClassifier();
            Logger.Enabled = true;
            var program = new ConsoleCompetitionOutput(
                tomatoDir,
                "TrainingSet.txt",
                loader => TomatoSetHelper.SplitExact(300, 300, loader),
                classifier,
                "TestSet.txt"
            );
            program.Start();
        }

        static void GUI()
        {
            Logger.Enabled = true;
            new GUIProgram(
                tomatoDir,
                "TrainingSet.txt",
                loader => TomatoSetHelper.SplitExact(300, 300, loader),
                new Size(800, 600)
            ).Start();    
        }

        static TomatoClassifier standardClassifier()
        {
            var extractor = new LBPExtractor(false);
            return new TomatoClassifier(extractor, SVMClassifier.Linear(0.43f)); //best performance with 0.43
        }
    }
}
