using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Model.DTO
{
    public class UpdateRegionRequestDto
    {

        [Required]
        [MinLength(3, ErrorMessage ="Code Has To be Minimum of 3 Characters")]
        [MaxLength(3,ErrorMessage ="Code Maximum To Be of 3 Characters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage ="Name Maximum To be of 100 Characters")]
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
    }
}
