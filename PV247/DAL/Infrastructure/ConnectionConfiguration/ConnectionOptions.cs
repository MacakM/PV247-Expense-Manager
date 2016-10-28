namespace DAL.Infrastructure.ConnectionConfiguration
{
    /// <summary>
    /// Class is used to deliver application-level configuration to EF contexts.
    /// </summary>
    public class ConnectionOptions
    {
        public const string ExpenseManagerConnectionStringName = "DefaultConnection";

        public string ConnectionString { get; set; }
    }
}
