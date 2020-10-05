using Generated;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class DisconnectService : Generated.DisconnectService.DisconnectServiceBase
    {
        public override Task<Google.Protobuf.WellKnownTypes.Empty> Disconnect(DisconnectRequest request, ServerCallContext context)
        {
            ServerLogic.Instance.RemoveUser(request.User);
            ServerLogic.logger.Information($"User with name {request.User.Name} disconnected");
            ServerLogic.logger.Information($"Id {request.User.Id} is no longer in use");
            return Task.FromResult(new Google.Protobuf.WellKnownTypes.Empty());
        }
    }
}
