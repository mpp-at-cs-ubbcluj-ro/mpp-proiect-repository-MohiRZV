package protobuffprotocol;


import domain.Account;
import domain.FestivalDTO;
import domain.TicketDTO;

import java.sql.Date;
import java.util.Arrays;

public class ProtoUtils {
    public static String getError(FestivalProtocol.Response response) {
        String errorMessage = response.getError();
        return errorMessage;
    }

    public static FestivalProtocol.Request createLoginRequest(Account user) {
        FestivalProtocol.Account acc = FestivalProtocol.Account.newBuilder().setUsername(user.getUsername()).setPassword(user.getPassword()).build();
        FestivalProtocol.Request request= FestivalProtocol.Request.newBuilder().setType(FestivalProtocol.Request.Type.Login)
                .setUser(acc).build();
        return request;
    }

    public static FestivalProtocol.Request createLogoutRequest(Account user) {
        FestivalProtocol.Account acc = FestivalProtocol.Account.newBuilder().setUsername(user.getUsername()).setPassword(user.getPassword()).build();
        FestivalProtocol.Request request= FestivalProtocol.Request.newBuilder().setType(FestivalProtocol.Request.Type.Logout)
                .setUser(acc).build();
        return request;
    }

    public static Account getUser(FestivalProtocol.Response response) {
        Account user=new Account(response.getUser().getUsername(), response.getUser().getPassword(), response.getUser().getName());
        return user;
    }

    public static Account getUser(FestivalProtocol.Request request){
        Account user=new Account(request.getUser().getUsername(), request.getUser().getPassword(), request.getUser().getName());
        return user;
    }
    public static FestivalProtocol.Response createOkResponse() {
        FestivalProtocol.Response response=FestivalProtocol.Response.newBuilder()
                .setType(FestivalProtocol.Response.Type.Ok).build();
        return response;
    }

    public static FestivalProtocol.Response createErrorResponse(String message) {
        FestivalProtocol.Response response=FestivalProtocol.Response.newBuilder()
                .setType(FestivalProtocol.Response.Type.Error)
                .setError(message).build();
        return response;
    }

    public static FestivalProtocol.Response createLoginResponse(Account account) {
        FestivalProtocol.Account user=FestivalProtocol.Account.newBuilder()
                .setUsername(account.getUsername()).setName(account.getName()).setPassword(account.getPassword()).build();
        FestivalProtocol.Response response=  FestivalProtocol.Response.newBuilder()
                .setType( FestivalProtocol.Response.Type.Login)
                .setUser(user).build();
        return response;
    }

    public static Iterable<FestivalDTO> getFestivals(FestivalProtocol.Response response) {
        FestivalDTO[] festivalDTOS = new FestivalDTO[response.getFestivalsCount()];
        for(int i=0;i<response.getFestivalsCount();i++){
            FestivalProtocol.FestivalDTO festivalDTO=response.getFestivals(i);
            FestivalDTO festival = new FestivalDTO(festivalDTO.getFestivalID(),festivalDTO.getName(), Date.valueOf(festivalDTO.getDate().split(" ")[0]),festivalDTO.getLocation(), festivalDTO.getSeats(),festivalDTO.getSoldSeats());
            festivalDTOS[i]=festival;
        }
        return Arrays.asList(festivalDTOS.clone());
    }

    public static FestivalProtocol.Request createFestivalRequest(Date date) {
        FestivalProtocol.Request request= FestivalProtocol.Request.newBuilder().setType(FestivalProtocol.Request.Type.SearchByDate)
                .setDate(date.toString()).build();
        return request;
    }
    public static FestivalProtocol.Request createFestivalRequest() {
        FestivalProtocol.Request request= FestivalProtocol.Request.newBuilder().setType(FestivalProtocol.Request.Type.SearchByDate)
                .build();
        return request;
    }

    public static FestivalProtocol.Response createFestivalResponse(Iterable<FestivalDTO> festivalDTOS) {
        FestivalProtocol.Response.Builder response= FestivalProtocol.Response.newBuilder()
                .setType(FestivalProtocol.Response.Type.SearchByDate);
        for (FestivalDTO festivalDTO: festivalDTOS){
            FestivalProtocol.FestivalDTO festival=FestivalProtocol.FestivalDTO.newBuilder()
                    .setFestivalID(festivalDTO.getFestivalID()).setDate(festivalDTO.getDate().toString())
                    .setLocation(festivalDTO.getLocation()).setName(festivalDTO.getName())
                    .setSeats(festivalDTO.getSeats()).setSoldSeats(festivalDTO.getSoldSeats()).build();
            response.addFestivals(festival);
        }

        return response.build();
    }

    public static FestivalProtocol.Request createSellTicketRequest(TicketDTO ticketDTO) {
        FestivalProtocol.TicketDTO ticket = FestivalProtocol.TicketDTO.newBuilder().setFestivalID(ticketDTO.getFestivalID())
                .setClient(ticketDTO.getClient()).setSeats(ticketDTO.getSeats()).build();
        FestivalProtocol.Request request= FestivalProtocol.Request.newBuilder().setType(FestivalProtocol.Request.Type.SellTicket)
                .setTicket(ticket).build();
        return request;
    }

    public static TicketDTO getTicket(FestivalProtocol.TicketDTO ticket) {
        TicketDTO ticketDTO = new TicketDTO(ticket.getFestivalID(),ticket.getSeats(),ticket.getClient());
        return  ticketDTO;
    }

    public static FestivalProtocol.Response createTicketSoldResponse(TicketDTO ticket) {
        FestivalProtocol.TicketDTO ticketDTO = FestivalProtocol.TicketDTO.newBuilder().setFestivalID(ticket.getFestivalID())
                .setClient(ticket.getClient()).setSeats(ticket.getSeats()).build();
        FestivalProtocol.Response response=FestivalProtocol.Response.newBuilder()
                .setType(FestivalProtocol.Response.Type.SellTicket).setTicket(ticketDTO).build();
        return response;
    }
}
