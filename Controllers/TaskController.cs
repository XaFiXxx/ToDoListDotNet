class TaskController
{

    private AppDbContext db;

    public TaskController(AppDbContext db)
    {
        this.db = db;
    }

    public TaskItem AddTask(CreateTaskRequest request)
    {
        TaskItem task = new TaskItem(
            request.Title,
            false
        );

        db.Tasks.Add(task);
        db.SaveChanges();

        return task;
    }

    public bool DeleteTask(int id)
    {
        foreach (TaskItem task in db.Tasks.ToList())
        {
            if (task.Id == id)
            {
                db.Tasks.Remove(task);
                db.SaveChanges();
                return true;
            }
        }
        return false;
    }

    public bool UpdateTask(int id, UpdateTaskRequest request)
    {
        foreach (TaskItem task in db.Tasks.ToList())
        {
            if (task.Id == id)
            {
                task.UpdateTitle(request.Title);
                task.Complete(request.IsCompleted);
                db.SaveChanges();
                return true;
            }
        }
        return false;
    }

    public List<TaskItem> GetTasks()
    {
        return db.Tasks.ToList();
    }
}