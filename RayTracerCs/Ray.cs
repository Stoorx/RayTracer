using System.Numerics;

namespace RayTracerCs
{
    public struct Ray
    {
        public Ray(Vector3 origin, Vector3 direction, SpectralColor color)
        {
            Origin = origin;
            Direction = direction;
            ColorFilter = color;
        }

        public Vector3 Origin { get; }
        public Vector3 Direction { get; }
        public SpectralColor ColorFilter { get; }
        public Vector3 At(float distance) => Origin + Direction * distance;

        public Ray Reflect(Vector3 normal, Vector3 point, SpectralColor colorFilter)
        {
            return new Ray(
                point,
                Vector3.Reflect(Direction, normal),
                ColorFilter.ApplyFilter(colorFilter)
            );
        }
    }
}