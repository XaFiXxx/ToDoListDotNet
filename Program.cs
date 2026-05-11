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

app.MapGet("/tasks", async (AppDbContext db) =>
{
    TaskController taskController = new TaskController(db);
    return Results.Ok(await taskController.GetTasks());
});

app.MapGet("/task/{id}", async (AppDbContext db, int id) =>
{
    TaskController taskController = new TaskController(db);

    TaskItem? task = await taskController.ShowTask(id);

    if (task != null)
    {
        return Results.Ok(task);
    }
    return Results.NotFound();
});

app.MapPost("/tasks", async (AppDbContext db, CreateTaskRequest request) =>
{
    TaskController taskController = new TaskController(db);

    TaskValidator taskValidator = new TaskValidator();

    if (!taskValidator.IsValidTitle(request.Title))
    {
        return Results.BadRequest("Le titre est obligatoire.");
    }

    TaskItem createdTask = await taskController.AddTask(request);

    return Results.Created($"/tasks/{createdTask.Id}", createdTask);
});

app.MapDelete("/tasks/{id}", async (AppDbContext db, int id) =>
{
    TaskController taskController = new TaskController(db);

    TaskValidator taskValidator = new TaskValidator();

    if (!taskValidator.IsValidId(id))
    {
        return Results.BadRequest("L'ID doit être positif.");
    }

    bool isDeleted = await taskController.DeleteTask(id);

    if (isDeleted)
    {
        return Results.NoContent();
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapPut("/tasks/{id}", async (AppDbContext db, int id, UpdateTaskRequest request) =>
{
    TaskController taskController = new TaskController(db);

    TaskValidator taskValidator = new TaskValidator();

    if (!taskValidator.IsValidId(id))
    {
        return Results.BadRequest("L'ID doit être positif.");
    }

    if (!taskValidator.IsValidTitle(request.Title))
    {
        return Results.BadRequest("Le titre est obligatoire.");
    }

    bool isUpdated = await taskController.UpdateTask(id, request);

    if (isUpdated)
    {
        return Results.Ok(await taskController.GetTasks());
    }
    else
    {
        return Results.NotFound();
    }
});

app.Run();