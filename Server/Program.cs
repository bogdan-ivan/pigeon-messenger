using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Server server = new Server(Configuration.HOST, Configuration.SERVER_PORT))
            {
                server.CloseServerAction = () => Console.ReadKey();
                server.Start();
            }
        }
    }
}
