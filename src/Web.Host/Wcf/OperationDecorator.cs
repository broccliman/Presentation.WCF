using System;
using System.ServiceModel.Dispatcher;

namespace Web.Host.Wcf
{
    /// <summary>
    /// The base class for all invokers; decorator pattern.
    /// </summary>
    public abstract class OperationDecorator : IOperationInvoker
    {
        /// <summary>
        /// The decorated invoker.
        /// </summary>
        private readonly IOperationInvoker _innerInvoker;

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationDecorator"/> class.
        /// </summary>
        protected OperationDecorator(IOperationInvoker innerInvoker)
        {
            _innerInvoker = innerInvoker;
        }

        /// <summary>
        /// Gets a value that specifies whether the <see cref="M:System.ServiceModel.Dispatcher.IOperationInvoker.Invoke(System.Object,System.Object[],System.Object[]@)"/> or <see cref="M:System.ServiceModel.Dispatcher.IOperationInvoker.InvokeBegin(System.Object,System.Object[],System.AsyncCallback,System.Object)"/> method is called by the dispatcher.
        /// </summary>
        /// <returns>
        /// true if the dispatcher invokes the synchronous operation; otherwise, false.
        /// </returns>
        bool IOperationInvoker.IsSynchronous
        {
            get { return _innerInvoker.IsSynchronous; }
        }

        /// <summary>
        /// Returns an <see cref="T:System.Array"/> of parameter objects.
        /// </summary>
        /// <returns>
        /// The parameters that are to be used as arguments to the operation.
        /// </returns>
        object[] IOperationInvoker.AllocateInputs()
        {
            return _innerInvoker.AllocateInputs();
        }

        /// <summary>
        /// Returns an object and a set of output objects from an instance and set of input objects.  
        /// </summary>
        /// <param name="instance">The object to be invoked.</param>
        /// <param name="inputs">The inputs to the method.</param>
        /// <param name="outputs">The outputs from the method.</param>
        /// <returns>The return value.</returns>
        public virtual object Invoke(object instance, object[] inputs, out object[] outputs)
        {
            return _innerInvoker.Invoke(instance, inputs, out outputs);
        }

        /// <summary>
        /// An asynchronous implementation of the <see cref="M:System.ServiceModel.Dispatcher.IOperationInvoker.Invoke(System.Object,System.Object[],System.Object[]@)"/> method.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.IAsyncResult"/> used to complete the asynchronous call.
        /// </returns>
        /// <param name="instance">The object to be invoked.</param><param name="inputs">The inputs to the method.</param><param name="callback">The asynchronous callback object.</param><param name="state">Associated state data.</param>
        IAsyncResult IOperationInvoker.InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
        {
            return _innerInvoker.InvokeBegin(instance, inputs, callback, state);
        }

        /// <summary>
        /// The asynchronous end method.
        /// </summary>
        /// <returns>
        /// The return value.
        /// </returns>
        /// <param name="instance">The object invoked.</param><param name="outputs">The outputs from the method.</param><param name="result">The <see cref="T:System.IAsyncResult"/> object.</param>
        object IOperationInvoker.InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
        {
            return _innerInvoker.InvokeEnd(instance, out outputs, result);
        }
    }
}