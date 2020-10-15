using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace SortPixel.Services.SortServices
{
    public abstract class SortStrategyAbstract : ISortStrategy
    {
        public abstract Task<byte[]> Sort(Stream stream);

        protected virtual Color[] FlatteningBitmap(Bitmap bitmap)
        {
            var colorsArray = new Color[bitmap.Height * bitmap.Width];
            var helperIndex = 0;
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    colorsArray[helperIndex] = bitmap.GetPixel(j, i);
                    helperIndex++;
                }
            }

            return colorsArray;
        }

        protected virtual void UnflatteningColorsArray(Bitmap bitmap, Color[] colorsArray)
        {
            var helperIndex = 0;
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    bitmap.SetPixel(j, i, colorsArray[helperIndex]);
                    helperIndex++;
                }
            }
        }

        protected int SumRGB(Color color) => color.R + color.G + color.B;
    }
}
