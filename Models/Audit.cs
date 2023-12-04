using TestLatviaProject.Enums;

namespace TestLatviaProject.Models
{
    public class Audit
    {
        public int? Id { get; set; }
        public AuditType? AuditType { get; set; }
        public string? TableName { get; set; }
        public DateTime? DateTime { get; set; }
        public string? PrimaryKey { get; set; }
        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
        public string? AffectedColumns { get; set; }
        public string? UserName { get; set; }
    }
}
