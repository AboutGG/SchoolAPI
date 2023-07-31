using SchoolAPI.Interfaces;
using SchoolAPI.Models;

namespace SchoolAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        #region Attributes
        private readonly SchoolContext _context;
        #endregion

        #region Costructor
        public UserRepository(SchoolContext context) 
        { 
            this._context = context;
        }
        #endregion

        #region Interfaces

        
        /// <summary> Get all the users </summary>
        public ICollection<User> GetUsers() 
        { 
            return _context.Users.OrderBy(u => u.Id).ToList();
        }

        /// <summary> Get one user having the id </summary>
        public User GetUserById(Guid id)
        {
            return this._context.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        /// <summary> Get all having the username </summary>
        //public ICollection<User> GetUsersByUsername(string username)
        //{
        //    return this._context.Users.Where(u => u.Username == username).ToList();
        //}

        /// <summary> check if id exist </summary>
        public bool UserExists(Guid id)
        {
            return this._context.Users.Any(u => u.Id == id);
        }

        public bool UserExists(string username)
        {
            return this._context.Users.Any(u => u.Username.Trim().ToLower() == username.Trim().ToLower());
        }
        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        //save the changes on db
        public bool Save()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }

        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }

        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }

        public bool CreateEntity(User entity)
        {
            _context.Add(entity);
            return Save();
        }


        #endregion

    }
}
