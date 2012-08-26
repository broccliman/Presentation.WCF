using Contracts.Core;

namespace Web.Host.Business
{
    /// <summary>
    /// Defines the contract for a component that handles an incoming request.
    /// </summary>
	public interface IRequestHandler
	{
        /// <summary>
        /// Sets the incoming request.
        /// </summary>
        /// <param name="request">The request.</param>
		void SetRequest(object request);

        /// <summary>
        /// Gets the response.
        /// </summary>
        /// <returns>The generated response.</returns>
		object GetResponse();

        /// <summary>
        /// Executes the business logic to handle the request.
        /// </summary>
		void Execute();
	}

    /// <summary>
    /// The base class for a message handler.
    /// </summary>
    /// <typeparam name="TRequest">The type of request.</typeparam>
    /// <typeparam name="TResponse">The type of response.</typeparam>
    public abstract class RequestHandler<TRequest, TResponse> : IRequestHandler
        where TRequest : IRequest<TResponse>
        where TResponse : IResponse, new()
    {
        /// <summary>
        /// Sets the incoming request.
        /// </summary>
        /// <param name="request">The request.</param>
        void IRequestHandler.SetRequest(object request)
        {
            Request = (TRequest)request;
        }

        /// <summary>
        /// Gets the response.
        /// </summary>
        /// <returns>The generated response.</returns>
        object IRequestHandler.GetResponse()
        {
            return Response;
        }

        /// <summary>
        /// Executes the business logic to handle the request.
        /// </summary>
        void IRequestHandler.Execute()
        {
            Response = new TResponse();
            OnExecute();
        }

        /// <summary>
        /// The request.
        /// </summary>
        public TRequest Request { get; set; }

        /// <summary>
        /// The response.
        /// </summary>
        public TResponse Response { get; private set; }

        /// <summary>
        /// Executes the business logic to handle the request.
        /// </summary>
        protected abstract void OnExecute();
    }
}