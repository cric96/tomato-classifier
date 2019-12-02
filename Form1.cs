using Emgu.CV;
using Emgu.CV.ML;
using Emgu.CV.ML.MlEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace _01_TextureClassification
{
    public partial class Form1 : Form
    {
        string indexFilePath = "";
        Image<Gray, double> avgCl0, avgCl1, avgCl2, avgCl3;
        Image<Bgr, byte> testImage;
        Matrix<double> testCoOccurrrenceMatrix;
        TextureClassifier classifier;   
        int m = 100000;  //Magnification factor for visualization 
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonTraining_Click(object sender, EventArgs e)
        {
            // Train the texture classifier using training images and labels
            int[] trainingLabels = null;
            var images = LoadImagesAndLabels(indexFilePath, out trainingLabels);
            int k = (int)numericUpDownK.Value;
            classifier = new TextureClassifier(k);
            Matrix<double>[] avgCoOccurrMatrices;
            classifier.Train(images, trainingLabels, out avgCoOccurrMatrices);
            UpdateImages(avgCoOccurrMatrices);
        }

        private void UpdateImages(Matrix<double>[] avgCoOccurrMatrices)
        {
            // Show the average unoriented co-occurrence matrix of each class
            avgCl0 = new Image<Gray, double>(256, 256);
            avgCl1 = new Image<Gray, double>(256, 256);
            avgCl2 = new Image<Gray, double>(256, 256);
            avgCl3 = new Image<Gray, double>(256, 256);
            (avgCoOccurrMatrices[0] * m).CopyTo(avgCl0);
            imageBoxCM_0.Image = avgCl0;
            (avgCoOccurrMatrices[1] * m).CopyTo(avgCl1);
            imageBoxCM_1.Image = avgCl1;
            (avgCoOccurrMatrices[2] * m).CopyTo(avgCl2);
            imageBoxCM_2.Image = avgCl2;
            (avgCoOccurrMatrices[3] * m).CopyTo(avgCl3);
            imageBoxCM_3.Image = avgCl3;
        }

        private void buttonLoadTestImage_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Jpg images(*.jpg) | *.jpg";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ClearBg();
                    textBoxTestImagePath.Text = dlg.FileName;
                    testImage = new Image<Bgr, byte>(dlg.FileName);
                    imageBoxTestInputImage.Image = testImage;
                    int cl = classifier.Test(testImage.Convert<Gray, byte>(), out testCoOccurrrenceMatrix);
                    var img = new Image<Gray, double>(256, 256);
                    (testCoOccurrrenceMatrix * m).CopyTo(img);
                    imageBoxTestCoOccMatrix.Image = img;
                    // Highlight estimated class
                    switch (cl)
                    {
                        case 0:
                            imageBoxCM_Fields.BackColor = Color.LightBlue;
                            break;
                        case 1:
                            imageBoxCM_Houses.BackColor = Color.LightBlue;
                            break;
                        case 2:
                            imageBoxCM_River.BackColor = Color.LightBlue;
                            break;
                        case 3:
                            imageBoxCM_Wood.BackColor = Color.LightBlue;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void ClearBg()
        {
            // Clear selection
            imageBoxCM_Fields.BackColor = Color.White;
            imageBoxCM_Houses.BackColor = Color.White;
            imageBoxCM_River.BackColor = Color.White;
            imageBoxCM_Wood.BackColor = Color.White;
        }

        private Image<Gray, byte>[] LoadImagesAndLabels(string indexFilePath, out int[] trainingLabels)
        {
            // Load images and labels from index file
            var dir = Path.GetDirectoryName(indexFilePath);
            var rows = File.ReadAllLines(indexFilePath);
            var trainingImages = new Image<Gray, byte>[rows.Length];
            trainingLabels = new int[rows.Length];

            for (int r = 0; r < rows.Length; r++)
            {
                var info = rows[r].Split('\t');
                trainingImages[r] = new Image<Gray, byte>(Path.Combine(dir, info[0]));
                trainingLabels[r] = Convert.ToInt32(info[1]);
            }
            return trainingImages;
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Txt Files(*.txt)|*.txt";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    textBoxIdxFilePath.Text = dlg.FileName;
                    indexFilePath = dlg.FileName;
                }
            }
        }

        private void tabControlTraining_Selected(object sender, TabControlEventArgs e)
        {
            // Show class co-occurrence matrices
            imageBoxCM_Fields.Image = avgCl0;
            imageBoxCM_Houses.Image = avgCl1;
            imageBoxCM_River.Image = avgCl2;
            imageBoxCM_Wood.Image = avgCl3;
        }
    }
}
