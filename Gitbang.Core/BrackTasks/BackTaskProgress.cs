using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Gitbang.Core.BrackTasks;

public class BackTaskProgress
{
    public readonly int TotalProgress = 100;
    
    private readonly object _updateLock = new ();
    private double _currentProgress;
    private string _message;
    private BackTaskProgressState _state;

    private Action _cancellationAction;
    private Action _progressAction;

    private readonly StringBuilder _ouputBuffer = new (1024);

    public double CurrentProgress
    {
        get
        {
            lock (_updateLock)
            {
                return _currentProgress;
            }
        }
    }

    public string Message
    {
        get
        {
            lock (_updateLock)
            {
                return _message;
            }
                
        }
    }

    public BackTaskProgressState State
    {
        get
        {
            lock (_updateLock)
            {
                return _state;
            }
        }
        set
        {
            lock (_updateLock)
            {
                _state = value;
            }
        }
    }

    public bool IsCandeled
    {
        get
        {
            lock (_updateLock)
            {
                return _state == BackTaskProgressState.Canceled;
            }
        }
    }

    public string Output
    {
        get
        {
            lock (_updateLock)
            {
                return _ouputBuffer.ToString ();
            }
        }
    }

    public void AppendOutputLine(string line)
    {
        lock (_updateLock)
        {
            _ouputBuffer.AppendLine(line);
        }
    }

    public void Update(double progress, string message, BackTaskProgressState state = BackTaskProgressState.InProgress)
    {
        lock (_updateLock)
        {
            _currentProgress = progress;
            _message = message;
            _state = state;
        }

        _progressAction?.Invoke();
    }

    public void Cancel()
    {
        lock (_updateLock)
        {
            _cancellationAction?.Invoke();
            _state = BackTaskProgressState.Canceled;
        }
    }

    public void SetCancellationAction(Action cancellationAction)
    {
        lock (_updateLock)
        {
            _cancellationAction = cancellationAction;
        }
    }

    public void SetProgressaction(Action progressAction)
    {
        lock (_updateLock)
        {
            _progressAction = progressAction;
        }
    }

}