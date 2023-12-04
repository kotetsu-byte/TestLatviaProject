using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using TestLatviaProject.Enums;

namespace TestLatviaProject.Models
{
    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            this.entry = entry;
        }

        public EntityEntry? entry { get; set; }
        public string? userName { get; set; }
        public string? tableName { get; set; }
        public Dictionary<string, object>? keyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object>? oldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object>? newValues { get; } = new Dictionary<string, object>();
        public AuditType? AuditType { get; set; }
        public List<string>? changedColumns { get; } = new List<string>();
        public Audit ToAudit()
        {
            var audit = new Audit()
            {
                UserName = userName,
                AuditType = AuditType,
                TableName = tableName,
                DateTime = DateTime.UtcNow,
                PrimaryKey = JsonConvert.SerializeObject(keyValues),
                OldValues = oldValues.Count == 0 ? null : JsonConvert.SerializeObject(oldValues),
                NewValues = newValues.Count == 0 ? null : JsonConvert.SerializeObject(newValues),
                AffectedColumns = changedColumns.Count == 0 ? null : JsonConvert.SerializeObject(changedColumns)
            };
            return audit;
        }
    }
}
