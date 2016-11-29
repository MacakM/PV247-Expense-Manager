using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExpenseManager.Business.Utilities.BadgeCertification.BadgeCertifiers;

namespace ExpenseManager.Business.Utilities.BadgeCertification
{
    /// <summary>
    /// Manages all implementations of IBadgeCertifier
    /// </summary>
    public class BadgeCertifierResolver : IBadgeCertifierResolver
    {
        private readonly IList<BadgeCertifier> _badgeCertifiers = new List<BadgeCertifier>(); 

        /// <summary>
        /// For now the badge certifier resolver, instantiates all its dependencies (classes implementing IBadgeCertifier),
        /// however this should be done by some proper DI framwork as mentioned in comment inside the ctor.
        /// </summary>
        public BadgeCertifierResolver()
        {
            // TODO: this is far from optimal solution, the best way will be to integrate proper DI framework within PL to do the job and solve several issues at once
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => type.IsSubclassOf(typeof(BadgeCertifier))))
            {
                _badgeCertifiers.Add(Activator.CreateInstance(type) as BadgeCertifier);
            }
        }

        /// <summary>
        /// Gets instance of badge certifier according to the badge name
        /// </summary>
        /// <param name="badgeName">The name of the badge to find certifier for</param>
        /// <returns>Badge certifier with corresponding name or null, if not found</returns>
        public BadgeCertifier ResolveBadgeCertifier(string badgeName)
        {
            return string.IsNullOrEmpty(badgeName) ? null :
                _badgeCertifiers.FirstOrDefault(certifier => certifier.GetBadgeName().Equals(badgeName));
        }
    }
}
