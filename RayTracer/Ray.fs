module RayTracer.Ray


open System.Numerics
open Color

type Ray = {
    origin : Vector3;
    direction : Quaternion;
    color : SpectralColor
}