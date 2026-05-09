class TaskItem
{
    public int Id { get; private set; }
    public string Title { get; private set; } = "";
    public bool IsCompleted { get; private set; }

    public TaskItem(int id, string title, bool isCompleted)
    {
        Id = id;
        Title = title;
        IsCompleted = isCompleted;
    }

    public void Complete(bool IsCompleted)
    {
        IsCompleted = true;
    }

    public void UpdateTitle(string title)
    {
        Title = title;
    }
}

class CreateTaskRequest

{
    public string Title { get; set; } = "";
}

class UpdateTaskRequest
{
    public string Title { get; set; } = "";
    public bool IsCompleted { get; set; }
}