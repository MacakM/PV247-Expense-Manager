namespace BL.Infrastructure.Mapping
{
    /// <summary>
    /// Entity to/from DTO mapper, taken from unreleased project of RigantiInfrastructure solution, all credit goes to Tomas Herceg.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TDTO">The type of the DTO.</typeparam>
    public interface IEntityDTOMapper<TEntity, TDTO>
    {
        TDTO MapToDTO(TEntity source);
        void PopulateEntity(TDTO source, TEntity target);
    }
}