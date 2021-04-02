using System;
using System.Net.Sockets;

using System.Threading;
using LabMPP.repository;
using LabMPP.service;
using network.client;
using ServerTemplate;
using services;

namespace server
{
    class StartServer
    {
        static void Main(string[] args)
        {
            
            AccountRepo userRepo=new AccountRepo();
            ArtistRepo artistRepo=new ArtistRepo();
            FestivalRepo festivalRepo=new FestivalRepo();
            TicketRepo ticketRepo=new TicketRepo();

            LoginService userService=new LoginService(userRepo);
            ArtistService artistService=new ArtistService(artistRepo);
            FestivalService festivalService=new FestivalService(festivalRepo,artistRepo);
            TicketService ticketService=new TicketService(ticketRepo,festivalRepo);
            MainPageService mainPageService=new MainPageService(artistService,festivalService,ticketService);

            LoginService loginService=new LoginService(new AccountRepo());
            IServices services=new ServerImpl(loginService, mainPageService);

           // IChatServer serviceImpl = new ChatServerImpl();
			SerialServer server = new SerialServer("127.0.0.1", 55555, services);
            server.Start();
            Console.WriteLine("Server started ...");
            //Console.WriteLine("Press <enter> to exit...");
            Console.ReadLine();
            
        }
    }

    public class SerialServer: ConcurrentServer 
    {
        private IServices server;
        private ClientWorker worker;
        public SerialServer(string host, int port, IServices server) : base(host, port)
            {
                this.server = server;
                Console.WriteLine("SerialServer...");
        }
        protected override Thread createWorker(TcpClient client)
        {
            worker = new ClientWorker(server, client);
            return new Thread(new ThreadStart(worker.run));
        }
    }
    
}
