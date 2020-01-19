using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherApplicationTest.ViewModel;


namespace WeatherApplicationTest.UControl
{
    public class Command : ICommand
    {
        public WeaterAppInput WeaterAppInput { get; set; }

        public Command(WeaterAppInput weaterAppInput)
        {
            WeaterAppInput = weaterAppInput;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        

        public void Execute(object parameter)
        {
            
            WeaterAppInput.MakeQuerzAsync();
            
        }
    }
}
