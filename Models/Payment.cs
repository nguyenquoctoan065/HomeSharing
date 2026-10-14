using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeSharing.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        public int BookingId { get; set; }

        [ForeignKey("BookingId")]
        public virtual Booking Booking { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required, StringLength(50)]
        public string Method { get; set; } = "Mô phỏng Online";

        [Required, StringLength(50)]
        public string Status { get; set; } = "Chưa thanh toán";

        [StringLength(100)]
        public string? TransactionCode { get; set; }

        public DateTime? PaidAt { get; set; }
    }
}