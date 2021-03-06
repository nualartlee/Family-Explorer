﻿/* 
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
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace FamilyExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public FamilyView family;        

        public MainWindow()
        {
            InitializeComponent();                       
            family = FamilyView.Instance;           
            this.DataContext = family;

            // Check for passed arguments
            var args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                // Check if file is passed
                var fileName = args[1];
                if (File.Exists(fileName))
                {
                    family.Open(fileName);                    
                }               
            }

            this.Closing += MainWindow_Closing;  
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {           
            e.Cancel = !family.CanClose();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            family.EndSetCommand();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            family.EndSetCommand();
        }

        private void ResetView_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ResetView_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TreeZoomBorder.ResetView();
        }

        private void ZoomIn_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ZoomIn_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TreeZoomBorder.ZoomIn();
        }

        private void ZoomOut_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ZoomOut_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TreeZoomBorder.ZoomOut();
        }

        private void ResetZoom_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ResetZoom_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TreeZoomBorder.ResetZoom();
        }

        private void MoveUp_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void MoveUp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TreeZoomBorder.MoveUp();
        }

        private void MoveDown_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void MoveDown_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TreeZoomBorder.MoveDown();
        }

        private void MoveLeft_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void MoveLeft_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TreeZoomBorder.MoveLeft();
        }

        private void MoveRight_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void MoveRight_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TreeZoomBorder.MoveRight();
        }

        private void Center_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Center_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TreeZoomBorder.ResetPan();
        }

        private void Print_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Print_Executed(object sender, ExecutedRoutedEventArgs e)
        {            
            PrintPreview(TreeCanvas, TreeCanvas.ActualWidth, TreeCanvas.ActualHeight);
        }

        private void PrintView_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void PrintView_Executed(object sender, ExecutedRoutedEventArgs e)
        {            
            PrintPreview(TreeZoomBorder, TreeScrollViewer.ActualWidth, TreeScrollViewer.ActualHeight);            
        }

        private string _previewWindowXaml =
                        @"<Window
                            xmlns                 ='http://schemas.microsoft.com/netfx/2007/xaml/presentation'
                            xmlns:x               ='http://schemas.microsoft.com/winfx/2006/xaml'
                            Title                 ='@@TITLE'
                            Height                ='@@HEIGHT'
                            Width                 ='@@WIDTH'
                            WindowStartupLocation ='CenterOwner'>
                            <DocumentViewer Name='dv1'/>
                            </Window>";

        private void PrintPreview(Visual visual, double width, double height)
        {
            string title = FamilyExplorerWindow.Title + " (Print Preview)";
            string fileName = System.IO.Path.GetRandomFileName();            
            try
            {
                // write the XPS document
                using (XpsDocument doc = new XpsDocument(fileName, FileAccess.ReadWrite))
                {
                    XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);
                    writer.Write(visual);
                }

                // Read the XPS document into a dynamically generated preview window               
                using (XpsDocument doc = new XpsDocument(fileName, FileAccess.Read))
                {
                    FixedDocumentSequence fds = doc.GetFixedDocumentSequence();

                    string s = _previewWindowXaml;                    
                    s = s.Replace("@@TITLE", title.Replace("'", "&apos;"));
                    s = s.Replace("@@WIDTH", (width).ToString());
                    s = s.Replace("@@HEIGHT", (height).ToString());

                    using (var reader = new System.Xml.XmlTextReader(new StringReader(s)))
                    {                        
                        Window preview = System.Windows.Markup.XamlReader.Load(reader) as Window;
                        DocumentViewer dv1 = LogicalTreeHelper.FindLogicalNode(preview, "dv1") as DocumentViewer;                                                                                  
                        dv1.Document = fds as IDocumentPaginatorSource;
                                               

                        preview.Icon = this.Icon;
                        preview.WindowState = WindowState.Maximized;            
                        preview.ShowDialog();
                    }
                }
            }
            finally
            {
                if (File.Exists(fileName))
                {
                    try
                    {
                        File.Delete(fileName);
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void ShowAboutWindow(object sender, RoutedEventArgs e)
        {
            AboutWindow about = new AboutWindow();
            about.Owner = this;
            about.ShowDialog();
        }

        /// <summary>
        /// Finds a Child of a given item in the visual tree. 
        /// </summary>
        /// <param name="parent">A direct parent of the queried item.</param>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="childName">x:Name or Name of child. </param>
        /// <returns>The first parent item that matches the submitted type parameter. 
        /// If not matching item can be found, 
        /// a null parent is being returned.</returns>
        public static T FindChild<T>(DependencyObject parent, string childName)
   where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }
        
        private void TreeZoomBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Unselect all
            family.SelectPerson(null);
            family.SelectRelationship(null);            
        }        

        private void TreeScrollViewer_MouseMove(object sender, MouseEventArgs e)
        {
            if (family.SelectCommandInProgress)
            {                
                Place_SelectCommandPopup();
            }
        }

        private void SelectCommandPopup_Opened(object sender, EventArgs e)
        {
            if (family.SelectCommandInProgress)
            {                
                Place_SelectCommandPopup();
            }
        }

        private void Place_SelectCommandPopup()
        {
            Point currentPos = Mouse.GetPosition(Application.Current.MainWindow);
            SelectCommandPopup.HorizontalOffset = currentPos.X + 10;
            SelectCommandPopup.VerticalOffset = currentPos.Y + 15;
        }

        private void TreeScrollViewer_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Give focus to the tree to enable zoom and pan funcitons
            TreeScrollViewer.Focus();
        }

        private void ViewMenuItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Give focus to the tree to enable zoom and pan funcitons
            TreeScrollViewer.Focus();
        }
    }
}
