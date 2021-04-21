module RayTracer.Ray

open System.Numerics
open Color

type Ray =
    { origin: Vector3
      direction: Quaternion
      color: SpectralColor }

module Ray =
    let at (distance: float32) (ray: Ray): Vector3 =
        Vector3.Transform(Vector3.UnitZ, ray.direction)
        * distance
        + ray.origin
