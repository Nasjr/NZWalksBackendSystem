using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IUploadImageReopsitory imageReopsitory;

        public ImagesController(IUploadImageReopsitory imageReopsitory)
        {
            this.imageReopsitory = imageReopsitory;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadDto image)
        {
           
            ValidateFileUpload(image);
            if (ModelState.IsValid) {
                // Convert DTO to DomainModel

                var imageDomainModel = new Image
                {
                    file = image.File,
                    FileName = image.FileName,
                    FileDescription = image.FileDescription,
                    FileSizeInBytes = image.File.Length,
                    FileExtenstion = Path.GetExtension(image.File.FileName),


                };
            
                
                // Use Repo to upload image

               var uploadedImage = await imageReopsitory.UploadImage(imageDomainModel);

                // Convert back to Dto


                return Ok(imageDomainModel);

            }
            return BadRequest(ModelState);
        }


        private void ValidateFileUpload(ImageUploadDto image)
        {
            var ext = Path.GetExtension(image.File.FileName)?.Trim().ToLowerInvariant();

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(ext))
            {

                ModelState.AddModelError("file", "Unsupported file extension.");
            }



            // more than 10 mb
            if (image.File.Length > 10485670)
            {
                ModelState.AddModelError("file", "File size is more than 10mb please upload smaller file");
            }
        }
    }
}
