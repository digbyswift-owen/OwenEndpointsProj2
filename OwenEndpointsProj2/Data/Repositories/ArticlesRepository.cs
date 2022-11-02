using Dapper;
using OwenEndpointsProj2.Interfaces;
using OwenEndpointsProj2.Models;
using System.Data;
using System.Data.SqlClient;

namespace OwenEndpointsProj2.Repositories
{
    public class ArticlesRepository : IArticlesRepository
    {
        String connectionString = "server=xps13\\sqlexpress; database=OwenEndpoints; Integrated Security=true; Encrypt=False";
        private readonly DateTime _dateCreated;

        public ArticlesRepository()
        {
            _dateCreated = DateTime.Now;
        }

        public ArticlesResponse GetArticles()
        {
            string query = "SELECT * FROM Articles";
            ArticlesResponse articlesRes = new ArticlesResponse();

            using (IDbConnection db = new SqlConnection(connectionString))
            {

                db.Open();

                var articles = db.Query<Article>(query).ToList();
                articlesRes.Response = articles;
            } ;

            articlesRes.Timestamp = DateTime.Now;
            articlesRes.StatusCode = 200;

            return articlesRes;
        }

        public SingleArticleResponse GetArticleById(int id)
        {
            Article article = new Article();

            string query = $"SELECT * FROM Articles WHERE id = @id";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                article = db.QueryFirstOrDefault<Article>(query, new {id});
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

        public void DeleteArticleById(int id)
        {

        }

        public SingleArticleResponse PostArticle(string title, string author)
        {
            ArticlesResponse articlesRes = new ArticlesResponse();

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Open();
                var parameters = new { Title = title, Author = author };
                var sql = "INSERT INTO Articles (title, author) VALUES (@Title, @Author);";
                var rows = db.Execute(sql, parameters);
                if (rows > 0)
                {
                    Article postedArticle = new Article();
                    SingleArticleResponse response = new SingleArticleResponse();

                    postedArticle.Title = title;
                    postedArticle.Author = author;

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
                }
            };

            throw new NotImplementedException();
        }
    }
}
