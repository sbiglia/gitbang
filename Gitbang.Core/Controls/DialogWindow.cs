﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Styling;
using System.Linq.Expressions;

namespace Gitbang.Core.Controls
{
    public class DialogWindow: PlatformDependentWindow, IStyleable
	{
        static readonly Func<Window, object> _dialogResultField;

        public object DialogResult { get { return _dialogResultField(this); } }

        static DialogWindow()
        {
            var instanceParameter = Expression.Parameter(typeof(Window));
            var filed = typeof(Window).GetField("_dialogResult", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var getValueExp = Expression.Field(instanceParameter, filed);
            _dialogResultField = Expression.Lambda<Func<Window, object>>(getValueExp, instanceParameter).Compile();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Key == Key.Escape && e.KeyModifiers == KeyModifiers.None)
            {
                Close();
                e.Handled = true;
            }
        }

        Type IStyleable.StyleKey { get; } = typeof(Window);
    }
}
