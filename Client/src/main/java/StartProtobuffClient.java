import controller.Controller;
import controller.MainPageController;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;
import protobuffprotocol.ProtoProxy;
import service.IServices;

import java.io.IOException;
import java.util.Properties;

public class StartProtobuffClient extends Application {
    private static int defaultChatPort=55555;
    private static String defaultServer="localhost";

    public static void main(String[] args) {
        launch(args);

    }

    @Override
    public void start(Stage primaryStage) throws Exception {
        Properties clientProps=new Properties();
        try {
            clientProps.load(StartProtobuffClient.class.getResourceAsStream("/client.properties"));
            System.out.println("Client properties set. ");
            clientProps.list(System.out);
        } catch (IOException e) {
            System.err.println("Cannot find client.properties "+e);
            return;
        }
        String serverIP=clientProps.getProperty("server.host",defaultServer);
        int serverPort=defaultChatPort;
        try{
            serverPort=Integer.parseInt(clientProps.getProperty("server.port"));
        }catch(NumberFormatException ex){
            System.err.println("Wrong port number "+ex.getMessage());
            System.out.println("Using default port: "+defaultChatPort);
        }
        System.out.println("Using server IP "+serverIP);
        System.out.println("Using server port "+serverPort);

        IServices server=new ProtoProxy(serverIP, serverPort);
        FXMLLoader loader = new FXMLLoader(
                getClass().getClassLoader().getResource("views/loginView.fxml"));
        Parent root=loader.load();


        Controller ctrl =
                loader.<Controller>getController();
        ctrl.setServer(server);




        FXMLLoader mloader = new FXMLLoader(
                getClass().getClassLoader().getResource("views/mainPageView.fxml"));
        Parent croot=mloader.load();


        MainPageController mCtrl =
                mloader.<MainPageController>getController();
        mCtrl.setServer(server);

        ctrl.setController(mCtrl);
        ctrl.setParent(croot);

        ctrl.setStage(primaryStage);

        primaryStage.setTitle("Login Festival Tickets");
        primaryStage.setScene(new Scene(root));
        primaryStage.setWidth(800);
        primaryStage.show();
    }
}
