using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Emgu.CV.UI;

namespace Challenge.Helper
{
    using Range = Tuple<int, int>;
    public static class ImageHelper
    {

        /// <summary>
        /// remove the background using the given mask
        /// </summary>
        /// <typeparam name="C">The image color (Gray, Bgr...)</typeparam>
        /// <typeparam name="D">The color depth (Byte, float,...)</typeparam>
        /// <returns>new image with a uniform background (black color)</returns>
        public static Image<C,D> RemoveBackground<C,D>(Image<C,D> image, Image<Gray, Byte> mask) 
            where C : struct, IColor
            where D : new()
        {
            return image.Copy(mask);
        }

        private static Range EmptyRange = new Range(0, 0);
        private static int Start(this Tuple<int, int> range)
        {
            return range.Item1;
        }
        private static int End(this Tuple<int, int> range)
        {
            return range.Item2;
        }
        private static int Size(this Tuple<int, int> range)
        {
            return range.Item2 - range.Item1;
        }
        private static bool isGreater(this Range first, Range second)
        {
            if (second == null) { return true; }
            return first.Size() > second.Size() ? true : false;
        }

        /// <summary>
        /// Compute a bounding box of the largest object with a determined colour. 
        /// </summary>
        /// <param name="image">a binary image</param>
        /// <param name="high">the colour to find in the image</param>
        /// <returns>The rectangle of the biggest object in the foreground</returns>
        public static Rectangle GetMaxBoundingBox(Image<Gray, Byte> image, Gray high)
        {
            var cols = image.Cols;
            var rows = image.Rows;
            var dilated = image.Dilate(1);
            Range maxRangeX = maxRangeIn(rows, cols, (x, y) => dilated[x, y], high);
            Range maxRangeY = maxRangeIn(cols, rows, (x, y) => dilated[y, x], high);
            return new Rectangle(maxRangeX.Start(),maxRangeY.Start(),maxRangeX.Size(),maxRangeY.Size());
        }
        private static Range maxRangeIn(int outerCount, int innerCount, Func<int, int, Gray> pixelSelection, Gray high)
        {
            var range = EmptyRange;
            for (int x = 0; x < outerCount; x++)
            {
                int start = -1;
                int end = 0;
                for (int y = 0; y < innerCount; y++)
                {
                    var pixel = pixelSelection(x, y);
                    if (pixel.Equals(high) && start == -1)
                    {
                        start = y;
                        end = y;
                    }
                    else if (pixel.Equals(high))
                    {
                        end++;
                    }
                    else if (start != -1)
                    {
                        var newRange = new Range(start, end);
                        if (newRange.isGreater(range))
                        {
                            range = newRange;
                        }
                    } else
                    {
                        start = -1;
                    }
                }
            }
            return range;
        }
    }
}
