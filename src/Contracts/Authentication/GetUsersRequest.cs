using System.Runtime.Serialization;
using Contracts.Core;

namespace Contracts.Authentication
{
    /// <summary>
    /// The request for getting all users.
    /// </summary>
    [DataContract]
    public class GetUsersRequest : IRequest<GetUsersResponse>
    {
    }
}