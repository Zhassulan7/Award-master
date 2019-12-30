using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Awards.Models
{
    public class ImageHelper
    {
        public string WriteImage(IFormFile image, ApplicationContext context)
        {
            if (image != null)
            {
                string imagePath = "wwwroot/image/" + Guid.NewGuid() + image.FileName;
                context.ImagesAndPhotos.Add(new ImagesData() { AddedTime = DateTime.Now, ImgPaths = imagePath });
                context.SaveChanges();
                var fileStream = new FileStream(imagePath, FileMode.Create);
                image.CopyTo(fileStream);
                fileStream.Close();
                return imagePath.Substring(7);
            }
            return null;
        }
        public void DeletePhoto(string imagePath)
        {
            imagePath = "wwwroot" + imagePath;
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }                       
        }
        public void EditImage(IFormFile image, string imgPath)
        {
            imgPath = "wwwroot" + imgPath;
            if (image != null)
            {                
                if (File.Exists(imgPath))
                {
                    var fileStream = new FileStream(imgPath, FileMode.Truncate);
                    fileStream.Close();
                    var newFileStream = new FileStream(imgPath, FileMode.Open);
                    image.CopyTo(newFileStream);
                    newFileStream.Close();
                }              
            }
            else
            {
                var fileStream = new FileStream(imgPath, FileMode.Truncate);
                fileStream.Close();
            }        
        }
    }
}
