module RayTracer.Image

open RayTracer.Color

type Image =
    { pixels: SpectralColor [,]
      hitsCount: uint [,] }

module Image =

    let init w h =
        { pixels = Array2D.create w h SpectralColor.initZero
          hitsCount = Array2D.create w h 0u }

    let getWidth i = i.pixels.GetLength 0
    let getHeight i = i.pixels.GetLength 1

    let accumulatePixel (image: Image) x y (luminance: SpectralColor) =
        let pixel = image.pixels.[x, y]
        let hits = image.hitsCount.[x, y] + 1u

        let newPixelValues =
            Array.map2 (fun o n -> o + ((n - o) / (hits |> float32))) pixel.values luminance.values

        lock image (fun () ->
            image.pixels.[x, y] <- { values = newPixelValues }
            image.hitsCount.[x, y] <- hits)
