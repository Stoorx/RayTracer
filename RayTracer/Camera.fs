module RayTracer.Camera

open System
open System.Numerics
open Ray
open Color

type Camera =
    { origin: Vector3 // x, y, z
      direction: Quaternion // yaw, pitch, roll
      viewAngle: Vector3 } // horizontal, vertical, distance

module Camera =
    let init (origin: Vector3) (direction: Quaternion) (viewAngle: Vector3) =
        { origin = origin
          direction = direction
          viewAngle = viewAngle }

    let moveCamera (camera: Camera) (origin: Vector3): Camera = { camera with origin = origin }

    let castRay (camera: Camera) (pixel: Vector2): Ray =
        let horizontalAngle =
            MathF.Atan2(pixel.X * camera.viewAngle.X, camera.viewAngle.Z)

        let verticalAngle =
            MathF.Atan2(pixel.Y * camera.viewAngle.Y, camera.viewAngle.Z)

        { origin = camera.origin
          direction =
              camera.direction
              * Quaternion.CreateFromYawPitchRoll(horizontalAngle, -verticalAngle, 0.0f)
          color = SpectralColor.initOne }
