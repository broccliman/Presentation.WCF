using System.Web.Mvc;
using Web.Host.Infrastructure;

namespace Web.Host.Controllers
{
    /// <summary>
    /// The audit controller.
    /// </summary>
    public class AuditController : Controller
    {
        /// <summary>
        /// The index action.
        /// </summary>
        /// <returns>The index view.</returns>
        public ActionResult Index()
        {
            // View all audits in the view
            return View(AuditStack.GetAll());
        }

        /// <summary>
        /// The clear action.
        /// </summary>
        /// <returns>The redirect to index result.</returns>
        public ActionResult Clear()
        {
            // Clear any existing audits
            AuditStack.Clear();

            // Redirect to the index view
            return RedirectToAction("Index");
        }
    }
}
