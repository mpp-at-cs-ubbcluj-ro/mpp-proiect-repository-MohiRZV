package controller;

import domain.Account;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.TextField;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Modality;
import javafx.stage.Stage;
import service.*;
import utils.AlertDisplayer;

import java.io.IOException;

public class Controller {

    LoginService loginService;
    ArtistService artistService;
    CustomerService customerService;
    FestivalService festivalService;
    TicketService ticketService;
    EmployeeService employeeService;

    public void setService(LoginService loginService, ArtistService artistService, CustomerService customerService, FestivalService festivalService, TicketService ticketService, EmployeeService employeeService){
        this.loginService=loginService;
        this.artistService = artistService;
        this.customerService = customerService;
        this.festivalService = festivalService;
        this.ticketService = ticketService;
        this.employeeService=employeeService;
    }
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
            account=loginService.getAccount(username,password);
        } catch (BadCredentialsException e) {
            AlertDisplayer.showErrorMessage(null,e.getMessage());
            return;
        }
        connect(account);
    }
    @FXML
    public void onBtnLoginSU(){
        connectAsSU();
    }
    private void connectAsSU(){
        FXMLLoader loader = new FXMLLoader();
        loader.setLocation(getClass().getResource("/views/manageView.fxml"));


        AnchorPane root = null;
        try {
            root = (AnchorPane) loader.load();
        } catch (IOException ioException) {
            ioException.printStackTrace();
        }

        Stage dialogStage = new Stage();
        dialogStage.setTitle("User GUI");
        dialogStage.initModality(Modality.WINDOW_MODAL);
        Scene scene = new Scene(root);
        dialogStage.setScene(scene);

        ManagementController controller= loader.getController();
        MainPageService pageService=new MainPageService(customerService,artistService,festivalService,ticketService,employeeService);
        controller.setServices(pageService);
        dialogStage.show();

    }
    private void connect(Account account){
        FXMLLoader loader = new FXMLLoader();
        loader.setLocation(getClass().getResource("/views/mainPageView.fxml"));


        AnchorPane root = null;
        try {
            root = (AnchorPane) loader.load();
        } catch (IOException ioException) {
            ioException.printStackTrace();
        }

        Stage dialogStage = new Stage();
        dialogStage.setTitle("User GUI");
        dialogStage.initModality(Modality.WINDOW_MODAL);
        Scene scene = new Scene(root);
        dialogStage.setScene(scene);

        MainPageController controller= loader.getController();
        MainPageService pageService=new MainPageService(customerService,artistService,festivalService,ticketService,employeeService);
        controller.setServices(account,pageService);
        dialogStage.show();
    }
}
