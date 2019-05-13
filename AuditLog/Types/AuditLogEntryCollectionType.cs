using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuditLog.Types
{
    public class AuditLogEntryCollectionType : ObjectGraphType
    {
        public IEnumerable<AuditLogEntry> Entries { get; set; } = Enumerable.Empty<AuditLogEntry>();
    }
}
