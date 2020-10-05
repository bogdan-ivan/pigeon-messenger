using Client.Command;
using Generated;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Client.View;
using Xamarin.Forms.Dynamic;
using System.Media;

namespace Client.ViewModel
{
    public class ChatViewModel : BaseViewModel
    {
        private Channel channel = new Channel($"{Configuration.HOST}:{Configuration.PORT}", ChannelCredentials.Insecure);

        private ThreadSafeObservableCollection<User> connectedUsers;
        private ThreadSafeObservableCollection<User> filteredConnectedUsers;
        private ThreadSafeObservableCollection<Message> currentConversation;
        private AsyncDuplexStreamingCall<User, User> usersStream;
        private ObservableDictionary<int, ThreadSafeObservableCollection<Message>> allConversations;
        private string message;
        private string name;
        private string profilePictureRGB;
        public static string RegisterName;
        private string initials;
        private string isVisible;
        private string searchContent;

        public string IsVisible
        {
            get => isVisible;
            set
            {
                isVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }

        public string SearchContent
        {
            get => searchContent;
            set
            {
                searchContent = value;
                if (searchContent != null || searchContent != "")
                {
                    filterUsers();
                }
                else
                {
                    FilteredConnectedUsers = connectedUserDeepCopy();
                }
                OnPropertyChanged("SearchContent");
            }
        }
        public string Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }

        public User CurrentUser { get; set; }

        public ThreadSafeObservableCollection<User> FilteredConnectedUsers
        {
            get => filteredConnectedUsers;
            set
            {
                filteredConnectedUsers = value;
                OnPropertyChanged("FilteredConnectedUsers");
            }
        }

        public ThreadSafeObservableCollection<User> ConnectedUsers
        {
            get => connectedUsers;
            set
            {
                connectedUsers = value;
                OnPropertyChanged("ConnectedUsers");
            }
        }
        public ThreadSafeObservableCollection<Message> CurrentConversation
        {
            get => currentConversation;
            set
            {
                currentConversation = value;
                OnPropertyChanged("CurrentConversation");
            }
        }

        public ObservableDictionary<int, ThreadSafeObservableCollection<Message>> AllConversations
        {
            get => allConversations;
            set
            {
                allConversations = value;
                OnPropertyChanged("AllConversations");
            }
        }


        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string ProfilePictureRGB
        {
            get => profilePictureRGB;
            set
            {
                profilePictureRGB = value;
                OnPropertyChanged("ProfilePictureRGB");
            }
        }

        public string Initials
        {
            get => initials;
            set
            {
                initials = value;
                OnPropertyChanged("Initials");
            }
        }

        public static User User
        {
            get; set;
        }

        private User selectedUser;
        public User SelectedUser
        {
            get => selectedUser;
            set
            {
                if (value != null)
                {
                    selectedUser = value;
                    SelectedUserId = value.Id;
                    OnPropertyChanged("SelectedUser");
                }
            }
        }

        private int selecteduserid;

        public int SelectedUserId
        {
            get => selecteduserid;
            set
            {
                selecteduserid = value;
                IsVisible = "Visible";

                if (allConversations.ContainsKey(value))
                {
                    CurrentConversation = AllConversations[value];
                }
                else
                {
                    allConversations.Add(value, new ThreadSafeObservableCollection<Message>());
                    CurrentConversation = AllConversations[value];
                }

                OnPropertyChanged("SelectedUserId");
            }
        }
        public static Channel Channel { get; set; }

        public static AsyncDuplexStreamingCall<Message, Message> CurrentRegister { get; set; }

        private static ChatViewModel Instance = null;

        public static ChatViewModel InstanceReturned
        {
            get => Instance;
        }

        public static ChatViewModel GetInstance()
        {
            if (Instance == null)
            {
                Instance = new ChatViewModel();
            }
            return Instance;
        }

        private async void RegisterTask(User me)
        {
                var connect = new Generated.ConnectService.ConnectServiceClient(channel);

                var response = await connect.ConnectAsync(new Generated.ConnectRequest
                {
                    User = me
                });


                me.Id = response.Id;
                CurrentUser = me;


                var register = connect.Register();
                await register.RequestStream.WriteAsync(new Message { Sender = me });

                User = me;
                CurrentRegister = register;
                Channel = channel;
                Thread listenForMessage = new Thread(new ThreadStart(ListenForMessages));
                listenForMessage.Start();
                ConnectedUsers = new ThreadSafeObservableCollection<User>();
                AllConversations = new ObservableDictionary<int, ThreadSafeObservableCollection<Message>>();
                var stub = new Generated.UserListService.UserListServiceClient(Channel);
                usersStream = stub.GetUsers();
                await usersStream.RequestStream.WriteAsync(User);
                Thread listenForUsers = new Thread(new ThreadStart(GetUsersFromServer));
                listenForUsers.Start();
        }

        private ChatViewModel()
        {
            IsVisible = "Hidden";
            string color = RandomColor.generateColor();
            setInitials();
            User me = new User { Id = 0, Name = RegisterName, Initials = Initials, ProfilePictureRGB = color };
            Thread registerTask = new Thread(() => RegisterTask(me));
            registerTask.Start();
        }

        ~ChatViewModel()
        {
            Disconnect();
        }
        void ListenForMessages()
        {
            var readTask = Task.Run(() =>
            {
                while (CurrentRegister.ResponseStream.MoveNext(cancellationToken: CancellationToken.None).Result)
                {
                    var senderId = CurrentRegister.ResponseStream.Current.Sender.Id;
                    if (allConversations.ContainsKey(senderId))
                    {
                        allConversations[senderId].Add(CurrentRegister.ResponseStream.Current);
                    }
                }
            });
        }

        ThreadSafeObservableCollection<User> connectedUserDeepCopy()
        {
            ThreadSafeObservableCollection<User> connectedUsers = new ThreadSafeObservableCollection<User>();
            foreach (User user in ConnectedUsers)
                connectedUsers.Add(user.Clone());
            return connectedUsers;
        }

        void filterUsers()
        {
            if (SearchContent != null && SearchContent != String.Empty)
            {
                FilteredConnectedUsers.Clear();
                foreach (User user in connectedUsers)
                    if (user.Name.Contains(SearchContent))
                        FilteredConnectedUsers.Add(user.Clone());
            }
            else
            {
                FilteredConnectedUsers = connectedUserDeepCopy();
            }
        }

        private void selectGeneralChat()
        {
            SelectedUser = ConnectedUsers[0];
            SelectedUser = CurrentUser;
            SelectedUser = ConnectedUsers[0];
        }

        void GetUsersFromServer()
        {
            var readTask = Task.Run(() =>
            {
                while (usersStream.ResponseStream.MoveNext(cancellationToken: CancellationToken.None).Result)
                {
                    if (usersStream.ResponseStream.Current.Id >= 0 && usersStream.ResponseStream.Current.Id != CurrentUser.Id)
                    {
                        ConnectedUsers.Add(usersStream.ResponseStream.Current);
                        allConversations.Add(usersStream.ResponseStream.Current.Id, new ThreadSafeObservableCollection<Message>());
                        filterUsers();
                        Name = usersStream.ResponseStream.Current.Name;
                        if (SelectedUser == null)
                            selectGeneralChat();
                    }
                    else
                    {
                        usersStream.ResponseStream.Current.Id = -usersStream.ResponseStream.Current.Id;
                        ConnectedUsers.Remove(usersStream.ResponseStream.Current);
                    }
                }
            });
        }
        public async void Disconnect()
        {
            try
            {
                CurrentRegister.RequestStream.CompleteAsync().Wait();
                usersStream.RequestStream.CompleteAsync().Wait();
                var disconnect = new Generated.DisconnectService.DisconnectServiceClient(Channel);
                await disconnect.DisconnectAsync(new DisconnectRequest { User = User });
                Channel.ShutdownAsync().Wait();
            }
            catch (System.InvalidOperationException)
            {
                return;
            }
        }

        public ICommand Send
        {
            get
            {
                return new ParammeterCommand(async () =>
                {
                    if (SelectedUser != null)
                    {
                        User speakTo = new User { Id = SelectedUser.Id, Name = SelectedUser.Name };
                        if (allConversations.ContainsKey(speakTo.Id))
                        {
                            allConversations[speakTo.Id].Add(new Message { Sender = User, Receiver = speakTo, Content = Message, Timestamp = DateTime.Now.ToString() });
                        }
                        var message = Message;
                        var messenger = new ChatService.ChatServiceClient(Channel);
                        await messenger.SendMessageAsync(new Message { Sender = User, Receiver = speakTo, Content = message, Timestamp = DateTime.Now.ToString() });
                        Message = string.Empty;
                        new SoundPlayer("../../Resources/MessageSentSound.wav").Play();
                        return;
                    }
                });
            }
        }

        private void setInitials()
        {
            string[] splittedName = RegisterName.Split(' ');

            if (splittedName.Length == 1)
            {
                Initials = splittedName[0][0].ToString().ToUpper();
                return;
            }

            Initials = splittedName[0][0].ToString().ToUpper() + splittedName[1][0].ToString().ToUpper();
        }
    }

}
