using FluentNHibernate.Mapping;
using OwenEndpointsProj2.Models;

namespace OwenEndpointsProj2.Mappings
{
    public class ArticleMap : ClassMap<Article>
    {
        public ArticleMap()
        {
            Id(x => x.Id);
            Map(x => x.Title);
            Map(x => x.Author);
            Table("Articles");
        }
    }
}
