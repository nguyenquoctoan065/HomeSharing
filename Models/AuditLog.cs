using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeSharing.Models
{
    public class AuditLog
    {
        [Key]
        public int AuditId { get; set; }

        [Required]
        public int ActorId { get; set; }

        [ForeignKey("ActorId")]
        public virtual User Actor { get; set; } = null!;

        [Required, StringLength(50)]
        public string Action { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string TargetType { get; set; } = string.Empty;

        [Required]
        public int TargetId { get; set; }

        [StringLength(255)]
        public string? Reason { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}