using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeSharing.Models
{
    public class RoomImage
    {
        [Key]
        public int ImageId { get; set; }

        [Required]
        public int RoomId { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; } = null!;

        [Required, StringLength(500)]
        public string Url { get; set; } = string.Empty;

        public bool IsCover { get; set; } = false;
        public int SortOrder { get; set; } = 0;
    }
}