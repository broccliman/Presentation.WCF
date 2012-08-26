using Contracts.Core;

namespace Contracts.Authentication
{
    /// <summary>
    /// The request for deleting a user.
    /// </summary>
    public class DeleteUserRequest : IRequest<DeleteUserResponse>
    {
        /// <summary>
        /// The id of the user to delete.
        /// </summary>
        public string Id { get; set; }
    }

    /// <summary>
    /// The response for deleting a user.
    /// </summary>
    public class DeleteUserResponse : IResponse
    {
    }
}