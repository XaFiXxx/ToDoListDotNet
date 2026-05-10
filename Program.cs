using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

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

