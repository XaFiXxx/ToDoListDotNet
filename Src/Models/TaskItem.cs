namespace ToDoListCSharp.Src.Models;

public class TaskItem
{
    public int Id { get; private set; }
    public string Title { get; private set; } = "";
    public bool IsCompleted { get; private set; }
    public int UserId { get; private set; }
    public User? User { get; private set; }

    public TaskItem(string title, bool isCompleted, int userId)
    {
        Title = title;
        IsCompleted = isCompleted;
        UserId = userId;
    }

    public void Complete(bool IsCompleted)
    {
        this.IsCompleted = IsCompleted;
    }

    public void UpdateTitle(string title)
    {
        Title = title;
    }
}