
using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace RestaurantAPI.Services
{
    public class ImageService
    {
        private string imageDirectory = "Images";

        public string SaveImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
                return null;

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
            var imagePath = Path.Combine(imageDirectory, uniqueFileName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            return imagePath;
        }
    }
}
