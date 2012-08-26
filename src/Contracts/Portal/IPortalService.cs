using System.ServiceModel;

namespace Contracts.Portal
{
    /// <summary>
    /// Defines the contract for a service that acts as a portal.
    /// </summary>
    [ServiceContract]
    public interface IPortalService
    {
        /// <summary>
        /// Executes the given request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The response.</returns>
        [OperationContract]
        byte[] Execute(byte[] request);
    }
}