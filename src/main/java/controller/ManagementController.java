package controller;

import domain.validators.ValidationException;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.scene.control.DatePicker;
import javafx.scene.control.TextField;
import service.MainPageService;
import utils.AlertDisplayer;

import java.sql.Date;

public class ManagementController {
    private MainPageService mainPageService;
    @FXML
    TextField textFieldArtistName;
    @FXML
    TextField textFieldArtistGenre;

    public void setServices(MainPageService mainPageService) {
        this.mainPageService = mainPageService;
    }

    public void onBtnAddArtist(ActionEvent actionEvent) {
        String name=textFieldArtistName.getText();
        String genre=textFieldArtistGenre.getText();
        try {
            mainPageService.addArtist(name,genre);
        } catch (ValidationException e) {
            AlertDisplayer.showErrorMessage(null,e.getMessage());
        }
    }
    @FXML
    TextField textFieldFestivalName;
    @FXML
    TextField textFieldFestivalGenre;
    @FXML
    TextField textFieldFestivalLocation;
    @FXML
    TextField textFieldFestivalSeats;
    @FXML
    TextField textFieldFestivalArtistID;
    @FXML
    DatePicker datePickerFestival;
    public void onBtnAddFestival(ActionEvent actionEvent) {
        Date date = null;
        String location=null;
        String name=null;
        String genre=null;
        Long seats=0l;
        Long aid=null;
        try {
            date = Date.valueOf(datePickerFestival.getValue());
            location = textFieldFestivalLocation.getText();
            name = textFieldFestivalName.getText();
            genre = textFieldFestivalGenre.getText();
            seats = Long.parseLong(textFieldFestivalSeats.getText());
            aid = Long.parseLong(textFieldFestivalArtistID.getText());
        }catch (Exception ex){
            AlertDisplayer.showErrorMessage(null,"Toate fieldurile trebuie sa fie completate!");
            return;
        }
        try {
            mainPageService.addFestival(date,location,name,genre,seats,aid);
        } catch (ValidationException e) {
            AlertDisplayer.showErrorMessage(null,e.getMessage());
        }
    }
}
