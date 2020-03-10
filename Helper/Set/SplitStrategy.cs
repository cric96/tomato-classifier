using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challenge.Helper;
using Challenge;
namespace Challenge.Helper.Set
{
    public delegate IDictionary<SetType, List<Tomato>> SplitStrategy(ImageLabelledLoaderHelper images);
}
