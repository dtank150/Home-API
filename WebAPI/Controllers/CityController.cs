using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly DBContext context;

        public CityController(DBContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cities = await context.cities.ToListAsync();
            return Ok(cities);
        }
       /* [HttpGet("{id}")]
        public string Get(int id)
        {
            return "Atlanta";
        }*/
       //Post api/City/Add?CityName="Phonix"
       //Post api/City/Add/New York
       [HttpPost("Add")]
       [HttpPost("Add/CityName")]
        public async Task<IActionResult> AddCity(string name)
        {
            City city = new City();
            city.Name = name;
            await context.cities.AddAsync(city);
            await context.SaveChangesAsync();
            return Ok(city);
        }
        //Post api/City/Post --Post the data in JSON Format
        [HttpPost("Post")]
        public async Task<IActionResult> AddCity(City city)
        {
            await context.cities.AddAsync(city);
            await context.SaveChangesAsync();
            return Ok(city);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var city = await context.cities.FindAsync(id);
            context.cities.Remove(city);
            await context.SaveChangesAsync();
            return Ok(city);
        }
    }
}
