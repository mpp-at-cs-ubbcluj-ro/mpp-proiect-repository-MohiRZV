package controller;

import domain.Account;
import domain.Festival;
import domain.validators.ValidationException;
import javafx.collections.FXCollections;
import javafx.fxml.FXML;
import javafx.scene.control.ChoiceBox;
import javafx.scene.control.TextArea;
import javafx.scene.control.TextField;
import service.MainPageService;
import service.TicketService;
import utils.AlertDisplayer;

import java.util.ArrayList;

public class SellTicketController {
    private MainPageService mainPageService;
    private Account account;
    private Festival festival;

    public void setServices(Account account, MainPageService mainPageService,Festival festival) {
        this.account=account;
        this.mainPageService = mainPageService;
        this.festival=festival;
        init();
    }
    @FXML
    TextArea textAreaFestival;

    @FXML
    ChoiceBox<Long> choiceBoxNumarBilete;
    private void init(){
        textAreaFestival.setText(festival.toString());
        long leftTickets= festival.getSeats()-mainPageService.getSoldSeats(festival.getId());
        ArrayList<Long> arrayList=new ArrayList<>();
        for(long i=1;i<=leftTickets;i++)
            arrayList.add(i);
        choiceBoxNumarBilete.setItems(FXCollections.observableArrayList(arrayList));
    }

    @FXML
    TextField textFieldNumeCumparator;
    @FXML
    public void onBtnSell(){
        Long seats=choiceBoxNumarBilete.getValue();
        String client=textFieldNumeCumparator.getText();
        if(client.isEmpty())
        {
            AlertDisplayer.showErrorMessage(null,"Numele nu poate fi vid");
            return;
        }
        try {
            mainPageService.sellTicket(festival,seats,client);
        } catch (ValidationException e) {
            AlertDisplayer.showErrorMessage(null,e.getMessage());
        }
    }
}
