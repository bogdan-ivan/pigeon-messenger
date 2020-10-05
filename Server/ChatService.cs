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
    class ChatService : Generated.ChatService.ChatServiceBase
    {
        public override Task<Empty> SendMessage(Message request, ServerCallContext context)
        {
            ServerLogic.Instance.SendMessage(request);
            ServerLogic.logger.Information($"{request.Sender.Name} sent a message to {request.Receiver.Name}");
            return Task.FromResult(new Empty());
        }
    }
}
