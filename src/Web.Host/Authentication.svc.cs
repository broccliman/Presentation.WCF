using System;
using System.Linq;
using System.Transactions;
using Contracts.Authentication;
using Web.Host.Business;
using Web.Host.Infrastructure;
using Web.Host.Wcf;

namespace Web.Host
{
    /// <summary>
    /// The default implementation of <see cref="IAuthenticationService" />.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        /// <summary>
        /// Authenticates a user.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The resposne.</returns>
        public AuthenticateResponse AuthenticateUser(AuthenticateRequest request)
        {
            AuditStack.Push(request);

            try
            {
                var response = new AuthenticateResponse();

                using (var tx = new TransactionScope())
                using (var session = Db.OpenSession())
                {
                    var user = session.Query<User>().FirstOrDefault(x => x.Username == request.Username && x.Password == request.Password);

                    if (user == null)
                    {
                        response.IsAuthenticated = false;
                    }
                    else
                    {
                        response.IsAuthenticated = true;
                        user.NumberOfLogins++;
                        session.Store(user);
                        session.SaveChanges();
                    }

                    tx.Complete();
                }

                AuditStack.Push(response);
                return response;
            }
            catch (Exception ex)
            {
                ElmahLogger.Log(ex);
                throw;
            }
        }

        /// <summary>
        /// Gets all configured users.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The response.</returns>
        [ServiceExtensions(Audit = true, Elmah = true, RequestHandler = true, Transactional = true)]
        public GetUsersResponse GetAllUsers(GetUsersRequest request)
        {
            throw new NotSupportedException("Request should be routed to a request handler.");
        }
    }
}
