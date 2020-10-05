using Client.ViewModel;
using Generated;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
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

    public partial class MessageView : UserControl
    {
        public ChatViewModel LocalInstance { get; set; }
        private const string BORDER = "MyBorder";
        private const int COLUMN_ALIGNMENT = 1;

        public MessageView()
        {
            InitializeComponent();
            ((INotifyCollectionChanged)MessageListBox.Items).CollectionChanged += MessageListBox_CollectionChanged;
            MessageListBox.SelectionChanged += MessageListBox_SelectionChanged;
            LocalInstance = ChatViewModel.GetInstance();
        }

        private void MessageListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach(Message mes in MessageListBox.Items)
            {
                MessageListBox.UpdateLayout();
                ListBoxItem myListBoxItem =
                (ListBoxItem)(MessageListBox.ItemContainerGenerator.ContainerFromItem(mes));

                if (myListBoxItem == null)
                    continue;

                ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);
                DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                Border myBorder = (Border)myDataTemplate.FindName(BORDER, myContentPresenter);

                if ((myListBoxItem.Content as Message).Sender.Id == LocalInstance.CurrentUser.Id)
                {
                    Grid.SetColumn(myBorder, COLUMN_ALIGNMENT);
                }
            }
            MessageListBox.UpdateLayout();
        }

        public void MessageListBox_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                MessageListBox.ScrollIntoView(e.NewItems[0]);
                if (MessageListBox.Items.Count > 0)
                    MessageListBox.SelectedItem = MessageListBox.Items[MessageListBox.Items.Count - 1];
            } 
            else
            {
                if (MessageListBox.Items.Count > 0)
                    MessageListBox.SelectedItem = MessageListBox.Items[MessageListBox.Items.Count - 1];
            }
        }


        private childItem FindVisualChild<childItem>(DependencyObject obj) where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                {
                    return (childItem)child;
                }
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
    }
}
