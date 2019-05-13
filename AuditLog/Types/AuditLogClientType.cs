using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditLog.Types
{
    public class AuditLogClientType : ObjectGraphType<AuditLogClient>
    {
        public AuditLogClientType(AuditLogData data)
        {
            Name = "AuditLogClient";

            Field(ale => ale.Name, nullable: true).Description("The name of audit log client");
            Field<ListGraphType<EntryInterface>>(
                "entries",
                resolve: context => data.GetEntriesAsync(context.Source.Name)
            );

            Interface<ClientInterface>();
        }
    }
}
