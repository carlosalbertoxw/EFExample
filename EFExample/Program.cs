using EFExample;
using EFExample.ModelsBE;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<TasksContext>(p => p.UseInMemoryDatabase("TasksDB"));
builder.Services.AddSqlServer<TasksContext>(builder.Configuration.GetConnectionString("TaskDBConnection"));


// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/tasks", async ([FromServices] TasksContext dbContext) =>
{
    Dictionary<String, Object> result = new Dictionary<String, Object>();
    try
    {
        result.Add("status", true);
        result.Add("result", dbContext.TaskBE.Include(p => p.Category));
        return Results.Ok(result);
    }
    catch(Exception ex)
    {
        result.Add("status", false);
        result.Add("result", ex.Message);
        return Results.BadRequest(result);
    }
});

app.MapPost("/api/task", async ([FromServices] TasksContext dbContext, [FromBody] TaskBE task) =>
{
    Dictionary<String, Object> result = new Dictionary<String, Object>();
    try
    {
        task.TaskId = Guid.NewGuid();
        task.CreationDate = DateTime.Now;

        await dbContext.TaskBE.AddAsync(task);

        await dbContext.SaveChangesAsync();

        result.Add("status", true);
        result.Add("result", "Ok");
        return Results.Ok(result);
    }
    catch(Exception ex)
    {
        result.Add("status", false);
        result.Add("result", ex.Message);
        return Results.BadRequest(result);
    }
});

app.MapPut("/api/task/{id}", async ([FromServices] TasksContext dbContext, [FromBody] TaskBE task, [FromRoute] Guid id) =>
{
    Dictionary<String, Object> result = new Dictionary<String, Object>();
    try
    {
        var t = dbContext.TaskBE.Find(id);

        if (t != null)
        {
            t.CategoryId = task.CategoryId;
            t.Title = task.Title;
            t.PriorityTask = task.PriorityTask;
            t.Description = task.Description;

            await dbContext.SaveChangesAsync();

            result.Add("status", true);
            result.Add("result", "Ok");
            return Results.Ok(result);
        }
        result.Add("status", false);
        result.Add("result", "Not Found");
        return Results.NotFound(result);
    }
    catch (Exception ex)
    {
        result.Add("status", false);
        result.Add("result", ex.Message);
        return Results.BadRequest(result);
    }
});

app.MapDelete("/api/task/{id}", async ([FromServices] TasksContext dbContext, [FromRoute] Guid id) =>
{
    Dictionary<String, Object> result = new Dictionary<String, Object>();
    try
    {
        var t = dbContext.TaskBE.Find(id);

        if (t != null)
        {
            dbContext.Remove(t);

            await dbContext.SaveChangesAsync();

            result.Add("status", true);
            result.Add("result", "Ok");
            return Results.Ok(result);
        }
        result.Add("status", false);
        result.Add("result", "Not Found");
        return Results.NotFound(result);
    }
    catch (Exception ex)
    {
        result.Add("status", false);
        result.Add("result", ex.Message);
        return Results.BadRequest(result);
    }
});

app.Run();
