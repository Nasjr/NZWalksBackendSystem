using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks.API.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }
        [Required]
        public string FileName { get; set; }
        public string FileDescription { get; set; }
        public long FileSizeInBytes { get; set; }
        [NotMapped]
        public IFormFile file { get; set; }
        public string FileExtenstion { get; set; }
        public string FilePath { get; set; }
    }
}
