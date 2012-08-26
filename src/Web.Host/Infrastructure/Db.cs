using Raven.Client;
using Raven.Client.Embedded;
using Web.Host.Business;

namespace Web.Host.Infrastructure
{
    /// <summary>
    /// Used to demo a sample database session.
    /// </summary>
    public static class Db
    {
        /// <summary>
        /// The document store.
        /// </summary>
        private static readonly EmbeddableDocumentStore DocumentStore = new EmbeddableDocumentStore();

        /// <summary>
        /// Initializes the sample data set.
        /// </summary>
        public static void Initialize()
        {
            DocumentStore.RunInMemory = true;
            DocumentStore.Initialize();

            // Build sample data set
            using (var session = DocumentStore.OpenSession())
            {
                session.Store(new User { Username = "admin", Password = "admin" });
                session.Store(new User { Username = "jersey_admin", Password = "gym_tan_laundry" });
                session.Store(new User { Username = "foo", Password = "bar" });
                session.SaveChanges();
            }
        }

        /// <summary>
        /// Opens a session to the database.
        /// </summary>
        /// <returns>The session.</returns>
        public static IDocumentSession OpenSession()
        {
            return DocumentStore.OpenSession();
        }
    }
}