using MediatR;
using OwenEndpointsProj2.Models;
using OwenEndpointsProj2.Queries;
using System.Data.SqlClient;
using System.Data;
using Dapper;


namespace OwenEndpointsProj2.Handlers
{
    public class GetArticlesHandler : IRequestHandler<GetArticlesQuery, ArticlesResponse>
    {

        String connectionString = "server=xps13\\sqlexpress; database=OwenEndpoints; Integrated Security=true; Encrypt=False";

        public async Task<ArticlesResponse> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {
            string query = "SELECT * FROM Articles";
            ArticlesResponse articlesRes = new ArticlesResponse();

            using (IDbConnection db = new SqlConnection(connectionString))
            {

                db.Open();

                var articles = db.Query<Article>(query).ToList();
                articlesRes.Response = articles;
            };

            articlesRes.Timestamp = DateTime.Now;
            articlesRes.StatusCode = 200;


            return articlesRes;
        }
    }
}
