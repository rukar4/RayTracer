using Models.Props;

class RayTracer
{
    private static Vector bgColor = new Vector(0.2, 0.2, 0.2);
    private static Vector white = new Vector(1.0, 1.0, 1.0);
    private static Vector red = new Vector(1.0, 0.0, 0.0);
    private static Vector green = new Vector(0.0, 1.0, 0.0);
    private static Vector blue = new Vector(0.0, 0.0, 1.0);
    private static Vector yellow = new Vector(1.0, 1.0, 0.0);
    private static Vector barkBrown = new Vector(0.361, 0.2, 0.09);

    static void Main()
    {
        Scene scene1 = SceneParser.Parse("Scenes/scene1.txt");
        List<Scene> scenes = [scene1];

        double aspectRatio = 1.0;
        int width = 1080;

        // Find image height using aspect ratio.
        int height = (int)(width / aspectRatio);
        height = height < 1 ? 1 : height;


        foreach (Scene scene in scenes)
        {
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
            PPMWriter writer = new PPMWriter($"res/{scene.GetFileName()}", height, width);
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
                    color = prop.GetSurfaceColor(point, camCenter, scene);
                }
            }
        }
        return color;
    }

    // Scenes
    private static Scene GetScene1()
    {
        // Scene 1 Code
        Scene scene1 = new Scene(
            new Vector(),
            new Vector(0.0, 0.0, 1.0),
            new Vector(0.0, 1.0, 0.0),
            90.0,
            "scene_1.ppm"
        );

        scene1.SetLight(
            new Vector(0.0, 1.0, 0.0),
            new Vector(0.0, 0.0, 0.0),
            new Vector(1.0, 1.0, 1.0)
        );
        scene1.SetBG(bgColor);

        Sphere purpleSphere = new Sphere(0.4);
        purpleSphere.SetColor(
            0.7, 0.2, 0.1, 16.0,
            new Vector(1.0, 0.0, 1.0),
            new Vector(1.0, 1.0, 1.0)
        );

        scene1.AddProp(purpleSphere);
        return scene1;
    }
}

/*
// Program 6 Scene 1
        Scene scene4 = new Scene("scene_4.ppm");
        scene4.SetLight(new Vector(0.0, 1.0, 0.0), new Vector(), white);
        scene4.SetBG(bgColor);

        Sphere reflSphr1 = new Sphere(0.25, 0.0, 0.3, -1.0);
        reflSphr1.SetColor(0.0, 0.1, 0.1, 10.0, new Vector(0.75, 0.75, 0.75), white);
        reflSphr1.SetRefl(0.9);

        Triangle bluTri1 = new Triangle(new Vector(0.0, -0.7, -0.5), new Vector(1.0, 0.4, -1.0), new Vector(0.0, -0.7, -1.5));
        bluTri1.SetColor(0.9, 1.0, 0.1, 4.0, blue, white);

        Triangle yelTri1 = new Triangle(new Vector(0.0, -0.7, -0.5), new Vector(0.0, -0.7, -1.5), new Vector(-1.0, 0.4, -1.0));
        yelTri1.SetColor(0.9, 1.0, 0.1, 4.0, yellow, white);

        scene4.AddProps(new List<Prop> { reflSphr1, bluTri1, yelTri1 });

        // Program 6 Scene 2
        Scene scene5 = new Scene("scene_5.ppm");
        scene5.SetLight(new Vector(1.0, 0.0, 0.0), new Vector(0.1, 0.1, 0.1), white);
        scene5.SetBG(bgColor);

        Sphere whiteSphere = new Sphere(0.05, 0.5, 0.0, -0.15);
        whiteSphere.SetColor(0.8, 0.1, 0.3, 4.0, white, white);

        Sphere redSphere = new Sphere(0.08, 0.3, 0.0, -0.1);
        redSphere.SetColor(0.8, 0.8, 0.1, 32.0, red, new Vector(0.5, 1.0, 0.5));

        Sphere grnSphere = new Sphere(0.3, -0.6, 0.0, 0.0);
        grnSphere.SetColor(0.7, 0.5, 0.1, 64.0, green, new Vector(0.5, 1.0, 0.5));

        Sphere reflSphr2 = new Sphere(0.3, 0.1, -0.55, 0.25);
        reflSphr2.SetColor(0.0, 0.1, 0.1, 10.0, new Vector(0.75, 0.75, 0.75), white);
        reflSphr2.SetRefl(0.9);

        Triangle bluTri2 = new Triangle(new Vector(0.3, -0.3, -0.4), new Vector(0.0, 0.3, -0.1), new Vector(-0.3, -0.3, 0.2));
        bluTri2.SetColor(0.9, 0.9, 0.1, 32.0, blue, white);

        Triangle yelTri2 = new Triangle(new Vector(-0.2, 0.1, 0.1), new Vector(-0.2, -0.5, 0.2), new Vector(-0.2, 0.1, -0.3));
        yelTri2.SetColor(0.9, 0.5, 0.1, 32.0, yellow, white);

        scene5.AddProps(new List<Prop> { whiteSphere, redSphere, grnSphere, reflSphr2, bluTri2, yelTri2 });

        Scene scene6 = new Scene("scene_6.ppm");
        scene6.SetLight(new Vector(0.0, 0.5, 1.0), new Vector(0.1, 0.1, 0.1), white);
        scene6.SetBG(bgColor);

        // Triangle reflTri1 = new Triangle(new Vector(0.0, -0.5, 0.25), new Vector(1.6, 0.2, -1.5), new Vector(0.0, -0.25, -1.0));
        // reflTri1.SetColor(0.0, 0.1, 0.1, 10.0, new Vector(0.75, 0.75, 0.75), white);
        // reflTri1.SetRefl(0.9);

        // Triangle reflTri2 = new Triangle(new Vector(0.0, -0.5, 0.25), new Vector(-1.6, 0.2, -1.5), new Vector(0.0, -0.25, -1.0));
        // reflTri2.SetColor(0.0, 0.1, 0.1, 10.0, new Vector(0.75, 0.75, 0.75), white);
        // reflTri2.SetRefl(0.9);

        Triangle reflTri3 = new Triangle(new Vector(0.0, -0.5, 0.25), new Vector(-1.6, 0.2, -1.5), new Vector(1.6, 0.2, -1.5));
        reflTri3.SetColor(0.0, 0.1, 0.1, 10.0, new Vector(0.75, 0.75, 0.75), white);
        reflTri3.SetRefl(0.9);

        Sphere brnSph = new Sphere(0.2, 0.0, 0.8, -0.3);
        brnSph.SetColor(0.7, 0.5, 0.1, 64.0, barkBrown, white);

        Sphere grnSph2 = new Sphere(0.2, 0.0, -0.15, -0.3);
        grnSph2.SetColor(0.7, 0.5, 0.1, 2.0, green, white);

        Sphere bluSph2 = new Sphere(0.2, -0.3, 0.1, -0.5);
        bluSph2.SetColor(0.9, 1.0, 0.1, 4.0, blue, white);

        Sphere redSph2 = new Sphere(0.2, 0.3, 0.1, -0.5);
        redSph2.SetColor(0.8, 0.8, 0.1, 32.0, red, white);

        Sphere reflSphr3 = new Sphere(0.2, 0.0, 0.25, -0.3);
        reflSphr3.SetColor(0.0, 0.1, 0.1, 10.0, new Vector(0.75, 0.75, 0.75), white);
        reflSphr3.SetRefl(0.9);

        scene6.AddProps(new List<Prop> { reflTri3, brnSph, bluSph2, redSph2, reflSphr3, grnSph2 });
*/

// Project 5 Code

        // // Scene 1 Code
        // Scene scene1 = new Scene("scene_1.ppm");
        // scene1.SetLight(
        //     new Vector(0.0, 1.0, 0.0), 
        //     new Vector(0.0, 0.0, 0.0), 
        //     new Vector(1.0, 1.0, 1.0)
        // );
        // scene1.SetBG(bgColor);
        
        // Sphere purpleSphere = new Sphere(0.4);
        // purpleSphere.SetColor(
        //     0.7, 0.2, 0.1, 16.0,
        //     new Vector(1.0, 0.0, 1.0), 
        //     new Vector(1.0, 1.0, 1.0)
        // );
        
        // scene1.AddProp(purpleSphere);

        // // Scene 2 Code
        // Scene scene2 = new Scene("scene_2.ppm");
        // scene2.SetLight(
        //     new Vector(1.0, 1.0, 1.0), 
        //     new Vector(0.1, 0.1, 0.1),
        //     new Vector(1.0, 1.0, 1.0)
        // );
        // scene2.SetBG(bgColor);

        // Sphere whiteSphere = new Sphere(0.15, 0.45, 0.0, -0.15);
        // whiteSphere.SetColor(
        //     0.8, 0.1, 0.3, 4.0,
        //     new Vector(1.0, 1.0, 1.0), 
        //     new Vector(1.0, 1.0, 1.0)
        // );

        // Sphere redSphere = new Sphere(0.2, 0.0, 0.0, -0.1);
        // redSphere.SetColor(
        //     0.6, 0.3, 0.1, 32.0,
        //     new Vector(1.0, 0.0, 0.0),
        //     new Vector(1.0, 1.0, 1.0)
        // );

        // Sphere greenSphere = new Sphere(0.3, -0.6, 0.0, 0.0);
        // greenSphere.SetColor(
        //     0.7, 0.2, 0.1, 64.0,
        //     new Vector(0.0, 1.0, 0.0),
        //     new Vector(0.5, 1.0, 0.5)
        // );

        // Sphere blueSphere = new Sphere(10000.0, 0.0, -10000.5, 0.0);
        // blueSphere.SetColor(
        //     0.9, 0.0, 0.1, 16.0,
        //     new Vector(0.0, 0.0, 1.0),
        //     new Vector(1.0, 1.0, 1.0)
        // );

        // scene2.AddProps(new List<Prop> { blueSphere, whiteSphere, redSphere, greenSphere });

        // Scene scene3 = new Scene("scene_3.ppm");
        // scene3.SetLight(
        //     new Vector(10.0, 10.0, -5.0), 
        //     new Vector(0.1, 0.1, 0.1),
        //     new Vector(1.0, 1.0, 1.0)
        // );
        // scene3.SetBG(new Vector());

        // Sphere mars = new Sphere(0.35, -0.3, -0.3, 0.5);
        // mars.SetColor(
        //     0.7, 0.2, 0.5, 3.0,
        //     new Vector(0.9, 0.3, 0.1),
        //     new Vector(1.0, 1.0, 1.0)
        // );

        // Sphere earth = new Sphere(0.4, 0.3, -0.1, 0.0);
        // earth.SetColor(
        //     0.8, 0.5, 0.6, 64.0,
        //     new Vector(0.0, 0.0, 1.0),
        //     new Vector(1.0, 1.0, 1.0)
        // );

        // Sphere venus = new Sphere(0.3, -0.1, 0.55, -0.4);
        // venus.SetColor(
        //     0.5, 0.1, 0.8, 4.0,
        //     new Vector(0.9, 0.85, 0.75), 
        //     new Vector(1.0, 1.0, 1.0)
        // );

        // Sphere mercury = new Sphere(0.25, 0.65, 0.8, -0.55);
        // mercury.SetColor(
        //     0.8, 0.1, 0.9, 4.0,
        //     new Vector(0.6, 0.4, 0.2),
        //     new Vector(1.0, 1.0, 1.0)
        // );

        // scene3.AddProps(new List<Prop> { mars, earth, venus, mercury });