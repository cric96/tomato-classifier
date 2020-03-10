using Challenge.Helper;
using Emgu.CV;
using Emgu.CV.ML;
using Emgu.CV.ML.MlEnum;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Classifier
{
    public interface IClassifier
    {
        void Train(float[,] features, int[] labels);
        
        Tuple<int, double> Classify(float[] feature);
    }
    /// <summary>
    /// pattern adapter: wrap the svm defined in opencv.
    /// </summary>
    public class SVMClassifier : IClassifier
    {
        private SVM svm;

        public SVMClassifier(SVM svm)
        {
            this.svm = svm;
        }

        public static SVMClassifier Linear(float nu)
        {
            var svm = new SVM();
            svm.SetKernel(SVM.SvmKernelType.Linear);
            svm.Type = SVM.SvmType.OneClass;
            svm.Nu = nu;
            return new SVMClassifier(svm);
        }
        public Tuple<int, double> Classify(float[] feature)
        {
            var vec = new VectorOfFloat(feature);
            var label = svm.Predict(vec);
            double score = svm.Predict(vec, flags: 1);
            score = Util.Sigmoid(score);
            return new Tuple<int, double>((int) label, score);
        }

        public void Train(float[,] features, int[] labels)
        {
            var trainData = new TrainData(new Matrix<float>(features), DataLayoutType.RowSample, new VectorOfInt(labels));
            svm.TrainAuto(trainData);

        }
    }
}
