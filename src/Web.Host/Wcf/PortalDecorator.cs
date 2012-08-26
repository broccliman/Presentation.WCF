using System.ServiceModel.Dispatcher;
using Contracts.Portal;

namespace Web.Host.Wcf
{
    /// <summary>
    /// The portal invoker.
    /// </summary>
	public class PortalDecorator : OperationDecorator
	{
        /// <summary>
        /// Intitializes a new instance of the <see cref="PortalDecorator" /> class.
        /// </summary>
		public PortalDecorator(IOperationInvoker innerInvoker)
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
            // Get the request and unwrap from payload container
			var requestPayload = (byte[])inputs[0];
			var request = PortalPayload.FromByteArray(requestPayload);

            // Put the response into a payload container, and return value
			var response = base.Invoke(instance, new[] { request }, out outputs);
			var responsePayload = PortalPayload.ToByteArray(response);

			outputs = new object[] { };
			return responsePayload;
		}
	}
}