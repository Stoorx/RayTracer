using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace RayTracerCs
{
    class Program
    {
        static void Main(string[] args)
        {
            var camera = new Camera(new Vector3(0f, 0f, -1f), Quaternion.Identity, new Vector3(1f, 1f, 1f));
            var lights = new LightSource(
                new Vector3(0f, 1f, 1f),
                Quaternion.Identity,
                new List<Triangle>()
                {
                    new Triangle(new Vector3(-0.1f, 0.1f, 0f), new Vector3(0.1f, 0.1f, 0f),
                        new Vector3(-0.1f, -0.1f, 0f)),
                    new Triangle(new Vector3(0.1f, 0.1f, 0f), new Vector3(0.1f, -0.1f, 0f),
                        new Vector3(-0.1f, -0.1f, 0f))
                },
                100,
                SpectralColor.EqualEnergetic());
            var objects = new MaterialObject(
                new Vector3(0f, 0f, 1f),
                Quaternion.Identity,
                new List<Triangle>()
                {
                    new Triangle(new Vector3(-0.1f, 0.9f, 0f), new Vector3(0.1f, 0.1f, 0f),
                        new Vector3(-0.1f, -0.1f, 0f)),
                    new Triangle(new Vector3(0.1f, 0.1f, 0f), new Vector3(0.1f, -0.1f, 0.2f),
                        new Vector3(-0.1f, -0.1f, 0f))
                },
                SpectralColor.One()
            );
            var scene = new Scene(new List<MaterialObject>() {objects}, new List<LightSource>() {lights});

            var renderer = new Renderer(scene, camera);
            var image = renderer.Render(100, 100);
            var imgStr = HDRPrinter.StringifyImage(image);
            File.WriteAllText("img.txt", imgStr);
        }
    }
}