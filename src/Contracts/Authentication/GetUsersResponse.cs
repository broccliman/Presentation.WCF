using System.Collections.Generic;
using System.Runtime.Serialization;
using Contracts.Core;

namespace Contracts.Authentication
{
    /// <summary>
    /// The response for getting all users.
    /// </summary>
    [DataContract]
    public class GetUsersResponse : IResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUsersResponse" /> class.
        /// </summary>
        public GetUsersResponse()
        {
            Users = new List<UserResponse>();
        }

        /// <summary>
        /// The collection of users.
        /// </summary>
        [DataMember]
        public List<UserResponse> Users { get; set; }
    }

    /// <summary>
    /// The representation of a user.
    /// </summary>
    [DataContract]
    public class UserResponse
    {
        /// <summary>
        /// The id.
        /// </summary>
        [DataMember]
        public string Id { get; set; }

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

        /// <summary>
        /// The number of logins.
        /// </summary>
        [DataMember]
        public int NumberOfLogins { get; set; }
    }
}