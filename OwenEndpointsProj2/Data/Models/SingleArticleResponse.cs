namespace OwenEndpointsProj2.Models
{
    public class SingleArticleResponse
    {
        public DateTime Timestamp { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public Article? Response { get; set; }
    }
}
