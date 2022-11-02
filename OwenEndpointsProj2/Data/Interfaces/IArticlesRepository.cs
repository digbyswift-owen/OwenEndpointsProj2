using OwenEndpointsProj2.Models;

namespace OwenEndpointsProj2.Interfaces
{
    public interface IArticlesRepository
    {
        ArticlesResponse GetArticles();
        SingleArticleResponse GetArticleById(int articleId);

        SingleArticleResponse PostArticle(string title, string author);
        void DeleteArticleById(int articleId);
    }
}
