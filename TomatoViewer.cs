using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Challenge;
using Challenge.Helper;
using Emgu.CV;
using Emgu.CV.Structure;
using Challenge.Classifier.Extractor;
using System.ComponentModel;

namespace Challenge.View
{
    /// <summary>
    /// This enumeration describes the button type in this form.
    /// </summary>
    enum ButtonType
    {
        EXTRACT_TRAIN, EXTRACT_VALIDATION, CHECK_PERFORMANCE
    }
    /// <summary>
    /// This class is the graphical part of the MVC application showing tomato feature and the classifier performance (in terms of errors).
    /// The main objectives are:
    ///     - extract and visualize features (and the extraction progress) from the training set;
    ///     - extract and visualize features (and the extraction progress) from the training set;
    ///     - train and verify classifier performance in terms of classification mismatch;
    ///     - show classification mismatch.
    /// </summary>
    public class TomatoViewer
    {
        //GUI components
        private const int BUTTON_WIDTH = 150;
        private const int RIGHT_PADDING = 30;
        private const int IMAGE_SIZE = 128;
        private Form form = new Form();
        private ListView imagesContainer = new ListView();
        private ImageList images = new ImageList();
        private BackgroundWorker imageLoader = new BackgroundWorker(); //used to do background task (feature extraction, image classification..)
        private List<Button> buttons;
        //utilities attributes
        private ILogger log = Logger.OnConsole();
        private int maxImageShowCount;
        public Controller Controller { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="windowsSize"></param>
        /// <param name="maxImageShowCount">the max number of image in the image list.</param>
        public TomatoViewer(Size windowsSize, int maxImageShowCount = 1000)
        {
            form.Text = "Tomato classifier";
            this.maxImageShowCount = maxImageShowCount;
            //Init all controls
            InitBackgroundTask();
            InitButtons();
            InitImageList(windowsSize);
            //init main container
            form.SetBounds(0, 0, windowsSize.Width, windowsSize.Height);
            var mainPane = new FlowLayoutPanel();
            mainPane.Width = windowsSize.Width;
            mainPane.Height = windowsSize.Height;
            //add controls into the scene.
            buttons.ForEach(button => mainPane.Controls.Add(button));
            mainPane.Controls.Add(imagesContainer);
            form.Controls.Add(mainPane);
        }

        private void InitBackgroundTask()
        {
            imageLoader.DoWork += OnClick;
            imageLoader.RunWorkerCompleted += OnEnd;
        }
        private void InitButtons()
        {
            buttons = new Button[] { new Button(), new Button(), new Button() }.ToList();
            int i = 0;
            buttons.ForEach(button => InitButton(button, (ButtonType)i++));
        }

        private void InitButton(Button button, ButtonType type)
        {
            button.Width = BUTTON_WIDTH;
            button.Text = type.ToString().ToLower().Replace("_", " ");
            button.Click += (sender, ev) =>
            {
                buttons.ForEach(b => b.Enabled = false);
                imageLoader.RunWorkerAsync(type);
            };
        }
        
        private void InitImageList(Size size)
        {
            imagesContainer.LargeImageList = images;
            imagesContainer.View = System.Windows.Forms.View.LargeIcon;
            images.ImageSize = new Size(IMAGE_SIZE, IMAGE_SIZE);
            images.ColorDepth = ColorDepth.Depth16Bit;
            imagesContainer.Width = size.Width - RIGHT_PADDING;
            imagesContainer.Height = size.Height / 2;
        }
        //used by image loader to create an asynk task
        private void OnClick(object sender, DoWorkEventArgs e)
        { 
            switch ((ButtonType)e.Argument)
            {
                case ButtonType.EXTRACT_TRAIN:
                    Controller.ExtractOnTrain();
                    break;
                case ButtonType.EXTRACT_VALIDATION:
                    Controller.ExtractOnValidation();
                    break;
                case ButtonType.CHECK_PERFORMANCE:
                    Controller.CheckPerformance();
                    break;
            }
        }
        //at the end of computations, the buttons must be enabled.
        private void OnEnd(object sender, RunWorkerCompletedEventArgs e)
        {
            buttons.ForEach(button => button.Enabled = true);
        }
        public void Show()
        {
            Application.Run(form);
        }
        /// <summary>
        /// Add images on the images list, this operation is thread-safe and could be done by another thread.
        /// </summary>
        /// <param name="images"> The images that the user wants to put into the image list</param>
        public void AddImages(List<VisualEvent> images)    
        {
            RunOnUiThread(() =>
            {
                if (this.images.Images.Count < maxImageShowCount) //bound used to limit the total images in the GUI
                {
                    foreach (VisualEvent tomatoImage in images)
                    {
                        AddImage(tomatoImage.Description, tomatoImage.Image);
                    }
                }
            });
            log.LogNewLine("ends..");
        }
        private void AddImage(String description, Bitmap image)
        {
            if (images.Images.Count < maxImageShowCount)
            {
                this.images.Images.Add(image);
                ListViewItem item = new ListViewItem();
                item.Text = description;
                item.ImageIndex = this.images.Images.Count - 1;
                this.imagesContainer.Items.Add(item);
            }
        }
        /// <summary>
        /// Removes all images on the image list. This operation is thread-safe.
        /// </summary>
        public void ClearImages()
        {
            RunOnUiThread(() =>
            {
                this.imagesContainer.Items.Clear();
                images.Images.Clear();
            });
        }
        //utility method used to wrap external method invocation inside the view thread.
        private void RunOnUiThread(Action action)
        {
            form.Invoke(action);
        }
    }
}
