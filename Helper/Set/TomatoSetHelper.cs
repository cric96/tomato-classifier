using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challenge;
using Challenge.Helper;
using Emgu.CV;
using Emgu.CV.Structure;

using Emgu.CV.CvEnum;
namespace Challenge.Helper.Set
{
    using ExpandedDescriptor = Tuple<Image<Bgr, Byte>[], Image<Gray, Byte>[], Image<Gray, Byte>[], int[]>;
    public enum SetType
    {
        TRANING = 0, VALIDATION = 1
    }
    
    public static class TomatoSetHelper
    {
        public static ExpandedDescriptor Expand(List<Tomato> tomatoes)
        {
            var colorImages = tomatoes.Select(desc => desc.ColoredImage).ToArray();
            var mono1images = tomatoes.Select(desc => desc.GrayImage1).ToArray();
            var mono2images = tomatoes.Select(desc => desc.GrayImage2).ToArray();
            var labels = tomatoes.Select(desc => (int)desc.Type).ToArray();
            return new ExpandedDescriptor(colorImages, mono1images, mono2images, labels);
        }
        public static Dictionary<SetType, List<Tomato>> SplitExact(
            int trainOkCount,
            int trainBadCount,
            ImageLabelledLoaderHelper helper)
        {
            var splitted = splitClasses(helper.Images);
            var trainOk = splitted[TomatoType.GOOD].Take(trainOkCount);
            var trainBad = splitted[TomatoType.BAD].Take(trainBadCount);
            var validationOk = splitted[TomatoType.GOOD].Skip(trainOkCount).Take(helper.GoodTomatoCount - trainOkCount);
            var validationBad = splitted[TomatoType.BAD].Skip(trainBadCount).Take(helper.BadTomatoCount - trainBadCount);
            var result = new Dictionary<SetType, List<Tomato>>();
            result.Add(SetType.TRANING,trainOk.Concat(trainBad).ToList());
            result.Add(SetType.VALIDATION, validationOk.Concat(validationBad).ToList());
            return result;
        }

        public static Dictionary<SetType, List<Tomato>> SplitAll(ImageLabelledLoaderHelper helper)
        {
            var splitted = splitClasses(helper.Images);
            var trainOk = splitted[TomatoType.GOOD];
            var trainBad = splitted[TomatoType.BAD];
            var result = new Dictionary<SetType, List<Tomato>>();
            result.Add(SetType.TRANING, trainOk.Concat(trainBad).ToList());
            result.Add(SetType.VALIDATION, new List<Tomato>());
            return result;
        }

        public static List<Tomato> AugmentData(List<Tomato> tomatoes)
        {
            var copyTomatoes = new List<Tomato>(tomatoes);
            var tomatosAugmented = new List<Tomato>();
            foreach(Tomato norm in tomatoes)
            {
                var flipH = new Tomato(
                    (int) norm.Type,
                    norm.ColoredImage.Flip(FlipType.Horizontal),
                    norm.GrayImage1.Flip(FlipType.Horizontal),
                    norm.GrayImage2.Flip(FlipType.Horizontal),
                    norm.Name
                );
                var flipV = new Tomato(
                   (int)norm.Type,
                    norm.ColoredImage.Flip(FlipType.Vertical),
                    norm.GrayImage1.Flip(FlipType.Vertical),
                    norm.GrayImage2.Flip(FlipType.Vertical),
                    norm.Name
                );
                tomatosAugmented.Add(flipV);
                tomatosAugmented.Add(flipH);
            }
            copyTomatoes.AddRange(tomatosAugmented);
            return copyTomatoes;
        }

        public static Dictionary<TomatoType, List<Tomato>> splitClasses(List<Tomato> descriptors)
        {
            var grouppedImage = descriptors.GroupBy(descriptor => descriptor.Type)
                .Select(desc => new Tuple<TomatoType, List<Tomato>>(desc.Key, desc.ToList()))
                .ToDictionary(elem => elem.Item1, elem => elem.Item2);
            return grouppedImage;
        }

    }
}
