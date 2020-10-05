using Client.Command;
using Client.View;
using Generated;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Client.ViewModel
{
    class JoinViewModel : BaseViewModel
    {
        
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private bool isAllEmpty(string nume)
        {
            foreach (char c in nume)
                if (c != ' ')
                    return false;
            return true;
        }

        public ICommand Connect
        {
            get
            {  
                return new ParammeterCommand(async () =>
                {
                    if (!String.IsNullOrEmpty(Name) && Name[0] != ' ' && !isAllEmpty(Name))
                    {
                        ChatViewModel.RegisterName = Name;
                        MainWindowViewModel.Instance.ActiveScreen = ChatViewModel.GetInstance();
                        return;
                    }
                    MessageBox.Show("Choose your name first!");
                });
            }
        }
    }
}
