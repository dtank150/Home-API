using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class PropertyController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        /*private readonly IPhotoService photoService;*/

        public PropertyController(IUnitOfWork uow, IMapper mapper /*IPhotoService photoService*/)
        {
            this.uow = uow;
            this.mapper = mapper;
            /*this.photoService = photoService;*/
        } 
        [HttpGet("List/{sellRent}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyList(int sellRent)
        {
            var properties =  await uow.PropertyRepository.GetPropertiesAsync(sellRent);
            var propertyListDTO = mapper.Map<IEnumerable<PropertyListDto>>(properties);
            return Ok(propertyListDTO);
        }
        [HttpGet("Detail/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPropertyDetail(int id)
        {
            var property = await uow.PropertyRepository.GetPropertyDetailAsync(id);
            var propertyDTO = mapper.Map<PropertyDetailDto>(property);
            return Ok(propertyDTO);
        }
        [HttpPost("Add")]
        [Authorize]
        public async Task<IActionResult> AddProperty(PropertyDto propertyDto)
        {
            var property = mapper.Map<Property>(propertyDto);
            var userId = GetUserId();
            property.PostedBy = userId;
            property.LastUpdatedBy = userId;
            uow.PropertyRepository.AddProperty(property);
            await uow.SaveAsync();
            return StatusCode(201);
        }
        /*[HttpPost("Add/Image/{propId}")]
        [Authorize]
        public async Task<IActionResult> AddProperty(IFormFile file, int propId)
        {
            var result = await photoService.UploadPhotoAsync(file);
            if (result.Error != null)
                return BadRequest(result.Error.Message);
            *//*var property = await uow.PropertyRepository.GetPropertyDetailAsync(propId);
            var photo = new Photo
            {
                ImageUrl = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };
            if(property.Photos.Count == 0)
            {
                photo.IsPrimary = true;
            }*//*\
                * property.Photos.Add(photo);
                * await uow.SaveAsync();
            return Ok(201);
        }*/
        /* [HttpPost("Add/Photo/{propId}/{photoPublicId}")]
         [Authorize]
         public async Task<IActionResult> AddPropertyPhoto(int propId, string photoPublicId)
         {
             var userId = GetUserId();
             var property = await uow.PropertyRepository.GetPropertyByIdAsync(propId);
             if (property == null)
                 return BadRequest("No such property or photo exists");

             if (property.PostedBy != userId)
                 return BadRequest("You are not authorize to change the photo");

             var photo = property.Photos.FirstOrDefault(p => p.PublicId == photoPublicId);

             if (photo == null)
                 return BadRequest("No Such property or photo exists");

             if (photo.IsPrimary)
                 return BadRequest("This is already Primary Photo");

             var currentPrimary = property.Photos.FirstOrDefault(p => p.IsPrimary);
             if (currentPrimary != null) currentPrimary.IsPrimary = false;
             photo.IsPrimary = true;

             if (await uow.SaveAsync()) return NoContent();

             return BadRequest("Some error has occured, failed to set primary photo");
         }*/
       /* [HttpDelete("Delete/Photo/{propId}/{photoPublicId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int propId, string photoPublicId)
        {
            var userId = GetUserId();
            var property = await uow.PropertyRepository.GetPropertyByIdAsync(propId);
            if (property == null)
                return BadRequest("No such property or photo exists");

            if (property.PostedBy != userId)
                return BadRequest("You are not authorize to delete the photo");

            var photo = property.Photos.FirstOrDefault(p => p.PublicId == photoPublicId);

            if (photo == null)
                return BadRequest("No Such property or photo exists");

            if (photo.IsPrimary)
                return BadRequest("You can not delete Primary Photo");

            var result = await photoService.DeletePhotoAsync(photo.PublicId);
            if(result.Error !=null)
               return BadRequest(result.Error.Message);

            property.Photos.Remove(photo);

            if (await uow.SaveAsync()) return Ok();

            return BadRequest("Some error has occured, failed to delete photo");
        }*/
    }
}
