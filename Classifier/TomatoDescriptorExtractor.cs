using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challenge.Helper;
using System.Drawing;
using Emgu.CV.CvEnum;
using Challenge.Feature;

namespace Challenge.Classifier.Extractor
{
    public class VisualEvent
    {
        public string Description { get; }
        public Bitmap Image { get; }
        public VisualEvent(string description, Bitmap image)
        {
            this.Description = description;
            this.Image = image;
        }
    }
    public abstract class TomatoDescriptorExtractor
    {
        public bool VisualLogEnabled { get; set; }
        protected readonly ILogger log = Logger.OnConsole();

        public TomatoDescriptorExtractor(bool visualLog)
        {
            this.VisualLogEnabled = visualLog;
        }
        public List<TomatoDescriptor> ExtractDescriptors(Image<Bgr, byte>[] colorImages, Image<Gray, byte>[] mono01Images, Image<Gray, byte>[] mono02Images, int[] labels)
        {
            var data = new List<TomatoDescriptor>();
            var setSize = colorImages.Length;
            Parallel.For(0, setSize,
                () => new List<TomatoDescriptor>(),
                (j, loop, subtotal) =>
                {
                    log.Log(".");
                    var extracted = ExtractDescriptor(colorImages[j], mono01Images[j], mono02Images[j], labels[j]);
                    subtotal.Add(extracted);
                    return subtotal;
                },
                (partialResult) => {
                    lock (data)
                    {
                        data.AddRange(partialResult);
                    }
                }
            );
            return data;
        }

        public abstract TomatoDescriptor ExtractDescriptor(Image<Bgr, byte> color, Image<Gray, byte> mono01, Image<Gray, byte> mono02, int label);

        public delegate void VisualEventHandler(object sender, List<VisualEvent> visualEvent);

        public event VisualEventHandler VisualEventManager;

        protected void Inform(List<VisualEvent> images)
        {
            if(VisualLogEnabled)
            {
                VisualEventManager?.Invoke(this, images);
            }
        }
    }
    public class LBPExtractor : TomatoDescriptorExtractor
    {
        private static readonly Gray MIN_GRAY = new Gray(50);
        private static readonly Gray MAX_GRAY = new Gray(255);
        private static readonly int TOMATO_SIZE = 128;
        private static readonly int ITERATION_MORPH_COUNT = 5;
        private static readonly int SMOOTH_FILTER_SIZE = 3;
        private static readonly int THR_ADAPTIVE_WINDOW_SIZE = 21;

        
        public LBPExtractor(bool visualLog) : base(visualLog){}

        public override TomatoDescriptor ExtractDescriptor(Image<Bgr, byte> color, Image<Gray, byte> mono01, Image<Gray, byte> mono02, int label)
        {
            var imagesExtracted = new List<VisualEvent>(); //image used to show feature in external context (for example, the view)
            imagesExtracted.Add(new VisualEvent("original, class = " + label, color.ToBitmap()));
            //compute tomato boundary and mask
            var tomatoMask = ComputeTomatoMask(mono01);
            imagesExtracted.Add(new VisualEvent("mask", tomatoMask.ToBitmap()));
            var tomatoRect = ImageHelper.GetMaxBoundingBox(tomatoMask, MAX_GRAY);
            //remove background and crops the image
            var cleanTomato = ImageHelper.RemoveBackground(color, tomatoMask);
            var tomatoDescription = cleanTomato.GetSubRect(tomatoRect).Resize(TOMATO_SIZE, TOMATO_SIZE, Inter.LinearExact);
            imagesExtracted.Add(new VisualEvent("cropped", tomatoDescription.ToBitmap()));

            tomatoDescription = tomatoDescription.SmoothGaussian(SMOOTH_FILTER_SIZE);
            imagesExtracted.Add(new VisualEvent("smoothed", tomatoDescription.ToBitmap()));

            var binarized = ExtractBinaryFrom(tomatoDescription, imagesExtracted);
            var adapatedForLBP = Util.ToBiolabImage(binarized);

            var lbpFeature = ExtractLBPFeature(adapatedForLBP);

            //adapt data
            var features = new List<double>(lbpFeature).ConvertAll(element => (float)element);
            var descriptior = new TomatoDescriptor(features.ToArray(), (TomatoType)label);
            Inform(imagesExtracted);
            return descriptior;
        }
        private Image<Gray, Byte> ComputeTomatoMask(Image<Gray, Byte> tomato)
        {
            var binary = tomato.ThresholdBinary(MIN_GRAY, MAX_GRAY);
            return binary.Erode(ITERATION_MORPH_COUNT).Dilate(ITERATION_MORPH_COUNT);
        }

        private Image<Gray, Byte> ExtractBinaryFrom(Image<Bgr, Byte> tomatoDescription, List<VisualEvent> imagesExtracted)
        {
            var hsvImage = tomatoDescription.Convert<Hsv, byte>();
            var thrOnChannelRed = tomatoDescription[2].ThresholdAdaptive(MAX_GRAY, AdaptiveThresholdType.GaussianC, ThresholdType.BinaryInv, THR_ADAPTIVE_WINDOW_SIZE, new Gray(4));
            var thrOnChannelSaturation = hsvImage[1].ThresholdAdaptive(MAX_GRAY, AdaptiveThresholdType.GaussianC, ThresholdType.BinaryInv, THR_ADAPTIVE_WINDOW_SIZE, new Gray(9));
            imagesExtracted.Add(new VisualEvent("extracted info red", (thrOnChannelRed + thrOnChannelSaturation).ToBitmap()));
            imagesExtracted.Add(new VisualEvent("extracted info saturation", (thrOnChannelRed + thrOnChannelSaturation).ToBitmap()));
            imagesExtracted.Add(new VisualEvent("final image", (thrOnChannelRed + thrOnChannelSaturation).ToBitmap()));
            return thrOnChannelRed + thrOnChannelSaturation;
        }
        private double[] ExtractLBPFeature(BioLab.ImageProcessing.Image<byte> image)
        {
            var featureExtractorNear = new LBP(1, 8, LBP.mappingType.rotationInvariant, LBP.histogramType.normalized);
            return featureExtractorNear.LBPcode(image).Features.ToArray();
        }
    }

}
