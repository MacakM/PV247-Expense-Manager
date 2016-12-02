using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using static System.AppDomain;

namespace ExpenseManager.Business.Infrastructure.CastleWindsor
{
    internal static class RegisterExtensionMethods
    {
        internal static IRegistration[] RegisterManyBasedOn(this IKernel containerKernel, Type baseType, bool asSingletons = false)
        {
            var typesToRegister = TypesFromCurrentAssembly
                .Where(type => !type.IsAbstract && !type.IsInterface &&
                        type.BaseType != null && type.BaseType.IsGenericType &&
                         type.BaseType.GetGenericTypeDefinition() == baseType)
                .ToList();
            var registrations = new IRegistration[typesToRegister.Count];


            for (var i = 0; i < typesToRegister.Count; i++)
            {
                var usingFactoryMethod = typeof(RegisterExtensionMethods)
                    .GetMethod(nameof(RegisterComponentForProxy), BindingFlags.NonPublic | BindingFlags.Static);
                var usingFactoryMethodRef = usingFactoryMethod.MakeGenericMethod(typesToRegister[i],
                    TypesFromCurrentAssembly.First(type => type.GetInterfaces().Contains(typesToRegister[i]) && type.IsClass));
                registrations[i] = usingFactoryMethodRef
                    .Invoke(new BusinessLayerInstaller(), new object[] { containerKernel, asSingletons }) as IRegistration;
            }
            return registrations;
        }

        internal static IRegistration[] RegisterManyWithDefaultInterfacesBasedOnInterface<T>(this IKernel containerKernel, bool asSingletons = false) where T : class
        {
            var typesToRegister = TypesFromCurrentAssembly
                .Where(type => type.GetInterfaces().Contains(typeof(T)) && type.IsInterface)
                .ToList();
            var registrations = new IRegistration[typesToRegister.Count];

            for (var i = 0; i < typesToRegister.Count; i++)
            {
                var usingFactoryMethod = typeof(RegisterExtensionMethods)
                    .GetMethod(nameof(RegisterComponentForProxy), BindingFlags.NonPublic | BindingFlags.Static);
                var usingFactoryMethodRef = usingFactoryMethod.MakeGenericMethod(typesToRegister[i], 
                    TypesFromCurrentAssembly.First(type => type.GetInterfaces().Contains(typesToRegister[i]) && type.IsClass));
                registrations[i] = usingFactoryMethodRef
                    .Invoke(new BusinessLayerInstaller(), new object[] { containerKernel, asSingletons }) as IRegistration;
            }
            return registrations;
        }

        internal static IRegistration[] RegisterManyBasedOnInterface<T>(this IKernel containerKernel, bool asSingletons = false) 
            where T : class
        {
            var typesToRegister = TypesFromCurrentAssembly
                .Where(type => type.GetInterfaces().Contains(typeof(T)) && type.IsClass /* type.IsSubclassOf(typeof(T))*/)
                .ToList();
            return containerKernel.ProcessTypesToRegister<T>(typesToRegister, asSingletons);
        }

        /// <summary>
        /// Proxy for extension method RegisterComponentFor used when invocation is performed via reflection.
        /// </summary>
        /// <param name="containerKernel">DI kernel</param>
        /// <param name="asSingleton">Register component as singleton, otherwise use transient</param>
        /// <returns>The component registration</returns>
        internal static IRegistration RegisterComponentForProxy<TBase, TImplementation>(IKernel containerKernel, bool asSingleton) where TBase : class where TImplementation : class, TBase
        {
            return containerKernel.RegisterComponentFor<TBase, TImplementation>(asSingleton);
        }

        internal static IRegistration RegisterComponentFor<TBase, TImplementation>(this IKernel containerKernel, bool asSingleton)
            where TBase : class where TImplementation : class, TBase
        {
            var ctorParameters = typeof(TImplementation).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)
                                                .FirstOrDefault()
                                                ?.GetParameters();

            var ctorArgs = new object[ctorParameters?.Length ?? 0];
            for (var i = 0; i < ctorParameters?.Length; i++)
            {
                ctorArgs[i] = containerKernel.Resolve(ctorParameters[i].ParameterType);
            }

            return RegisterComponentForCore<TBase, TImplementation>(asSingleton, ctorArgs);
        }
       
        internal static IRegistration RegisterComponentWithCollectionFor<TBase, TImplementation, TCollectionItemBase>(this IKernel containerKernel, bool asSingleton)
            where TBase : class 
            where TImplementation : class, TBase
        {
            var ctorArgs = new object[]
            {
                containerKernel.ResolveAll<TCollectionItemBase>()
            };

            return RegisterComponentForCore<TBase, TImplementation>(asSingleton, ctorArgs);
        }

        private static IRegistration[] ProcessTypesToRegister<T>(this IKernel containerKernel, List<Type> typesToRegister, bool asSingletons = false)
        {
            var registrations = new IRegistration[typesToRegister.Count];

            for (var i = 0; i < typesToRegister.Count; i++)
            {
                var usingFactoryMethod = typeof(RegisterExtensionMethods)
                    .GetMethod(nameof(RegisterComponentForProxy), BindingFlags.NonPublic | BindingFlags.Static);
                var usingFactoryMethodRef = usingFactoryMethod.MakeGenericMethod(typeof(T), typesToRegister[i]);
                registrations[i] = usingFactoryMethodRef
                    .Invoke(new BusinessLayerInstaller(), new object[] {containerKernel, asSingletons}) as IRegistration;
            }
            return registrations;
        }

        private static IRegistration RegisterComponentForCore<TBase, TImplementation>(bool asSingleton, object[] ctorArgs)
            where TBase : class where TImplementation : class, TBase
        {
            return ctorArgs.Any() ? 
                RegisterComponentWithParametersCore<TBase, TImplementation>(asSingleton, ctorArgs) :
                RegisterParameterlessComponentForCore<TBase, TImplementation>(asSingleton);
        }

        private static IRegistration RegisterComponentWithParametersCore<TBase, TImplementation>(bool asSingleton, object[] ctorArgs)
            where TBase : class where TImplementation : class, TBase
        {
            if (asSingleton)
            {
                return Component.For<TBase>()
                    .UsingFactoryMethod(kernel => Activator.CreateInstance(typeof(TImplementation), BindingFlags.Instance | BindingFlags.NonPublic, null, ctorArgs.Any() ? ctorArgs : null, null, null) as TImplementation)
                    .LifestyleSingleton();
            }
            return Component.For<TBase>()
                .UsingFactoryMethod(kernel => Activator.CreateInstance(typeof(TImplementation), BindingFlags.Instance | BindingFlags.NonPublic, null, ctorArgs.Any() ? ctorArgs : null, null, null) as TImplementation)
                .LifestyleTransient();
        }

        private static IRegistration RegisterParameterlessComponentForCore<TBase, TImplementation>(bool asSingleton)
            where TBase : class where TImplementation : class, TBase
        {
            if (asSingleton)
            {
                return Component.For<TBase>()
                    .UsingFactoryMethod(kernel => Activator.CreateInstance(typeof(TImplementation), true) as TImplementation)
                    .Named(typeof(TImplementation).FullName)
                    .LifestyleSingleton();
            }
            return Component.For<TBase>()
                .UsingFactoryMethod(kernel => Activator.CreateInstance(typeof(TImplementation), true) as TImplementation)
                .Named(typeof(TImplementation).FullName)
                .LifestyleTransient();
        }

        private static readonly Type[] TypesFromCurrentAssembly = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Union(CurrentDomain
                    .GetAssemblies()
                    .SingleOrDefault(assembly => assembly.GetName().Name == "ExpenseManager.Database")?
                    .GetTypes())
            .ToArray();
    }
}
