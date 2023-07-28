using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Data.Repo
{
    public class FurnishingTypeRepository : IFurnishingTypeRepository
    {
        private readonly DBContext context;

        public FurnishingTypeRepository(DBContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<FurnishingType>> GetFurnishingTypesAsync()
        {
            return await context.FurnishingTypes.ToListAsync();
        }
    }
}
