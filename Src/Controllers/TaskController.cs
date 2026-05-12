using Microsoft.AspNetCore.Mvc;
using ToDoListCSharp.Src.Models;

[ApiController]
[Route("tasks")]
public class TaskController : ControllerBase
{
    private readonly TaskService taskService;

    private readonly TaskMapper taskMapper;

    public TaskController(TaskService taskService, TaskMapper taskMapper)
    {
        this.taskService = taskService;
        this.taskMapper = taskMapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        List<TaskItem> tasks = await taskService.GetTasks();

        List<TaskResponse> response = taskMapper.ToResponseList(tasks);

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ShowTask([FromRoute] int id)
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
        TaskItem createdTask = await taskService.AddTask(request);

        TaskResponse response = taskMapper.ToResponse(createdTask);

        return Created($"/tasks/{response.Id}", response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTask([FromRoute] int id)
    {
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

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTask([FromRoute] int id, UpdateTaskRequest request)
    {
        TaskItem? updatedTask = await taskService.UpdateTask(id, request);

        if (updatedTask == null)
        {
            return NotFound();
        }

        TaskResponse response = taskMapper.ToResponse(updatedTask);

        return Ok(response);
    }
}