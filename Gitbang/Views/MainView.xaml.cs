﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using Avalonia.Styling;
using AvaloniaEdit.Highlighting;
using AvaloniaEdit.Highlighting.Xshd;
using FluentAvalonia.Styling;
using Gitbang.Core.Controls;
using Gitbang.Core.Settings;
using Gitbang.Core.Settings.Interfaces;
using Gitbang.Models;
using Gitbang.ViewModels;
using Serilog;


namespace Gitbang.Views
{
    public partial class MainView : PlatformDependentWindow
    {
        private readonly ISessionSettings _sessionSettings;
        
        private TreeView _treeView;
        private Grid _mainGrid;
        private ContentPresenter _mainPane;
        private Menu _mainMenu;
        private ColumnDefinition _leftMainGridColumn;
        private ColumnDefinition _rightMainGridColumn;
        
        public MainView()
        {

            if (App.ConfigurationManager == null)
            {
                Log.Logger.Fatal("ConfigurationManager could not be null");
                if(!Design.IsDesignMode)
                    return;
            }

            _sessionSettings = App.ConfigurationManager.SessionSettings;

            InitializeComponents();
            
            var faTheme = AvaloniaLocator.Current.GetService<FluentAvaloniaTheme>();

            faTheme.RequestedTheme = "Dark";
            faTheme.PreferSystemTheme = false;
            

#if DEBUG
            this.AttachDevTools();
#endif


        }

        private void InitializeComponents()
        {
            AvaloniaXamlLoader.Load(this);

            //Just to avoid crashing Avalonia's Designer
            if (Design.IsDesignMode)
            {
                return;
            }

            _mainMenu = this.FindControl<Menu>("MainMenu");
            _mainGrid = this.FindControl<Grid>("MainGrid");

             
            _leftMainGridColumn = _mainGrid.ColumnDefinitions[0];
            _rightMainGridColumn = _mainGrid.ColumnDefinitions[2];

            if (_sessionSettings.SplitterPosition is > 0 and < 1)
            {
                _leftMainGridColumn.Width = new GridLength(_sessionSettings.SplitterPosition, GridUnitType.Star);
                _rightMainGridColumn.Width = new GridLength(1 - _sessionSettings.SplitterPosition, GridUnitType.Star);
            }

            _treeView = this.FindControl<TreeView>("RepoIndexTreeView");
            _treeView.SelectionChanged += TreeView_SelectionChanged;
            
            var themesNames = new List<string>();
            var themes = new List<IStyle>();
            
            if (themes.Count == 0)
            {
                var light = AvaloniaRuntimeXamlLoader.Parse<StyleInclude>(
                    @"<StyleInclude xmlns='https://github.com/avaloniaui' Source='resm:Avalonia.Themes.Default.Accents.BaseLight.xaml?assembly=Avalonia.Themes.Default'/>");
                themes.Add(light);
                themesNames.Add(Core.Properties.Resources.LightTheme);


                var dark = AvaloniaRuntimeXamlLoader.Parse<StyleInclude>(
                    @"<StyleInclude xmlns='https://github.com/avaloniaui' Source='resm:Avalonia.Themes.Default.Accents.BaseDark.xaml?assembly=Avalonia.Themes.Default'/>");
                themes.Add(dark);
                themesNames.Add(Core.Properties.Resources.DarkTheme);
            }


            var themesDropDown = this.Find<ComboBox>("Themes");
            themesDropDown.Items = themesNames;
            themesDropDown.SelectionChanged += (sender, e) =>
            {
                var faTheme = AvaloniaLocator.Current.GetService<FluentAvaloniaTheme>();

                if (themesDropDown.SelectedItem == null)
                    return;
                
                faTheme.RequestedTheme = (string)themesDropDown.SelectedItem;
                _sessionSettings.Theme = faTheme.RequestedTheme;

            };

            Styles.Add(themes[0]);
            int selectedTheme = themesNames.IndexOf(_sessionSettings.Theme);
            themesDropDown.SelectedIndex = selectedTheme < 0 ? 0 : selectedTheme;

            TemplateApplied += MainView_TemplateApplied;
        }

        private void MainView_TemplateApplied(object? sender, Avalonia.Controls.Primitives.TemplateAppliedEventArgs e)
        {
            Application.Current?.FocusManager?.Focus(this);
        }

        internal void TreeView_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
           // _treeView.ContextMenu.Open();
        }

        private void ApplyTheme()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) && Styles.TryGetResource("ThemeBackgroundBrush", out object backgroundColor) && backgroundColor is ISolidColorBrush brush)
            {
                // HACK: SetTitleBarColor is a method in Avalonia.Native.WindowImpl
                var setTitleBarColorMethod = PlatformImpl.GetType().GetMethod("SetTitleBarColor");
                setTitleBarColorMethod?.Invoke(PlatformImpl, new object[] { brush.Color });
            }


            //Reload text editor
            //DecompileSelectedNodes();
        }


        protected override bool HandleClosing()
        {
            _sessionSettings.SplitterPosition = _leftMainGridColumn.Width.Value /
                                                (_leftMainGridColumn.Width.Value + _rightMainGridColumn.Width.Value);
            return base.HandleClosing();
        }
        
        private void TextBoxGroupOnPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            switch (e.Property.Name)
            {
                case "IsVisible" when e.NewValue is true:
                {
                    if (sender is TextBox textbox)
                    {
                        textbox.Focus();
                        textbox.SelectAll();
                    }

                    break;
                }
                case "IsFocused" when e.OldValue is true && e.NewValue is false:
                {
                    if (sender is TextBox { DataContext: TreeViewNodeModelBase viewModel, IsVisible: true})
                    {
                        viewModel.CancelEditCommand.Execute(null);
                    }

                    break;
                }
            }
        }

        private void TextBoxRepoOnPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            switch (e.Property.Name)
            {
                case "IsVisible" when e.NewValue is true:
                {
                    if (sender is TextBox textbox)
                    {
                        textbox.Focus();
                        textbox.SelectAll();
                    }

                    break;
                }
                case "IsFocused" when e.OldValue is true && e.NewValue is false:
                {
                    if (sender is TextBox { DataContext: TreeViewNodeModelBase viewModel, IsVisible: true })
                    {
                        viewModel.CancelEditCommand.Execute(null);
                    }

                    break;
                }
            }
        }
    }
}