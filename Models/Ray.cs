public class Ray {
    public readonly Vector origin;
    public readonly Vector dir;

    public Ray(Vector origin, Vector dir) {
        this.origin = origin;
        this.dir = dir;
    }

    public Vector At(double t) {
        return origin + t * dir;
    }
}