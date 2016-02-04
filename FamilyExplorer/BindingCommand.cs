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
    public class BindingCommand : ICommand

   {

        public Predicate<object> CanExecuteDelegate { get; set; }

        public Action<object> ExecuteDelegate { get; set; }

        public bool CanExecute(object parameter)

           {

             if (CanExecuteDelegate != null)

                 return CanExecuteDelegate(parameter);

             return true;// if there is no can execute default to true

        }
    
        public event EventHandler CanExecuteChanged

           {

            add { CommandManager.RequerySuggested += value; }

             remove { CommandManager.RequerySuggested -= value; }

        }
  
        public void Execute(object parameter)

          {

              if (ExecuteDelegate != null)

                ExecuteDelegate(parameter);

          }     

   }
}
