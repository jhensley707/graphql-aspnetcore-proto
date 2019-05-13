using AuditLog.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuditLog
{
    /// <summary>
    /// Provides access methods to the underlying data repository
    /// </summary>
    public class AuditLogData
    {
        private IDataAccess _dataAccess;

        public AuditLogData(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess ?? throw new ArgumentNullException("dataAccess");
        }

        public Task<AuditLogClient> GetClientByNameAsync(string name)
        {
            return Task.FromResult(_dataAccess.GetAuditLogClientByName(name));
        }

        /// <summary>
        /// Adds a new audit log entry to the specified database.
        /// Creates new Id and Timestamp
        /// </summary>
        /// <param name="name">Database name</param>
        /// <param name="entry">Data document</param>
        /// <returns></returns>
        public AuditLogEntry AddAuditLog(string name, AuditLogEntry entry)
        {
            // Todo: pass in the database (client id). From authentication context or as data param?
            entry.TimestampUtc = DateTime.UtcNow.ToString("o");
            _dataAccess.AddAuditLogEntry(name, entry);
            return entry;
        }

        public Task<IEnumerable<AuditLogEntry>> GetEntryByIdAsync(string name, string id)
        {
            // Todo: pass in the database (client id). From authentication context or as data param?
            var filterCriteria = new FilterCriteria { Id = id };
            return Task.FromResult(_dataAccess.GetAuditLogEntries(name, filterCriteria));
        }

        public Task<IEnumerable<AuditLogEntry>> GetEntriesAsync(string name)
        {
            return Task.FromResult(_dataAccess.GetAuditLogEntries(name, null));
        }
    }
}
