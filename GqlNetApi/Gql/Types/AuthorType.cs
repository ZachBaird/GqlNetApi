using GqlNetApi.Data.Models;
using GraphQL.Types;

namespace GqlNetApi.Gql.Types
{
    public class AuthorType : ObjectGraphType<Author>
    {
        public AuthorType()
        {
            Name = "Author";

            Field(x => x.Id, type: typeof(IdGraphType)).Description("The Author's Id");
            Field(x => x.Name).Description("The Author's Name");
            Field(x => x.Books, type: typeof(ListGraphType<BookType>)).Description("Books the Author has written");
        }
    }
}
