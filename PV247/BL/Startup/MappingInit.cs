using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTOs;
using DAL.Entities;

namespace BL.Startup
{
    public static class MappingInit
    {
        public static void ConfigureMapping()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Plan, PlanDTO>().ReverseMap();
                
                // TODO add other entities

                
            });
        }

    }
}
