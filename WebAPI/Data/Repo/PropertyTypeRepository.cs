using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Data.Repo
{
    public class PropertyTypeRepository  : IPropertyType
    {
        private readonly DBContext context;

        public PropertyTypeRepository(DBContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<PropertyType>> GetPropertyTypesAsync()
        {
            return await context.PropertyTypes.ToListAsync();
        }
    }
}
