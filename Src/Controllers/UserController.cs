using Microsoft.AspNetCore.Mvc;
using ToDoListCSharp.Src.Models;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly UserService userService;

    private readonly UserMapper userMapper;

    public UserController(UserService userService, UserMapper userMapper)
    {
        this.userService = userService;
        this.userMapper = userMapper;
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(CreateUserRequest request)
    {
        User createUser = await userService.AddUser(request);

        UserResponse response = userMapper.ToResponse(createUser);

        return Created($"/users/{response.Id}", response);
    }
}