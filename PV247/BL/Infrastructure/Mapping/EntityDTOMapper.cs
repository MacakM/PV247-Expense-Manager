using AutoMapper;

namespace BL.Infrastructure.Mapping
{
    /// <summary>
    /// Entity to/from DTO mapper, taken from unreleased project of RigantiInfrastructure solution, all credit goes to Tomas Herceg.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TDTO">The type of the DTO.</typeparam>
    public class EntityDTOMapper<TEntity, TDTO> : IEntityDTOMapper<TEntity, TDTO>
    {
        public TDTO MapToDTO(TEntity source)
        {
            return Mapper.Map<TDTO>(source);
        }

        public void PopulateEntity(TDTO source, TEntity target)
        {
            Mapper.Map(source, target);
        }
    }
}