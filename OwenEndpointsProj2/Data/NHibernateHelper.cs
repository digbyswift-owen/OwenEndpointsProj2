using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using OwenEndpointsProj2.Handlers;

namespace OwenEndpointsProj2.Data
{
    public class NHibernateHelper
    {
        public static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(
                    MsSqlConfiguration.MsSql2008
                        .ConnectionString("server=xps13\\sqlexpress; database=OwenEndpoints; Integrated Security=true; Encrypt=False")
                    )
                .Mappings(m => 
                    m.FluentMappings.AddFromAssemblyOf<PostArticleHandler>())
                .BuildSessionFactory();
        }
    }
}