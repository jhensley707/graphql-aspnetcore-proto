using System;
using System.Collections.Generic;
using System.Text;

namespace AuditLog.Types
{
    public class AuditLogClient
    {
        public string Name { get; set; }

        public List<AuditLogEntry> Entries { get; set; }
    }
}
