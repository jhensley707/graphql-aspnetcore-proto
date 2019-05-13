using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditLog.Types
{
    public class EntryInterface : InterfaceGraphType<EntryBase>
    {
        public EntryInterface()
        {
            Name = "Entry";

            Field(e => e.Id).Description("The id of the entry");
            Field(e => e.Name).Description("The event type name of the entry");
            Field(e => e.UserId, nullable: true).Description("The user that originated the event request");
            Field(ale => ale.IpAddress, nullable: true).Description("IP Address of the request origin");
            Field(ale => ale.Description, nullable: true).Description("Description details of the audit log entry");
            Field(ale => ale.Outcome, nullable: true).Description("Success or fail outcome of the entry");
            Field(ale => ale.TimestampUtc, nullable: true).Description("Universal date and time of the audit log event");
        }
    }
}
