package protobuffprotocol;


import domain.Account;
import domain.FestivalDTO;
import domain.TicketDTO;
import service.IObserver;
import service.IServices;
import service.ServiceException;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.Socket;
import java.sql.Date;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;

public class ProtoProxy implements IServices {
    private String host;
      private int port;

      private IObserver client;

      private InputStream input;
      private OutputStream output;
      private Socket connection;

      private BlockingQueue<FestivalProtocol.Response> qresponses;
      private volatile boolean finished;
      public ProtoProxy(String host, int port) {
          this.host = host;
          this.port = port;
          qresponses=new LinkedBlockingQueue<FestivalProtocol.Response>();
      }

      public Account login(Account user, IObserver client) throws ServiceException {
          initializeConnection();
          sendRequest(ProtoUtils.createLoginRequest(user));
          FestivalProtocol.Response response=readResponse();
          if (response.getType()== FestivalProtocol.Response.Type.Login){
              this.client=client;
              return ProtoUtils.getUser(response);
          }
          if (response.getType()== FestivalProtocol.Response.Type.Error){
              String errorText=ProtoUtils.getError(response);
              closeConnection();
              throw new ServiceException(errorText);
          }
          return null;
      }

    @Override
    public Iterable<FestivalDTO> searchByDate(Date date) throws ServiceException {
        if(date==null)
            sendRequest(ProtoUtils.createFestivalRequest());
        else
            sendRequest(ProtoUtils.createFestivalRequest(date));
        FestivalProtocol.Response response=readResponse();
        if (response.getType()== FestivalProtocol.Response.Type.Error){
            String errorText=ProtoUtils.getError(response);
            closeConnection();
            throw new ServiceException(errorText);
        }
        if (response.getType()== FestivalProtocol.Response.Type.SearchByDate){
            return ProtoUtils.getFestivals(response);
        }
        return null;
    }

    @Override
    public void sellTicket(Integer festivalID, Long seats, String client) throws ServiceException {
        sendRequest(ProtoUtils.createSellTicketRequest(new TicketDTO(festivalID,seats,client)));
        FestivalProtocol.Response response=readResponse();
        if (response.getType()== FestivalProtocol.Response.Type.Error){
            String errorText=ProtoUtils.getError(response);
            closeConnection();
            throw new ServiceException(errorText);
        }
        System.out.println("Ticket sold - proxy");
    }

    public void logout(Account user, IObserver client) throws ServiceException {
        sendRequest(ProtoUtils.createLogoutRequest(user));
        FestivalProtocol.Response response=readResponse();
        closeConnection();
        if (response.getType()== FestivalProtocol.Response.Type.Error){
            String errorText=ProtoUtils.getError(response);
            throw new ServiceException(errorText);
        }
    }

      private void closeConnection() {
          finished=true;
          try {
              input.close();
              output.close();
              connection.close();
              client=null;
          } catch (IOException e) {
              e.printStackTrace();
          }

      }

      private void sendRequest(FestivalProtocol.Request request)throws ServiceException{
          try {
              System.out.println("Sending request ..."+request);
              //request.writeTo(output);
              request.writeDelimitedTo(output);
              output.flush();
              System.out.println("Request sent.");
          } catch (IOException e) {
              throw new ServiceException("Error sending object "+e);
          }

      }

      private FestivalProtocol.Response readResponse() throws ServiceException{
          FestivalProtocol.Response response=null;
          try{
              response=qresponses.take();

          } catch (InterruptedException e) {
              e.printStackTrace();
          }
          return response;
      }
      private void initializeConnection() throws ServiceException {
           try {
              connection=new Socket(host,port);
              output=connection.getOutputStream();
              //output.flush();
              input=connection.getInputStream();     //new ObjectInputStream(connection.getInputStream());
              finished=false;
              startReader();
          } catch (IOException e) {
              e.printStackTrace();
          }
      }
      private void startReader(){
          Thread tw=new Thread(new ReaderThread());
          tw.start();
      }


      private void handleUpdate(FestivalProtocol.Response updateResponse){

          if (updateResponse.getType().equals(FestivalProtocol.Response.Type.SellTicket)){
              TicketDTO ticketDTO=ProtoUtils.getTicket(updateResponse.getTicket());
              System.out.println("Some tickets were sold");
              try{
                  client.ticketsSold(ticketDTO);
              }catch (ServiceException e){
                  e.printStackTrace();
              }
          }

      }
      private class ReaderThread implements Runnable{
          public void run() {
              while(!finished){
                  try {
                      FestivalProtocol.Response response= FestivalProtocol.Response.parseDelimitedFrom(input);
                      System.out.println("response received "+response);

                      if (isUpdateResponse(response.getType())){
                           handleUpdate(response);
                      }else{
                          try {
                              qresponses.put(response);
                          } catch (InterruptedException e) {
                              e.printStackTrace();
                          }
                      }
                  } catch (IOException e) {
                      System.out.println("Reading error "+e);
                  }
              }
          }
      }

    private boolean isUpdateResponse(FestivalProtocol.Response.Type type){
        return type == FestivalProtocol.Response.Type.SellTicket;
    }
}
