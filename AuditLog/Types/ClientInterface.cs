using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditLog.Types
{
    public class ClientInterface : InterfaceGraphType <AuditLogClient>
    {
        public ClientInterface()
        {
            Name = "Client";

            Field(e => e.Name).Description("The name of the client");

            Field<ListGraphType<EntryInterface>>("entries");
        }

    }
}
