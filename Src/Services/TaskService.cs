using Microsoft.EntityFrameworkCore;
using ToDoListCSharp.Src.Models;

public class TaskService
{

    private AppDbContext db;

    public TaskService(AppDbContext db)
    {
        this.db = db;
    }

    public async Task<TaskItem> AddTask(CreateTaskRequest request)
    {
        TaskItem task = new TaskItem(
            request.Title,
            false,
            request.UserId
        );

        db.Tasks.Add(task);
        await db.SaveChangesAsync();

        return task;
    }

    public async Task<bool> DeleteTask(int id)
    {
        TaskItem? task = await db.Tasks.FirstOrDefaultAsync(task => task.Id == id);

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
        TaskItem? task = await db.Tasks.FirstOrDefaultAsync(task => task.Id == id);

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
        return await db.Tasks.ToListAsync();
    }

    public async Task<TaskItem?> ShowTask(int id)
    {
        return await db.Tasks.FirstOrDefaultAsync(task => task.Id == id);
    }
}