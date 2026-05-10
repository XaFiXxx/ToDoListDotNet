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

app.MapGet("/tasks", (AppDbContext db) =>
{
    TaskController taskController = new TaskController(db);
    return taskController.GetTasks();
});

app.MapPost("/tasks", (AppDbContext db, CreateTaskRequest request) =>
{
    TaskController taskController = new TaskController(db);

    taskController.AddTask(request);

    return taskController.GetTasks();
});

app.MapDelete("/tasks/{id}", (AppDbContext db, int id) =>
{
    TaskController taskController = new TaskController(db);

    taskController.DeleteTask(id);

    return taskController.GetTasks();
});

app.MapPut("/tasks/{id}", (AppDbContext db, int id, UpdateTaskRequest request) =>
{
    TaskController taskController = new TaskController(db);

    taskController.UpdateTask(id, request);

    return taskController.GetTasks();
});

app.Run();

