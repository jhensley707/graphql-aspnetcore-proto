using System;
using System.Collections.Generic;
using System.Text;

namespace AuditLog.Types
{
    public abstract class EntryBase
    {
        /// <summary>
        /// A unique event id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The event name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// A user id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The event occurrence timestamp in UTC
        /// </summary>
        public string TimestampUtc { get; set; }

        /// <summary>
        /// Source of the event
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Additional details of the event
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Outcome of the event
        /// </summary>
        public string Outcome { get; set; }

        // Todo: Are there any collections here? Perhaps user permissions/roles
    }
}
