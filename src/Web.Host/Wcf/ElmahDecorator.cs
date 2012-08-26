using System;
using System.ServiceModel.Dispatcher;
using Web.Host.Infrastructure;

namespace Web.Host.Wcf
{
    /// <summary>
    /// The ELMAH invoker.
    /// </summary>
    public class ElmahDecorator : OperationDecorator
    {
        /// <summary>
        /// Intitializes a new instance of the <see cref="ElmahDecorator" /> class.
        /// </summary>
        public ElmahDecorator(IOperationInvoker innerInvoker)
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
            try
            {
                return base.Invoke(instance, inputs, out outputs);
            }
            catch (Exception ex)
            {
                ElmahLogger.Log(ex);
                throw;
            }
        }
    }
}