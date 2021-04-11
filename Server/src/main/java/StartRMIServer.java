import repository.*;
import service.*;

import java.io.IOException;
import java.rmi.registry.LocateRegistry;
import java.rmi.registry.Registry;
import java.rmi.server.UnicastRemoteObject;
import java.util.Properties;

public class StartRMIServer {
    public static void main(String[] args) {

        Properties serverProps=new Properties();
        try {
            serverProps.load(StartRMIServer.class.getResourceAsStream("/server.properties"));
            System.out.println("Server properties set. ");
            serverProps.list(System.out);
        } catch (IOException e) {
            System.err.println("Cannot find server.properties "+e);
            return;
        }
        // UserRepository repo=new UserRepositoryMock();
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

//        if (System.getSecurityManager() == null) {
//            System.setSecurityManager(new SecurityManager());
//        }

        try {
            LocateRegistry.createRegistry(1099);
            String name = serverProps.getProperty("rmi.serverID","Festivals");
            IServices stub =(IServices) UnicastRemoteObject.exportObject(services, 0);
            Registry registry = LocateRegistry.getRegistry();
            System.out.println("before binding");
            registry.rebind(name, stub);
            System.out.println("server bound");
        } catch (Exception e) {
            System.err.println("server exception:"+e);
            e.printStackTrace();
        }

    }

}
