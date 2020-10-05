using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        public BaseViewModel activeScreen = new JoinViewModel();

        public BaseViewModel ActiveScreen
        {
            get { return activeScreen; }


            set { activeScreen = value; OnPropertyChanged(nameof(ActiveScreen)); }
        }

        public MainWindowViewModel()
        {
            Instance = this;
        }
        public static MainWindowViewModel Instance { get; private set; }

    }
}
