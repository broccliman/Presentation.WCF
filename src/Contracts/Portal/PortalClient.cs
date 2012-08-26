using System.ServiceModel;
using Contracts.Core;
using Contracts.Utility;

namespace Contracts.Portal
{
    /// <summary>
    /// The service client for <see cref="IPortalService" />.
    /// </summary>
    public class PortalClient : ServiceAdapter
    {
        /// <summary>
        /// Executes the portal with the given request.
        /// </summary>
        /// <typeparam name="T">The type of response.</typeparam>
        /// <param name="request">The request.</param>
        /// <param name="endpointName">The (optional) endpoint name in the config file.</param>
        /// <returns>The response.</returns>
        public static T Execute<T>(IRequest<T> request, string endpointName = "")
            where T : IResponse
        {
            T response;

            // Get the endpoint name
            endpointName = ResolveEndpointName<IPortalService>(endpointName);

            // Serialize the request
            var requestPayload = PortalPayload.ToByteArray(request);

            // Invoke the portal
            using (var factory = new ChannelFactory<IPortalService>(endpointName))
            {
                var service = factory.CreateChannel();
                var responsePayload = service.Execute(requestPayload);
                response = (T)PortalPayload.FromByteArray(responsePayload);
            }

            return response;
        }
    }
}