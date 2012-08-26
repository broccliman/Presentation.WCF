namespace Web.Host.Business
{
    /// <summary>
    /// The representation of a user in the database.
    /// </summary>
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int NumberOfLogins { get; set; }
    }
}