using Models.Props;

public class Scene {
    // Camera Variables
    public readonly Vector camDir;
    public readonly Vector camCenter;
    public readonly Vector camUp;
    public readonly double focalLength;
    public readonly double fov;

    // Lighting and Color
    Vector dirToLight = new Vector();
    Vector ambient = new Vector();
    Vector lightColor = new Vector();
    Vector bgColor = new Vector();

    // Objects
    List<Prop> props = new();

    string fileName;

    public Scene(Vector camDir, Vector camCenter, Vector camUp, double fov, string outputFile = "scene.ppm")
    {
        fileName = outputFile;
        this.fov = fov;

        // Define camera
        this.camCenter = camCenter;
        this.camDir = camDir;
        this.camUp = camUp;
        focalLength = (camCenter - camDir).Magnitude();
    }

    public string GetFileName() {
        return fileName;
    }

    public void SetLight(Vector dirToLight, Vector ambient, Vector lightColor)
    {
        this.dirToLight = dirToLight;
        this.dirToLight.Normalize();
        this.ambient = ambient;
        this.lightColor = lightColor;
    }

    public Vector GetLightDir() {
        return dirToLight;
    }

    public void SetLightDir(Vector dirToLight) {
        this.dirToLight = dirToLight;
        this.dirToLight.Normalize();

    }

    public Vector GetAmbientColor() {
        return ambient;
    }

    public void SetAmbientColor(Vector ambient) {
        this.ambient = ambient;
    }

    public Vector GetLightColor() {
        return lightColor;
    }

    public void SetLightColor(Vector lightColor) {
        this.lightColor = lightColor;
    }

    public Vector GetBG() {
        return bgColor;
    }

    public void SetBG(Vector bgColor) {
        this.bgColor = bgColor;
    }

    public Vector LightReflection(Vector normal) {
        double nl = normal.Dot(dirToLight);
        return 2 * normal * nl - dirToLight;
    }

    public void AddProp(Prop prop) {
        props.Add(prop);
    }

    public void AddProps(IList<Prop> props) {
        this.props.AddRange(props);
    }

    public List<Prop> GetProps() {
        return props;
    }
}