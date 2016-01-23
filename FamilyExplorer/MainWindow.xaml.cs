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

        public ContextMenu PersonContextMenu()
        {            
            
            ContextMenu mainMenu = new ContextMenu();

            MenuItem itemEdit = new MenuItem();
            itemEdit.Header = "Edit";
            mainMenu.Items.Add(itemEdit);

            MenuItem itemDelete = new MenuItem();
            itemDelete.Header = "Delete";
            mainMenu.Items.Add(itemDelete);

            MenuItem itemAdd = new MenuItem();
            itemAdd.Header = "Add New";                        
           
            MenuItem itemAddMother = new MenuItem();
            itemAddMother.Header = "Mother";
            itemAddMother.Command = CustomCommands.AddMother;                    
            itemAdd.Items.Add(itemAddMother);
            MenuItem itemAddFather = new MenuItem();
            itemAddFather.Header = "Father";
            itemAdd.Items.Add(itemAddFather);
            MenuItem itemAddPartner = new MenuItem();
            itemAddPartner.Header = "Partner";
            itemAdd.Items.Add(itemAddPartner);
            MenuItem itemAddFriend = new MenuItem();
            itemAddFriend.Header = "Friend";
            itemAdd.Items.Add(itemAddFriend);
            MenuItem itemAddChild = new MenuItem();
            itemAddChild.Header = "Child";
            itemAdd.Items.Add(itemAddChild);            
            Separator separator2 = new Separator();
            itemAdd.Items.Add(separator2);
            MenuItem itemAddAbuser = new MenuItem();
            itemAddAbuser.Header = "Abuser";
            itemAdd.Items.Add(itemAddAbuser);
            MenuItem itemAddVictim = new MenuItem();
            itemAddVictim.Header = "Victim";
            itemAdd.Items.Add(itemAddVictim);

            mainMenu.Items.Add(itemAdd);

            MenuItem itemSet = new MenuItem();
            itemSet.Header = "Set";

            MenuItem itemSetMother = new MenuItem();
            itemSetMother.Header = "Mother";
            itemSet.Items.Add(itemSetMother);
            MenuItem itemSetFather = new MenuItem();
            itemSetFather.Header = "Father";
            itemSet.Items.Add(itemSetFather);
            MenuItem itemSetPartner = new MenuItem();
            itemSetPartner.Header = "Partner";
            itemSet.Items.Add(itemSetPartner);
            MenuItem itemSetFriend = new MenuItem();
            itemSetFriend.Header = "Friend";
            itemSet.Items.Add(itemSetFriend);
            MenuItem itemSetChild = new MenuItem();
            itemSetChild.Header = "Child";
            itemSet.Items.Add(itemSetChild);
            Separator separator3 = new Separator();
            itemSet.Items.Add(separator3);
            MenuItem itemSetAbuser = new MenuItem();
            itemSetAbuser.Header = "Abuser";
            itemSet.Items.Add(itemSetAbuser);
            MenuItem itemSetVictim = new MenuItem();
            itemSetVictim.Header = "Victim";
            itemSet.Items.Add(itemSetVictim);

            mainMenu.Items.Add(itemSet);
            return mainMenu;
        }

        private void AddMother_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            family.AddMother_CanExecute(sender, e);
        }

        private void AddMother_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            family.AddMother_Executed(sender, e);
        }

        
    }
}
