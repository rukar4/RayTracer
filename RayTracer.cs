using System;
using System.Diagnostics.Contracts;
using System.IO;

class RayTracer {
    // Light Vectors
    private static Vector dirToLight = new Vector(0.0, 1.0, 0.0);
    private static Vector ambient = new Vector(0.0, 0.0, 0.0);

    // Color vectors
    private static Vector bgColor = new Vector(0.2, 0.2, 0.2);
    private static Vector lightColor = new Vector(1.0, 1.0, 1.0);
    
    static void Main() {
        double aspectRatio = 16.0 / 9.0;
        int width = 400;

        // Find image height using aspect ratio.
        int height = (int) (width / aspectRatio);
        height = height < 1 ? 1 : height;

        // Field of View
        double fov = 90.0;

        // Compute viewport size
        double viewportHeight = 2.0 * Math.Tan(fov * Math.PI / 180.0 / 2);
        double viewportWidth = viewportHeight * ((double) width / height);

        // Camera variables
        double focalLength = 1.0;
        Vector camDir = new Vector(0, 0, 0);
        Vector camCenter = new Vector(0, 0, 1);
        Vector camUp = new Vector(0, 1, 0);
        
        // Viewport vectors
        Vector viewportU = new Vector(viewportWidth, 0, 0);
        Vector viewportV = new Vector(0, -viewportHeight, 0);

        // Pixel sizes
        Vector du = viewportU / width;
        Vector dv = viewportV / height;


        // Compute upper left pixel
        Vector viewPortUpperLeft = camCenter - new Vector(0, 0, focalLength) - viewportU / 2 - viewportV / 2;
        Vector startPixel = viewPortUpperLeft + (du + dv) / 2;

        PPMWriter writer = new PPMWriter("sphere.ppm", height, width);
        for (int i = 0; i < height; ++i) {
            Console.Write($"\rScanlines remaining: {height - i} ");
            Console.Out.Flush();

            for (int j = 0; j < width; ++j) {
                var pixelCenter = startPixel + (i * du) + (j * dv);
                var rayDirection = pixelCenter - camCenter;

                Ray ray = new Ray(camCenter, rayDirection);
                Vector pixelColor = GetRayColor(ray);

                writer.WriteRGB(pixelColor);
            }

            writer.WriteLine();
        }
        writer.Dispose();

        Console.WriteLine("\nDone         \n");
    }

    private static Vector GetRayColor(Ray ray) {
        return bgColor;
    }
}