using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    public class FurnishingTypeController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public FurnishingTypeController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        [HttpGet("List")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFurnishingType()
        {
            var furnishingTypes = await uow.FurnishingTypeRepository.GetFurnishingTypesAsync();
            var furnishingTypeDto = mapper.Map<IEnumerable<KeyValuePairDto>>(furnishingTypes);
            return Ok(furnishingTypeDto);
        }
    }
}
