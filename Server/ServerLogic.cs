using Generated;
using Grpc.Core;
using System;
using System.Collections.Generic;
using Serilog;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog.Core;

namespace Server
{

    class ServerLogic
    {
        public static ServerLogic Instance = new ServerLogic();
        public static Logger logger;

        private User generalChat;
        private static int AutoIncrement = 2;
        private List<User> users;
        private Dictionary<int, IServerStreamWriter<Message>> streams;
        private Dictionary<int, IServerStreamWriter<User>> subscribers;

        public List<User> Users { get { return users; } }

        public ServerLogic()
        {
            users = new List<User>();
            streams = new Dictionary<int, IServerStreamWriter<Message>>();
            subscribers = new Dictionary<int, IServerStreamWriter<User>>();
            generalChat = new User { Id = 1, Initials = "GC", LastMessage = "", Name = "General Chat", ProfilePictureRGB = "029adb" };
            logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
            AddUser(generalChat);
        }

        /// <summary>
        /// Adds a user to the active users list
        /// </summary>
        /// <param name="user">The user to be added</param>
        /// <returns></returns>
        public int AddUser(User user)
        {
            user.Id = AutoIncrement++;
            users.Add(user);
            NotifySubscribers(user, true);
            return user.Id;
        }

        /// <summary>
        /// Registers a users write stream to make him able to receive messages from other users
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <param name="stream">Write stream of the user</param>
        public void RegisterUser(int id, IServerStreamWriter<Message> stream)
        {
            try
            {
                streams.Add(id, stream);
            }
            catch (Exception e)
            {
                logger.Error("ERROR WHILE REGISTER USER!\n" + e.Message);
            }
        }

        /// <summary>
        /// Removes a user from the active users list
        /// </summary>
        /// <param name="user">The user to be removed</param>
        public void RemoveUser(User user)
        {
            NotifySubscribers(user, false);
            try
            {
                users.Remove(user);
                logger.Information($"{user.Name} has been deleted");
            }
            catch (NotSupportedException e)
            {
                logger.Error($"Couldn't delete user {user.Name}\n" + e.Message);
            }
            try
            {
                streams.Remove(user.Id);
                logger.Information($"{user.Name} message stream has been closed");
            }
            catch (ArgumentNullException e)
            {
                logger.Error($"Couldn't close user {user.Name} message stream\n" + e.Message);
            }
            try
            {
                subscribers.Remove(user.Id);
                logger.Information($"{user.Name} has been unsubscribed from the user list");
            }
            catch (ArgumentNullException e)
            {
                logger.Error($"Couldn't unsubscribe {user.Name} from the user list\n" + e.Message);
            }
        }

        /// <summary>
        /// Subscribes an user to be notified about new connections
        /// </summary>
        /// <param name="user">User to be subscribed</param>
        /// <param name="stream">Writing stream</param>
        public void SubscribeUser(User user, IServerStreamWriter<User> stream)
        {
            try
            {
                subscribers.Add(user.Id, stream);
            }
            catch (Exception e)
            {
                logger.Error($"Couldn't subscribe user with id {user.Id}\n" + e.Message);
            }
        }

        /// <summary>
        /// Notifies all subscribed users about a new user connection
        /// </summary>
        /// <param name="user">New user that connected</param>
        /// <param name="active">True if connected; False if disconnected</param>
        public void NotifySubscribers(User user, bool active)
        {
            var originalId = user.Id;
            if (active)
            {
                logger.Information($"All subscribers have been notified about {user.Name}'s connection");
            }
            else
            {
                user.Id = -user.Id;
                logger.Information($"All subscribers have been notified about {user.Name}'s disconnection");
            }
            foreach (var subscriber in subscribers)
            {
                try
                {
                    subscriber.Value.WriteAsync(user);
                }
                catch (Exception)
                {
                    logger.Warning($"User with id {subscriber.Key} doesn't need to be notified");
                }
            }
            user.Id = originalId;
        }

        /// <summary>
        /// Redirects a message to the right receiver
        /// </summary>
        /// <param name="message">Message to be sent</param>
        public void SendMessage(Message message)
        {
            try
            {
                IServerStreamWriter<Message> receiverStream;
                if (message.Receiver.Id != generalChat.Id)
                {
                    streams.TryGetValue(message.Receiver.Id, out receiverStream);
                    receiverStream.WriteAsync(message);
                }
                else
                {
                    var originalSenderId = message.Sender.Id;
                    message.Content = "*" + message.Sender.Name + "* : " + message.Content;
                    message.Sender = generalChat;
                    for (int i = 0; i < users.Count; i++)
                    {
                       if(users[i].Id != originalSenderId && users[i].Id != generalChat.Id)
                        {
                            message.Receiver = users[i];
                            streams.TryGetValue(users[i].Id, out receiverStream);
                            receiverStream.WriteAsync(message);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("Error while sending message!" + e.Message);
            }
        }

    }
}
