using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/tasks")]
public class TaskController : ControllerBase {
    private readonly TaskDatabase _taskDatabase;

    public TaskController(TaskDatabase taskDatabase) {
        _taskDatabase = taskDatabase;
    }


    [HttpPost]
    public List<TaskItem> CreateTask(TaskItem task) {
        return _taskDatabase.CreateTask(task);
    }

    [HttpGet("{id}")]
    public ActionResult<TaskItem> ReadTask(Guid id) {
        var task = _taskDatabase.ReadTask(id);
        if (task is null) return NotFound();
        return Ok(task);
    }

    [HttpPut("{id}")]
    public ActionResult<List<TaskItem>> UpdateTask(Guid id, TaskItem updatedTask) {
        var result = _taskDatabase.UpdateTask(id, updatedTask);
        if (result is null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public ActionResult<List<TaskItem>> DeleteTask(Guid id) {
        var result = _taskDatabase.DeleteTask(id);
        if (result is null) return NotFound();
        return Ok(result);
    }

    //ActionResult vs IActionResult
}