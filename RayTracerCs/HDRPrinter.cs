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
                foreach (var spectralColor in image.Pixels)
                {
                    sb.AppendJoin(", ", spectralColor.Values).AppendLine();
                }
            }

            return sb.ToString();
        }
    }
}