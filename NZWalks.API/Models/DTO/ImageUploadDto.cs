using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks.API.Models.DTO
{
    public class ImageUploadDto
    {
        [Required]
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }
}