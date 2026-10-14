using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeSharing.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [Required]
        public int BookingId { get; set; }

        [ForeignKey("BookingId")]
        public virtual Booking Booking { get; set; } = null!;

        [Required]
        public int RoomId { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; } = null!;

        [Required]
        public int GuestId { get; set; }

        [ForeignKey("GuestId")]
        public virtual User Guest { get; set; } = null!;

        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(1000)]
        public string? Comment { get; set; }

        [Required, StringLength(50)]
        public string Status { get; set; } = "Hiển thị";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}