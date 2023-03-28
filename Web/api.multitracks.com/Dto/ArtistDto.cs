using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api.multitracks.com.Dto
{
    public class ArtistDto
    {
        [Required, MaxLength(100)]
        public string title { get; set; }
        [Required, MaxLength(4000)]
        public string biography { get; set; }
        [Required, MaxLength(500)]
        public string imageURL { get; set; }
        [Required, MaxLength(500)]
        public string heroURL { get; set; }
    }
}
