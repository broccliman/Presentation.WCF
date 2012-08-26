using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Web.Host.Wcf
{
    /// <summary>
    /// The attribute used to extend the functionality/behavior of a service, or an operation.
    /// </summary>
    public class ServiceExtensionsAttribute : Attribute, IServiceBehavior, IOperationBehavior
    {
        #region Properties

        /// <summary>
        /// Indicates that data should be audited.
        /// </summary>
        public bool Audit { get; set; }

        /// <summary>
        /// Indicates that ELMAH should be used.
        /// </summary>
        public bool Elmah { get; set; }

        /// <summary>
        /// Indicates that the service is acting as a portal.
        /// </summary>
        public bool Portal { get; set; }

        /// <summary>
        /// Indicates that request handlers should be used.
        /// </summary>
        public bool RequestHandler { get; set; }

        /// <summary>
        /// Indicates that transactions should be used.
        /// </summary>
        public bool Transactional { get; set; }

        #endregion Properties

        #region IServiceBehavior Implementation

        /// <summary>
        /// Provides the ability to inspect the service host and the service description to confirm that the service can run successfully.
        /// </summary>
        /// <param name="serviceDescription">The service description.</param><param name="serviceHostBase">The service host that is currently being constructed.</param>
        void IServiceBehavior.Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        /// <summary>
        /// Provides the ability to pass custom data to binding elements to support the contract implementation.
        /// </summary>
        /// <param name="serviceDescription">The service description of the service.</param><param name="serviceHostBase">The host of the service.</param><param name="endpoints">The service endpoints.</param><param name="bindingParameters">Custom objects to which binding elements have access.</param>
        void IServiceBehavior.AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
            endpoints
                .SelectMany(x => x.Contract.Operations)
                .Where(x => x.DeclaringContract.ContractType != typeof(IMetadataExchange))
                .ToList()
                .ForEach(x =>
                {
                    if (!x.Behaviors.Any(b => b.GetType() == GetType()))
                        x.Behaviors.Add(this);
                });
        }

        /// <summary>
        /// Provides the ability to change run-time property values or insert custom extension objects such as error handlers, message or parameter interceptors, security extensions, and other custom extension objects.
        /// </summary>
        /// <param name="serviceDescription">The service description.</param><param name="serviceHostBase">The host that is currently being built.</param>
        void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        #endregion IServiceBehavior Implementation

        #region IOperationBehavior Implementation

        /// <summary>
        /// Implements a modification or extension of the service across an operation.
        /// </summary>
        /// <param name="operationDescription">The operation being examined. Use for examination only. If the operation description is modified, the results are undefined.</param>
        /// <param name="dispatchOperation">The run-time object that exposes customization properties for the operation described by <paramref name="operationDescription"/>.</param>
        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            if (RequestHandler)
                dispatchOperation.Invoker = new RequestHandlerDecorator(dispatchOperation.Invoker);

            if (Transactional)
                dispatchOperation.Invoker = new TransactionalDecorator(dispatchOperation.Invoker);

            if (Audit)
                dispatchOperation.Invoker = new AuditDecorator(dispatchOperation.Invoker);

            if (Portal)
                dispatchOperation.Invoker = new PortalDecorator(dispatchOperation.Invoker);

            if (Elmah)
                dispatchOperation.Invoker = new ElmahDecorator(dispatchOperation.Invoker);
        }

        /// <summary>
        /// Implement to confirm that the operation meets some intended criteria.
        /// </summary>
        /// <param name="operationDescription">The operation being examined. Use for examination only. If the operation description is modified, the results are undefined.</param>
        void IOperationBehavior.Validate(OperationDescription operationDescription)
        {
        }

        /// <summary>
        /// Implements a modification or extension of the client across an operation.
        /// </summary>
        /// <param name="operationDescription">The operation being examined. Use for examination only. If the operation description is modified, the results are undefined.</param>
        /// <param name="clientOperation">The run-time object that exposes customization properties for the operation described by <paramref name="operationDescription"/>.</param>
        void IOperationBehavior.ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
        }

        /// <summary>
        /// Implement to pass data at runtime to bindings to support custom behavior.
        /// </summary>
        /// <param name="operationDescription">The operation being examined. Use for examination only. If the operation description is modified, the results are undefined.</param>
        /// <param name="bindingParameters">The collection of objects that binding elements require to support the behavior.</param>
        void IOperationBehavior.AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
        }

        #endregion IOperationBehavior Implementation
    }
}