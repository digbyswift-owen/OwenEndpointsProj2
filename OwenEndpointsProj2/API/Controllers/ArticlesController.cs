using Dapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MySqlX.XDevAPI.Common;
using OwenEndpointsProj2.Commands;
using OwenEndpointsProj2.Interfaces;
using OwenEndpointsProj2.Models;
using OwenEndpointsProj2.Queries;
using OwenEndpointsProj2.Repositories;
using System.Data;

    
namespace OwenEndpointsProj2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ArticlesController(IMediator mediator)
        {
            _mediator = mediator;
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
            var article = await _mediator.Send(new PostArticleCommand(title, author));

            return Ok(article);
        }
    }
}
