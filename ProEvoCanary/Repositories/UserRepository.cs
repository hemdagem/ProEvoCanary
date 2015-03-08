using System;
using System.Collections.Generic;
using ProEvoCanary.Helpers;
using ProEvoCanary.Helpers.Interfaces;
using ProEvoCanary.Models;
using ProEvoCanary.Repositories.Interfaces;
using IUser = ProEvoCanary.Models.Interfaces.IUser;

namespace ProEvoCanary.Repositories
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

        public IUser GetUser(string username)
        {
            IUser userModel = null;

            var parameters = new Dictionary<string, IConvertible> { { "@Username", username } };

            using (var reader = _dbHelper.ExecuteReader("sp_GetLoginDetails", parameters))
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
                                reader["Username"].ToString(),
                                (int)reader["UserType"]);
                            break;
                    }
                }

            }

            return userModel;
        }

        public List<IUser> GetUsers()
        {
            var userModel = new List<IUser>();
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