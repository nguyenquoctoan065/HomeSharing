using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeSharing.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        public int RoomId { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; } = null!;

        [Required]
        public int GuestId { get; set; }

        [ForeignKey("GuestId")]
        public virtual User Guest { get; set; } = null!;

        [Required]
        public DateTime CheckIn { get; set; }

        [Required]
        public DateTime CheckOut { get; set; }

        [Range(1, 20)]
        public int Adults { get; set; } = 1;

        [Range(0, 20)]
        public int Children { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        [Required, StringLength(50)]
        public string Status { get; set; } = "Chờ thanh toán";

        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CancelledAt { get; set; }

        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public virtual Review? Review { get; set; }
    }
}