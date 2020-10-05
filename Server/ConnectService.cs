using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generated;
using Grpc.Core;

namespace Server
{
    internal class ConnectService : Generated.ConnectService.ConnectServiceBase
    {
        public override Task<ConnectResponse> Connect(ConnectRequest request, ServerCallContext context)
        {
            ServerLogic.logger.Information($"User with name {request.User.Name} connected");
            int response = ServerLogic.Instance.AddUser(request.User);
            ServerLogic.logger.Information($"He received the id {request.User.Id}");
            return Task.FromResult(new ConnectResponse { Id = response });
        }

        public override async Task Register(IAsyncStreamReader<Message> requestStream, IServerStreamWriter<Message> responseStream, ServerCallContext context)
        {
            if (!await requestStream.MoveNext())
            {
                return;
            }

            do
            {
                ServerLogic.Instance.RegisterUser(requestStream.Current.Sender.Id, responseStream);
                ServerLogic.logger.Information($"Registerd user {requestStream.Current.Sender.Name}");
            } while (await requestStream.MoveNext());
        }
    }
}
