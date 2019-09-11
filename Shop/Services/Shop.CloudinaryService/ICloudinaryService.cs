using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.CloudinaryService
{
    public interface ICloudinaryService
    {
        Task<string> UploadFile(IFormFile file);
    }
}
