using System.Numerics;

namespace RayTracerCs
{
    public class Camera
    {
        public Camera(Vector3 origin, Quaternion direction, Vector3 viewAngle)
        {
            Origin = origin;
            Direction = direction;
            ViewAngle = viewAngle;
        }

        public Vector3 Origin { get; }
        public Quaternion Direction { get; }
        public Vector3 ViewAngle { get; }

        public Ray CastRay(Vector2 pixel)
        {
            return new Ray(
                Origin,
                Vector3.Normalize(
                    Vector3.Transform(
                        new Vector3(pixel.X * ViewAngle.X, pixel.Y * ViewAngle.Y, ViewAngle.Z),
                        Direction
                    )
                ),
                SpectralColor.One()
            );
        }
    }
}