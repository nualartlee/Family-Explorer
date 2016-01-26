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

        public Family family;
        
        public MainWindow()
        {
            InitializeComponent();
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

        private void AddSibling_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            family.AddSibling_CanExecute(sender, e);
        }

        private void AddSibling_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            family.AddSibling_Executed(sender, e);
        }

    }
}
