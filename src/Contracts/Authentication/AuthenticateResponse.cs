using System.Runtime.Serialization;
using Contracts.Core;

namespace Contracts.Authentication
{
    /// <summary>
    /// The response for authenticating a user.
    /// </summary>
    [DataContract]
    public class AuthenticateResponse : IResponse
    {
        /// <summary>
        /// Indicates that the credentials are valid.
        /// </summary>
        [DataMember]
        public bool IsAuthenticated { get; set; }
    }
}