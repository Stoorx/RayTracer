using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;

namespace RayTracerCs
{
    public class TracerObject
    {
        public TracerObject(Vector3 origin, Quaternion rotation, List<Triangle> triangles)
        {
            Origin = origin;
            Rotation = rotation;
            Triangles = triangles;
            for (int i = 0; i < Triangles.Count; i++)
            {
                Triangles[i].Owner = this;
            }
        }

        public Vector3 Origin { get; }
        public Quaternion Rotation { get; }
        public List<Triangle> Triangles { get; }

        public List<Triangle> TranslateToWorld()
        {
            return Triangles.Select(t => t.Translate(Origin, Rotation)).ToList();
        }
    }
}