using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Data.Repo
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly DBContext context;

        public PropertyRepository(DBContext context)
        {
            this.context = context;
        }
        public void AddProperty(Property property)
        {
            context.Properties.Add(property);
        }

        public void DeleteProperty(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Property>> GetPropertiesAsync(int sellRent)
        {
            var properties = await context.Properties
                .Include(p => p.PropertyType)
                .Include(p => p.City)
                .Include(p => p.FurnishingType)
                .Include(p => p.Photos)
                .Where(p=> p.SellRent == sellRent)
                .ToListAsync();
            return properties;
        }
        
        public async Task<Property> GetPropertyDetailAsync(int id)
        {
            var properties = await context.Properties
            .Include(p => p.PropertyType)
            .Include(p => p.City)
            .Include(p => p.FurnishingType)
            .Include(p => p.Photos)
            .Where(p => p.Id == id)
            .FirstAsync();

            return properties;
        }
        public async Task<Property> GetPropertyByIdAsync(int id)
        {
            var properties = await context.Properties
            .Include(p => p.Photos)
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();

            return properties;
        }

    }
}
