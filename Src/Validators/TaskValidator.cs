public class TaskValidator
{
    public bool IsValidId(int id)
    {
        return id > 0;
    }

    public bool IsValidTitle(string title)
    {
        return !string.IsNullOrWhiteSpace(title);
    }
}