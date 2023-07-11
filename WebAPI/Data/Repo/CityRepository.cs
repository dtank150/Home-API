using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Data.Repo
{
    public class CityRepository : ICityRepository
    {
        private readonly DBContext context;

        public CityRepository(DBContext context)
        {
            this.context = context;
        }
        public void Add(City city)
        {
            context.cities.AddAsync(city);
        }

        public void Delete(int id)
        {
            var city = context.cities.Find(id);
            context.cities.Remove(city);
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await context.cities.ToListAsync();
        }

    }
}
