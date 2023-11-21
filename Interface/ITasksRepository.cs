using TestLatviaProject.Models;

namespace TestLatviaProject.Interface
{
    public interface ITasksRepository
    {
        Task<IEnumerable<Tasks>> GetAllTasks();
        Task<Tasks> GetTaskById(int id);
        bool Create(Tasks tasks);
        bool Update(Tasks tasks);
        bool Delete(int id);
        bool Save();
    }
}
