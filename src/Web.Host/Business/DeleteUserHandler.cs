//using System;
//using System.Linq;
//using Contracts.Authentication;
//using Web.Host.Infrastructure;
//
//namespace Web.Host.Business
//{
//    /// <summary>
//    /// The handler to delete a user.
//    /// </summary>
//    public class DeleteUserHandler : RequestHandler<DeleteUserRequest, DeleteUserResponse>
//    {
//        /// <summary>
//        /// Executes the business logic to handle the request.
//        /// </summary>
//        protected override void OnExecute()
//        {
//            User user;
//
//            // Delete a user with the specified id
//            using (var session = Db.OpenSession())
//            {
//                user = session.Query<User>().Single(x => x.Id == Request.Id);
//                session.Delete(user);
//                session.SaveChanges();
//            }
//
//            // Do not allow the admin account to be deleted
//            if (user.Username == "admin")
//            {
//                throw new Exception("Admin user account cannot be deleted.");
//            }
//        }
//    }
//}