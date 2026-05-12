using ToDoListCSharp.Src.Models;

public class TaskMapper
{
    public TaskResponse ToResponse(TaskItem task)
    {
        return new TaskResponse
        {
            Id = task.Id,
            Title = task.Title,
            IsCompleted = task.IsCompleted
        };
    }

    public List<TaskResponse> ToResponseList(List<TaskItem> tasks)
    {
        return tasks.Select(task => ToResponse(task)).ToList();
    }
}