﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Repo;
using WebAPI.Interfaces;

namespace WebAPI.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBContext context;

        public UnitOfWork(DBContext context)
        {
            this.context = context;
        }
        public ICityRepository CityRepository => new CityRepository(context);

        public IUserRepository UserRepository => new UserRepository(context);

        public IPropertyRepository PropertyRepository => new PropertyRepository(context);

        public IPropertyType PropertyType => new PropertyTypeRepository(context);

        public IFurnishingTypeRepository FurnishingTypeRepository => new FurnishingTypeRepository(context);

        public async Task<bool> SaveAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}
