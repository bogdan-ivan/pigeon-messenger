using System.Collections.Generic;

namespace Client.ViewModel
{
    public class ChatListViewModel : BaseViewModel
    {
        public ChatListViewModel()
        {
            InstantiateChatListView();
        }
        public ChatViewModel ChatVM
        {
            get => chatViewModel;
            set
            {
                chatViewModel = value;
            }
        }
        private ChatViewModel chatViewModel;

        private void InstantiateChatListView()
        {
            ChatVM = ChatViewModel.GetInstance();
        }

        public List<ChatListItemViewModel> Users { get; set; }
    }
}
