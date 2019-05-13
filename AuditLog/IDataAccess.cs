using AuditLog.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditLog
{
    public interface IDataAccess
    {
        AuditLogClient GetAuditLogClientByName(string name);

        void AddAuditLogEntry(string database, AuditLogEntry entry);

        IEnumerable<AuditLogEntry> GetAuditLogEntries(string database, FilterCriteria filterCriteria);

    }
}
