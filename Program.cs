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
    return Results.Ok(taskController.GetTasks());
});

app.MapPost("/tasks", (AppDbContext db, CreateTaskRequest request) =>
{
    TaskController taskController = new TaskController(db);

    if (string.IsNullOrWhiteSpace(request.Title))
    {
        return Results.BadRequest("Le titre est obligatoire.");
    }

    TaskItem createdTask = taskController.AddTask(request);

    return Results.Created($"/tasks/{createdTask.Id}", createdTask);
});

app.MapDelete("/tasks/{id}", (AppDbContext db, int id) =>
{
    TaskController taskController = new TaskController(db);

    if (id <= 0)
    {
        return Results.BadRequest("L'ID ne peut pas être négatif.");
    }

    bool isDeleted = taskController.DeleteTask(id);

    if (isDeleted)
    {
        return Results.NoContent();
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapPut("/tasks/{id}", (AppDbContext db, int id, UpdateTaskRequest request) =>
{
    TaskController taskController = new TaskController(db);

    if (id <= 0)
    {
        return Results.BadRequest("L'ID ne peut pas être négatif.");
    }
    else if (string.IsNullOrWhiteSpace(request.Title))
    {
        return Results.BadRequest("Le titre est obligatoire.");
    }

    bool isUpdated = taskController.UpdateTask(id, request);

    if (isUpdated)
    {
        return Results.Ok(taskController.GetTasks());
    }
    else
    {
        return Results.NotFound();
    }
});

app.Run();

