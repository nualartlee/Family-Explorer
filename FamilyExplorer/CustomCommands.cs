using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FamilyExplorer
{
    public static class CustomCommands
    {
        public static readonly RoutedUICommand AddMother = new RoutedUICommand
                        (
                                "AddMother",
                                "AddMother",
                                typeof(CustomCommands),
                                new InputGestureCollection()
                                {
                                        new KeyGesture(Key.M, ModifierKeys.Alt)
                                }
                        );

    }
}
