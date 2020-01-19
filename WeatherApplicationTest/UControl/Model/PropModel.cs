using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WeatherApplicationTest.UControl.Model
{
    public class PropModel:INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropChange(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs (propName));
        }

        public PropModel()
        {
            
        }

        
    }
}
