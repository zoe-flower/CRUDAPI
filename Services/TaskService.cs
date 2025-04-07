public class TaskService
{
    private readonly List<TaskItem> _tasks = new List<TaskItem>();
    public IReadOnlyList<TaskItem> Tasks => _tasks;
    private readonly ILogger<TaskService> _logger;
    public TaskService(ILogger<TaskService> logger)
    {
        _logger = logger;
    }

    public List<TaskItem> CreateTask(TaskItem task)
    {
        Guid id = Guid.NewGuid();
        task.Id = id;
        _tasks.Add(task);
        _logger.LogInformation($"Task created with ID: {task.Id}");
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
        return _tasks;
    }
}


// Separate into a task repository aka fake db
// use a memory cache IMemoryCache - can configure hours to be alive
// use cache in repository
// think of how to cache request to db.
// typically conroller calls service, calls caching layer, calls db. 
// is in cache? yes, return. If no get from db and cache. configure timne in cache.Add()
// Look at how I can alter cache time in IOptions. Create Json in app settinggs and create a model based of the json. configure that in program.cs
// replace writeline with ILogger
// make things async