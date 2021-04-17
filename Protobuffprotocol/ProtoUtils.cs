using System;
using System.Collections;
using System.Collections.Generic;
using LabMPP.domain;

using Protobuffprotocol;
using FestivalDTO = LabMPP.domain.FestivalDTO;
using proto=Protobuffprotocol;
namespace protobuf
{
    public class ProtoUtils
    {
        public static String getError( proto.Response response) {
        String errorMessage = response.Error;
        return errorMessage;
    }

    public static Request createLoginRequest(LabMPP.domain.Account user)
    {
        proto.Account acc = new proto.Account {Username = user.id,Password = user.password,Name = user.name};
        Request request = new proto.Request {Type = proto.Request.Types.Type.Login, User = acc};
        return request;
    }

    public static  proto.Request createLogoutRequest(LabMPP.domain.Account user) {
        proto.Account acc = new proto.Account {Username = user.id,Password = user.password,Name = user.name};
        Request request = new proto.Request {Type = proto.Request.Types.Type.Login, User = acc};
        return request;
    }

    public static LabMPP.domain.Account getUser( proto.Response response) {
        LabMPP.domain.Account user=new LabMPP.domain.Account(response.User.Username, response.User.Password, response.User.Name);
        return user;
    }

    public static LabMPP.domain.Account getUser( proto.Request request){
        LabMPP.domain.Account user=new LabMPP.domain.Account(request.User.Username, request.User.Password, request.User.Name);
        return user;
    }
    public static  proto.Response createOkResponse()
    {
        proto.Response response = new proto.Response {Type = proto.Response.Types.Type.Ok};
        return response;
    }

    public static  proto.Response createErrorResponse(String message) {
        proto.Response response= new proto.Response {Type = proto.Response.Types.Type.Error};
        return response;
    }

    public static  proto.Response createLoginResponse(LabMPP.domain.Account account) {
         proto.Account user= new proto.Account {Username = account.id,Password = account.password,Name = account.name};
         proto.Response response=   new proto.Response {Type = proto.Response.Types.Type.Login, User = user};
         
        return response;
    }

    public static IEnumerable<LabMPP.domain.FestivalDTO> getFestivals( proto.Response response) {
        LabMPP.domain.FestivalDTO[] festivalDTOS = new LabMPP.domain.FestivalDTO[response.Festivals.Count];
        for(int i=0;i<response.Festivals.Count;i++)
        {
            proto.FestivalDTO festivalDTO = response.Festivals[i];
             LabMPP.domain.FestivalDTO festival = new LabMPP.domain.FestivalDTO(festivalDTO.Name, DateTime.Parse(festivalDTO.Date),TimeSpan.Zero, festivalDTO.Location, (int)festivalDTO.Seats,(int)festivalDTO.SoldSeats, (int)festivalDTO.FestivalID);
            festivalDTOS[i]=festival;
        }

        return festivalDTOS;
    }

    public static  proto.Request createFestivalRequest(DateTime date) {
         proto.Request request= new proto.Request {Type = proto.Request.Types.Type.SearchByDate, Date = date.ToString("yyyy-MM-dd")};  
        return request;
    }
    public static  proto.Request createFestivalRequest() {
         proto.Request request=  new proto.Request {Type = proto.Request.Types.Type.SearchByDate};  
        return request;
    }

    public static  proto.Response createFestivalResponse(IEnumerable<FestivalDTO> festivalDTOS) {
         proto.Response response=  new proto.Response {Type = proto.Response.Types.Type.SearchByDate};
         foreach (var festivalDTO in festivalDTOS)
         {
             proto.FestivalDTO festival = new proto.FestivalDTO
             {
                 FestivalID = festivalDTO.Id, Date = festivalDTO.Data.ToString("yyyy-MM-dd"), Name = festivalDTO.Name,
                 Location = festivalDTO.Location, Seats = festivalDTO.AvailableSeats, SoldSeats = festivalDTO.SoldSeats
             };
             response.Festivals.Add(festival);
         }

         return response;
    }

    public static  proto.Request createSellTicketRequest(LabMPP.domain.TicketDTO ticketDTO)
    {
        proto.TicketDTO ticket = new proto.TicketDTO
            {Client = ticketDTO.client, Seats = ticketDTO.seats, FestivalID = ticketDTO.festivalID};
        proto.Request request = new Request {Type = Request.Types.Type.SellTicket, Ticket = ticket};
        return request;
    }

    public static LabMPP.domain.TicketDTO getTicket( proto.TicketDTO ticket) {
        LabMPP.domain.TicketDTO ticketDTO = new LabMPP.domain.TicketDTO(ticket.FestivalID,ticket.Seats,ticket.Client);
        return  ticketDTO;
    }

    public static  proto.Response createTicketSoldResponse(LabMPP.domain.TicketDTO ticket) {
        proto.TicketDTO ticketDTO = new proto.TicketDTO
            {Client = ticket.client, Seats = ticket.seats, FestivalID = ticket.festivalID};
         proto.Response response= new Response {Type = Response.Types.Type.SellTicket, Ticket = ticketDTO};
        return response;
    }
    }
}