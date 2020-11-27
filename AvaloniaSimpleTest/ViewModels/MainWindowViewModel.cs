using AvaloniaSimpleTest.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AvaloniaSimpleTest.ViewModels
{
    
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<MyClass> Items { get; }

        public string Greeting => "Welcome to Avalonia!";

        public MainWindowViewModel()
        {
            Items = new ObservableCollection<MyClass>();
            Items.Add(new MyClass
            {
                Name = "1",
                Children = new ObservableCollection<MyClass>
            {
                new MyClass
                {
                    Name = "1_1",
                    Children = new ObservableCollection<MyClass>
                    {
                        new MyClass
                        {
                            Name = "1_1_1"
                        },
                        new MyClass
                        {
                            Name = "1_1_2"
                        }
                    }
                },
                new MyClass
                {
                    Name = "1_2"
                }
            }
            });

            Items.Add(new MyClass
            {
                Name = "2",
                Children = new ObservableCollection<MyClass>
            {
                new MyClass
                {
                    Name = "2_1",
                    Children = new ObservableCollection<MyClass>
                    {
                        new MyClass
                        {
                            Name = "1_1_1"
                        },
                        new MyClass
                        {
                            Name = "1_1_2"
                        }
                    }
                },
                new MyClass
                {
                    Name = "1_2"
                }
            }
            });
        }
    }
}
