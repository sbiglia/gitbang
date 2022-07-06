using Avalonia.Controls;
using Newtonsoft.Json;

namespace Gitbang.Core.Settings.Interfaces;

public interface ISessionSettings
{
    public WindowState WindowState { get; set; }

    public double SplitterPosition { get; set; }

    public double TopPaneSplitterPosition { get; set; }

    public double BottomPaneSplitterPosition { get; set; }

    public string Theme { get; set; }
}