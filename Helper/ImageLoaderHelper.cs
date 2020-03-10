using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System;
using Challenge;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Helper
{
    /// <summary>
    /// Channel using to load grayscale image
    /// </summary>
    public enum MonoChannel
    {
        MONO1, MONO2
    }
    /// <summary>
    /// Skeleton of image loader data set. 
    /// This class uses a file descriptor to find tomato images.
    /// </summary>
    public abstract class CommonImageLoader
    {
        protected const String COLOR_NAME = "Color";
        protected const String MONO1_NAME = "Mono_01";
        protected const String MONO2_NAME = "Mono_02";
        protected readonly char sep;
        protected List<Tomato> _descriptors = new List<Tomato>();
        
        public List<Tomato> Images
        {
            get => new List<Tomato>(_descriptors);
        }
        
        public CommonImageLoader(String indexFilePath, char separator)
        {
            this.sep = separator;
            LoadFromPath(indexFilePath);
        }

        protected abstract void LoadFromPath(string path);

        protected Tuple<Image<Bgr, Byte>, Image<Gray, Byte>, Image<Gray, Byte>> LoadImages(String directory, String colorNameFile)
        {
            var mono1fileName = colorNameFile.Replace(COLOR_NAME, MONO1_NAME);
            var mono2fileName = colorNameFile.Replace(COLOR_NAME, MONO2_NAME);
            return new Tuple<Image<Bgr, Byte>, Image<Gray, Byte>, Image<Gray, Byte>>(
                new Image<Bgr, Byte>(Path.Combine(directory, colorNameFile)),
                new Image<Gray, Byte>(Path.Combine(directory, mono1fileName)),
                new Image<Gray, Byte>(Path.Combine(directory, mono2fileName))
            );
        }
    }
    /**
     * for each image, there is a label that tells the tomato type (bad or ok).
     */
    public class ImageLabelledLoaderHelper : CommonImageLoader
    {
        public int GoodTomatoCount
        {
            get; private set;
        }
        public int BadTomatoCount
        {
            get; private set;
        }


        public ImageLabelledLoaderHelper(string indexFilePath, char sep) : base(indexFilePath, sep) { }

        protected override void LoadFromPath(string path)
        {
            var dir = Path.GetDirectoryName(path);
            var rows = File.ReadAllLines(path);
            foreach (String row in rows)
            {
                var splitted = row.Split(sep);
                var label = Int16.Parse(splitted[1]);
                if ((TomatoType)label == TomatoType.GOOD) 
                {
                    GoodTomatoCount++;
                } 
                else 
                {
                    BadTomatoCount++;
                }
                var images = LoadImages(dir, splitted[0]);
                var tomatoType = Int16.Parse(splitted[1]);
                _descriptors.Add(
                    new Tomato(tomatoType, images.Item1, images.Item2, images.Item3, splitted[0])
                );
            }
        }
    }
    /**
     * the class load images using the file descriptor.
     */
    public class ImageLoaderHelper : CommonImageLoader
    {
        public ImageLoaderHelper(string indexFilePath) : base(indexFilePath, '\t') { }

        protected override void LoadFromPath(string path)
        {
            var dir = Path.GetDirectoryName(path);
            var rows = File.ReadAllLines(path);
            foreach (String row in rows)
            {

                var images = LoadImages(dir, row);
                var tomatoType = TomatoType.UNCLASSIFIED;
                _descriptors.Add(
                    new Tomato((int)tomatoType, images.Item1, images.Item2, images.Item3, row)
                );
            }
        }
    }
}
