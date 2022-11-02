using MediatR;
using OwenEndpointsProj2.Interfaces;
using OwenEndpointsProj2.Models;
using OwenEndpointsProj2.Queries;

namespace OwenEndpointsProj2.Handlers
{
    public class GetArticlesHandler : IRequestHandler<GetArticlesQuery, ArticlesResponse>
    {
        private readonly IArticlesRepository _articlesQueryRepository;

        public GetArticlesHandler(IArticlesRepository articlesQueryRepository)
        {
            _articlesQueryRepository = articlesQueryRepository;
        }

        public async Task<ArticlesResponse> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {
            ArticlesResponse articles = _articlesQueryRepository.GetArticles();

            return articles;
        }
    }
}
