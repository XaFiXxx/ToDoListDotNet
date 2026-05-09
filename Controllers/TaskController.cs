class TaskController
{
    private List<TaskItem> tasks = new List<TaskItem>();

    public TaskController()
    {
        tasks.Add(new TaskItem(1, "Tâche 1", false));
        tasks.Add(new TaskItem(2, "Tâche 2", false));
        tasks.Add(new TaskItem(3, "Tâche 3", true));
    }

    public void AddTask(CreateTaskRequest request)
    {
        tasks.Add(new TaskItem(
            tasks.Count + 1,
            request.Title,
            false
        ));
    }

    public void DeleteTask(int id)
    {
        foreach (TaskItem task in tasks)
        {
            if (task.Id == id)
            {
                tasks.Remove(task);
                break;
            }
        }
    }

    public void UpdateTask(int id, UpdateTaskRequest request)
    {
        foreach (TaskItem task in tasks)
        {
            if (task.Id == id)
            {
                task.UpdateTitle(request.Title);
                task.Complete(request.IsCompleted);
                break;
            }
        }
    }

    public List<TaskItem> GetTasks()
    {
        return tasks;
    }
}