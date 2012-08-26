namespace Contracts.Core
{
    /// <summary>
    /// Defines the contract for a request object.
    /// </summary>
    /// <typeparam name="TResponse">The type of response for the request.</typeparam>
    public interface IRequest<TResponse>
        where TResponse : IResponse
    {
    }
}