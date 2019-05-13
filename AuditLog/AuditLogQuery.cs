using AuditLog.Types;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AuditLog
{
    /// <summary>
    /// An implementation of ObjectGraphType which provides
    /// field definitions to retrieve data
    /// </summary>
    public class AuditLogQuery : ObjectGraphType<object>
    {
        public AuditLogQuery(AuditLogData data)
        {
            Name = "Query";

            Field<EntryInterface>("entry", resolve: context => data.GetEntryByIdAsync("client01", "3"));

            Field<ClientInterface>("client", resolve: context => data.GetClientByNameAsync("client01"));

            Field<AuditLogClientType>(
                "auditLogClient",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name", Description = "name of the client" }
                ),
                resolve : context => data.GetClientByNameAsync(context.GetArgument<string>("name"))
            );

            Field<AuditLogEntryType>(
                "auditLogEntry",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name", Description = "client id of the entry" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the entry" }
                ),
                resolve: context => {
                    var result = data.GetEntryByIdAsync(context.GetArgument<string>("name"),
                     context.GetArgument<string>("id")).Result;
                    return result.SingleOrDefault();
                }
            );

            Field<AuditLogEntryCollectionType>(
                "auditLogEntries",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name", Description = "name of the client" }
                ),
                resolve: context => new AuditLogEntryCollectionType { Entries = data.GetEntryByIdAsync(context.GetArgument<string>("name"), null).Result }
            );
        }
    }
}
