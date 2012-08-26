using System.ServiceModel.Dispatcher;
using System.Transactions;

namespace Web.Host.Wcf
{
    /// <summary>
    /// The transactional invoker.
    /// </summary>
    public class TransactionalDecorator : OperationDecorator
    {
        /// <summary>
        /// Intitializes a new instance of the <see cref="TransactionalDecorator" /> class.
        /// </summary>
        public TransactionalDecorator(IOperationInvoker innerInvoker)
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
            using (var tx = new TransactionScope())
            {
                var response = base.Invoke(instance, inputs, out outputs);
                tx.Complete();
                return response;
            }
        }
    }
}