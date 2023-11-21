using Microsoft.EntityFrameworkCore;
using TestLatviaProject.Data;
using TestLatviaProject.Interface;
using TestLatviaProject.Models;

namespace TestLatviaProject.Repository
{
    public class TasksRepository : ITasksRepository
    {
        private readonly TestDBContext _context;

        public TasksRepository(TestDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tasks>> GetAllTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<Tasks> GetTaskById(int id)
        {
            return await _context.Tasks.Where(t => t.Id == id).FirstOrDefaultAsync();
        }

        public bool Create(Tasks tasks) 
        { 
            _context.Tasks.Add(tasks);

            return Save();
        }

        public bool Update(Tasks tasks)
        {
            _context.Tasks.Update(tasks);

            return Save();
        }

        public bool Delete(int id)
        {
            var task = _context.Tasks.Where(t => t.Id == id).FirstOrDefault();

            _context.Tasks.Remove(task);

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }
    }
}
