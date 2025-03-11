namespace Models.Props {

    public abstract class Prop {
        protected int MAX_DEPTH = 3;

        protected double Kd = 0.0, Ks = 0.0, Ka = 0.0, Kgls = 0.0, Refl = 0.0;
        protected Vector Od = new Vector(0, 0, 0);
        protected Vector Os = new Vector(1.0, 1.0, 1.0);
    
        public void SetColor(double Kd, double Ks, double Ka, double Kgls, Vector Od, Vector Os) {
            this.Kd = Kd;
            this.Ks = Ks;
            this.Ka = Ka;
            this.Od = Od;
            this.Os = Os;
            this.Kgls = Kgls;
        }

        public void SetRefl(double Refl) {
            this.Refl = Refl;
        }

        public double GetRefl() {
            return Refl;
        }

        public Vector GetSurfaceColor(Vector point, Vector origin, Scene scene, int depth = 0) {
            Vector L = scene.GetLightDir();
            
            Vector N = GetNormal(point);
            Vector R = scene.LightReflection(N);
            Vector V = (origin - point).UnitVector();

            Vector color = Ka * scene.GetAmbientColor() * Od + 
                    Kd * scene.GetLightColor() * Od * Math.Max(0, N.Dot(L)) + 
                    Ks * scene.GetLightColor() * Os * Math.Pow(Math.Max(0, R.Dot(V)), Kgls);

            // Detect if the point is in shadow
            Ray shadowRay = new Ray(point, L);
            foreach (Prop prop in scene.GetProps()) {
                if (prop != this && prop.GetIntersection(shadowRay) != null) {
                    return color * 0.5;
                }
            }

            if (Refl == 0 || depth == MAX_DEPTH) {
                return color;
            } else {
                Vector reflColor = scene.GetBG();
                Vector reflDir = -V.Reflect(N).UnitVector();
                Ray reflRay = new Ray(point + reflDir * 0.001, reflDir);

                Vector? closestInter = null;
                Prop? closestProp = null;

                double closest = double.MaxValue;
                foreach (Prop prop in scene.GetProps()) {
                    if (prop != this) {
                        Vector? intersection = prop.GetIntersection(reflRay);

                        if (intersection != null) {
                            double distance = (intersection - point).Magnitude();

                            if (distance < closest) {
                                closest = distance;
                                closestInter = intersection;
                                closestProp = prop;
                            }
                        }
                    }
                }
                
                if (closestInter != null && closestProp != null) {
                    reflColor = closestProp.GetSurfaceColor(closestInter, reflRay.origin, scene, depth + 1);
                }

                return color + reflColor * Refl;
            }
        }
    
        public abstract Vector GetNormal(Vector point);
        public abstract Vector? GetIntersection(Ray ray);
    }
}