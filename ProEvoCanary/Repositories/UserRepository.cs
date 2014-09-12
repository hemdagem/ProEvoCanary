using ProEvoCanary.Helpers;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories.Interfaces;
using IUser = ProEvoCanary.Models.Interfaces.IUser;

namespace ProEvoCanary.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IdBHelper _dbHelper;

        public UserRepository(IdBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public UserRepository() : this(new DBHelper()) { }

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


        public int CreateUser(string userName, string forename, string surname, string emailAddress)
        {
            _dbHelper.AddParameter("@Username", userName);
            _dbHelper.AddParameter("@Forename", forename);
            _dbHelper.AddParameter("@Surname", surname);
            _dbHelper.AddParameter("@Email", emailAddress);

            return  _dbHelper.ExecuteScalar("sp_AddNewUser");
        }
    }
}