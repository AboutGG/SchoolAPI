﻿using SchoolAPI.Models;

namespace SchoolAPI.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUserById(Guid id);
        
        /// <summary> UserExists check using userId </summary>
        /// <param name="userId"></param>
        /// <returns>true if exist , false if not</returns>
        bool UserExists(Guid userId);

        /// <summary> UserExists check using username </summary>
        /// <param name="username"></param>
        /// <returns>true if exist , false if not</returns>
        bool UserExists(string username);

        /// <summary> Create a user </summary>
        /// <param name="user"></param>
        /// <returns>true successful, false not successful</returns>
        bool CreateUser(User user);

        bool DeleteUser (User user);
        bool Save();

        /// <summary> UpdateUser </summary>
        /// <param name="user"></param>
        /// <returns>if the user is updated</returns>
        bool UpdateUser(User user);

        bool CreateEntity (User entity);

    }
}
