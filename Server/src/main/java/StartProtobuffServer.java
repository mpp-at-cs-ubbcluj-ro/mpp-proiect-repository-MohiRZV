import protobuffprotocol.AbstractServer;
import protobuffprotocol.ProtobuffConcurrentServer;
import repository.*;
import service.*;

import java.io.IOException;
import java.util.Properties;


public class StartProtobuffServer {
    private static int defaultPort=55555;
    public static void main(String[] args) {


        Properties serverProps=new Properties();
        try {
            serverProps.load(StartRpcServer.class.getResourceAsStream("/server.properties"));
            System.out.println("Server properties set. ");
            serverProps.list(System.out);
        } catch (IOException e) {
            System.err.println("Cannot find server.properties "+e);
            return;
        }
        EmployeeRepo userRepo=new EmployeeRepo(serverProps);
        ArtistRepo artistRepo=new ArtistRepo(serverProps);
        FestivalRepo festivalRepo=new FestivalRepo(serverProps);
        TicketRepo ticketRepo=new TicketRepo(serverProps);

        EmployeeService userService=new EmployeeService(userRepo);
        ArtistService artistService=new ArtistService(artistRepo);
        FestivalService festivalService=new FestivalService(festivalRepo,artistRepo);
        TicketService ticketService=new TicketService(ticketRepo,festivalRepo);
        MainPageService mainPageService=new MainPageService(artistService,festivalService,ticketService,userService);

        LoginService loginService=new LoginService(new AccountRepo(serverProps));
        IServices services=new ServerImpl(loginService, mainPageService);
        int serverPort=defaultPort;
        try {
            serverPort = Integer.parseInt(serverProps.getProperty("server.port"));
        }catch (NumberFormatException nef){
            System.err.println("Wrong  Port Number"+nef.getMessage());
            System.err.println("Using default port "+defaultPort);
        }
        System.out.println("Starting server on port: "+serverPort);
        AbstractServer server = new ProtobuffConcurrentServer(serverPort, services);
        try {
            server.start();
        } catch (ServiceException e) {
            System.err.println("Error starting the server" + e.getMessage());
        }



    }
}
