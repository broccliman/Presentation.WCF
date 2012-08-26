using System;
using System.IO;
using System.Web;
using System.Web.Hosting;
using Elmah;

namespace Web.Host.Infrastructure
{
    /// <summary>
    /// The component that log exceptions to elmah.
    /// </summary>
    public static class ElmahLogger
    {
        /// <summary>
        /// Logs the given exception.
        /// </summary>
        /// <param name="ex">The exception.</param>
        public static void Log(Exception ex)
        {
            // http://elegantcode.com/2010/02/05/integrating-elmah-for-a-wcf-service/

            var context = new HttpContext(new SimpleWorkerRequest("", "", new StringWriter()));
            var elmah = ErrorLog.GetDefault(context);
            elmah.Log(new Error(ex));
        }
    }
}