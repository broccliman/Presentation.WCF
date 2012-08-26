using System;
using System.ServiceModel;

namespace Contracts.Utility
{
    /// <summary>
    /// The base class for any component that invokes a wcf service.
    /// </summary>
    public class ServiceAdapter
    {
        /// <summary>
        /// Gets the endpoint name for the service call.
        /// </summary>
        protected static string ResolveEndpointName<T>(string endpointName)
        {
            return !string.IsNullOrEmpty(endpointName) ? endpointName : typeof(T).Name;
        }
    }

    /// <summary>
    /// The base class for any component that invokes a wcf service.
    /// </summary>
    /// <typeparam name="TService">The wcf service type.</typeparam>
    public class ServiceAdapter<TService> : ServiceAdapter
        where TService : class
    {
        /// <summary>
        /// Invokes the proxy.
        /// </summary>
        /// <typeparam name="T">The response type of the remote call.</typeparam>
        /// <param name="method">The method to invoke on the proxy.</param>
        /// <param name="endpointName">The endpoint name in the config file.</param>
        /// <returns>The wcf service response.</returns>
        public static T Execute<T>(Func<TService, T> method, string endpointName = "")
        {
            endpointName = ResolveEndpointName<TService>(endpointName);

            using (var factory = new ChannelFactory<TService>(endpointName))
            {
                var service = factory.CreateChannel();
                return method(service);
            }
        }
    }
}