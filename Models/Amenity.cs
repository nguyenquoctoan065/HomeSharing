using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeSharing.Models
{
    public class Amenity
    {
        [Key]
        public int AmenityId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<RoomAmenity> RoomAmenities { get; set; } = new List<RoomAmenity>();
    }

    public class RoomAmenity
    {
        public int RoomId { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; } = null!;

        public int AmenityId { get; set; }

        [ForeignKey("AmenityId")]
        public virtual Amenity Amenity { get; set; } = null!;
    }
}