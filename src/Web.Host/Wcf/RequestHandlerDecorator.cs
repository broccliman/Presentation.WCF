using System;
using System.ServiceModel.Dispatcher;
using System.Linq;
using Web.Host.Business;

namespace Web.Host.Wcf
{
    /// <summary>
    /// The request handler invoker.
    /// </summary>
	public class RequestHandlerDecorator : OperationDecorator
	{
        /// <summary>
        /// Intitializes a new instance of the <see cref="RequestHandlerDecorator" /> class.
        /// </summary>
		public RequestHandlerDecorator(IOperationInvoker innerInvoker)
			: base(innerInvoker)
		{
		}

		/// <summary>
		/// Returns an object and a set of output objects from an instance and set of input objects.  
		/// </summary>
		/// <param name="instance">The object to be invoked.</param>
		/// <param name="inputs">The inputs to the method.</param>
		/// <param name="outputs">The outputs from the method.</param>
		/// <returns>The return value.</returns>
		public override object Invoke(object instance, object[] inputs, out object[] outputs)
		{
			// Get the request
			var request = inputs[0];

			// Create the handler for the request
			var handler = GetHandlerInstance(request.GetType());
			handler.SetRequest(request);
			handler.Execute();

			// Return the response
			var response = handler.GetResponse();
			outputs = new[] { response };
			return response;
		}

		/// <summary>
		/// Gets an instance of a handler from the given type.
		/// </summary>
		private IRequestHandler GetHandlerInstance(Type requestType)
		{
            // NOTE - 
            // IOC CONTAINERS MAKES THIS MUCH MORE SIMPLE

			foreach (var t in GetType().Assembly.GetTypes())
			{
				if (t.GetInterfaces().Contains(typeof(IRequestHandler)) && !t.IsAbstract)
				{
					if (t.GetProperty("Request").PropertyType == requestType)
						return (IRequestHandler)Activator.CreateInstance(t);
				}
			}

			throw new Exception("Could not find a message handler for the given request of type " + requestType);
		}
	}
}