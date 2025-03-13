using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/tasks")]
public class TaskController : ControllerBase {
    private readonly TaskService _taskService;

    public TaskController(TaskService taskService) {
        _taskService = taskService;
    }

    [HttpPost]
    public List<TaskItem> CreateTask(TaskItem task) {
        return _taskService.CreateTask(task);
    }

    [HttpGet("{id}")]
    public TaskItem ReadTask(Guid id) {
        return _taskService.ReadTask(id);
    }

    [HttpPut("{id}")]
    public List<TaskItem> UpdateTask(Guid id, TaskItem updatedTask) {
        return _taskService.UpdateTask(id, updatedTask);
    }

    [HttpDelete("{id}")]
    public List<TaskItem> DeleteTask(Guid id) {
        return _taskService.DeleteTask(id);
    }
}