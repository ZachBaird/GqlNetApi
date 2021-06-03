using GqlNetApi.Data;
using GqlNetApi.Gql.Types;
using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GqlNetApi.Gql.Queries
{
    public class AuthorQuery : ObjectGraphType
    {
        public AuthorQuery(AppDbContext dbCtx)
        {
            Field<AuthorType>(
                "Author",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "id", Description = "The Id of the Author" }),
                resolve: ctx =>
                {
                    var id = ctx.GetArgument<int>("id");
                    var author = dbCtx
                        .Authors
                        .Include(a => a.Books)
                        .FirstOrDefault(a => a.Id == id);

                    return author;
                });

            Field<ListGraphType<AuthorType>>(
                "Authors",
                resolve: ctx =>
                {
                    var authors = dbCtx.Authors.Include(a => a.Books);
                    return authors;
                });
        }
    }
}
