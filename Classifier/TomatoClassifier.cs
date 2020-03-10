using Emgu.CV;
using Emgu.CV.Structure;
using System;
using Emgu.CV.ML;
using System.Collections.Generic;
using Emgu.CV.CvEnum;
using Emgu.CV.UI;
using Emgu.CV.ML.MlEnum;
using System.Drawing;
using Challenge.Helper;
using Emgu.CV.Util;
using System.Runtime.InteropServices;
using Emgu.CV.Features2D;
using Emgu.CV.XFeatures2D;
using System.Drawing.Imaging;
using Challenge.Feature;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using Challenge.Classifier.Extractor;
namespace Challenge.Classifier
{
    public class TomatoClassifier
    {
        private SVM svm = new SVM();
        private ILogger log = Logger.OnConsole();
        public List<IDictionary<String, Bitmap>> FeatureExtracted { get; private set; }
        private readonly TomatoDescriptorExtractor extractor;
        private readonly IClassifier classifier;
        public TomatoClassifier(TomatoDescriptorExtractor extractor, IClassifier classifier)
        {
            this.extractor = extractor;
            this.classifier = classifier;
        }
        private void CheckArgumentCorrectness(Image<Bgr, byte>[] colorImages, Image<Gray, byte>[] mono01Images, Image<Gray, byte>[] mono02Images, int[] labels)
        {
            if (colorImages.Length != mono01Images.Length || mono01Images.Length != mono02Images.Length || mono02Images.Length != labels.Length)
            {
                throw new ArgumentException("the length of data must be the same");
            }
        }
       
        public bool Train(Image<Bgr, byte>[] colorImages, Image<Gray, byte>[] mono01Images, Image<Gray, byte>[] mono02Images, int[] labels)
        {
            CheckArgumentCorrectness(colorImages, mono01Images, mono02Images, labels);
            log.LogNewLine("start training...");
             //checks on data
            this.FeatureExtracted = new List<IDictionary<String, Bitmap>>();
            var data = extractor.ExtractDescriptors(colorImages, mono01Images, mono02Images, labels);
            var featuresExtractedOn = data.ConvertAll(element => element.Features).ToArray();
            if(featuresExtractedOn.Length == 0)
            {
                return false;
            }
            var adaptFeature = Util.AdaptFeature(featuresExtractedOn);
            classifier.Train(adaptFeature, labels);
            log.LogNewLine("");
            log.LogNewLine("train ends..");
            return true;
        }

        public int Classify(Image<Bgr, byte> colorImage, Image<Gray, byte> mono01Image, Image<Gray, byte> mono02Image, out double score)
        {
            var data = extractor.ExtractDescriptor(colorImage, mono01Image, mono02Image, (int) TomatoType.UNCLASSIFIED);
            var prediction = classifier.Classify(data.Features);
            score = prediction.Item2;
            return prediction.Item1;
        }

    }

}
