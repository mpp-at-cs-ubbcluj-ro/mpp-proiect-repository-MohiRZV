package controller;

import domain.Account;
import domain.Customer;
import domain.Employee;
import domain.Festival;
import javafx.beans.property.SimpleLongProperty;
import javafx.beans.property.SimpleObjectProperty;
import javafx.beans.property.SimpleStringProperty;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.DatePicker;
import javafx.scene.control.Label;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Modality;
import javafx.stage.Stage;
import service.*;
import utils.AlertDisplayer;

import java.io.IOException;
import java.sql.Date;
import java.time.LocalDate;
import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.StreamSupport;

public class MainPageController {
    ObservableList<Festival> festivalModel = FXCollections.observableArrayList();
    ObservableList<Festival> festivalSModel = FXCollections.observableArrayList();
    private Account account;
    private MainPageService mainPageService;

    private Employee agent;
    public void setServices(Account account, MainPageService mainPageService) {
        this.account = account;
        this.mainPageService=mainPageService;
        init();

    }
    @FXML
    Label labelUser;
    //toti artistii
    @FXML
    TableView<Festival> tableViewArtist;
    @FXML
    TableColumn<Festival,String> tcArtistNume;
    @FXML
    TableColumn<Festival, Date> tcArtistDate;
    @FXML
    TableColumn<Festival, String> tcArtistLocatie;
    @FXML
    TableColumn<Festival, Long> tcArtistNrl;
    @FXML
    TableColumn<Festival, Long> tcArtistNrlo;

    //filtrare artisti
    @FXML
    TableView<Festival> tableViewSArtist;
    @FXML
    TableColumn<Festival,String> tcSArtistNume;
    @FXML
    TableColumn<Festival, Date> tcSArtistDate;
    @FXML
    TableColumn<Festival, String> tcSArtistLocatie;
    @FXML
    TableColumn<Festival, Long> tcSArtistNrl;
    @FXML
    TableColumn<Festival, Integer> tcSArtistOra;
    private void init(){
        agent=mainPageService.getAgent(account);
        labelUser.setText(agent.getName());
        initModelFestivals();
    }

    @FXML
    public void initialize(){
        tcArtistNume.setCellValueFactory(cell->new SimpleStringProperty(cell.getValue().getArtist().getName()));
        tcArtistDate.setCellValueFactory(new PropertyValueFactory<Festival,Date>("date"));
        tcArtistLocatie.setCellValueFactory(new PropertyValueFactory<>("location"));
        tcArtistNrl.setCellValueFactory(new PropertyValueFactory<>("seats"));
        tcArtistNrlo.setCellValueFactory(cell->{
            Long id=cell.getValue().getId();
            return new SimpleObjectProperty(mainPageService.getSoldSeats(id));
        });
        tableViewArtist.setItems(festivalModel);

        tcSArtistNume.setCellValueFactory(cell->new SimpleStringProperty(cell.getValue().getArtist().getName()));
        tcSArtistDate.setCellValueFactory(new PropertyValueFactory<Festival,Date>("date"));
        tcSArtistLocatie.setCellValueFactory(new PropertyValueFactory<>("location"));
        tcSArtistNrl.setCellValueFactory(new PropertyValueFactory<>("seats"));

        //tcSArtistOra.setCellValueFactory(new PropertyValueFactory<>("ora??"));
        tableViewSArtist.setItems(festivalSModel);
    }

    //TODO logout
    //TODO c# add dto - poate si aici?
    private void initModelFestivals(){
        Iterable<Festival> festivals=mainPageService.getFestivals();
        List<Festival> festivalList= StreamSupport.stream(festivals.spliterator(),false).collect(Collectors.toList());
        festivalModel.setAll(festivalList);
    }

    private void initModelSFestivals(Date date){
        Iterable<Festival> festivals=mainPageService.getFestivalsByDate(date);
        List<Festival> festivalList= StreamSupport.stream(festivals.spliterator(),false).collect(Collectors.toList());
        festivalSModel.setAll(festivalList);
    }

    @FXML
    DatePicker datePickerArtist;
    public void onBtnSearchByDate(ActionEvent actionEvent) {
        LocalDate date=datePickerArtist.getValue();
        initModelSFestivals(Date.valueOf(date));
    }


    @FXML
    public void onBtnSellTicket(){
        Festival festival=tableViewArtist.getSelectionModel().getSelectedItem();
        if(festival==null){
            AlertDisplayer.showErrorMessage(null,"Trebuie sa selectati un festival");
            return;
        }
        FXMLLoader loader = new FXMLLoader();
        loader.setLocation(getClass().getResource("/views/sellTicketView.fxml"));


        AnchorPane root = null;
        try {
            root = (AnchorPane) loader.load();
        } catch (IOException ioException) {
            ioException.printStackTrace();
        }

        Stage dialogStage = new Stage();
        dialogStage.setTitle("Sell ticket");
        dialogStage.initModality(Modality.WINDOW_MODAL);
        Scene scene = new Scene(root);
        dialogStage.setScene(scene);

        SellTicketController controller= loader.getController();
        controller.setServices(account,mainPageService,festival);
        dialogStage.show();
    }
}
