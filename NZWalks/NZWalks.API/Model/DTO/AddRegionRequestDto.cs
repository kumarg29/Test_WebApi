using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Model.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage ="Code Has to be minimum of 3 Characters")]
        [MaxLength(3, ErrorMessage ="Code has to be maximum of 3 Characters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage ="Name has to be max of 100 characters")]
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
    }
}

