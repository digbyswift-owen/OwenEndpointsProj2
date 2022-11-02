using MediatR;
using OwenEndpointsProj2.Interfaces;
using OwenEndpointsProj2.Models;
using OwenEndpointsProj2.Commands;

namespace OwenEndpointsProj2.Handlers
{
    public class PostArticleHandler : IRequestHandler<PostArticleCommand, SingleArticleResponse>
    {
        private readonly IArticlesRepository _articlesRepository;

        public PostArticleHandler(IArticlesRepository articlesRepository)
        {
            _articlesRepository = articlesRepository;
        }

        public async Task<SingleArticleResponse> Handle(PostArticleCommand request, CancellationToken cancellationToken)
        {
            SingleArticleResponse article = _articlesRepository.PostArticle(request.Title, request.Author);

            return article;
        }
    }

}
