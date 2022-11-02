using MediatR;
using OwenEndpointsProj2.Interfaces;
using OwenEndpointsProj2.Models;
using OwenEndpointsProj2.Queries;

namespace OwenEndpointsProj2.Handlers
{
    public class GetArticleByIdHandler : IRequestHandler<GetArticleByIdQuery, SingleArticleResponse>
    {
        private readonly IArticlesRepository _articlesQueryRepository;
        
        public GetArticleByIdHandler(IArticlesRepository articlesQueryRepository)
        {
            _articlesQueryRepository = articlesQueryRepository;
        }

        public async Task<SingleArticleResponse> Handle(GetArticleByIdQuery request, CancellationToken cancellation)
        {
            SingleArticleResponse article = _articlesQueryRepository.GetArticleById(request.Id);

            return article;
        }
    }
}
