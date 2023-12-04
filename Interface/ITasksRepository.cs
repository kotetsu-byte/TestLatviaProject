using TestLatviaProject.Models;

namespace TestLatviaProject.Interface
{
    public interface ITasksRepository
    {
        Task<IEnumerable<Tasks>> GetAllTasks();
        Task<Tasks> GetTaskById(int id);
        Task<bool> Create(Tasks tasks);
        Task<bool> Update(Tasks tasks);
        Task<bool> Delete(int id);
        Task<bool> Save();
    }
}
