using MediatR;
using OwenEndpointsProj2.Models;

namespace OwenEndpointsProj2.Commands
{
    public class PostArticleCommand : IRequest<SingleArticleResponse>
    {
        public PostArticleCommand(string? title, string? author)
        {
            Title = title;
            Author = author;
        }
        public string? Title { get; set; }
        public string? Author { get; set; }

    }
}
