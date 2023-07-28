using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class PhotoService
    {
        /*   private readonly Cloudinary cloudinary;
           public PhotoService(IConfiguration config)
           {
               Account account = new Account(
                   config.GetSection("CloudinarySetting:CloudName").Value,
                   config.GetSection("CloudinarySetting:ApiKey").Value,
                   config.GetSection("CloudinarySetting:ApiSecret").Value);
               cloudinary = new Cloudinary(account);
           }
           public async Task<DeleteResult> DeletePhotoAsync(string publicId){
                var deleteParams = new DeletionParams(publicId);
                var result = await cloudinary.DestroyAsync(deleteParams);
                return result;
           }
           public async Task<ImageUploadResult> UploadPhotoAsync(IFormFile photo)
           {
               var uploadResult = new ImageUploadResult();
               if(photo.Length > 0)
               {
                   using var stream = photo.OpenReadStream();
                   var uploadParams = new ImageUploadParams
                   {
                       File = new FileDescription(photo.FileName, stream),
                       Transformation = new Transformation().Height(500).Width(800)
                   };
                   uploadResult = await cloudinary.UploadAsync(uploadParams);
               }
               return uploadResult;
           }*/
    }
}
