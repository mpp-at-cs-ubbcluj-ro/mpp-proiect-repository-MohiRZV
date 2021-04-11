package service;

import domain.Account;
import domain.FestivalDTO;

import java.rmi.Remote;
import java.rmi.RemoteException;
import java.sql.Date;

public interface IServices extends Remote {
    Account login(Account user,IObserver client) throws ServiceException, BadCredentialsException, RemoteException;
    Iterable<FestivalDTO> searchByDate(Date date) throws ServiceException, RemoteException;
    void sellTicket(Integer festivalID, Long seats, String client) throws ServiceException, RemoteException;
    void logout(Account user, IObserver client) throws ServiceException, RemoteException;
}
