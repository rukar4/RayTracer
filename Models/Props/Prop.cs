namespace Models.Props {
    public abstract class Prop {
        protected double Kd = 0.0, Ks = 0.0, Ka = 0.0, Kgls = 0.0;
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
    
        public Vector GetSurfaceColor(Vector point, Vector camCenter, Scene scene) {
            Vector L = scene.GetLightDir();
            Vector N = GetNormal(point);
            Vector R = scene.LightReflection(N);
            Vector V = (camCenter - point).UnitVector();
            return  Ka * scene.GetAmbientColor() * Od + 
                    Kd * scene.GetLightColor() * Od * Math.Max(0, N.Dot(L)) + 
                    Ks * scene.GetLightColor() * Os * Math.Pow(Math.Max(0, R.Dot(V)), Kgls);
        }
    
        public abstract Vector GetNormal(Vector point);
        public abstract Vector? GetIntersection(Ray ray);
    }
}