using AuditLog.Types;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditLog
{
    /// <summary>
    /// An implementation of the ObjectGraphType which
    /// provides field definitions to create data
    /// </summary>
    public class AuditLogMutation : ObjectGraphType
    {
        public AuditLogMutation(AuditLogData data)
        {
            Name = "Mutation";

            Field<AuditLogEntryType>(
                "createAuditLogEntry",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType>
                    { Name = "clientId" },
                    new QueryArgument<NonNullGraphType<AuditLogEntryInputType>>
                    { Name = "auditLogEntry" }),
                resolve: context => 
                {
                    var clientId = context.GetArgument<string>("clientId");
                    var entry = context.GetArgument<AuditLogEntry>("auditLogEntry");
                    return data.AddAuditLog(clientId, entry);
                });
        }
    }
}
