namespace Client.ViewModel.DataForTest
{
    public class ChatListItemsTest : ChatListItemViewModel
    {
        /// <summary>
        /// single instance to test binding
        /// </summary>
        public static ChatListItemsTest Instance => new ChatListItemsTest();

        public ChatListItemsTest()
        {
            Initials = "BA";
            Name = "Bratosin Alexandru";
            Message = "Exemplu de ultim mesaj primit / trimis prea lung";
            ProfilePictureRGB = "3099c5";
        }
    }
}
