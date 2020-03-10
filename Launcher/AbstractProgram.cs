using Challenge.Helper;
using Challenge.Helper.Set;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Program
{
    /// <summary>
    /// The abstract skeleton of a program described in this context.
    /// It assumes that each program are based on a dir where are placed index to retrieve tomato images.
    /// </summary>
    public abstract class AbstractProgram
    {
        protected readonly ILogger log;
        protected readonly string indexFilePath;
        protected readonly string tomatoDirPath;
        private readonly SplitStrategy splitStrategy;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tomatoDirPath"> Base dir of images and index files </param>
        /// <param name="indexFileName"> Main index file used to load train tomato images</param>
        /// <param name="split"> This object tells how to split the image load from the index</param>
        public AbstractProgram(string tomatoDirPath, 
            string indexFileName,
            SplitStrategy split)
        {
            log = Logger.OnConsole();
            this.tomatoDirPath = tomatoDirPath;
            this.indexFilePath = Path.Combine(tomatoDirPath, indexFileName);
            this.splitStrategy = split;
        }
        
        public void Start()
        {
            //template method, each program choose its application logic.
            var imageLoader = new ImageLabelledLoaderHelper(indexFilePath, '\t');
            OnStart(splitStrategy.Invoke(imageLoader));
        }
        protected abstract void OnStart(IDictionary<SetType, List<Tomato>> imageSets);
        //utility method used to compute a path starts from the base dir
        protected string ComputePath(string fileName)
        {
            return Path.Combine(tomatoDirPath, fileName);
        }

    }
}
