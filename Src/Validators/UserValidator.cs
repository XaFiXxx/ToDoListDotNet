using ToDoListCSharp.Src.Models;

public class UserValidator
{
    public bool IsValidUsername(string username)
    {
        return !string.IsNullOrWhiteSpace(username);
    }
}