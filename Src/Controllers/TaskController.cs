using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("tasks")]
public class TaskController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTasks(AppDbContext db)
    {
        TaskService taskService = new TaskService(db);

        return Ok(await taskService.GetTasks());
    }
}