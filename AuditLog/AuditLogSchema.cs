using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditLog
{
    /// <summary>
    /// Maps the Query and Mutation for the Schema
    /// </summary>
    public class AuditLogSchema : Schema
    {
        public AuditLogSchema(IDependencyResolver resolver)
            : base(resolver)
        {
            Query = resolver.Resolve<AuditLogQuery>();
            Mutation = resolver.Resolve<AuditLogMutation>();
        }
    }
}
