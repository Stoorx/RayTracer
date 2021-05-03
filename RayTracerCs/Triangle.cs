using System;
using System.Numerics;

namespace RayTracerCs
{
    public struct Triangle
    {
        public Triangle(Vector3 a, Vector3 b, Vector3 c, TracerObject owner = null)
        {
            A = a;
            B = b;
            C = c;
            Owner = owner;
        }

        public Vector3 A { get; }
        public Vector3 B { get; }
        public Vector3 C { get; }
        public TracerObject Owner { get; }

        public Vector3 Cross() => Vector3.Cross(B - A, C - A);
        public Vector3 Normal() => Vector3.Normalize(Cross());
        public float Square() => Cross().Length();

        public Vector3? Hit(Ray ray)
        {
            var epsilon = 1e-9f;
            var e1 = this.B - this.A;
            var e2 = this.C - this.A;
            var pvec = Vector3.Cross(ray.Direction, e2);
            var det = Vector3.Dot(e1, pvec);

            if (det < epsilon && det > -epsilon)
            {
                return null;
            }
            else
            {
                var invdet = 1f / det;
                var tvec = ray.Origin - this.A;
                var u = (Vector3.Dot(tvec, pvec)) * invdet;

                if (u < 0f || u > 1f)
                {
                    return null;
                }
                else
                {
                    var qvec = Vector3.Cross(tvec, e1);

                    var v =
                        (Vector3.Dot(ray.Direction, qvec)) * invdet;

                    if (v < 0f || u + v > 1f)
                    {
                        return null;
                    }
                    else
                    {
                        var t = Vector3.Dot(e2, qvec) * invdet;
                        if (t <= 0f)
                            return null;
                        else
                            return ray.At(t);
                    }
                }
            }
        }

        public Triangle Translate(Vector3 origin, Quaternion rotation)
        {
            Vector3 TranslatePoint(Vector3 p) => Vector3.Add(Vector3.Transform(p, rotation), origin);
            return new Triangle(TranslatePoint(A), TranslatePoint(B), TranslatePoint(C), Owner);
        }
    }
}