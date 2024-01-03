using Microsoft.EntityFrameworkCore;
using TestLatviaProject.Data;
using TestLatviaProject.Interface;
using TestLatviaProject.Models;

namespace TestLatviaProject.Repository
{
    public class UserAdminRepository : IUserAdminRepository
    {
        private readonly TestDBContext _context;

        public UserAdminRepository(TestDBContext context)
        {
            _context = context;
        }

        public async Task<ICollection<UserAdmin>> GetAllUserAdmins()
        {
            return await _context.UserAdmins.ToListAsync();
        }

        public async Task<UserAdmin> GetUserAdminById(int id)
        {
            return await _context.UserAdmins.Where(ua => ua.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<ICollection<Tasks>> GetAllTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        public bool Create(UserAdmin userAdmin)
        {
            _context.UserAdmins.Add(userAdmin);

            return Save();
        }

        public bool Update(UserAdmin userAdmin) 
        {
            _context.UserAdmins.Update(userAdmin);

            return Save();
        }

        public bool Delete(int id) 
        {
            var userAdmin = _context.UserAdmins.Where(ua => ua.Id == id).FirstOrDefault();

            _context.UserAdmins.Remove(userAdmin);

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }
    }
}
