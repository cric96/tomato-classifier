using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challenge.Helper.Set;
using Challenge.View;

namespace Challenge.Program
{
    /// <summary>
    /// this program launch a GUI for evaluating the results of classification and extractions using the LBPExtractor and 
    /// SVMClassifier.
    /// </summary>
    class GUIProgram : AbstractProgram
    {
        private Size windowSize;
        /// <summary>
        /// for other params, <see>AbstractProgram</see>
        /// </summary>
        public GUIProgram(string tomatoDirPath, 
            string indexFileName, 
            SplitStrategy split,
            Size windowSize) : base(tomatoDirPath, indexFileName, split)
        {
            this.windowSize = windowSize;
        }

        protected override void OnStart(IDictionary<SetType, List<Tomato>> imageSets)
        {
            var controller = new Controller(imageSets);
            var view = new TomatoViewer(windowSize);
            controller.StartApplication(view);
        }
    }
}
