public class Scene {
    // Lighting and Color
    Vector dirToLight = new Vector();
    Vector ambient = new Vector();
    Vector lightColor = new Vector();
    Vector bgColor = new Vector();

    // Objects
    List<Sphere> spheres = new();
    public Scene() {
    }

    public void AddLight(Vector dirToLight, Vector ambient, Vector lightColor) {
        this.dirToLight = dirToLight;
        this.ambient = ambient;
        this.lightColor = lightColor;
    }

    public void SetBG(Vector bgColor) {
        this.bgColor = bgColor;
    }

    public Vector GetBG() {
        return bgColor;
    }

    public void AddSphere(Sphere sphere) {
        spheres.Add(sphere);
    }

    public void AddSpheres(IList<Sphere> newSpheres) {
        spheres.AddRange(newSpheres);
    }

    public List<Sphere> GetSpheres() {
        return spheres;
    }
}