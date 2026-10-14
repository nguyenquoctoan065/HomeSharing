using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeSharing.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        [Required]
        public int HostId { get; set; }

        [ForeignKey("HostId")]
        public virtual User Host { get; set; } = null!;

        [Required, StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Province { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string District { get; set; } = string.Empty;

        [Required, StringLength(255)]
        public string Address { get; set; } = string.Empty;

        [Range(50000, 50000000)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerNight { get; set; }

        [Range(1, 20)]
        public int MaxGuests { get; set; }

        [Required, StringLength(50)]
        public string Status { get; set; } = "Đang hoạt động";

        public double AvgRating { get; set; } = 0;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public virtual ICollection<RoomImage> RoomImages { get; set; } = new List<RoomImage>();
        public virtual ICollection<RoomAmenity> RoomAmenities { get; set; } = new List<RoomAmenity>();
        public virtual ICollection<BlockedDate> BlockedDates { get; set; } = new List<BlockedDate>();
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}