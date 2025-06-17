using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class LocalImageRepository : IUploadImageReopsitory
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IWebHostEnvironment environment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public LocalImageRepository(NZWalksDbContext dbContext,
            IWebHostEnvironment environment,
            IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = dbContext;
            this.environment = environment;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<Image> UploadImage(Image image)
        {
            var localFilePath = Path.Combine(environment.ContentRootPath, "Images", image.FileName + image.FileExtenstion);
            // Upload Image To Local Path
            using var stream = new FileStream(localFilePath,FileMode.Create);
            await image.file.CopyToAsync(stream);

            // Save Actual File location not the local Path
            // e.g https://localhost:1234/images/image.jpeg
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}" +
                $"://{httpContextAccessor.HttpContext.Request.Host}" +
                $"{httpContextAccessor.HttpContext.Request.PathBase}" + 
                $"/Images/{image.FileName}{image.FileExtenstion}";
            image.FilePath = urlFilePath;
            await dbContext.images.AddAsync(image);
            await dbContext.SaveChangesAsync();
            return image;
        }
    }
}
