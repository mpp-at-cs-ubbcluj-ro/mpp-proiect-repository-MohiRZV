package service;

import domain.TicketDTO;

import java.rmi.Remote;
import java.rmi.RemoteException;

public interface IObserver {
    void ticketsSold(TicketDTO ticket) throws ServiceException;
}
