using GqlNetApi.Data.Models;
using GraphQL.Types;

namespace GqlNetApi.Gql.Types
{
    public class BookType : ObjectGraphType<Book>
    {
        public BookType()
        {
            Name = "Book";

            Field(x => x.Id, type: typeof(IdGraphType)).Description("The Id of the Book");
            Field(x => x.Name).Description("The Name of the Book");
            Field(x => x.Genre).Description("The Genre of the Book");
            Field(x => x.Published).Description("If the book is published or not");
        }
    }
}
