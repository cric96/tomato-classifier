using System;
using System.Collections.Generic;
using System.Linq;
using BioLab.Common;
using BioLab.ImageProcessing;
using BioLab.Math.LinearAlgebra;

namespace Challenge.Feature
{
    public class LBP
    {
        # region properties and constructors
        public enum mappingType { uniform, rotationInvariant, uniformRotationInvariant };
        public enum histogramType { basic, normalized };
        public double Radius { get; set; }
        public int Neighbors { get; set; }
        public mappingType Mtype { get; set; }
        public histogramType Htype { get; set; }
        public int[] Table { get; set; }
        public int NewMax { get; set; }
        private Matrix Codes;

        public LBP()
        {
            Radius = 1;
            Neighbors = 8;
            Mtype = mappingType.uniform;
            Htype = histogramType.normalized;
            initTable();
            computeMapping();
        }

        public LBP(double myRadius, int myNeighbors, mappingType myMtype, histogramType myHtype)
        {
            Radius = myRadius;
            Neighbors = myNeighbors;
            Mtype = myMtype;
            Htype = myHtype;
            initTable();
            computeMapping();
        }

        public FeatureVector LBPcode(Image<byte> inputImage)
        {
            // number of bins in the histogram
            int bins = (int)Math.Pow(2, Neighbors);
            FeatureVector LBPhist = new FeatureVector(bins);
            double sum = 0;
            computeCodes(inputImage);
            //showCodes();

            for (int i = 0; i < Codes.RowCount; i++)
            {
                for (int j = 0; j < Codes.ColumnCount; j++)
                {
                    LBPhist[(int)Codes[i, j]] += 1;
                }
            }
            if (Htype == histogramType.normalized)
            {
                sum = LBPhist.Sum();
                for (int i = 0; i < LBPhist.Count; i++)
                {
                    LBPhist[i] /= sum;
                }

            }
            return LBPhist;
        }

        public FeatureVector LBPcode(Image<byte> inputImage, int numCols, int numRows)
        {
            // number of bins in the histogram
            int bins = (int)Math.Pow(2, Neighbors);
            var blockWidth = inputImage.Width / numCols;
            var blockHeight = inputImage.Height / numRows;
            int blockPixelCount = blockWidth * blockHeight;
            // Image binarization
            ImageBinarization binarization = new ImageBinarization(inputImage, 1);
            var binarizedImage = binarization.Execute();
            var res = new FeatureVector(numRows * numCols);
            List<double> resList = new List<double>();
            if (Mtype == mappingType.uniform || Mtype == mappingType.uniformRotationInvariant)
                bins = Neighbors * (Neighbors - 1) + 2;
            for (int x = 0; x < numCols; x++)
            {
                for (int y = 0; y < numRows; y++)
                {
                    var hist = LBPcode(inputImage.GetSubImage(x * blockWidth, y * blockHeight, blockWidth, blockHeight).ToByteImage());
                    for (int i = 0; i < bins; i++)
                    {
                        resList.Add(hist[i]);
                    }
                }
            }
            return new FeatureVector(resList.ToArray());
        }


        public void showCodes()
        {
            for (int i = 0; i < Codes.RowCount; i++)
            {
                string s = "";
                for (int j = 0; j < Codes.ColumnCount; j++)
                {
                    s += "/" + Codes[i, j].ToString();
                }
            }
        }

        public void showTable()
        {
            string s = "";
            for (int i = 0; i < Table.Length; i++)
            {
                s += "/" + Table[i].ToString();
            }
        }

        #endregion properties and constructors

        #region service methods
        private void initTable()
        {
            Table = new int[(int)Math.Pow(2, Neighbors)];
            for (int i = 0; i < Table.Length; i++)
            {
                Table[i] = i;
            }
        }
        private void computeMapping()
        {
            NewMax = 0;
            switch (Mtype)
            {
                case mappingType.uniform:
                    int index = 0;
                    NewMax = Neighbors * (Neighbors - 1) + 3;
                    for (int i = 0; i < Table.Length; i++)
                    {
                        // Rotate left
                        int j = (i << 1) & ((int)Math.Pow(2, Neighbors) - 1);
                        j = j | ((i >> (Neighbors - 1)) & 1);
                        // xor(i,j)
                        int k = i ^ j;
                        // Conto il numero di bit a 1
                        int numT = PopCount.Execute(k);
                        if (numT <= 2)
                        {
                            Table[i] = index++;
                        }
                        else
                        {
                            Table[i] = NewMax - 1;
                        }
                    }
                    break;
                case mappingType.rotationInvariant:
                    int rm, r;
                    int[] tmpMap = new int[(int)Math.Pow(2, Neighbors)];

                    for (int i = 0; i < tmpMap.Length; i++)
                    {
                        tmpMap[i] = -1;
                    }
                    for (int i = 0; i < tmpMap.Length; i++)
                    {
                        rm = i;
                        r = i;
                        for (int j = 1; j < Neighbors; j++)
                        {
                            // Rotate left
                            int k = (r << 1) & ((int)Math.Pow(2, Neighbors) - 1);
                            r = k | ((r >> (Neighbors - 1)) & 1);
                            if (r < rm)
                            {
                                rm = r;
                            }
                        }
                        if (tmpMap[rm] < 0)
                        {
                            tmpMap[rm] = NewMax++;
                        }
                        Table[i] = tmpMap[rm];
                    }

                    break;
                case mappingType.uniformRotationInvariant:
                    NewMax = Neighbors + 2;
                    for (int i = 0; i < Table.Length; i++)
                    {
                        // Rotate left
                        int j = (i << 1) & ((int)Math.Pow(2, Neighbors) - 1);
                        j = j | ((i >> (Neighbors - 1)) & 1);
                        // xor(i,j)
                        int k = i ^ j;
                        // Conto il numero di bit a 1
                        int numT = PopCount.Execute(k);
                        if (numT <= 2)
                        {
                            Table[i] = PopCount.Execute(i);
                        }
                        else
                        {
                            Table[i] = Neighbors + 1;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void computeCodes(Image<byte> inputImage)
        {
            const double eps = 1.0e-6;
            double minY, maxY, minX, maxX;
            int bSizeY, bSizeX;
            int origX, origY;
            int dx, dy;
            double x, y, tx, ty;
            int fx, fy, cx, cy, rx, ry;
            double w1, w2, w3, w4;
            Image<double> imageRegion, imageBlock;
            Image<byte> region;
            Matrix binaryMatrix;
            Matrix tempMatrix;

            // coordinates of neighbors
            Matrix sPoints = new Matrix(Neighbors, 2);  //MatrixBuilder.CreateMatrix(Neighbors, 2);
            // angle step
            double angleStep = 2 * Math.PI / Neighbors;
            for (int i = 0; i < Neighbors; i++)
            {
                sPoints[i, 0] = -Radius * Math.Sin(i * angleStep);
                sPoints[i, 1] = Radius * Math.Cos(i * angleStep);
            }

            minY = sPoints.GetColumn(0).ComputeMinimum();
            maxY = sPoints.GetColumn(0).ComputeMaximum();
            minX = sPoints.GetColumn(1).ComputeMinimum();
            maxX = sPoints.GetColumn(1).ComputeMaximum();
            // Block size, each LBP code is computes within a block of size bSizeY*bSizeX
            bSizeY = (int)(Math.Ceiling(Math.Max(maxY, 0)) - Math.Floor(Math.Min(minY, 0)) + 1);
            bSizeX = (int)(Math.Ceiling(Math.Max(maxX, 0)) - Math.Floor(Math.Min(minX, 0)) + 1);
            // Coordinates of origin (0,0) in the block
            origY = (int)Math.Abs(Math.Floor(Math.Min(minY, 0)));
            origX = (int)Math.Abs(Math.Floor(Math.Min(minX, 0)));
            // the minimum size of the input image depends on the radius
            if ((inputImage.Width < bSizeX) || (inputImage.Height < bSizeY))
            {
                throw new Exception("Input image too small.");
            }
            else
            {
                // Calculate dx and dy
                dx = inputImage.Width - bSizeX + 1;
                dy = inputImage.Height - bSizeY + 1;
                // initialize the binary matrix;
                binaryMatrix = new Matrix(dy, dx);
                // Fill the center pixel matrix 
                //imageRegion = (Image<double>)inputImage.GetSubImage(origX, origY, dx, dy);
                region = inputImage.GetSubImage(origX, origY, dx, dy).ToByteImage();
                imageRegion = new Image<double>(dx, dy);
                for (int i = 0; i < region.PixelCount; i++)
                {
                    imageRegion[i] = (double)region[i];
                }

                // Initialize Codes matrix all elements are zero
                Codes = new Matrix(dy, dx);
                tempMatrix = new Matrix(dy, dx);
                // compute LBP code 
                for (int i = 0; i < Neighbors; i++)
                {
                    y = sPoints[i, 0] + origY;
                    x = sPoints[i, 1] + origX;
                    // compute floors, ceils and rounds for x and y
                    fy = (int)Math.Floor(y);
                    fx = (int)Math.Floor(x);
                    cy = (int)Math.Ceiling(y);
                    cx = (int)Math.Ceiling(x);
                    ry = (int)Math.Round(y);
                    rx = (int)Math.Round(x);
                    // check if interpolation is needed
                    if ((Math.Abs(x - rx) < eps) && (Math.Abs(y - ry) < eps))
                    {
                        // no interpolation, use original datatypes
                        //imageBlock=(Image<double>)inputImage.GetSubImage(rx, ry, dx, dy);
                        region = inputImage.GetSubImage(rx, ry, dx, dy).ToByteImage();
                        imageBlock = new Image<double>(dx, dy);
                        for (int ii = 0; ii < region.PixelCount; ii++)
                        {
                            imageBlock[ii] = (double)region[ii];
                        }
                    }
                    else
                    {
                        //interpolation needed
                        ty = y - fy;
                        tx = x - fx;
                        // calculate interploation weights
                        w1 = (1 - tx) * (1 - ty);
                        w2 = tx * (1 - ty);
                        w3 = (1 - tx) * ty;
                        w4 = tx * ty;
                        // compute interpolated pixel values
                        imageBlock = new Image<double>(dx, dy);
                        for (int j = 0; j < dy; j++)
                        {
                            for (int k = 0; k < dx; k++)
                            {
                                imageBlock[j, k] = w1 * inputImage[fy + j, fx + k] +
                                                 w2 * inputImage[fy + j, cx + k] +
                                                 w3 * inputImage[cy + j, fx + k] +
                                                 w4 * inputImage[cy + j, cx + k];
                            }
                        }

                    }
                    // finally compute the binary matrix
                    for (int j = 0; j < dy; j++)
                    {
                        for (int k = 0; k < dx; k++)
                        {
                            binaryMatrix[j, k] = (imageBlock[j, k] >= imageRegion[j, k] ? 1 : 0);
                        }
                    }
                    // update the result matrix
                    binaryMatrix.Multiply(Math.Pow(2, i));
                    Codes += binaryMatrix;
                }
                // apply the mapping
                for (int j = 0; j < dy; j++)
                {
                    for (int k = 0; k < dx; k++)
                    {
                        Codes[j, k] = Table[(int)Codes[j, k]];
                    }
                }
            }
        }

        #endregion service methods
    }
}
