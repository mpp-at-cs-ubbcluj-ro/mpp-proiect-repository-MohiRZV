import controller.Controller;
import controller.MainPageController;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;
import service.IServices;

import java.io.IOException;
import java.rmi.registry.LocateRegistry;
import java.rmi.registry.Registry;
import java.util.Properties;


public class StartRMIClient extends Application {
    private static String defaultServer="localhost";
    public static void main(String[] args) {

        launch(args);

    }

    @Override
    public void start(Stage primaryStage) throws Exception {
        Properties clientProps=new Properties();
        try {
            clientProps.load(StartRMIClient.class.getResourceAsStream("/client.properties"));
            System.out.println("Client properties set. ");
            clientProps.list(System.out);
        } catch (IOException e) {
            System.err.println("Cannot find client.properties "+e);
            return;
        }

       /* if (System.getSecurityManager() == null) {
            System.setSecurityManager(new SecurityManager());
        }*/
        String name=clientProps.getProperty("rmi.serverID","Festivals");
        String serverIP=clientProps.getProperty("server.host",defaultServer);
        try {

            Registry registry = LocateRegistry.getRegistry(serverIP);
            IServices server = (IServices) registry.lookup(name);
            System.out.println("Obtained a reference to remote server");

            FXMLLoader loader = new FXMLLoader(
                    getClass().getClassLoader().getResource("views/loginView.fxml"));
            Parent root=loader.load();


            Controller ctrl =
                    loader.<Controller>getController();
            ctrl.setServer(server);




            FXMLLoader mloader = new FXMLLoader(
                    getClass().getClassLoader().getResource("views/mainPageView.fxml"));
            Parent croot=mloader.load();


            MainPageController chatCtrl =
                    mloader.<MainPageController>getController();
            chatCtrl.setServer(server);

            ctrl.setController(chatCtrl);
            ctrl.setParent(croot);

            ctrl.setStage(primaryStage);

            primaryStage.setTitle("Login Festival Tickets");
            primaryStage.setScene(new Scene(root));
            primaryStage.setWidth(800);
            primaryStage.show();

        } catch (Exception e) {
            System.err.println("Initialization  exception:"+e);
            e.printStackTrace();
        }
    }
}
