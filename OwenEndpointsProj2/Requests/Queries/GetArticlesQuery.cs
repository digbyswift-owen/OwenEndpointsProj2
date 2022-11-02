using MediatR;
using OwenEndpointsProj2.Models;

namespace OwenEndpointsProj2.Queries
{
    public class GetArticlesQuery : IRequest<ArticlesResponse>
    {
        public GetArticlesQuery()
        {

        }
    }
}
