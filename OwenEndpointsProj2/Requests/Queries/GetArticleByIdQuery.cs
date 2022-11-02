using MediatR;
using OwenEndpointsProj2.Models;

namespace OwenEndpointsProj2.Queries
{
    public class GetArticleByIdQuery : IRequest<SingleArticleResponse>
    {
        public GetArticleByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
