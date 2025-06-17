using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public interface IUploadImageReopsitory
    {
        public Task<Image> UploadImage(Image image);
    }
}
