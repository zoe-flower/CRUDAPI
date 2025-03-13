using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualBasic;

public class TaskService {
    public List<TaskItem> _tasks = new List<TaskItem>();

    public List<TaskItem> CreateTask(TaskItem task) {
        Guid id = Guid.NewGuid();
        task.Id = id;
        _tasks.Add(task);
        return _tasks;
    }

    public TaskItem ReadTask(Guid id) {
        TaskItem task = _tasks.Find(task => task.Id == id);
        Console.WriteLine($"Task: {task.Title}");
        return task;
    }

  public List<TaskItem> UpdateTask(Guid id, TaskItem updatedTask) {
    var task = _tasks.Find(task => task.Id == id);
    if (task != null) {
        int index = _tasks.IndexOf(task);
        _tasks[index] = updatedTask;
    }
    return _tasks;
}

    public List<TaskItem> DeleteTask(Guid id) {
        var taskToRemove = _tasks.FirstOrDefault(t => t.Id == id);
        _tasks.Remove(taskToRemove);
        return _tasks;
    }
}