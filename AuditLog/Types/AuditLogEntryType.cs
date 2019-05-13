using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditLog.Types
{
    public class AuditLogEntryType : ObjectGraphType<AuditLogEntry>
    {
        public AuditLogEntryType(AuditLogData data)
        {
            Name = "AuditLogEntry";
            
            Field(ale => ale.Id).Description("The id of the audit log entry");
            Field(ale => ale.Name, nullable: true).Description("The event type name of audit log entry");
            Field(ale => ale.UserId, nullable: true).Description("The user that originated the event request");
            Field(ale => ale.IpAddress, nullable: true).Description("IP Address of the request origin");
            Field(ale => ale.Description, nullable: true).Description("Description details of the audit log entry");
            Field(ale => ale.Outcome, nullable: true).Description("Success or fail outcome of the entry");
            Field(ale => ale.TimestampUtc, nullable: true).Description("Universal date and time of the audit log event");
            //Field<Date>(ale => ale.TimestampUtc, nullable: true).Description("Universal date and time of the audit log event");

            Interface<EntryInterface>();
        }
    }
}
