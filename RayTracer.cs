using System;
using System.IO;

class RayTracer {
    static void Main() {
        int height = 256;
        int width = 256;

        PPMWriter writer = new PPMWriter("sphere.ppm", height, width);

        for (int i = 0; i < height; ++i) {
            Console.Write($"\rScanlines remaining: {height - i} ");
            Console.Out.Flush();
            for (int j = 0; j < width; ++j) {
                Vector color = new Vector((double) i / (width - 1), 0.0, (double) j / (height - 1));
                writer.WriteRGB(color);
            }
            writer.WriteLine();
        }
        writer.Dispose();

        Console.WriteLine("\nDone         \n");
    }
}