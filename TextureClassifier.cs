using Emgu.CV;
using Emgu.CV.ML;
using Emgu.CV.ML.MlEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _01_TextureClassification
{
    public class TextureClassifier
    {
        private static int CO_MATRIX_SIZE = 256;
        private KNearest knn; 
        private double[] minVals, maxVals; //range per capire la normalizzazione di questi vettori di feature
        public int K { get; set; }  // K value for the KNN classifier
        private int defaultK = 1; 

        public TextureClassifier()
        {
            K = defaultK;
        }
        public TextureClassifier(int k)
        {
            K = k;
        }

        public void Train(Image<Gray, byte>[] images, int[] trainingLabels, out Matrix<double>[] avgCoOccurrMatrices)
        {
            Matrix<float> tData;
            Matrix<int> tLab;

            // Feature extraction
            var trainingImages = SplitImagesPerClass(images, trainingLabels); //imagini splittate per classe in base alle etichette
            Matrix<double>[][] coOccurrMatrices = ComputeUnorientedCoOccurrenceMatrices(trainingImages); //
            avgCoOccurrMatrices = ComputeAverageCoOccurrenceMatricesPerClass(coOccurrMatrices);
            ExtractFeatures(coOccurrMatrices, out tData, out tLab);
            ComputeNormalizationFactors(tData);
            NormalizeData(tData);
            // KNN Classifier training
            knn = new KNearest();
            knn.DefaultK = K;
            knn.Train(new TrainData(tData, DataLayoutType.RowSample, tLab));
        }

        public int Test(Image<Gray, byte> testImg, out Matrix<double> testCoOccurrMatrix)
        {
            // Classification of a single test image
            testCoOccurrMatrix = ComputeUnorientedCoOccurrenceMatrix(testImg);
            var fv = ExtractFeatureVector(testCoOccurrMatrix);
            Matrix<float> sample = new Matrix<float>(1, fv.Length);
            for (int i = 0; i < fv.Length; i++)
            {
                sample[0, i] = fv[i];
            }
            NormalizeData(sample);
            return (int)knn.Predict(sample.GetRow(0));
        }
        //calcola la matrice non orientata per ogni immagine del traning set
        private Matrix<double>[][] ComputeUnorientedCoOccurrenceMatrices(Image<Gray, byte>[][] trainingImages)
        {
            var res = new Matrix<double>[trainingImages.GetLength(0)][];
            for (int c = 0; c < trainingImages.GetLength(0); c++)
            {
                res[c] = new Matrix<double>[trainingImages[c].Length];
                for (int i = 0; i < trainingImages[c].Length; i++)
                {
                    res[c][i] = ComputeUnorientedCoOccurrenceMatrix(trainingImages[c][i]);
                }
            }
            return res;
        }

        private Matrix<double> ComputeUnorientedCoOccurrenceMatrix(Image<Gray, byte> img)
        {
            var left = ComputeCoOccurrenceMatrix(img, -1, 0);
            var top = ComputeCoOccurrenceMatrix(img, 0, -1);
            var topLeft = ComputeCoOccurrenceMatrix(img, -1, -1);
            var topRigth = ComputeCoOccurrenceMatrix(img, 1, -1);
            var res = new Matrix<double>(CO_MATRIX_SIZE, CO_MATRIX_SIZE);
            for(int i = 0; i < CO_MATRIX_SIZE; i ++)
            {
                for(int j = 0; j < CO_MATRIX_SIZE; j++)
                {
                    res[i, j] = (left[i, j] + top[i, j] + topLeft[i, j] + topRigth[i, j]) / 4d;
                }
            }
            return res;
        }

        Matrix<double> ComputeCoOccurrenceMatrix(Image<Gray, byte> img, int dx, int dy) 
        {
            var deltaSize = 1;
            var coOccurence = new Matrix<double>(CO_MATRIX_SIZE, CO_MATRIX_SIZE);
            for(int column = deltaSize; column < img.Cols - deltaSize; column ++)
            {
                for (int row = deltaSize; row < img.Rows; row++)
                {
                    int center = (int) img[row, column].Intensity;
                    int delta = (int) img[row + dy, column + dx].Intensity;
                    coOccurence[center, delta]++;
                }
            }
            double sumValue = coOccurence.Sum;
            return coOccurence.Mul(1.0 / sumValue);
        }

        private Matrix<double>[] ComputeAverageCoOccurrenceMatricesPerClass(Matrix<double>[][] coOccurrMatrices)
        {
            var res = new Matrix<double>[coOccurrMatrices.GetLength(0)];
            for (int c = 0; c < coOccurrMatrices.GetLength(0); c++)
            {
                res[c] = new Matrix<double>(256, 256);
                for (int i = 0; i < coOccurrMatrices[c].Length; i++)
                {
                    res[c] = res[c].Add(coOccurrMatrices[c][i]);
                }
                res[c] /= coOccurrMatrices[c].Length;
            }
            return res;
        }

        private void NormalizeData(Matrix<float> tData)
        {
            // Normalize feature vectors according to the range of values observed during training
            double[] ranges = new double[minVals.Length];
            for (int i = 0; i < ranges.Length; i++)
            {
                ranges[i] = maxVals[i] - minVals[i];
            }
            for (int r = 0; r < tData.Rows; r++)
            {
                for (int c = 0; c < tData.Cols; c++)
                {
                    tData[r, c] = (float)((tData[r, c] - minVals[c]) / ranges[c]);
                }
            }
        }

        private void ComputeNormalizationFactors(Matrix<float> tData)
        {
            // Feature-wise computation of the minimum and maximum values for feature normalization
            minVals = new double[tData.Cols];
            maxVals = new double[tData.Cols];
            for (int r = 0; r < tData.Rows; r++)
            {
                for (int c = 0; c < tData.Cols; c++)
                {
                    minVals[c] = Math.Min(minVals[c], tData[r, c]);
                    maxVals[c] = Math.Max(maxVals[c], tData[r, c]);
                }
            }
        }

        private void ExtractFeatures(Matrix<double>[][] coOccurrMatrices, out Matrix<float> tData, out Matrix<int> tLab)
        {
            var tDataCount = 0;
            for (int c = 0; c < coOccurrMatrices.GetLength(0); c++)
            {
                tDataCount += coOccurrMatrices[c].Length;
            }
            tData = new Matrix<float>(tDataCount, 4);
            tLab = new Matrix<int>(tDataCount, 1);
            int idx = 0;
            for (int c = 0; c < coOccurrMatrices.GetLength(0); c++)
            {
                for (int i = 0; i < coOccurrMatrices[c].Length; i++)
                {
                    var m = coOccurrMatrices[c][i];
                    var v = ExtractFeatureVector(m); //estrai il vettore di feature da una matrice di co occorrenza 
                    for (int j = 0; j < v.Length; j++)
                    {
                        tData[idx, j] = v[j]; //la normalizzazione viene eseguita esternamente
                    }
                    tLab[idx++, 0] = c;
                }
            }
        }

        float[] ExtractFeatureVector(Matrix<double> m) //estrae le feature in quattro dimensioni
        {
            float[] res = new float[4];
            res[0] = (float)ComputeHomogeneity(m);
            res[1] = (float)ComputeContrast(m);
            res[2] = (float)ComputeEntropy(m);
            res[3] = (float)ComputeEnergy(m);
            return res;
        }
        private double ComputeHomogeneity(Matrix<double> matrix)
        {
            var powedMatrix = ApplyOn(matrix, (element,i,j) => element * element);
            var sum = powedMatrix.Sum;
            return Math.Sqrt(sum);
        }

        private double ComputeContrast(Matrix<double> matrix)
        {
            var contrastMatrix = ApplyOn(matrix, (element, i, j) => Math.Pow(i - j, 2) * element);
            var sum = contrastMatrix.Sum;
            return sum;
        }

        private double ComputeEntropy(Matrix<double> matrix)
        {

            var entropy = ApplyOn(matrix, (element, i, j) =>
            {
                if (element == 0)
                {
                    return 0;
                }
                else
                {
                    return element * Math.Log(element);
                }
            });
            var sum = entropy.Sum;
            return -sum;
        }

        private double ComputeEnergy(Matrix<double> matrix)
        {
            var energy = ApplyOn(matrix, (element, i, j) => element / (1 + Math.Abs(i - j)));
            return energy.Sum;
        }

        private Image<Gray, byte>[][] SplitImagesPerClass(Image<Gray, byte>[] images, int[] trainingLabels)
        {
            var classCount = trainingLabels.Max() + 1;
            List<Image<Gray, byte>>[] imgPerClassList = new List<Image<Gray, byte>>[classCount];
            for (int i = 0; i < classCount; i++)
            {
                imgPerClassList[i] = new List<Image<Gray, byte>>();
            }
            for (int i = 0; i < trainingLabels.Length; i++)
            {
                imgPerClassList[trainingLabels[i]].Add(images[i]);
            }
            var res = new Image<Gray, byte>[classCount][];
            for (int i = 0; i < classCount; i++)
            {
                res[i] = imgPerClassList[i].ToArray();
            }
            return res;
        }
        private Matrix<double> ApplyOn(Matrix<double> matrix, Func<double, double, double, double> strategy)
        {
            var res = new Matrix<double>(matrix.Rows, matrix.Cols);
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Cols; j++)
                {
                    res[i, j] = strategy.Invoke(matrix[i, j],i,j);
                }
            }
            return res;
        }

    }


}
