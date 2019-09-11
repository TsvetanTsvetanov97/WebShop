using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Shop.CloudinaryService
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;
        public CloudinaryService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }
        public async Task<string> UploadFile(IFormFile file)
        {
            var fileStream = file.OpenReadStream();
            ImageUploadParams uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
            {
                File = new FileDescription("file", fileStream)
            };

            ImageUploadResult uploadResult = await this.cloudinary.UploadAsync(uploadParams);
            return uploadResult.Uri.ToString();

        }
    }
}
