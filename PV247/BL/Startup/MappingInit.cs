using AutoMapper;
using BL.DTOs;
using DAL.Entities;

namespace BL.Startup
{
    public static class MappingInit
    {
        /// <summary>
        /// Configures mapping between entities and DTOs
        /// </summary>
        public static void ConfigureMapping()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<User, UserDTO>().ReverseMap();
                config.CreateMap<Plan, PlanDTO>().ReverseMap();               
                // TODO add other entities

            });
        }
    }
}
