using System.Security.Claims;

namespace graphql_web
{
    public class GraphQLUserContext
    {
        public ClaimsPrincipal User { get; set; }
    }
}
