using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gitbang.Core.BrackTasks;

public class BackTaskManager
{
    private static readonly int MaxUserBackTasks = 20;
    private object _lock = new();
    private TaskScheduler _taskScheduler;
    private List<BackTask> _activeBackTasks;
    private readonly LinkedList<BackTask> _userBackTasks;

    public BackTaskManager()
    {
        _activeBackTasks = new List<BackTask>();
        _userBackTasks = new LinkedList<BackTask>(); 
        _taskScheduler = TaskScheduler.Default;
    }

    public BackTask[] GetAllBackTasks()
    {
        lock (_lock)
        {
            return _userBackTasks.ToArray();
        }
    }

    public BackTask Add(string name, Action<BackTaskProgress> action, bool showMessageWhenDone = true,
        bool longRunning = false)
    {
        BackTask task = new(name, action, showMessageWhenDone, longRunning);
        Schedule(task);
        return task;
    }


    public void Schedule(BackTask backTask)
    {
        Task task = new(() =>
        {
            backTask.Status = BackTaskStatus.Running;
            backTask.Action(backTask.Progress);
            RemoveJob(backTask);
            backTask.Status = BackTaskStatus.Finished;
        }, backTask.LongRunning ? TaskCreationOptions.LongRunning : TaskCreationOptions.None);
        AddTask(backTask);
        backTask.Status = BackTaskStatus.Scheduled;
        task.Start(_taskScheduler);
    }

    private void AddTask(BackTask backTask)
    {
        lock (_lock)
        {
            _activeBackTasks.Insert(0,backTask);
        }
    }

    private void RemoveJob(BackTask backTask)
    {
        lock (_lock)
        {
            _activeBackTasks.Remove(backTask);
        }

    }

}
