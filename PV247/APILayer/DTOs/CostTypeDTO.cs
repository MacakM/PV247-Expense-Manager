namespace APILayer.DTOs
{
    public class CostTypeDTO : ExpenseManagerDTO<int>
    {
        /// <summary>
        /// Name of this type.
        /// </summary>
        public string Name { get; set; }
    }
}
