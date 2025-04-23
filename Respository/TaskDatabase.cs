using Microsoft.Extensions.Caching.Memory;

public class TaskDatabase
{

    private readonly List<TaskItem> _tasks = new List<TaskItem>();
    private readonly ILogger<TaskService> _logger;
    private readonly IMemoryCache _cache;
    public TaskDatabase(ILogger<TaskService> logger, IMemoryCache cache)
    {
        _cache = cache;
        _tasks = new List<TaskItem>();
        _logger = logger;
    }

    public List<TaskItem> CreateTask(TaskItem task)
    {
        Guid id = Guid.NewGuid();
        task.Id = id;
        _tasks.Add(task);
        _logger.LogInformation($"Task created with ID: {task.Id}");
        _cache.Set(id, task, TimeSpan.FromMinutes(5));
        return _tasks;
    }

    public TaskItem? ReadTask(Guid id)
    {
        _logger.LogInformation($"Searching for task with ID: {id} ...");
        var task = _tasks.Find(t => t.Id == id);
        if (task != null)
        {
            _logger.LogInformation($"Task found: {task}");
            return task;
        } else {
            _logger.LogWarning($"Task with ID {id} not found.");
            return null;
        } 
    }

public List<TaskItem> UpdateTask(Guid id, TaskItem updatedTask)
{
    var task = _tasks.Find(task => task.Id == id);
    if (task != null)
    {
        int index = _tasks.IndexOf(task);
        _tasks[index] = updatedTask;
        _logger.LogInformation($"Task updated to: {task};");
        _cache.Set(id, updatedTask, TimeSpan.FromMinutes(5));
    }
    else
    {
        _logger.LogWarning($"Task with ID {id} not found.");
    }
    return _tasks;
}


    public List<TaskItem> DeleteTask(Guid id)
    {
        var taskToRemove = _tasks.FirstOrDefault(t => t.Id == id);

        if (taskToRemove != null)
        {
            _tasks.Remove(taskToRemove);
            _logger.LogInformation($"Removing {taskToRemove.Id} from the Task List.");
            _cache.Remove(id);
        } else  {
            _logger.LogWarning($"Task with ID {id} not found.");
        }
       return _tasks;
    }

    public List<TaskItem> GetAllTasks()
        {
        _logger.LogInformation("TASKS LIST:");
        foreach (var task in _tasks)
        {
            _logger.LogInformation($"{task}");
        }
        _cache.Set("tasks", _tasks, TimeSpan.FromMinutes(5));
        
        return _tasks;
    }
}