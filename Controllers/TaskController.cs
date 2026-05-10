class TaskController
{

    private AppDbContext db;

    public TaskController(AppDbContext db)
    {
        this.db = db;
    }

    public void AddTask(CreateTaskRequest request)
    {
        db.Tasks.Add(new TaskItem(
            db.Tasks.Count() + 1,
            request.Title,
            false
        ));

        db.SaveChanges();
    }

    public void DeleteTask(int id)
    {
        foreach (TaskItem task in db.Tasks)
        {
            if (task.Id == id)
            {
                db.Tasks.Remove(task);
                break;
            }
        }

        db.SaveChanges();
    }

    public void UpdateTask(int id, UpdateTaskRequest request)
    {
        foreach (TaskItem task in db.Tasks)
        {
            if (task.Id == id)
            {
                task.UpdateTitle(request.Title);
                task.Complete(request.IsCompleted);
                break;
            }
        }
        db.SaveChanges();
    }

    public List<TaskItem> GetTasks()
    {
        return db.Tasks.ToList();
    }
}