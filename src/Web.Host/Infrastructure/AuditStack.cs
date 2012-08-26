using System;
using System.Collections.Generic;
using Contracts.Utility;

namespace Web.Host.Infrastructure
{
    /// <summary>
    /// Used to demo the collection of audit records being recorded.
    /// </summary>
    public static class AuditStack
    {
        /// <summary>
        /// The collection of audits.
        /// </summary>
        private static readonly Stack<AuditEntry> Entries = new Stack<AuditEntry>();

        /// <summary>
        /// Gets all recorded audits.
        /// </summary>
        /// <returns>All audit entries.</returns>
        public static IEnumerable<AuditEntry> GetAll()
        {
            return Entries;
        }

        /// <summary>
        /// Pushes the given object onto the stack.
        /// </summary>
        /// <param name="objectToAudit">The object being audited.</param>
        public static void Push(object objectToAudit)
        {
            Entries.Push(new AuditEntry(objectToAudit));
        }

        /// <summary>
        /// Clears the collection of audits from memory.
        /// </summary>
        public static void Clear()
        {
            Entries.Clear();
        }
    }

    /// <summary>
    /// Represents an audit entry.
    /// </summary>
    public class AuditEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuditEntry" /> class.
        /// </summary>
        public AuditEntry(object objectToAudit)
        {
            Timestamp = DateTime.Now.ToLongTimeString();
            Type = objectToAudit.GetType().Name;
            Message = DataUtility.SerializeAsString(objectToAudit);
        }

        /// <summary>
        /// The time of the audit.
        /// </summary>
        public string Timestamp { get; private set; }

        /// <summary>
        /// The type of audit.
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// The message.
        /// </summary>
        public string Message { get; private set; }
    }
}