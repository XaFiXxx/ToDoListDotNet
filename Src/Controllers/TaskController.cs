using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("tasks")]
public class TaskController : ControllerBase
{
    private readonly TaskService taskService;
    private readonly TaskValidator taskValidator;

    private readonly TaskMapper taskMapper;

    public TaskController(TaskService taskService, TaskValidator taskValidator, TaskMapper taskMapper)
    {
        this.taskService = taskService;
        this.taskValidator = taskValidator;
        this.taskMapper = taskMapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        List<TaskItem> tasks = await taskService.GetTasks();

        List<TaskResponse> responses = tasks.Select(task => taskMapper.ToResponse(task)).ToList();

        return Ok(responses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ShowTask(int id)
    {
        TaskItem? task = await taskService.ShowTask(id);

        if (task == null)
        {
            return NotFound();
        }

        TaskResponse response = taskMapper.ToResponse(task);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddTask(CreateTaskRequest request)
    {
        if (!taskValidator.IsValidTitle(request.Title))
        {
            return BadRequest("Le titre est obligatoire.");
        }

        TaskItem createdTask = await taskService.AddTask(request);

        TaskResponse response = taskMapper.ToResponse(createdTask);

        return Created($"/tasks/{response.Id}", response);
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
            TaskItem? task = await taskService.ShowTask(id);

            if (task == null)
            {
                return NotFound();
            }

            TaskResponse response = taskMapper.ToResponse(task);

            return Ok(response);
        }
        return NotFound();
    }
}