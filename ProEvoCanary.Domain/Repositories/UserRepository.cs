using System;
using System.Collections.Generic;
using ProEvoCanary.Domain.Helpers;
using ProEvoCanary.Domain.Helpers.Interfaces;
using ProEvoCanary.Domain.Models;
using ProEvoCanary.Domain.Repositories.Interfaces;

namespace ProEvoCanary.Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDBHelper _dbHelper;
        private readonly IPasswordHash _passwordHash;

        public UserRepository(IDBHelper dbHelper, IPasswordHash passwordHash)
        {
            _dbHelper = dbHelper;
            _passwordHash = passwordHash;
        }

        public UserModel GetUser(string username)
        {
            UserModel userModel = null;

            var parameters = new Dictionary<string, IConvertible> { { "@Username", username } };

            using (var reader = _dbHelper.ExecuteReader("sp_GetLoginDetails", parameters))
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
            using (var reader = _dbHelper.ExecuteReader("sp_GetLoginDetails"))
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

        public int CreateUser(string userName, string forename, string surname, string emailAddress, string password)
        {
            if (String.IsNullOrEmpty(userName))
            {
                throw new NullReferenceException("Username cannot be empty");
            }
            if (String.IsNullOrEmpty(forename))
            {
                throw new NullReferenceException("Forename cannot be empty");
            }
            if (String.IsNullOrEmpty(surname))
            {
                throw new NullReferenceException("Surname cannot be empty");
            }
            if (String.IsNullOrEmpty(emailAddress))
            {
                throw new NullReferenceException("Email Address cannot be empty");
            }
            if (String.IsNullOrEmpty(password))
            {
                throw new NullReferenceException("Password cannot be empty");
            }

            var parameters = new Dictionary<string, IConvertible>
            {
                { "@Username", userName },
                { "@Forename", forename },
                { "@Surname", surname },
                { "@Email", emailAddress },
                { "@Password", _passwordHash.CreateHash(password) }
            };

            return _dbHelper.ExecuteScalar("sp_AddNewUser", parameters);
        }


        public UserModel Login(LoginModel loginModel)
        {
            if (String.IsNullOrEmpty(loginModel.Username) || String.IsNullOrEmpty(loginModel.Password))
            {
                throw new NullReferenceException("Username or Password is empty");
            }

            var parameters = new Dictionary<string, IConvertible>
            {
                {"@Username", loginModel.Username}
            };

            UserModel model = null;
            using (var reader = _dbHelper.ExecuteReader("sp_GetLoginDetails", parameters))
            {
                while (reader.Read())
                {
                    var hash = reader["Hash"].ToString();

                    if (_passwordHash.ValidatePassword(loginModel.Password, hash))
                    {
                        model = new UserModel((int)reader["Id"], reader["Forename"].ToString(), reader["Surname"].ToString(), loginModel.Username, (int)reader["UserType"]);
                    }
                }
            }

            return model;
        }
    }
}