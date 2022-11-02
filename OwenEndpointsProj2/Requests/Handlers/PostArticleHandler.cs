using MediatR;
using NHibernate;
using Dapper;
using OwenEndpointsProj2.Models;
using OwenEndpointsProj2.Commands;
using System.Data;
using System.Data.SqlClient;
using OwenEndpointsProj2.Data;
using Microsoft.Identity.Client;
using Org.BouncyCastle.Bcpg;

namespace OwenEndpointsProj2.Handlers
{
    public class PostArticleHandler : IRequestHandler<PostArticleCommand, SingleArticleResponse>
    {
        public async Task<SingleArticleResponse> Handle(PostArticleCommand request, CancellationToken cancellationToken)
        {
            var sessionFactory = NHibernateHelper.CreateSessionFactory();
            var articleSubmission = new Article { Title = request.Title, Author = request.Author };
            SingleArticleResponse response = new SingleArticleResponse();

            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.SaveOrUpdate(articleSubmission);
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
        }
    }
}

