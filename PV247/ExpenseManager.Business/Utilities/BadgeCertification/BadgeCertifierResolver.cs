using System.Collections.Generic;
using System.Linq;
using ExpenseManager.Business.Utilities.BadgeCertification.BadgeCertifiers;

namespace ExpenseManager.Business.Utilities.BadgeCertification
{
    /// <summary>
    /// Manages all implementations of IBadgeCertifier
    /// </summary>
    public class BadgeCertifierResolver : IBadgeCertifierResolver
    {
        private readonly IEnumerable<IBadgeCertifier> _badgeCertifiers;

        /// <summary>
        /// For now the badge certifier resolver, instantiates all its dependencies (classes implementing IBadgeCertifier),
        /// however this should be done by some proper DI framwork as mentioned in comment inside the ctor.
        /// </summary>
        public BadgeCertifierResolver(IEnumerable<IBadgeCertifier> badgeCertifiers)
        {
            _badgeCertifiers = badgeCertifiers;
        }

        /// <summary>
        /// Gets instance of badge certifier according to the badge name
        /// </summary>
        /// <param name="badgeName">The name of the badge to find certifier for</param>
        /// <returns>Badge certifier with corresponding name or null, if not found</returns>
        public IBadgeCertifier ResolveBadgeCertifier(string badgeName)
        {
            return string.IsNullOrEmpty(badgeName) ? null :
                _badgeCertifiers.FirstOrDefault(certifier => certifier.GetBadgeName().Equals(badgeName));
        }
    }
}
