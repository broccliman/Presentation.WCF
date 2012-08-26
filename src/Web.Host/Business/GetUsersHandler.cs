using System.Collections.Generic;
using System.Linq;
using Contracts.Authentication;
using Web.Host.Infrastructure;

namespace Web.Host.Business
{
    /// <summary>
    /// The request handler for getting all users.
    /// </summary>
    public class GetUsersHandler : RequestHandler<GetUsersRequest, GetUsersResponse>
    {
        /// <summary>
        /// Executes the business logic to handle the request.
        /// </summary>
        protected override void OnExecute()
        {
            List<UserResponse> users;

            using (var session = Database.OpenSession())
            {
                users = session
                    .Query<User>()
                    .Customize(x => x.WaitForNonStaleResults())
                    .OrderBy(x => x.Username)
                    .Select(x => new UserResponse
                    {
                        Id = x.Id,
                        NumberOfLogins = x.NumberOfLogins,
                        Password = x.Password,
                        Username = x.Username
                    })
                    .ToList();
            }

            Response.Users.AddRange(users);
        }
    }
}