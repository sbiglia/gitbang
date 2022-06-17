using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Avalonia;
using Avalonia.Controls;
using Gitbang.Core.Controls;

namespace Gitbang.Core
{
    public static class ViewModelLocator
    {
        public static readonly AvaloniaProperty AutoWireViewModelProperty;

        static ViewModelLocator()
        {
            AutoWireViewModelProperty = AvaloniaProperty.RegisterAttached<Control, bool>("AutoWireViewModel", typeof(ViewModelLocator), false);
            AutoWireViewModelProperty.Changed.Subscribe(args => AutoWireViewModelChanged(args?.Sender, args));
        }

        public static bool GetAutoWireViewModel(IAvaloniaObject control)
        {
            return (bool)control.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(IAvaloniaObject control, bool value)
        {
            control.SetValue(AutoWireViewModelProperty, value);
        }

        static void AutoWireViewModelChanged(IAvaloniaObject control, AvaloniaPropertyChangedEventArgs e)
        {
            if (Design.IsDesignMode)
                return;

            if (!(bool)e.NewValue)
                return;

            var view = control as Control;
            if (view == null)
                return;

            var viewType = control.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;

            var windowType = typeof(ThemedWindow);
            if (windowType.IsAssignableFrom(viewType))
                ThemeManager.Instance.EnableTheme(view as ThemedWindow);

            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);
            var viewModelType = Type.GetType(viewModelName);

            //var viewModel = ServiceLocator.Instance.Container.GetService(viewModelType) as ViewModelBase;
            //view.DataContext = viewModel;
            //viewModel.IsLoaded = true;
        }
    }
}
