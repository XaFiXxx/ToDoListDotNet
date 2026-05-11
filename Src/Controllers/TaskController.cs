using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("tasks")]
public class TaskController : ControllerBase
{
    private readonly TaskService taskService;
    private readonly TaskValidator taskValidator;

    public TaskController(TaskService taskService, TaskValidator taskValidator)
    {
        this.taskService = taskService;
        this.taskValidator = taskValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        return Ok(await taskService.GetTasks());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ShowTask(int id)
    {
        TaskItem? task = await taskService.ShowTask(id);

        if (task != null)
        {
            return Ok(task);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> AddTask(CreateTaskRequest request)
    {
        if (!taskValidator.IsValidTitle(request.Title))
        {
            return BadRequest("Le titre est obligatoire.");
        }

        TaskItem createdTask = await taskService.AddTask(request);

        return Created($"/tasks/{createdTask.Id}", createdTask);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
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
    public async Task<IActionResult> UpdateTask(int id, UpdateTaskRequest request)
    {
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