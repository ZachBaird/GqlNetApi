using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GqlNetApi.Gql.Queries
{
    public class GraphQlQuery
    {
        public string OperationName { get; set; }
        public string NamedQuery { get; set; }
        public string Query { get; set; }

        [JsonConverter(typeof(GraphQL.SystemTextJson.ObjectDictionaryConverter))]
        public Dictionary<string, object> Variables { get; set; }
    }
}
