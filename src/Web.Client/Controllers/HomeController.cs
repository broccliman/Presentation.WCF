using System;
using System.Web.Mvc;
using Contracts.Authentication;
using Contracts.Portal;
using System.Linq;

namespace Web.Client.Controllers
{
    /// <summary>
    /// The home controller.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// The index action.
        /// </summary>
        /// <returns>The index view.</returns>
        public ActionResult Index()
        {
            // Pull any data from a redirect into model state
            SyncTempDataWithModelState();

            // Get the collection of users in the database
            var response = AuthenticationClient.Execute(x => x.GetAllUsers(new GetUsersRequest()));

            // Return the index view with the users
            return View(response.Users);
        }

        /// <summary>
        /// The delete user action.
        /// </summary>
        /// <param name="id">The id of the user to delete.</param>
        /// <returns>Redirect to index view result.</returns>
        public ActionResult DeleteUser(string id)
        {
            try
            {
                PortalClient.Execute(new DeleteUserRequest { Id = id });
            }
            catch (Exception ex)
            {
                TempData.Add("Exception", ex.Message);
            }

            // Redirect to the index view
            return RedirectToAction("Index");
        }

        /// <summary>
        /// The login action.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>Redirect to index view result.</returns>
        public ActionResult Login(string username, string password)
        {
            // Build the request
            var request = new AuthenticateRequest
            {
                Username = username,
                Password = password
            };

            // Call the service and get a response
            var response = AuthenticationClient.Execute(x => x.AuthenticateUser(request));

            // If not authenticated, make a note for model state
            if (!response.IsAuthenticated)
            {
                TempData.Add("Authentication", "The username/password did not match any user.");
            }

            // Redirect to the index view
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Syncs data in the temp data collection with model state.
        /// </summary>
        private void SyncTempDataWithModelState()
        {
            if (!TempData.Any())
                return;

            foreach (var entry in TempData)
            {
                ModelState.AddModelError(entry.Key, entry.Value.ToString());
            }

            TempData.Clear();
        }
    }
}
