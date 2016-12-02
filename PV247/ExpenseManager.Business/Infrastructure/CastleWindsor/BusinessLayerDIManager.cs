using Castle.MicroKernel.Registration;
using Castle.Windsor;
using ExpenseManager.Database.Infrastructure.ConnectionConfiguration;

namespace ExpenseManager.Business.Infrastructure.CastleWindsor
{
    /// <summary>
    /// Responsible for bootstraping business layer
    /// </summary>
    public static class BusinessLayerDIManager
    {
        private static readonly WindsorContainer Container = new WindsorContainer();

        /// <summary>
        /// Register all business layer dependencies
        /// </summary>
        /// <param name="connectionOptions"></param>
        public static void BootstrapContainer(ConnectionOptions connectionOptions)
        {
            Container.Register(Component.For<ConnectionOptions>()
                        .Instance(connectionOptions)
                        .LifestyleSingleton());
            Container.Install(new BusinessLayerInstaller());
        }

        internal static T Resolve<T>() => Container.Resolve<T>();       
    }
}
