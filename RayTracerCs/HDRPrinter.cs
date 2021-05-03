using System;
using System.Text;

namespace RayTracerCs
{
    public class HDRPrinter
    {
        public static string StringifyImage(Image image)
        {
            var w = image.Width;
            var h = image.Height;
            var sb = new StringBuilder();
            sb.AppendLine($"{w.ToString()} {h.ToString()}");
            lock (image)
            {
                for (int r = 0; r < h; r++)
                {
                    for (int c = 0; c < w; c++)
                    {
                        var color = image.Pixels[c, r];
                        sb.AppendLine(String.Join(':', color.Values));
                    }
                }
            }

            return sb.ToString();
        }
    }
}