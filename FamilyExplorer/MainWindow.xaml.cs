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

        private bool isMouseCaptured;
        private double mouseVerticalPosition;
        private double mouseHorizontalPosition;

        public FamilyViewModel family;        

        public MainWindow()
        {
            InitializeComponent();            
            family = new FamilyViewModel();
            family.CreateNewFamily();
            this.DataContext = family;            
        }

        #region Person Commands

        private void AddMother_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            family.AddMother_CanExecute(sender, e);
        }

        private void AddMother_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            family.AddMother_Executed(sender, e);
        }

        private void AddFather_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            family.AddFather_CanExecute(sender, e);
        }

        private void AddFather_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            family.AddFather_Executed(sender, e);
        }

        private void AddSiblingEqualParents_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            family.AddSiblingEqualParents_CanExecute(sender, e);
        }

        private void AddSiblingEqualParents_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            family.AddSiblingEqualParents_Executed(sender, e);
        }

        private void AddSiblingByMother_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            family.AddSiblingByMother_CanExecute(sender, e);
        }

        private void AddSiblingByMother_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            family.AddSiblingByMother_Executed(sender, e);
        }

        private void AddSiblingByFather_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            family.AddSiblingByFather_CanExecute(sender, e);
        }

        private void AddSiblingByFather_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            family.AddSiblingByFather_Executed(sender, e);
        }

        private void AddFriend_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            family.AddFriend_CanExecute(sender, e);
        }

        private void AddFriend_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            family.AddFriend_Executed(sender, e);
        }

        private void AddPartner_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            family.AddPartner_CanExecute(sender, e);
        }

        private void AddPartner_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            family.AddPartner_Executed(sender, e);
        }

        private void AddChild_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            family.AddChild_CanExecute(sender, e);
        }

        private void AddChild_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            family.AddChild_Executed(sender, e);
        }

        private void AddAbuser_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            family.AddAbuser_CanExecute(sender, e);
        }

        private void AddAbuser_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            family.AddAbuser_Executed(sender, e);
        }

        private void AddVictim_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            family.AddVictim_CanExecute(sender, e);
        }

        private void AddVictim_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            family.AddVictim_Executed(sender, e);
        }

        private void SetMother_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            family.SetMother_CanExecute(sender, e);
        }

        private void SetMother_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            family.SetMother_Executed(sender, e);
        }

        private void SetFather_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            family.SetFather_CanExecute(sender, e);
        }

        private void SetFather_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            family.SetFather_Executed(sender, e);
        }

        private void SetFriend_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            family.SetFriend_CanExecute(sender, e);
        }

        private void SetFriend_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            family.SetFriend_Executed(sender, e);
        }

        private void SetPartner_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            family.SetPartner_CanExecute(sender, e);
        }

        private void SetPartner_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            family.SetPartner_Executed(sender, e);
        }

        private void SetChild_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            family.SetChild_CanExecute(sender, e);
        }

        private void SetChild_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            family.SetChild_Executed(sender, e);
        }

        private void SetAbuser_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            family.SetAbuser_CanExecute(sender, e);
        }

        private void SetAbuser_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            family.SetAbuser_Executed(sender, e);
        }

        private void SetVictim_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            family.SetVictim_CanExecute(sender, e);
        }

        private void SetVictim_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            family.SetVictim_Executed(sender, e);
        }

        private void PersonItem_MouseEnter(object sender, MouseEventArgs e)
        {            
            family.EnterSetCommandRelation((Person)((FrameworkElement)sender).DataContext);
        }

        private void PersonItem_MouseLeave(object sender, MouseEventArgs e)
        {
            family.ExitSetCommandRelation();
        }

        private void PersonItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            family.FinalizeSetCommand((Person)((FrameworkElement)sender).DataContext);           
        }

        #endregion Person Commands

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            family.EndSetCommand();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            family.EndSetCommand();
        }
       
        private void TreeCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            family.SetWindowSize(TreeCanvas.ActualWidth, TreeCanvas.ActualHeight);                      
        }
       
        private void TreeScrollViewer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ScrollViewer FamilyTreeScrollViewer = sender as ScrollViewer;
            mouseVerticalPosition = e.GetPosition(null).Y;
            mouseHorizontalPosition = e.GetPosition(null).X;
            isMouseCaptured = true;
            FamilyTreeScrollViewer.CaptureMouse();
        }

        private void TreeScrollViewer_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (isMouseCaptured)
            {
                // Calculate distance moved
                double deltaX = e.GetPosition(null).X - mouseHorizontalPosition;
                double deltaY = e.GetPosition(null).Y - mouseVerticalPosition;
                // Move Tree
                family.MoveTreePositionInWindow(deltaX, deltaY);
                // Record current position
                mouseHorizontalPosition = e.GetPosition(null).X;
                mouseVerticalPosition = e.GetPosition(null).Y;
                
            }
            // Tooltip for set commands
            ToolTipPopup.HorizontalOffset = e.GetPosition(TreeCanvas).X + 20;
            ToolTipPopup.VerticalOffset = e.GetPosition(TreeCanvas).Y + 30;
        }

        private void TreeScrollViewer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ScrollViewer FamilyTreeScrollViewer = sender as ScrollViewer;
            isMouseCaptured = false;
            FamilyTreeScrollViewer.ReleaseMouseCapture();
            mouseVerticalPosition = -1;
            mouseHorizontalPosition = -1;
        }

        private void TreeScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            //FamilyTreeListBoxScaleTransform.CenterX = e.GetPosition(null).X;
            //FamilyTreeListBoxScaleTransform.CenterY = e.GetPosition(null).Y;
           
            family.ScaleTree(Convert.ToDouble(e.Delta), e.GetPosition(TreeCanvas).X, e.GetPosition(TreeCanvas).Y);           
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            family.Save();
        }

        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            family.Open();
        }

        private void CenterTree_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CenterTree_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            family.CenterTreeInWindow();
        }

        private void Print_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Print_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Canvas ItemCanvas = FindChild<Canvas>(TreeListBox, "SubTreeCanvas");
            PrintPreview(ItemCanvas, TreeListBox.ActualWidth, TreeListBox.ActualHeight);
        }

        private void PrintView_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void PrintView_Executed(object sender, ExecutedRoutedEventArgs e)
        {            
            PrintPreview(TreeCanvas, TreeScrollViewer.ActualWidth, TreeScrollViewer.ActualHeight);            
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

                // Read the XPS document into a dynamically generated
                // preview Window 
                using (XpsDocument doc = new XpsDocument(fileName, FileAccess.Read))
                {
                    FixedDocumentSequence fds = doc.GetFixedDocumentSequence();

                    string s = _previewWindowXaml;                    
                    s = s.Replace("@@TITLE", title.Replace("'", "&apos;"));
                    s = s.Replace("@@WIDTH", (width + 200).ToString());
                    s = s.Replace("@@HEIGHT", (height + 200).ToString());

                    using (var reader = new System.Xml.XmlTextReader(new StringReader(s)))
                    {
                        Window preview = System.Windows.Markup.XamlReader.Load(reader) as Window;
                        DocumentViewer dv1 = LogicalTreeHelper.FindLogicalNode(preview, "dv1") as DocumentViewer;                                                                                  
                        dv1.Document = fds as IDocumentPaginatorSource;

                        preview.Icon = this.Icon;
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
    }
}
