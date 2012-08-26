using System;
using Contracts.Portal;
using Web.Host.Wcf;

namespace Web.Host
{
    /// <summary>
    /// The default implementation of <see cref="IPortalService" />.
    /// </summary>
    [ServiceExtensions(Audit = true, Elmah = true, Portal = true, RequestHandler = true, Transactional = true)]
    public class PortalService : IPortalService
    {
        /// <summary>
        /// Executes the given request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The response.</returns>
        public byte[] Execute(byte[] request)
        {
            throw new NotSupportedException("All requests should be routed through request handlers.");
        }
    }
}
