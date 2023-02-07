using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CustomerRecords.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> execute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> execute)
        {
            this.execute = execute;
        }

        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter) => execute(parameter);
    }
}
