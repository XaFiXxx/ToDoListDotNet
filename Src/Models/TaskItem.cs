public class TaskItem
{
    public int Id { get; private set; }
    public string Title { get; private set; } = "";
    public bool IsCompleted { get; private set; }

    public TaskItem(string title, bool isCompleted)
    {
        Title = title;
        IsCompleted = isCompleted;
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