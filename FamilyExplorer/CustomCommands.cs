/* 
Family Explorer - Record and View Family Relationships
Copyright(C) 2016  Javier Nualart Lee (nualartlee@yahoo.com)

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License version 3 as
published by the Free Software Foundation.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.If not, see<http://www.gnu.org/licenses/> */
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
        public static readonly RoutedUICommand CenterTree = new RoutedUICommand
                       (
                               "Center Tree",
                               "Center Tree",
                               typeof(CustomCommands),
                               new InputGestureCollection()
                               {
                                        new KeyGesture(Key.C, ModifierKeys.Alt)
                               }
                       );

        public static readonly RoutedUICommand ZoomIn = new RoutedUICommand
                      (
                              "Zoom In",
                              "Zoom In",
                              typeof(CustomCommands),
                              new InputGestureCollection()
                              {
                                        new KeyGesture(Key.Add, ModifierKeys.Control)
                              }
                      );

        public static readonly RoutedUICommand ZoomOut = new RoutedUICommand
                      (
                              "Zoom Out",
                              "Zoom Out",
                              typeof(CustomCommands),
                              new InputGestureCollection()
                              {
                                        new KeyGesture(Key.Subtract, ModifierKeys.Control)
                              }
                      );

        public static readonly RoutedUICommand MoveUp = new RoutedUICommand
                      (
                              "Move Up",
                              "Move Up",
                              typeof(CustomCommands),
                              new InputGestureCollection()
                              {
                                        new KeyGesture(Key.Up)
                              }
                      );

        public static readonly RoutedUICommand MoveDown = new RoutedUICommand
                      (
                              "Move Down",
                              "Move Down",
                              typeof(CustomCommands),
                              new InputGestureCollection()
                              {
                                        new KeyGesture(Key.Down)
                              }
                      );

        public static readonly RoutedUICommand MoveLeft = new RoutedUICommand
                      (
                              "Move Left",
                              "Move Left",
                              typeof(CustomCommands),
                              new InputGestureCollection()
                              {
                                        new KeyGesture(Key.Left)
                              }
                      );

        public static readonly RoutedUICommand MoveRight = new RoutedUICommand
                      (
                              "Move Right",
                              "Move Right",
                              typeof(CustomCommands),
                              new InputGestureCollection()
                              {
                                        new KeyGesture(Key.Right)
                              }
                      );
        public static readonly RoutedUICommand Print = new RoutedUICommand
                        (
                                "Print",
                                "Print",
                                typeof(CustomCommands),
                                new InputGestureCollection()
                                {
                                        new KeyGesture(Key.P, ModifierKeys.Control)
                                }
                        );

        public static readonly RoutedUICommand PrintView = new RoutedUICommand
                        (
                                "Print View",
                                "Print View",
                                typeof(CustomCommands),
                                new InputGestureCollection()
                                {
                                        new KeyGesture(Key.V, ModifierKeys.Control)
                                }
                        );

       
        

    }
}
