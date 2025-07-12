using Models.Props;

class RayTracer
{
    private static bool computeShadows = true;

    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: dotnet run [--no-shadows] <output directory> <scene1.txt> <scene2.txt> ...");
            return;
        }

        string? outputDir = null;
        List<string> sceneFiles = new();

        foreach (string arg in args)
        {
            if (arg == "--no-shadows")
            {
                computeShadows = false;
            }
            else if (outputDir == null)
            {
                outputDir = arg;
            }
            else
            {
                sceneFiles.Add(arg);
            }
        }

        if (outputDir == null || sceneFiles.Count == 0)
        {
            Console.WriteLine("Error: Output directory and at least one scene file are required.");
            Console.WriteLine("Usage: dotnet run [--no-shadows] <output directory> <scene1.txt> <scene2.txt> ...");
            return;
        }

        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        double aspectRatio = 1.0;
        int width = 1080;

        // Find image height using aspect ratio.
        int height = (int)(width / aspectRatio);
        height = height < 1 ? 1 : height;


        foreach (string sceneFile in sceneFiles)
        {
            try
            {
                Scene scene = SceneParser.Parse(sceneFile);
                // Compute viewport size
                double viewportHeight = 2.0 * Math.Tan(scene.fov * Math.PI / 180.0 / 2);
                double viewportWidth = viewportHeight;

                // Viewport vectors
                Vector viewportU = new Vector(viewportWidth, 0, 0);
                Vector viewportV = new Vector(0, -viewportHeight, 0);

                // Pixel sizes
                Vector du = viewportU / width;
                Vector dv = viewportV / height;

                // Compute upper left pixel
                Vector viewPortUpperLeft = scene.camCenter - new Vector(0, 0, scene.focalLength) - viewportU / 2 - viewportV / 2;
                Vector startPixel = viewPortUpperLeft + (du + dv) / 2;

                // Intialize writer
                PPMWriter writer = new PPMWriter($"{outputDir}/{scene.GetFileName()}", height, width);
                for (int i = 0; i < height; ++i)
                {
                    Console.Write($"\rScanlines remaining: {height - i} ");
                    Console.Out.Flush();

                    for (int j = 0; j < width; ++j)
                    {
                        var pixelCenter = startPixel + (i * dv) + (j * du);
                        var rayDirection = pixelCenter - scene.camCenter;

                        Ray ray = new Ray(scene.camCenter, rayDirection);
                        Vector pixelColor = GetPixel(ray, scene, scene.camCenter);

                        writer.WriteRGB(pixelColor);
                    }

                    writer.WriteLine();
                }
                writer.Dispose();

                Console.WriteLine("\nDone         \n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError processing {sceneFile}: {ex.Message}\n");
            }

        }
    }

    private static Vector GetPixel(Ray ray, Scene scene, Vector camCenter)
    {
        Vector color = scene.GetBG();

        double closest = double.MaxValue;
        foreach (Prop prop in scene.GetProps())
        {
            Vector? point = prop.GetIntersection(ray);
            if (point != null)
            {
                double distance = (point - ray.origin).Magnitude();

                if (distance < closest)
                {
                    closest = distance;
                    color = prop.GetSurfaceColor(point, camCenter, scene, 0, computeShadows);
                }
            }
        }
        return color;
    }
}
