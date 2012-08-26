using System.Runtime.Serialization;
using Contracts.Core;

namespace Contracts.Authentication
{
    /// <summary>
    /// The request for authenticating a user.
    /// </summary>
    [DataContract]
    public class AuthenticateRequest : IRequest<AuthenticateResponse>
    {
        /// <summary>
        /// The username.
        /// </summary>
        [DataMember]
        public string Username { get; set; }

        /// <summary>
        /// The password.
        /// </summary>
        [DataMember]
        public string Password { get; set; }
    }
}