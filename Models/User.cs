using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeSharing.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress, StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required, RegularExpression(@"^[0-9]{10,11}$"), StringLength(15)]
        public string Phone { get; set; } = string.Empty;

        [StringLength(500)]
        public string? AvatarUrl { get; set; }

        [Required]
        public UserRole Role { get; set; } = UserRole.Guest;

        public bool IsLocked { get; set; } = false;

        [StringLength(255)]
        public string? LockReason { get; set; }

        public DateTime? LockedAt { get; set; }

        public int? LockedBy { get; set; }

        [ForeignKey("LockedBy")]
        public virtual User? LockedByAdmin { get; set; }

        public int FailedLoginCount { get; set; } = 0;
        public DateTime? LockoutEnd { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;

        // Navigation Properties
        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public virtual ICollection<PasswordResetToken> PasswordResetTokens { get; set; } = new List<PasswordResetToken>();
        public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
    }
}