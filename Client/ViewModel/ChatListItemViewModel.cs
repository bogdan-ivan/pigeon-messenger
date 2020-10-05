namespace Client.ViewModel
{
    /// <summary>
    ///  view model for each chat list item ( each user )
    /// </summary>

    public class ChatListItemViewModel : BaseViewModel
    {
        public string Name { get; set; }

        public string Message { get; set; }

        /// <summary>
        ///  initials of user 
        /// </summary>
        public string Initials { get; set; }

        public string ProfilePictureRGB { get; set; }

        public bool NewMessage { get; set; }
    }
}
