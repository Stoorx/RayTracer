module RayTracer.Ray

open System.Numerics
open Color

type Ray =
    { origin: Vector3
      direction: Vector3
      color: SpectralColor }

module Ray =
    let at (ray: Ray) (distance: float32): Vector3 = ray.direction * distance + ray.origin
