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

        public static readonly RoutedUICommand Edit = new RoutedUICommand
                        (
                                "Edit",
                                "Edit",
                                typeof(CustomCommands),
                                new InputGestureCollection()
                                {
                                        new KeyGesture(Key.E, ModifierKeys.Alt)
                                }
                        );

        public static readonly RoutedUICommand Delete = new RoutedUICommand
                        (
                                "Delete",
                                "Delete",
                                typeof(CustomCommands),
                                new InputGestureCollection()
                                {
                                        new KeyGesture(Key.Delete)
                                }
                        );

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

        public static readonly RoutedUICommand AddFather = new RoutedUICommand
                        (
                                "AddFather",
                                "AddFather",
                                typeof(CustomCommands),
                                new InputGestureCollection()
                                {
                                        new KeyGesture(Key.F, ModifierKeys.Alt)
                                }
                        );

        public static readonly RoutedUICommand AddSiblingEqualParents = new RoutedUICommand
                        (
                                "Add Equal Parent Sibling",
                                "Add Equal Parent Sibling",
                                typeof(CustomCommands),
                                new InputGestureCollection()
                                {
                                        new KeyGesture(Key.S, ModifierKeys.Alt)
                                }
                        );

        public static readonly RoutedUICommand AddSiblingByMother = new RoutedUICommand
                        (
                                "Add Sibling By Mother",
                                "Add Sibling By Mother",
                                typeof(CustomCommands),
                                new InputGestureCollection()
                                {
                                        new KeyGesture(Key.J, ModifierKeys.Alt)
                                }
                        );

        public static readonly RoutedUICommand AddSiblingByFather = new RoutedUICommand
                       (
                               "Add Sibling By Father",
                               "Add Sibling By Father",
                               typeof(CustomCommands),
                               new InputGestureCollection()
                               {
                                        new KeyGesture(Key.K, ModifierKeys.Alt)
                               }
                       );

        public static readonly RoutedUICommand AddFriend = new RoutedUICommand
                       (
                               "AddFriend",
                               "AddFriend",
                               typeof(CustomCommands),
                               new InputGestureCollection()
                                {
                                        new KeyGesture(Key.D, ModifierKeys.Alt)
                                }
                       );

        public static readonly RoutedUICommand AddPartner = new RoutedUICommand
                        (
                                "AddPartner",
                                "AddPartner",
                                typeof(CustomCommands),
                                new InputGestureCollection()
                                {
                                        new KeyGesture(Key.P, ModifierKeys.Alt)
                                }
                        );       

        public static readonly RoutedUICommand AddChild = new RoutedUICommand
                       (
                               "AddChild",
                               "AddChild",
                               typeof(CustomCommands),
                               new InputGestureCollection()
                                {
                                        new KeyGesture(Key.C, ModifierKeys.Alt)
                                }
                       );

        public static readonly RoutedUICommand AddAbuser = new RoutedUICommand
                       (
                               "AddAbuser",
                               "AddAbuser",
                               typeof(CustomCommands),
                               new InputGestureCollection()
                                {
                                        new KeyGesture(Key.A, ModifierKeys.Alt)
                                }
                       );

        public static readonly RoutedUICommand AddVictim = new RoutedUICommand
                       (
                               "AddVictim",
                               "AddVictim",
                               typeof(CustomCommands),
                               new InputGestureCollection()
                                {
                                        new KeyGesture(Key.V, ModifierKeys.Alt)
                                }
                       );

        public static readonly RoutedUICommand SetMother = new RoutedUICommand
                       (
                               "SetMother",
                               "SetMother",
                               typeof(CustomCommands),
                               new InputGestureCollection()
                                {
                                        new KeyGesture(Key.M, ModifierKeys.Control)
                                }
                       );

        public static readonly RoutedUICommand SetFather = new RoutedUICommand
                        (
                                "SetFather",
                                "SetFather",
                                typeof(CustomCommands),
                                new InputGestureCollection()
                                {
                                        new KeyGesture(Key.F, ModifierKeys.Control)
                                }
                        );

        public static readonly RoutedUICommand SetFriend = new RoutedUICommand
                       (
                               "SetFriend",
                               "SetFriend",
                               typeof(CustomCommands),
                               new InputGestureCollection()
                                {
                                        new KeyGesture(Key.D, ModifierKeys.Control)
                                }
                       );

        public static readonly RoutedUICommand SetPartner = new RoutedUICommand
                        (
                                "SetPartner",
                                "SetPartner",
                                typeof(CustomCommands),
                                new InputGestureCollection()
                                {
                                        new KeyGesture(Key.P, ModifierKeys.Control)
                                }
                        );
       
        public static readonly RoutedUICommand SetChild = new RoutedUICommand
                       (
                               "SetChild",
                               "SetChild",
                               typeof(CustomCommands),
                               new InputGestureCollection()
                                {
                                        new KeyGesture(Key.C, ModifierKeys.Control)
                                }
                       );

        public static readonly RoutedUICommand SetAbuser = new RoutedUICommand
                       (
                               "SetAbuser",
                               "SetAbuser",
                               typeof(CustomCommands),
                               new InputGestureCollection()
                                {
                                        new KeyGesture(Key.A, ModifierKeys.Control)
                                }
                       );

        public static readonly RoutedUICommand SetVictim = new RoutedUICommand
                       (
                               "SetVictim",
                               "SetVictim",
                               typeof(CustomCommands),
                               new InputGestureCollection()
                                {
                                        new KeyGesture(Key.V, ModifierKeys.Control)
                                }
                       );


    }
}
