
namespace Chat.Core.Helpers
{
    public class JwtOptions
    {
        public string ValidIssuer { get; set; }
        public string Secret { get; set; }
        public string ValidAudience { get; set; }
        public int LifeTime { get; set; }
    }
}
