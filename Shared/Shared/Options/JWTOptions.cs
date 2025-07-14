namespace Shared.Options
{
    public class JWTOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Secret { get; set; }
        public double DurationInHours { get; set; }
     
    }
}
