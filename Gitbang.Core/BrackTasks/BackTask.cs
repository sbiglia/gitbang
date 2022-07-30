using System;
using System.Collections.Generic;
using System.Text;

namespace Gitbang.Core.BrackTasks
{
    public class BackTask
    {
        private readonly object _lock = new();
        private readonly BackTaskStatus _status;

        public string Name { get; }

        public DateTime CreatedTime { get; }

        public DateTime? ScheduledTime { get; private set; }
        public DateTime? StartedTime { get; private set; }
        public DateTime? FinishedTime { get; private set; }

        public Action<BackTaskProgress> Action { get; }

        public bool ShowMessageWhenDone { get; }
        public bool LongRunning { get; }
        public BackTaskProgress Progress { get; }


        public BackTaskStatus Status
        {
            get
            {
                lock (_lock)
                {
                    return _status;
                }
            }
            set
            {
                lock( _lock)
                    switch (value)
                    {
                        case BackTaskStatus.Scheduled:
                            ScheduledTime = DateTime.UtcNow;
                            break;
                        case BackTaskStatus.Running:
                            StartedTime = DateTime.UtcNow;
                            break;
                        case BackTaskStatus.Finished:
                            FinishedTime = DateTime.UtcNow;
                            break;
                    }
            }
        }

        public BackTask(string name, Action<BackTaskProgress> action, bool showMessageWhenDone, bool longRunning)
        {
            Name = name;
            CreatedTime = DateTime.UtcNow;
            _status = BackTaskStatus.Idle;
            Action = action;
            ShowMessageWhenDone = showMessageWhenDone;
            LongRunning = longRunning;
            Progress = new BackTaskProgress();
        }


    }
}
