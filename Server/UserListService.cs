using Generated;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class UserListService : Generated.UserListService.UserListServiceBase
    {
        public override async Task GetUsers(IAsyncStreamReader<User> requestStream, IServerStreamWriter<User> responseStream, ServerCallContext context)
        {
            var users = ServerLogic.Instance.Users;
            for (int i = 0; i < users.Count; ++i)
            {
                await responseStream.WriteAsync(users[i]);
            }

            if (!await requestStream.MoveNext())
            {
                return;
            }

            do
            {
                ServerLogic.logger.Information($"{requestStream.Current.Name} has received the list of users");
                ServerLogic.Instance.SubscribeUser(requestStream.Current, responseStream);
                ServerLogic.logger.Information($"{requestStream.Current.Name} has been subscribed to the list of users");
            } while (await requestStream.MoveNext());
 
        }
    }
}
