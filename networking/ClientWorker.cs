using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using LabMPP.domain;
using network.protocol;
using services;

namespace network.client
{
	
	public class ClientWorker :  IObserver 
	{
		private IServices server;
		private TcpClient connection;

		private NetworkStream stream;
		private IFormatter formatter;
		private volatile bool connected;
		public ClientWorker(IServices server, TcpClient connection)
		{
			this.server = server;
			this.connection = connection;
			try
			{
				
				stream=connection.GetStream();
                formatter = new BinaryFormatter();
				connected=true;
			}
			catch (Exception e)
			{
                Console.WriteLine(e.StackTrace);
			}
		}

		public virtual void run()
		{
			while(connected)
			{
				try
				{
                    object request = formatter.Deserialize(stream);
					object response =handleRequest((Request)request);
					if (response!=null)
					{
					   sendResponse((Response) response);
					}
				}
				catch (Exception e)
				{
                    Console.WriteLine(e.StackTrace);
				}
				
				try
				{
					Thread.Sleep(1000);
				}
				catch (Exception e)
				{
                    Console.WriteLine(e.StackTrace);
				}
			}
			try
			{
				stream.Close();
				connection.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error "+e);
			}
		}
		

		private Response handleRequest(Request request)
		{
			Response response =null;
			if (request is LoginRequest)
			{
				Console.WriteLine("Login request ...");
				LoginRequest logReq =(LoginRequest)request;
				Account user = logReq.User;
				try
                {
                    lock (server)
                    {
                        user=server.login(user, this);
                    }

                    return new LoginResponse(user);
                }
				catch (ServiceException e)
				{
					connected=false;
					return new ErrorResponse(e.Message);
				}
			}
			
			if (request is GetRequest)
			{
				Console.WriteLine("Get request ...");
				GetRequest getReq =(GetRequest)request;
				DateTime date = getReq.Date;
				try
				{
					GetResponse getResponse;
					lock (server)
					{
						if (date == default(DateTime))
							getResponse=new GetResponse(server.getAll());
						else
							getResponse=new GetResponse(server.searchByDate(date));
					}

					return getResponse;
				}
				catch (ServiceException e)
				{
					connected=false;
					return new ErrorResponse(e.Message);
				}
			}
			if (request is SellTicketRequest)
			{
				Console.WriteLine("Get request ...");
				SellTicketRequest sellReq =(SellTicketRequest)request;
				TicketDTO ticket = sellReq.TicketDto;
				try
				{
					lock (server)
					{
						server.sellTicket(ticket.festivalID,ticket.seats,ticket.client);
					}

					return new OkResponse();
				}
				catch (ServiceException e)
				{
					connected=false;
					return new ErrorResponse(e.Message);
				}
			}
			if (request is LogoutRequest)
			{
				Console.WriteLine("Logout request");
				LogoutRequest logReq =(LogoutRequest)request;
				Account user = logReq.User;
				try
				{
                    lock (server)
                    {
	                    server.logout(user, this);
                    }
					connected=false;
					return new OkResponse();

				}
				catch (ServiceException e)
				{
				   return new ErrorResponse(e.Message);
				}
			}
			
			return response;
		}

	private void sendResponse(Response response)
		{
			Console.WriteLine("sending response "+response);
            formatter.Serialize(stream, response);
            stream.Flush();
			
		}

	public void ticketsSold(TicketDTO ticket)
	{
		Console.WriteLine("sending ticket sold update response");
		formatter.Serialize(stream,new TicketSoldResponse(ticket));
		stream.Flush();
	}
	
	}

}