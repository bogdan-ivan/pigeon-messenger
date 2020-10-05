using System.Collections.Generic;

namespace Client.ViewModel.DataForTest
{
    public class ChatListTest : ChatListViewModel
    {
        /// <summary>
        /// single instance to test binding
        /// </summary>
        public static ChatListTest Instance => new ChatListTest();

        public ChatListTest()
        {
            var user = new ChatListItemViewModel
            {
                Name = "Bratosin Alexandru",
                Initials = "BA",
                Message = "Exemplu de mesaj primit / trimis prea lung",
                ProfilePictureRGB = "3099c5"
            };
            var user2 = new ChatListItemViewModel
            {
                Name = "Hrihoreanu Radu",
                Initials = "HA",
                Message = "Exemplu mesaj primit",
                ProfilePictureRGB = "3099c5"
            };
            var user3 = new ChatListItemViewModel
            {
                Name = "Ivan Bogdan",
                Initials = "IB",
                Message = "Exemplu mesaj primit",
                ProfilePictureRGB = "3099c5"
            };
            var user4 = new ChatListItemViewModel
            {
                Name = "Bivol Stefan",
                Initials = "BS",
                Message = "Exemplu mesaj primit",
                ProfilePictureRGB = "3099c5"
            };


            Users = new List<ChatListItemViewModel>();
            Users.Add(user);
            Users.Add(user2);
            Users.Add(user3);
            Users.Add(user4);
        }
    }
}
