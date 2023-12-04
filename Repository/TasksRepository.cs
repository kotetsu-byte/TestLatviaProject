using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TestLatviaProject.Data;
using TestLatviaProject.Interface;
using TestLatviaProject.Models;

namespace TestLatviaProject.Repository
{
    public class TasksRepository : ITasksRepository
    {
        private readonly TestDBContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContext;

        public TasksRepository(TestDBContext context, UserManager<User> userManager, IHttpContextAccessor httpContext)
        {
            _context = context;
            _userManager = userManager;
            _httpContext = httpContext;
        }

        public async Task<IEnumerable<Tasks>> GetAllTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<Tasks> GetTaskById(int id)
        {
            return await _context.Tasks.Where(t => t.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> Create(Tasks tasks) 
        { 
            _context.Tasks.Add(tasks);
            _context.Entry(tasks).State = EntityState.Added;
            return await Save();
        }

        public async Task<bool> Update(Tasks tasks)
        {
            _context.Tasks.Update(tasks);
            _context.Entry(tasks).State = EntityState.Modified;
            return await Save();
        }

        public async Task<bool> Delete(int id)
        {
            var task = _context.Tasks.Where(t => t.Id == id).FirstOrDefault();

            _context.Tasks.Remove(task);
            _context.Entry(task).State = EntityState.Deleted;
            return await Save();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync(_userManager.GetUserName(_httpContext.HttpContext.User));
            return saved > 0 ? true : false;
        }
    }
}
