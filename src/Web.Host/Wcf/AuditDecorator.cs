using System.ServiceModel.Dispatcher;
using Web.Host.Infrastructure;

namespace Web.Host.Wcf
{
    /// <summary>
    /// The audit invoker.
    /// </summary>
    public class AuditDecorator : OperationDecorator
    {
        /// <summary>
        /// Intitializes a new instance of the <see cref="AuditDecorator" /> class.
        /// </summary>
        public AuditDecorator(IOperationInvoker innerInvoker)
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
            AuditStack.Push(inputs[0]);

            var response = base.Invoke(instance, inputs, out outputs);

            AuditStack.Push(response);

            return response;
        }
    }
}