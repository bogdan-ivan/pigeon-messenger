using Generated;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class EchoClient : Generated.EchoClient.EchoClientBase
    {
        public override async Task Echo(IAsyncStreamReader<Message> requestStream, IServerStreamWriter<Message> responseStream, ServerCallContext context)
        {

            if (!await requestStream.MoveNext())
            {              
                return;
            }

            do
            {
                ServerLogic.logger.Information("Request stream current content: " + requestStream.Current.Content);
                if (!string.IsNullOrEmpty(requestStream.Current.Content))
                {
                    await responseStream.WriteAsync(requestStream.Current);
                }
            } while (await requestStream.MoveNext());
        }
    }
}
