package service;

import domain.Account;
import domain.FestivalDTO;

import java.rmi.Remote;
import java.sql.Date;

public interface IServices extends Remote {
    Account login(Account user,IObserver client) throws ServiceException, BadCredentialsException;
    Iterable<FestivalDTO> searchByDate(Date date) throws ServiceException;
    void sellTicket(Integer festivalID, Long seats, String client) throws ServiceException;
    void logout(Account user, IObserver client) throws ServiceException;
}
