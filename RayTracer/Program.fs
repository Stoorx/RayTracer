module RayTracer.Main

open System
open System.Globalization
open System.Numerics
open Camera
open Ray
open Geometry
open Color
open Image
open HDRPrinter

[<EntryPoint>]
let main argv =
//    let cam =
//        Camera.init (Vector3(0f, 0f, -10f)) (Quaternion.CreateFromYawPitchRoll(1f, 0.5f, 1.2f)) (Vector3(1f, 1f, 10f))
//
//    let c = Camera.castRay cam 1u
//
//    let t1 =
//        Triangle.init (Vector3(-100f, 100f, 0f)) (Vector3(1000f, 100f, 0f)) (Vector3(-100f, -1000f, 0f))
//    let t2 =
//        Triangle.init (Vector3(-100f, 100f, -10f)) (Vector3(1000f, 100f, -10f)) (Vector3(-100f, -1000f, -10f))
//        
//    let pixels =
//        [| Vector2(-1.0f, 1.0f)
//           Vector2(0.0f, 1.0f)
//           Vector2(1.0f, 1.0f)
//
//           Vector2(-1.0f, 0.0f)
//           Vector2(0.0f, 0.0f)
//           Vector2(1.0f, 0.0f)
//
//           Vector2(-1.0f, -1.0f)
//           Vector2(0.0f, -1.0f)
//           Vector2(1.0f, -1.0f) |]
//
//    let rays = pixels |> Array.map c
//
//    let hits = rays |> Array.map (Triangle.hit t1)
//
//    let hittedRays =
//        Array.zip rays hits
//        |> Array.filter (fun (r, h) -> h.IsSome)
//        |> Array.map (fun (r, h) -> (r, Option.get h))
//
//
//    let reflectedRays =
//        hittedRays |> Array.map (fun (r, p) -> Ray.reflect r (Triangle.normal t1) p SpectralColor.initOne)
//        
//    let hitReflected =
//        reflectedRays |> Array.map (Triangle.hit t2)
//        
//    let mapper (name: string) (vectArr: Vector3 array) =
//        vectArr
//        |> Array.map (fun v ->
//            $"({v.X.ToString(CultureInfo.InvariantCulture)},{v.Y.ToString(CultureInfo.InvariantCulture)})")
//        |> String.concat "\n"
//        |> printf "----%s----\n%s\n" name 
//
//    hittedRays
//    |> Array.map (fun (r, v) -> v)
//    |> mapper "rays"
//    
//    hitReflected
//    |> Array.filter (fun o -> o.IsSome)
//    |> Array.map (fun o -> Option.get o)
//    |> mapper "reflected"

    let i = Image.init 8 4
    Image.accumulatePixel i 2 3 (SpectralColor.initEqualEnergetic)
    let pi = HDRPrinter.printImage i
    printf "%s" pi
    0
