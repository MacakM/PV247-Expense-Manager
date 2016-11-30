using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExpenseManager.Database.DataAccess.FilterInterfaces;

namespace ExpenseManager.Business.DataTransferObjects.Factories
{
    internal static class FilterFactory 
    {
        internal static List<IFilter<T>> GetFilters<T>(params Tuple<string, object>[] values)
        {
            var notNullValues = values.Where(x => x.Item2 != null).ToList();
            var output = new List<IFilter<T>>();
            var filters = Assembly.GetAssembly(typeof (FilterFactory)).GetTypes().Where(t => !t.IsInterface && t.GetInterfaces().Any(i => i.Name == typeof(IFilter<>).Name && i.GenericTypeArguments[0] == typeof(T)) && MutualProperties(t, notNullValues).Any());

            foreach (var filter in filters)
            {
                var instance =(IFilter<T>)Activator.CreateInstance(filter);
                foreach (var value in MutualProperties(filter, notNullValues.ToList()))
                {
                    var property = filter.GetProperty(value.Item1, BindingFlags.Public | BindingFlags.Instance);
                    if (null != property && property.CanWrite)
                    {
                        property.SetValue(instance, value.Item2);
                    }
                }
                output.Add(instance);
            }
            return output;
        }
        
        private static IEnumerable<Tuple<string, object>> MutualProperties(Type type, List<Tuple<string, object>> values)
        {
            return values.Where(x => type.GetProperties().Select(y=>y.Name).Contains(x.Item1));
        }
    }
}
