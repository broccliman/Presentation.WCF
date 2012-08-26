using System.ServiceModel;

namespace Contracts.Authentication
{
    /// <summary>
    /// Defines the contract for a wcf service around authentication scenarios.
    /// </summary>
    [ServiceContract]
    public interface IAuthenticationService
    {
        /// <summary>
        /// Authenticates a user.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The resposne.</returns>
        [OperationContract]
        AuthenticateResponse AuthenticateUser(AuthenticateRequest request);

        /// <summary>
        /// Gets all configured users.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The response.</returns>
        [OperationContract]
        GetUsersResponse GetAllUsers(GetUsersRequest request);
    }
}
