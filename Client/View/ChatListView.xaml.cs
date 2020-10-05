using Client.ViewModel;
using Generated;
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

namespace Client.View
{
    /// <summary>
    /// Interaction logic for ListOfChatList.xaml
    /// </summary>
    public partial class ChatListView : UserControl
    {
        public ChatListView()
        {
            InitializeComponent();
        }

        private void MyListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var inst = ChatViewModel.GetInstance();
            inst.SelectedUser = MyListBox.SelectedItem as User;
        }
    }
}