using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace SortPixel.Services.SortServices
{
    public class SimpleSortService : ISortService
    {
        public Task<byte[]> Sort(Stream stream)
        {
            var task = new Task<byte[]>(() =>
            {
                var bitmap = new Bitmap(stream);
                var matrix = new int[bitmap.Width, bitmap.Height];
                var flat = new Color[bitmap.Width * bitmap.Height];
                
                for (int i = 0; i < bitmap.Width; i++)
                {
                    for (int j = 0; j < bitmap.Height; j++)
                    {
                        var pixel = bitmap.GetPixel(i, j);
                        flat[i + j] = pixel;    
                    }
                }

                Debug.WriteLine("matrix flatted");

                for (int i = 1; i < flat.Length; i++)
                {
                    for (int j = i-1; j >= 0; j--)
                    {
                        if(flat[i].B < flat[j].B)
                        {
                            var take = flat[j];
                            flat[j] = flat[i];
                            flat[i] = take;
                        }
                    }
                }

                Debug.WriteLine("flat sorted");

                var sortedMatrix = new int[bitmap.Width, bitmap.Height];

                for (int i = 0; i < bitmap.Width; i++)
                {
                    for (int j = 0; j < bitmap.Height; j++)
                    {
                        bitmap.SetPixel(i, j, flat[i+j]);
                    }
                }

                Debug.WriteLine("flat crossed");

                byte[] result;
                using (MemoryStream ms = new MemoryStream())
                {
                    bitmap.Save(ms, bitmap.RawFormat);
                    result = ms.ToArray();
                }
                return result;
            });
            task.Start();
            return task;
        }

        
    }
}
