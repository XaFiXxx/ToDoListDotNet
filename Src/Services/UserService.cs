using Microsoft.EntityFrameworkCore;
using ToDoListCSharp.Src.Models;

public class UserService
{
    private AppDbContext db;

    public UserService(AppDbContext db)
    {
        this.db = db;
    }

    public async Task<User> AddUser(CreateUserRequest request)
    {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        User user = new User(
            request.Username,
            request.Email,
            hashedPassword
        );

        db.Users.Add(user);
        await db.SaveChangesAsync();

        return user;
    }
}