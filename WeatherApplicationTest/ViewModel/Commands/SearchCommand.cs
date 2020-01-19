using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherApplicationTest.ViewModel.Commands;

namespace WeatherApplicationTest.ViewModel.Commands
{
    public class SearchCommand : ICommand
    {
        public WeaterAppInput WeaterAppInput { get; set; }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public SearchCommand(WeaterAppInput weaterAppInput)
        {
            WeaterAppInput = weaterAppInput;
        }

        public void Execute(object parameter)
        {
            WeaterAppInput.MakeQuerzAsync();
        }
    }
}
