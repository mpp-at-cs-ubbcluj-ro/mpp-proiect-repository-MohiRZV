package controller;

import domain.Account;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.TextField;
import javafx.stage.Stage;
import javafx.stage.WindowEvent;
import service.BadCredentialsException;
import service.IServices;
import service.ServiceException;
import utils.AlertDisplayer;

import java.io.Serializable;
import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;

public class Controller extends UnicastRemoteObject implements Serializable {

    IServices server;

    private MainPageController ctrl;
    private Parent parent;
    private Stage thisStage;
    public Controller() throws RemoteException {
    }
    public void setStage(Stage stage){thisStage=stage; ctrl.setLoginStage(thisStage);}
    public void setParent(Parent parent){ this.parent=parent;}

    public void setServer(IServices server) {
        this.server = server;
    }

    public void setController(MainPageController controller){ this.ctrl=controller; }
    @FXML
    TextField textFieldUsername;
    @FXML
    TextField textFieldPassword;
    @FXML
    public void onBtnLogin(){
        String username=textFieldUsername.getText();
        String password=textFieldPassword.getText();

        System.out.println(username+" "+password);
        if(username.isEmpty() || password.isEmpty()){
            AlertDisplayer.showMessage(null, Alert.AlertType.ERROR,"Eroare!","Username sau parola gresita!");
        }
        Account account=null;
        try {
            account=server.login(new Account(username, password, null),ctrl);

        } catch (BadCredentialsException | ServiceException | RemoteException e) {
            AlertDisplayer.showErrorMessage(null,e.getMessage());
            return;
        }
        connect(account);
        textFieldPassword.getScene().getWindow().hide();
    }

    private Stage stage = new Stage();
    private Scene scene = null;
    private void connect(Account account){
        //stage.setTitle("Window for " +account.getName());
        if(scene == null)
            scene = new Scene(parent);
        stage.setScene(scene);

        stage.setOnCloseRequest(new EventHandler<WindowEvent>() {
            @Override
            public void handle(WindowEvent event) {
                ctrl.logout();

                //System.exit(0);
            }
        });

        stage.show();
        ctrl.setAccount(account);
        //((Node)(actionEvent.getSource())).getScene().getWindow().hide();
    }
}
