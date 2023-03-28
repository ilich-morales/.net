using System.ComponentModel.DataAnnotations;

namespace api.multitracks.com.Dto
{
    public class ArtistFullDto : ArtistDto
    {
        [Required]
        public int artistID { get; set; }
        [Required]
        public DateTime dateCreation { get; set; }
    }
}
