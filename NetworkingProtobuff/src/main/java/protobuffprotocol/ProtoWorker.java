package protobuffprotocol;


import domain.Account;
import domain.FestivalDTO;
import domain.TicketDTO;
import service.BadCredentialsException;
import service.IObserver;
import service.IServices;
import service.ServiceException;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.Socket;
import java.sql.Date;


public class ProtoWorker implements Runnable, IObserver {
    private IServices server;
     private Socket connection;

     private InputStream input;
     private OutputStream output;
     private volatile boolean connected;
     public ProtoWorker(IServices server, Socket connection) {
         this.server = server;
         this.connection = connection;
         try{
             output=connection.getOutputStream() ;//new ObjectOutputStream(connection.getOutputStream());
             input=connection.getInputStream(); //new ObjectInputStream(connection.getInputStream());
             connected=true;
         } catch (IOException e) {
             e.printStackTrace();
         }
     }

     public void run() {
         while(connected){
             try {
                // Object request=input.readObject();
                 System.out.println("Waiting requests ...");
                 FestivalProtocol.Request request= FestivalProtocol.Request.parseDelimitedFrom(input);
                 System.out.println("Request received: "+request);
                 FestivalProtocol.Response response=handleRequest(request);
                 if (response!=null){
                    sendResponse(response);
                 }
             } catch (IOException e) {
                 e.printStackTrace();
             }
             try {
                 Thread.sleep(1000);
             } catch (InterruptedException e) {
                 e.printStackTrace();
             }
         }
         try {
             input.close();
             output.close();
             connection.close();
         } catch (IOException e) {
             System.out.println("Error "+e);
         }
     }


     private FestivalProtocol.Response handleRequest(FestivalProtocol.Request request){
         FestivalProtocol.Response response=null;
         switch (request.getType()) {
             case Login: {
                 System.out.println("Login request ...");
                 Account user = ProtoUtils.getUser(request);
                 try {
                     Account account = server.login(user, this);
                     return ProtoUtils.createLoginResponse(account);
                 } catch (ServiceException | BadCredentialsException e) {
                     connected = false;
                     return ProtoUtils.createErrorResponse(e.getMessage());
                 }
             }

             case Logout: {
                 System.out.println("Logout request");
                 Account user = ProtoUtils.getUser(request);
                 try {
                     server.logout(user, this);
                     connected = false;
                     return ProtoUtils.createOkResponse();

                 } catch (ServiceException e) {
                     return ProtoUtils.createErrorResponse(e.getMessage());
                 }
             }

             case SearchByDate: {
                 System.out.println("Festivals request");
                 Date date = null;
                 if(!request.getDate().isEmpty())
                    date=Date.valueOf(request.getDate());
                 try {
                     Iterable<FestivalDTO>festivalDTOS = server.searchByDate(date);
                     return ProtoUtils.createFestivalResponse(festivalDTOS);

                 } catch (ServiceException e) {
                     return ProtoUtils.createErrorResponse(e.getMessage());
                 }
             }

             case SellTicket: {
                 System.out.println("Sell ticket request");
                 TicketDTO ticketDTO= ProtoUtils.getTicket(request.getTicket());
                 try {
                     server.sellTicket(ticketDTO.getFestivalID(), ticketDTO.getSeats(),ticketDTO.getClient());
                     return ProtoUtils.createOkResponse();
                 } catch (ServiceException e) {
                     return ProtoUtils.createErrorResponse(e.getMessage());
                 }
             }
         }

         return response;
     }

     private void sendResponse(FestivalProtocol.Response response) throws IOException{
         System.out.println("sending response "+response);
         response.writeDelimitedTo(output);
         //output.writeObject(response);
         output.flush();
     }

    @Override
    public void ticketsSold(TicketDTO ticket) throws ServiceException {
        System.out.println("Ticket sold "+ticket);
        try {
            sendResponse(ProtoUtils.createTicketSoldResponse(ticket));
        } catch (IOException e) {
            throw new ServiceException("Sending error: "+e);
        }
    }
}
