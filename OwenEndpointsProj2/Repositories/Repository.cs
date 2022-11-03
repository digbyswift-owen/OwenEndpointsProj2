using MediatR;
using OwenEndpointsProj2.Models;
using OwenEndpointsProj2.Commands;
using OwenEndpointsProj2.Data;
using System.Reflection;
using NHibernate;
using ISession = NHibernate.ISession;
using OwenEndpointsProj2.Controllers;

namespace OwenEndpointsProj2.Repositories
{
    public class Repository
    {
        public ISession _session;
        private readonly ILogger<ArticlesController> _logger;

        public Repository(NHibernate.ISession session, ILogger<ArticlesController> logger)
        {
            _session = session;
            _logger = logger;
        }

        public SingleArticleResponse PostArticle(string author, string title)
        {
            Article articleSubmission = new Article { Title = title, Author = author };
            SingleArticleResponse response = new SingleArticleResponse();

            using (_session)
            {
                using (var transaction = _session.BeginTransaction())
                {
                    _logger.LogInformation("In PostArticle transaction");
                    try
                    {
                        //session.SaveOrUpdate(articleSubmission);

                        _session.Save(articleSubmission);

                        articleSubmission.Title = articleSubmission.Title;
                        //session.Update(articleSubmission);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                    finally
                    {
                        if (transaction.WasCommitted)
                        {
                            _logger.LogInformation("Transaction was commited");
                            response.Timestamp = DateTime.Now;
                            response.Message = "Post Committed";
                            response.StatusCode = 200;
                            response.Response = articleSubmission;
                        }
                        else if (transaction.WasRolledBack)
                        {
                            _logger.LogInformation("Transaction was rolled back");
                            response.Timestamp = DateTime.Now;
                            response.Message = "Post Was Not Commited";
                            response.StatusCode = 400;
                            response.Response = articleSubmission;
                        }
                    }

                    return response;
                }
            }

            //using (session)
            //{
            //    var s = session.Get<Article>(articleSubmission.Id);
            //    response.Response = s;
            //    return response;
            //}
        }
    }      
}
