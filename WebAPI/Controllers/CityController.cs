using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Dtos;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize]
    public class CityController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public CityController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            //throw new UnauthorizedAccessException();
            var cities = await uow.CityRepository.GetCitiesAsync();
            var citiesDto = mapper.Map<IEnumerable<CityDto>>(cities);

            //var citiesDto = from c in cities
            //                select new CityDto()
            //                {
            //                    Id = c.Id,
            //                    Name = c.Name
            //                };
            return Ok(citiesDto);
        }
       /* [HttpGet("{id}")]
        public string Get(int id)
        {
            return "Atlanta";
        }*/
       //Post api/City/Add?CityName="Phonix"
       //Post api/City/Add/New York
       //[HttpPost("Add")]
       //[HttpPost("Add/CityName")]
       // public async Task<IActionResult> AddCity(string name)
       // {
       //     City city = new City();
       //     city.Name = name;
       //     await context.cities.AddAsync(city);
       //     await context.SaveChangesAsync();
       //     return Ok(city);
       // }
        //Post api/City/Post --Post the data in JSON Format
        [HttpPost("Post")]
        public async Task<IActionResult> AddCity(CityDto cityDto)
        {
            var city = mapper.Map<City>(cityDto);
            city.LastUpdatedBy = 1;
            city.LastUpdateOn = DateTime.Now;
            /* var city = new City
             {
                 Name = cityDto.Name,
                 LastUpdatedBy = 1,
                 LastUpdateOn = DateTime.Now
             };*/
            uow.CityRepository.Add(city);
            await uow.SaveAsync();
            return StatusCode(201);
            /*return Ok(city);*/
        }
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, CityDto cityDto)
        {
            
                if (id != cityDto.Id)
                    return BadRequest("Update Not Allowed");
                var city = await uow.CityRepository.FindCity(id);
                if (city == null)
                    return BadRequest("Update Not Allowed");
                city.LastUpdatedBy = 1;
                city.LastUpdateOn = DateTime.Now;
                mapper.Map(cityDto, city);

                throw new Exception("Some Unknown Error Occured");
                await uow.SaveAsync();
                return StatusCode(200);

            
            
            
        }
       /* [HttpPut("Update/Name/{id}")]
        public async Task<IActionResult> UpdateDto(int id, CityUpdateDto cityDto)
        {
            var city = await uow.CityRepository.FindCity(id);
            city.LastUpdatedBy = 1;
            city.LastUpdateOn = DateTime.Now;
            mapper.Map(cityDto, city);
            await uow.SaveAsync();
            return StatusCode(200);
        }
        [HttpPatch("Update/{id}")]
        public async Task<IActionResult> UpdatePatch(int id, JsonPatchDocument<City> cityPatch)
        {
            var city = await uow.CityRepository.FindCity(id);
            city.LastUpdatedBy = 1;
            city.LastUpdateOn = DateTime.Now;
            cityPatch.ApplyTo(city, ModelState);
            await uow.SaveAsync();
            return StatusCode(200);
        }*/
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            uow.CityRepository.Delete(id);
            await uow.SaveAsync();
            return Ok(id);
        }
    }
}
