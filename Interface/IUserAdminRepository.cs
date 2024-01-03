using TestLatviaProject.Models;

namespace TestLatviaProject.Interface
{
    public interface IUserAdminRepository
    {
        Task<ICollection<UserAdmin>> GetAllUserAdmins();
        Task<UserAdmin> GetUserAdminById(int id);
        Task<ICollection<User>> GetAllUsers();
        Task<ICollection<Tasks>> GetAllTasks();
        bool Create(UserAdmin userAdmin);
        bool Update(UserAdmin userAdmin);
        bool Delete(int id);
        bool Save();
    }
}
