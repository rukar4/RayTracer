using Models.Props;

public static class SceneParser
{
    public static Scene Parse(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        string outputFile = "scene.ppm";

        // Camera Variables
        Vector camDir = new Vector();
        Vector camCenter = new Vector();
        Vector camUp = new Vector();
        double fov = 90.0;

        // Lighting
        Vector dirToLight = new Vector();
        Vector ambient = new Vector();
        Vector lightColor = new Vector();
        Vector bgColor = new Vector();

        List<Prop> props = new();

        Prop? currentProp = null;

        foreach (string rawLine in lines)
        {
            string line = rawLine.Trim();

            if (line == "" || line.StartsWith("#"))
            {
                continue;
            }

            string[] tokens = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string key = tokens[0];

            switch (key)
            {
                case "OutputFile":
                    outputFile = tokens[1];
                    break;

                case "CameraDirection":
                    camDir = ParseVector(tokens);
                    break;

                case "CameraCenter":
                    camCenter = ParseVector(tokens);
                    break;

                case "CameraUp":
                    camUp = ParseVector(tokens);
                    break;

                case "FieldOfView":
                    fov = double.Parse(tokens[1]);
                    break;

                case "DirectionToLight":
                    dirToLight = ParseVector(tokens);
                    break;

                case "LightColor":
                    lightColor = ParseVector(tokens);
                    break;

                case "AmbientLight":
                    ambient = ParseVector(tokens);
                    break;

                case "BackgroundColor":
                    bgColor = ParseVector(tokens);
                    break;

                case "Sphere":
                    if (currentProp != null)
                    {
                        props.Add(currentProp);
                    }
                    currentProp = new Sphere();
                    break;

                case "Center":
                    if (currentProp is Sphere centerSphere)
                    {
                        Vector center = ParseVector(tokens);
                        centerSphere.Center = center;
                    }
                    break;

                case "Radius":
                    if (currentProp is Sphere radiusSphere)
                    {
                        double r = double.Parse(tokens[1]);
                        radiusSphere.R = r;
                    }
                    break;

                case "Kd":
                    if (currentProp != null)
                        currentProp.Kd = double.Parse(tokens[1]);
                    break;

                case "Ks":
                    if (currentProp != null)
                        currentProp.Ks = double.Parse(tokens[1]);
                    break;

                case "Ka":
                    if (currentProp != null)
                        currentProp.Ka = double.Parse(tokens[1]);
                    break;

                case "Kgls":
                    if (currentProp != null)
                        currentProp.Kgls = double.Parse(tokens[1]);
                    break;

                case "Od":
                    if (currentProp != null)
                        currentProp.Od = ParseVector(tokens);
                    break;

                case "Os":
                    if (currentProp != null)
                        currentProp.Os = ParseVector(tokens);
                    break;

                case "Refl":
                    if (currentProp != null)
                        currentProp.Refl = double.Parse(tokens[1]);
                    break;
            }
        }

        Scene scene = new Scene(camDir, camCenter, camUp, fov, outputFile);
        scene.SetLight(dirToLight, ambient, lightColor);
        scene.SetBG(bgColor);
        scene.AddProps(props);

        return scene;
    }

    private static Vector ParseVector(string[] tokens)
    {
        return new Vector(
            double.Parse(tokens[1]),
            double.Parse(tokens[2]),
            double.Parse(tokens[3])
        );
    }
}