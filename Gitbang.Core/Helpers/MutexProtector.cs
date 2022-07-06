using System;
using System.Threading;

namespace Gitbang.Core.Helpers;

/// <summary>
/// Helper class that creates a mutex and waits for it to be released if already existed when it was created.
/// Used to serialize access to resources (files) when several instances of this application are running.
/// </summary>
public sealed class MutexProtector : IDisposable
{
    readonly Mutex mutex;

    public MutexProtector(string name)
    {
        this.mutex = new Mutex(true, name, out var createdNew);

        if (!createdNew)
        {
            try
            {
                mutex.WaitOne();
            }
            catch (AbandonedMutexException)
            {
            }
        }
    }

    public void Dispose()
    {
        mutex.ReleaseMutex();
        mutex.Dispose();
    }
}