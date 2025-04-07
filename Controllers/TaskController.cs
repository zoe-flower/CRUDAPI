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
    public ActionResult<TaskItem> ReadTask(Guid id) {
        var task = _taskService.ReadTask(id);
        if (task is null) return NotFound();
        return Ok(task);
    }

    [HttpPut("{id}")]
    public ActionResult<List<TaskItem>> UpdateTask(Guid id, TaskItem updatedTask) {
        var result = _taskService.UpdateTask(id, updatedTask);
        if (result is null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public ActionResult<List<TaskItem>> DeleteTask(Guid id) {
        var result = _taskService.DeleteTask(id);
        if (result is null) return NotFound();
        return Ok(result);
    }

    //ActionResult vs IActionResult
}