using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge
{
    /// <summary>
    /// the tomato classes to detect
    /// </summary>
    public enum TomatoType
    {
        GOOD = 1, BAD = 0, UNCLASSIFIED = 3
    }
    /// <summary>
    /// It is a container of each image captured used to describe a tomato
    /// </summary>
    public class Tomato
    {
        public String Name { get; private set; }
        public TomatoType Type { get; private set; }
        public Image<Bgr, Byte> ColoredImage { get; private set; }
        public Image<Gray, Byte> GrayImage1 { get; private set; }
        public Image<Gray, Byte> GrayImage2 { get; private set; }

        public Tomato(int label, 
            Image<Bgr, Byte> coloredImage, 
            Image<Gray, Byte> grayImage1, 
            Image<Gray, Byte> grayImage2,
            string name)
        {
            setType(label);
            this.ColoredImage = coloredImage;
            this.GrayImage1 = grayImage1;
            this.GrayImage2 = grayImage2;
            this.Name = name;
        }
        public Tomato(Image<Bgr, Byte> coloredImage, 
            Image<Gray, Byte> grayImage1, 
            Image<Gray, Byte> grayImage2,
            string name) : this(3, coloredImage, grayImage1, grayImage2, name) { }
        
        private void setType(int label)
        {
            if(label > 1 || label < 0)
            {
                Type = TomatoType.UNCLASSIFIED;
            } else
            {
                Type = label == 0 ? TomatoType.BAD : TomatoType.GOOD; 
            }
        }
    }
    /// <summary>
    /// It is a tomato description based on some feature represent as a float array.
    /// </summary>
    public struct TomatoDescriptor
    {
        public float[] Features { get; private set; }
        public TomatoType Label { get; private set; }

        public TomatoDescriptor(float[] features, TomatoType label)
        {
            this.Features = features;
            this.Label = label;
        }
    }
}
