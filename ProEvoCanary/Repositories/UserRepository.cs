using ProEvoCanary.Helpers;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories.Interfaces;

namespace ProEvoCanary.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDBHelper _dbHelper;

        public UserRepository(IDBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public IUser GetUser(string username)
        {
            _dbHelper.AddParameter("@Username", username);
            IUser userModel = null;
            using (var reader = _dbHelper.ExecuteReader("sp_GetLoginDetails"))
            {
                while (reader.Read())
                {
                    switch ((int)reader["UserType"])
                    {
                        case (int)UserType.Admin:
                            userModel = new AdminModel((int)reader["UserId"],
                                reader["Forename"].ToString(),
                                reader["Surname"].ToString(),
                                reader["Username"].ToString());
                            break;
                        case (int)UserType.Standard:
                            userModel = new UserModel((int)reader["UserId"],
                                reader["Forename"].ToString(),
                                reader["Surname"].ToString(),
                                reader["Username"].ToString());
                            break;
                    }
                }

            }

            return userModel;
        }



    }
}