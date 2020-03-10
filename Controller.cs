using Challenge.Classifier;
using Challenge.Helper;
using Challenge.Helper.Set;
using Challenge.View;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challenge.Classifier.Extractor;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace Challenge
{
    /// <summary>
    /// this class has the logic to extract and eval performance of:
    ///     - LBP feature extractor
    ///     - SVM classifier with nu = 0.43
    /// the result of each computation appears on the Tomato Viewer.
    /// </summary>
    public class Controller
    {
        private readonly TomatoClassifier classifier;
        private readonly TomatoDescriptorExtractor imageExtractor;
        private readonly IDictionary<SetType, List<Tomato>> tomatoSets;
        private readonly ILogger log = Logger.OnConsole();
        private List<VisualEvent> images;
        public TomatoViewer View { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tomatoSets">
        /// The tomato set divided into training and validation.
        /// In the training set, the classificator learns the svm params.
        /// The classifier predicts label on the validation set.
        /// </param>
        public Controller(
            IDictionary<SetType, List<Tomato>> tomatoSets)
        {
            this.images = new List<VisualEvent>();
            imageExtractor = new LBPExtractor(true);
            imageExtractor.VisualEventManager += OnImageComputed;
            classifier = new TomatoClassifier(new LBPExtractor(false), SVMClassifier.Linear(0.43f));
            this.tomatoSets = tomatoSets;
        }

        private void OnImageComputed(object sender, List<VisualEvent> visualEvents)
        {
            lock(images)
            {
                images.AddRange(visualEvents);
            }
        }
        /// <summary>
        /// Start an application based on the view. This method sets the controller and view in each object.
        /// </summary>
        /// <param name="view"></param>
        public void StartApplication(TomatoViewer view)
        {
            this.View = view;
            view.Controller = this;
            View.Show();
        }

        public void ExtractOnTrain()
        {
            log.LogNewLine("extraction..");
            this.images.Clear();
            View.ClearImages();
            ExtractFeature(imageExtractor, tomatoSets[SetType.TRANING]);
            View.AddImages(this.images);
        }

        public void ExtractOnValidation()
        {
            log.LogNewLine("extraction..");
            this.images.Clear();
            View.ClearImages();
            ExtractFeature(imageExtractor, tomatoSets[SetType.VALIDATION]);
            View.AddImages(this.images);
        }

        public void CheckPerformance()
        {
            this.images.Clear();
            View.ClearImages();
            log.LogNewLine("eval starts..");
            Train(classifier, tomatoSets[SetType.TRANING]);
            foreach (Tomato tomato in tomatoSets[SetType.VALIDATION])
            {
                log.Log(".");
                double score = 0;
                var result = classifier.Classify(tomato.ColoredImage, tomato.GrayImage1, tomato.GrayImage2, out score);
                if(result != (int) tomato.Type)
                {
                    this.images.Add(new VisualEvent("WRONG! expeted : " + tomato.Type, tomato.ColoredImage.ToBitmap()));
                    //this makes process extraction visualization possible:
                    imageExtractor.ExtractDescriptor(tomato.ColoredImage, tomato.GrayImage1, tomato.GrayImage2, (int)tomato.Type);
                }
            }
            log.LogNewLine("");
            View.AddImages(this.images);
        }
        private void Train(TomatoClassifier classifier, List<Tomato> tomatoes)
        {
            var adapted = TomatoSetHelper.Expand(tomatoes);
            classifier.Train(adapted.Item1, adapted.Item2, adapted.Item3, adapted.Item4);
        }
        private void ExtractFeature(TomatoDescriptorExtractor extractor, List<Tomato> tomatoes)
        {
            var adapted = TomatoSetHelper.Expand(tomatoes);
            extractor.ExtractDescriptors(adapted.Item1, adapted.Item2, adapted.Item3, adapted.Item4);
        }
    }
}
