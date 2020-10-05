using System;
using System.Collections.Generic;
using System.Linq;

namespace Server
{
    class Server : IDisposable
    {
        public Grpc.Core.Server GrpcServer { get; private set; }

        public Action CloseServerAction { get; set; }

        public IEnumerable<Grpc.Core.ServerServiceDefinition> Services
        {
            get
            {
                yield return Generated.ConnectService.BindService(new ConnectService());
                yield return Generated.DisconnectService.BindService(new DisconnectService());
                yield return Generated.EchoClient.BindService(new EchoClient());
                yield return Generated.ChatService.BindService(new ChatService());
                yield return Generated.UserListService.BindService(new UserListService());
            }
        }

        public Server(string host, int port)
        {
            GrpcServer = new Grpc.Core.Server()
            {
                Ports = { new Grpc.Core.ServerPort(host, port, Grpc.Core.ServerCredentials.Insecure) }
            };

            LoadServices();
        }

        public void Start()
        {
            GrpcServer.Start();

            ServerLogic.logger.Information(string.Format("Server started ({0}:{1}).", Configuration.HOST, Configuration.SERVER_PORT));
        }

        private void LoadServices()
        {
            Services.ToList().ForEach(service => GrpcServer.Services.Add(service));
        }

        public void Dispose()
        {
            CloseServerAction.Invoke();
            GrpcServer.ShutdownAsync().Wait();
            var port = GrpcServer.Ports.FirstOrDefault();
            ServerLogic.logger.Information("Server closed ({0}:{1}).", Configuration.HOST, Configuration.SERVER_PORT);
        }
    }
}
