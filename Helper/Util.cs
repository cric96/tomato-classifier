using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Helper
{
    using BiolabImage = BioLab.ImageProcessing.Image<byte>;
    public class Util
    {
        public static float[,] AdaptFeature(float[][] data)
        {
            int featureSize = data[0].Length;
            var adaptedFeature = new float[data.Length, featureSize];
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < featureSize; j++)
                {
                    adaptedFeature[i, j] = data[i][j];
                }
            }
            return adaptedFeature;
        }
        /**
         * using to adapt opencv image into biolab library
         */
        public static BiolabImage ToBiolabImage(Image<Gray, byte> cv)
        {
            var adapted = new BioLab.ImageProcessing.Image<byte>(cv.Width, cv.Height);
            for (int row = 0; row < cv.Rows; row++)
            {
                for (int column = 0; column < cv.Cols; column++)
                {
                    adapted[row, column] = (byte)cv[row, column].Intensity;
                }
            }
            return adapted;
        }

        public static double Sigmoid(double value)
        {
            return 1.0 / (1.0 + Math.Exp(-value));
        }
    }
}
