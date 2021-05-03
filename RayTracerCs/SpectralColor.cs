using System;
using System.Linq;

namespace RayTracerCs
{
    public class SpectralColor
    {
        public SpectralColor(float[] values)
        {
            if (values.Length != 40)
                throw new ArgumentException();

            this.Values = values;
        }

        public float[] Values { get; }

        public SpectralColor Normalize()
        {
            var sum = Values.Sum();
            return new SpectralColor(Values.Select(v => v / sum).ToArray());
        }

        public SpectralColor AddLuminance(SpectralColor lumi)
        {
            return new(Values.Zip(lumi.Values, (l, r) => l + r).ToArray());
        }

        public SpectralColor ApplyFilter(SpectralColor colorFilter)
        {
            return new(Values.Zip(colorFilter.Values, (c, f) => c * f).ToArray());
        }

        public static SpectralColor Zero()
        {
            var values = new float[40];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 0;
            }

            return new SpectralColor(values);
        }

        public static SpectralColor One()
        {
            var values = new float[40];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 1.0f;
            }

            return new SpectralColor(values);
        }

        public static SpectralColor EqualEnergetic()
        {
            var values = new float[40];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 0.025f;
            }

            return new SpectralColor(values);
        }
    }
}