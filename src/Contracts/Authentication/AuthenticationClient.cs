using Contracts.Utility;

namespace Contracts.Authentication
{
    /// <summary>
    /// The service client for <see cref="IAuthenticationService" />.
    /// </summary>
    public class AuthenticationClient : ServiceAdapter<IAuthenticationService>
    {
    }
}