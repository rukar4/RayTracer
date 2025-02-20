public class Ray {
    public readonly Vector origin;
    public readonly Vector dir;

    public Ray(Vector origin, Vector dir) {
        this.origin = origin;
        this.dir = dir;
        this.dir.Normalize();
    }

    public Vector At(double t) {
        return origin + t * dir;
    }

    public Vector? SphereIntersection(Sphere sphere) {
        Vector OC = sphere.center - origin;
        double t_ca = dir.Dot(OC);

        // Ray is pointing away from sphere
        if (t_ca < 0) {
            return null;
        }

        // Use squared value to compute t_hc squared using Pythagorean's Theorem.
        double magOC2 = OC.LengthSqrd();
        double t_hc2 = sphere.r * sphere.r - magOC2 + t_ca * t_ca;

        // Ray misses sphere since t_hc = 0 implies the ray is tangent.
        if (t_hc2 < 0) {
            return null;
        }

        // Check if the point is inside the sphere and calculate t for finding the intersection. 
        // If the length of OC < r, then it must be in the sphere.
        double t;
        double t_hc = Math.Sqrt(t_hc2);
        if (Math.Sqrt(magOC2) < sphere.r) {
            t = t_ca + t_hc;
        } else {
            t = t_ca - t_hc;
        }

        return At(t);
    }
}