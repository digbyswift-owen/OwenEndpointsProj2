namespace OwenEndpointsProj2.Models
{
    public class ArticlesResponse
    {
        public DateTime Timestamp { get; set; }
        public int StatusCode { get; set; }
        public List<Article>? Response { get; set; }
    }
}
