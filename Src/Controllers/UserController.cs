using Microsoft.AspNetCore.Mvc;
using ToDoListCSharp.Src.Models;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly UserService userService;

    private readonly UserMapper userMapper;

    private readonly UserValidator userValidator;

    public UserController(UserService userService, UserMapper userMapper, UserValidator userValidator)
    {
        this.userService = userService;
        this.userMapper = userMapper;
        this.userValidator = userValidator;
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(CreateUserRequest request)
    {
        if (!userValidator.IsValidUsername(request.Username))
        {
            return BadRequest("Le Pseudo est obligatoire.");
        }

        User createUser = await userService.AddUser(request);

        UserResponse response = userMapper.ToResponse(createUser);

        return Created($"/users/{response.Id}", response);
    }
}