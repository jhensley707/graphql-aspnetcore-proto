using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditLog.Types
{
    public class AuditLogEntryInputType : InputObjectGraphType<AuditLogEntry>
    {
        public AuditLogEntryInputType()
        {
            Name = "AuditLogEntryInput";

            Field(ale => ale.Id);
            Field(ale => ale.Name);
            Field(ale => ale.UserId);
            Field(ale => ale.TimestampUtc);
            Field(ale => ale.IpAddress);
            Field(ale => ale.Outcome);
            Field(ale => ale.Description);
        }
    }
}
