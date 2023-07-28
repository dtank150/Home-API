using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Interfaces
{
    public interface IUnitOfWork
    {
        ICityRepository CityRepository { get; }
        IUserRepository UserRepository { get; }
        IPropertyRepository PropertyRepository { get; }
        IPropertyType PropertyType { get; }
        IFurnishingTypeRepository FurnishingTypeRepository { get; }
        Task<bool> SaveAsync();
    }
}
