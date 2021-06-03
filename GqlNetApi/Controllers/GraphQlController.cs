using GqlNetApi.Data;
using GqlNetApi.Gql.Queries;
using GraphQL;
using GraphQL.SystemTextJson;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GqlNetApi.Controllers
{
    [ApiController]
    public sealed  class GraphQlController : Controller
    {
        private readonly AppDbContext _dbCtx;

        public GraphQlController(AppDbContext dbCtx)
        {
            _dbCtx = dbCtx;
        }

        [HttpPost("/graphql")]
        public async Task<IActionResult> Post([FromBody] GraphQlQuery query)
        {
            //var variables = query.Variables;
            //var values = StringExtensions.
            //Inputs = new Inputs(values);
            Inputs inputs = query.Variables.ToInputs();

            var schema = new Schema
            {
                Query = new AuthorQuery(_dbCtx)
            };

            var result = await new DocumentExecuter().ExecuteAsync(_ =>
            {
                _.Schema = schema;
                _.Query = query.Query;
                _.OperationName = query.OperationName;
                _.Inputs = inputs;
            });

            var documentWriter = new DocumentWriter();
            var serializedResult = await documentWriter.WriteToStringAsync(result);

            if (result.Errors?.Count > 0)
                return BadRequest();
            else
                return Ok(serializedResult);
        }
    }
}
