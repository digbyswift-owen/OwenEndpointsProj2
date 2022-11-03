using MediatR;
using OwenEndpointsProj2.Models;
using OwenEndpointsProj2.Commands;
using OwenEndpointsProj2.Data;
using System.Reflection;
using NHibernate;
using ISession = NHibernate.ISession;

namespace OwenEndpointsProj2.Repositories
{
    public class Repository
    {
        public ISession session;

        public Repository(NHibernate.ISession session)
        {
            this.session = session;
        }

        public SingleArticleResponse PostArticle(string author, string title)
        {
            Article articleSubmission = new Article { Title = title, Author = author };
            SingleArticleResponse response = new SingleArticleResponse();

            using (session)
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        //session.SaveOrUpdate(articleSubmission);

                        session.Save(articleSubmission);

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
                            response.Timestamp = DateTime.Now;
                            response.Message = "Post Committed";
                            response.StatusCode = 200;
                            response.Response = articleSubmission;
                        }
                        else if (transaction.WasRolledBack)
                        {
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
