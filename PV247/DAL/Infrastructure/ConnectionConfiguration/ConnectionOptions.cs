namespace DAL.Infrastructure.ConnectionConfiguration
{
    /// <summary>
    /// Class is used to deliver application-level configuration to EF contexts.
    /// </summary>
    public class ConnectionOptions
    {
        /// <summary>
        /// Connection string name.
        /// </summary>
        public const string ExpenseManagerConnectionStringName = "DefaultConnection";

        /// <summary>
        /// Connection string that is used to access the DB.
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
