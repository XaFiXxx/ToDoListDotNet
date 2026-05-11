using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddControllers();

builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<TaskValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>

   {

       options.SwaggerEndpoint("/openapi/v1.json", "Todo API v1");

   });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();