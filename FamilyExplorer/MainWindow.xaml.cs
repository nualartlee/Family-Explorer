using System;
using System.Collections.Generic;
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

namespace FamilyExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private int commandInProgress;

        public Family family;

        public MainWindow()
        {
            InitializeComponent();
            commandInProgress = 0;
            family = new Family();
            this.DataContext = family;
        }

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
            e.CanExecute = true;
        }

        private void SetMother_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            commandInProgress = 1;
            this.Cursor = Cursors.Cross;
        }

        private void endCommandInProgress()
        {
            commandInProgress = 0;
            this.Cursor = Cursors.Arrow;
        }

        private void FamilyTreeListBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = ItemsControl.ContainerFromElement(FamilyTreeListBox, e.OriginalSource as DependencyObject) as ListBoxItem;
            var SourceItem = FamilyTreeListBox.SelectedItem as ListBoxItem;
            if (item != null && SourceItem != null)
            {
                var personTarget = item.DataContext;                
                var personSource = SourceItem.DataContext;
                if (commandInProgress > 0 && personTarget.GetType().Equals(typeof(Person)) && personSource.GetType().Equals(typeof(Person)))
                {
                    switch (commandInProgress)
                    {
                        case 1:
                            family.SetPersonsMother((Person)personSource, (Person)personTarget);
                            break;
                        case 2:
                            break;
                        default:
                            break;
                    }
                }
            }
            endCommandInProgress();
        }

        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            endCommandInProgress();
        }

        

        private void FamilyTreeListBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (commandInProgress > 0)
            {
                this.Cursor = Cursors.Cross;
            }
        }

        private void FamilyTreeListBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (commandInProgress > 0)
            {
                this.Cursor = Cursors.No;
            }
        }

        private void PersonItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (commandInProgress > 0)
            {
                this.Cursor = Cursors.Hand;
            }
        }

        private void PersonItem_MouseLeave(object sender, MouseEventArgs e)
        {
            if (commandInProgress > 0)
            {
                this.Cursor = Cursors.Cross;
            }
        }
    }
}
