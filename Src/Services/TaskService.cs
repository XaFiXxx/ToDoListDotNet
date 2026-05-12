using Microsoft.EntityFrameworkCore;
using ToDoListCSharp.Src.Models;

public class TaskService
{
    private AppDbContext db;
    private readonly ICurrentUserService _currentUserService;

    public TaskService(AppDbContext db, ICurrentUserService currentUserService)
    {
        this.db = db;
        _currentUserService = currentUserService;
    }

    public async Task<TaskItem> AddTask(CreateTaskRequest request)
    {
        TaskItem task = new TaskItem(
            request.Title,
            false,
            _currentUserService.UserId
        );

        db.Tasks.Add(task);

        await db.SaveChangesAsync();

        return task;
    }

    public async Task<bool> DeleteTask(int id)
    {
        TaskItem? task = await db.Tasks.FirstOrDefaultAsync(task =>
            task.Id == id &&
            task.UserId == _currentUserService.UserId
        );

        if (task == null)
        {
            return false;
        }

        db.Tasks.Remove(task);

        await db.SaveChangesAsync();

        return true;
    }

    public async Task<TaskItem?> UpdateTask(int id, UpdateTaskRequest request)
    {
        TaskItem? task = await db.Tasks.FirstOrDefaultAsync(task =>
            task.Id == id &&
            task.UserId == _currentUserService.UserId
        );

        if (task == null)
        {
            return null;
        }

        task.UpdateTitle(request.Title);
        task.Complete(request.IsCompleted);

        await db.SaveChangesAsync();

        return task;
    }

    public async Task<List<TaskItem>> GetTasks()
    {
        return await db.Tasks
            .Where(task => task.UserId == _currentUserService.UserId)
            .ToListAsync();
    }

    public async Task<TaskItem?> ShowTask(int id)
    {
        return await db.Tasks.FirstOrDefaultAsync(task =>
            task.Id == id &&
            task.UserId == _currentUserService.UserId
        );
    }
}