using TestLatviaProject.Models;

namespace TestLatviaProject.Interface
{
    public interface IUserAdminRepository
    {
        Task<IEnumerable<UserAdmin>> GetAllUserAdmins();
        Task<UserAdmin> GetUserAdminById(int id);
        Task<IEnumerable<User>> GetAllUsers();
        Task<IEnumerable<Tasks>> GetAllTasks();
        bool Create(UserAdmin userAdmin);
        bool Update(UserAdmin userAdmin);
        bool Delete(int id);
        bool Save();
    }
}
