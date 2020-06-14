using System;
using System.Collections.Generic;
using ProEvoCanary.DataAccess.Models;
using ProEvoCanary.DataAccess.Repositories.Interfaces;

namespace ProEvoCanary.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbHelper _dbHelper;

        public UserRepository(IDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public UserModel GetUser(string username)
        {
            UserModel userModel = null;

            using (var reader = _dbHelper.GetItems("up_GetLoginDetails",username, new { Username =username}))
            {
                while (reader.Read())
                {
                    userModel = new UserModel((int)reader["UserId"],
                        reader["Forename"].ToString(),
                        reader["Surname"].ToString(),
                        reader["Username"].ToString(),
                        (int)reader["UserType"]);
                }
            }

            return userModel;
        }

        public List<UserModel> GetUsers()
        {
            var userModel = new List<UserModel>();
            using (var reader = _dbHelper.GetItems("up_GetLoginDetails", "AllUsers"))
            {
                while (reader.Read())
                {
                    userModel.Add(new UserModel
                    {
                        UserId =
                            (int)reader["UserId"],
                        Forename =
                            reader["Forename"].ToString(),
                        Surname =
                            reader["Surname"].ToString()
                    });
                }
            }
            return userModel;
        }

        public Guid CreateUser(Guid id,string userName, string forename, string surname, string emailAddress)
        {
            return _dbHelper.InsertItem("up_AddUser", id, new { Username = userName, Forename = forename, Surname = surname, Email = emailAddress });
        }
    }
}