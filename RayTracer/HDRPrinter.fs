module RayTracer.HDRPrinter

open System
open System.Text
open Image
open RayTracer.Color

module HDRPrinter =
    let printImage (image: Image) =
        let w = Image.getWidth image
        let h = Image.getHeight image
        let sb = StringBuilder()
        sb.AppendLine $"{w} {h}" |> ignore

        lock image (fun () ->
            image.pixels
            |> Array2D.iter (fun (e: SpectralColor) ->
                sb.AppendLine(e.values |> Array.map string |> String.concat ", ")
                |> ignore))

        sb.ToString()
