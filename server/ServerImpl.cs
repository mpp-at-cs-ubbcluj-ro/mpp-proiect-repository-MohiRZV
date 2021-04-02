using System.Collections.Generic;
using System;
using LabMPP.domain;
using LabMPP.domain.Validators;
using LabMPP.service;
using services;

namespace server
{
    public class ServerImpl: IServices
    {

    private LoginService loginService;
    private MainPageService mainPageService;
    private readonly IDictionary <String, IObserver> loggedClients;

    public ServerImpl(LoginService loginService, MainPageService mainPageService) {
        this.loginService=loginService;
        this.mainPageService=mainPageService;
        loggedClients=new Dictionary<String, IObserver>();
    }

    
    public Account login(Account user, IObserver client)
    {
        Account userOk=loginService.getAccount(user.id,user.password);
        if (userOk!=null){
            if(loggedClients.ContainsKey(user.id))
                throw new ServiceException("User already logged in.");
            loggedClients[user.id]= client;
            return userOk;
        }else
            throw new ServiceException("Authentication failed.");
    }

    public IEnumerable<FestivalDTO> searchByDate(DateTime date)
    {
        return mainPageService.GetFestivalDTOs(date);
    }
    
    public IEnumerable<FestivalDTO> getAll()
    {
        return mainPageService.GetFestivalDTOs();
    }

    public void sellTicket(int festivalID, long seats, string client)
    {
        try {
            mainPageService.sellTicket(festivalID,seats,client);
            foreach (var loggedClientsValue in loggedClients.Values)
            {
                loggedClientsValue.ticketsSold(new TicketDTO(festivalID,seats,client));
            }
        } catch (ValidationException e) {
            throw new ServiceException(e.Message);
        }
    }

    public void logout(Account user, IObserver client)
    {
        IObserver localClient=loggedClients[user.id];
        if (localClient==null)
            throw new ServiceException("User "+user.id+" is not logged in.");
        loggedClients.Remove(user.id);
    }
    }
}
