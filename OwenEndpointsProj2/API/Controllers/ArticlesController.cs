
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OwenEndpointsProj2.Commands;
using OwenEndpointsProj2.Queries;
using OwenEndpointsProj2.Repositories;

namespace OwenEndpointsProj2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly NHibernate.ISession _session;

        private readonly ILogger<ArticlesController> _logger;

        public ArticlesController(IMediator mediator, NHibernate.ISession session, ILogger<ArticlesController> logger)
        {
            _mediator = mediator;
            _session = session;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var articles = await _mediator.Send(new GetArticlesQuery());

            return Ok(articles);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var article = await _mediator.Send(new GetArticleByIdQuery(id));

            return Ok(article);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string title, string author)
        {
            _logger.LogInformation("In Articles Post Method");
            //var article = await _mediator.Send(new PostArticleCommand(title, author));
            Repository repository = new Repository(_session, _logger);
            var article = repository.PostArticle(author, title);

            return Ok(article);

        }
    }
}
