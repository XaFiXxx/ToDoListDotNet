var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Liste des tâches
TaskController taskController = new TaskController();

app.MapGet("/tasks", () =>
{
    return taskController.GetTasks();
}
);

app.MapPost("/tasks", (CreateTaskRequest request) =>
{
    taskController.AddTask(request);

    return taskController.GetTasks();
});

app.MapDelete("/tasks/{id}", (int id) =>
{
    taskController.DeleteTask(id);

    return taskController.GetTasks();
});

app.MapPut("/tasks/{id}", (int id, UpdateTaskRequest request) =>
{
    taskController.UpdateTask(id, request);

    return taskController.GetTasks();
});

app.Run();

