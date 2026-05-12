using ToDoListCSharp.Src.Models;

public class UserMapper
{
    public UserResponse ToResponse(User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
        };
    }
}