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
}