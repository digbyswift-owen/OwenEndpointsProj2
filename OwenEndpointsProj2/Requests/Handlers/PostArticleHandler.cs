using MediatR;
using Dapper;
using OwenEndpointsProj2.Models;
using OwenEndpointsProj2.Commands;
using System.Data;
using System.Data.SqlClient;

namespace OwenEndpointsProj2.Handlers
{
    public class PostArticleHandler : IRequestHandler<PostArticleCommand, SingleArticleResponse>
    {
        String connectionString = "server=xps13\\sqlexpress; database=OwenEndpoints; Integrated Security=true; Encrypt=False";

        public async Task<SingleArticleResponse> Handle(PostArticleCommand request, CancellationToken cancellationToken)
        {
            ArticlesResponse articlesRes = new ArticlesResponse();

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                var parameters = new { Title = request.Title, Author = request.Author };
                var transaction = "SET XACT_ABORT ON; BEGIN TRAN INSERT INTO Articles (title, author) VALUES (@Title, @Author); COMMIT TRAN";
                var rows = db.Execute(transaction, parameters);
                if (rows > 0)
                {
                    Article postedArticle = new Article();
                    SingleArticleResponse response = new SingleArticleResponse();

                    postedArticle.Title = request.Title;
                    postedArticle.Author = request.Author;

                    response.StatusCode = 200;
                    response.Timestamp = DateTime.Now;
                    response.Response = postedArticle;

                    return response;
                }
                else
                {
                    SingleArticleResponse response = new SingleArticleResponse();
                    response.StatusCode = 500;
                    response.Timestamp = DateTime.Now;
                    response.Response = null;

                    return response;
                }
            };
        }
    }

}
