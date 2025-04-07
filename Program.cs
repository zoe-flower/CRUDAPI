using Microsoft.Extensions.DependencyInjection; //throws an exception at runtime if the service wasn’t registered, and removes the compiler warning because taskService is guaranteed to be non-null.

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<TaskService>();

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

var taskService = app.Services.GetRequiredService<TaskService>(); //Added Required as GetService<T>() doesn't guarantee the service exists in the container and causes console warnings
var newTask = new TaskItem {
    Id = Guid.NewGuid(),
    Title = "Sample Task",
    Description = "This is a sample task.",
    IsCompleted = false,
};
var newTask2 = new TaskItem {
    Id = Guid.NewGuid(),
    Title = "Sample Task 2",
    Description = "This is a sample task 2.",
    IsCompleted = false,
};

taskService.CreateTask(newTask);
taskService.ReadTask(newTask.Id);
taskService.ReadTask(newTask2.Id);
taskService.CreateTask(newTask2);
taskService.ReadTask(newTask2.Id);
taskService.UpdateTask(newTask.Id, new TaskItem {
    Id = newTask.Id,
    Title = "Updated Task",
    Description = "This is an updated task.",
    IsCompleted = true,
});
taskService.GetAllTasks();
taskService.DeleteTask(newTask.Id);
taskService.GetAllTasks();


app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
