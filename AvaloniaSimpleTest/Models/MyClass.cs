using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AvaloniaSimpleTest.Models
{
    public class MyClass
    {
        public string Name { get; set; }

        public ObservableCollection<MyClass> Children { get; set; }
    }
}
