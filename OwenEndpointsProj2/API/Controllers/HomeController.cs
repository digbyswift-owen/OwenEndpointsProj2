using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using OwenEndpointsProj2.Models;
using System.ComponentModel.DataAnnotations;
using Dapper;
using System.Data;
using Microsoft.Identity.Client;

namespace OwenEndpointsProj2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        public EndpointsResponse GetEndpoints()
        {
            var endpointsRes = new EndpointsResponse();

            endpointsRes.Timestamp = DateTime.Now;
            endpointsRes.StatusCode = 200;
            endpointsRes.Response = new Endpoints().SeeAllEndpoints;

            return endpointsRes;
        }
    }
}
