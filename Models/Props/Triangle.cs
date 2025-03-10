using Models.Props;

public class Triangle : Prop {
    protected readonly Vector v0, v1, v2;

    public Triangle(Vector v0, Vector v1, Vector v2) {
        this.v0 = v0;
        this.v1 = v1;
        this.v2 = v2;
    }

    public override Vector GetNormal(Vector point) {
        return ((v1 - v0).Cross(v2 - v0)).UnitVector();
    }

    public override Vector? GetIntersection(Ray ray) {
        return new Vector(0.0, 0.0, 0.0);
    }
}