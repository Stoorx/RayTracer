using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;

namespace RayTracerCs
{
    public class TracerObject
    {
        public TracerObject(Vector3 origin, Quaternion rotation, IEnumerable<Triangle> triangles)
        {
            Origin = origin;
            Rotation = rotation;
            Triangles = triangles;
        }

        public Vector3 Origin { get; }
        public Quaternion Rotation { get; }
        public IEnumerable<Triangle> Triangles { get; }

        public IEnumerable<Triangle> TranslateToWorld()
        {
            return Triangles.Select(t => t.Translate(Origin, Rotation)).ToList();
        }
    }
}