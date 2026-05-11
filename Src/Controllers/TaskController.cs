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

    [HttpGet("{id}")]
    public async Task<IActionResult> ShowTask(AppDbContext db, int id)
    {
        TaskService taskService = new TaskService(db);

        TaskItem? task = await taskService.ShowTask(id);

        if (task != null)
        {
            return Ok(task);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> AddTask(AppDbContext db, CreateTaskRequest request)
    {
        TaskService taskService = new TaskService(db);

        TaskValidator taskValidator = new TaskValidator();

        if (!taskValidator.IsValidTitle(request.Title))
        {
            return BadRequest("Le titre est obligatoire.");
        }

        TaskItem createdTask = await taskService.AddTask(request);

        return Created($"/tasks/{createdTask.Id}", createdTask);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(AppDbContext db, int id)
    {
        TaskService taskService = new TaskService(db);

        TaskValidator taskValidator = new TaskValidator();

        if (!taskValidator.IsValidId(id))
        {
            return BadRequest("L'ID doit être positif.");
        }

        bool isDeleted = await taskService.DeleteTask(id);

        if (isDeleted)
        {
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(AppDbContext db, int id, UpdateTaskRequest request)
    {
        TaskService taskService = new TaskService(db);

        TaskValidator taskValidator = new TaskValidator();

        if (!taskValidator.IsValidId(id))
        {
            return BadRequest("L'ID doit être positif.");
        }

        if (!taskValidator.IsValidTitle(request.Title))
        {
            return BadRequest("Le titre est obligatoire.");
        }

        bool isUpdated = await taskService.UpdateTask(id, request);

        if (isUpdated)
        {
            return Ok(await taskService.GetTasks());
        }
        else
        {
            return NotFound();
        }
    }
}