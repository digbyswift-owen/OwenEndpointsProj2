using MediatR;
using Dapper;
using OwenEndpointsProj2.Models;
using OwenEndpointsProj2.Queries;
using System.Data;
using System.Data.SqlClient;

namespace OwenEndpointsProj2.Handlers
{
    public class GetArticleByIdHandler : IRequestHandler<GetArticleByIdQuery, SingleArticleResponse>
    {
        String connectionString = "server=xps13\\sqlexpress; database=OwenEndpoints; Integrated Security=true; Encrypt=False";

        public async Task<SingleArticleResponse> Handle(GetArticleByIdQuery request, CancellationToken cancellation)
        {
            Article article = new Article();

            string query = $"SELECT * FROM Articles WHERE id = @id";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                article = db.QueryFirstOrDefault<Article>(query, new { request.Id });
            }

            var articleRes = new SingleArticleResponse();
            if (article == null)
            {
                articleRes.Timestamp = DateTime.Now;
                articleRes.StatusCode = 200;
                articleRes.Response = article;
                articleRes.Message = "Id Not Found";

                return articleRes;
            }

            articleRes.Timestamp = DateTime.Now;
            articleRes.StatusCode = 200;
            articleRes.Response = article;
            articleRes.Message = "Article Retrieved";

            return articleRes;
        }
    }
}
