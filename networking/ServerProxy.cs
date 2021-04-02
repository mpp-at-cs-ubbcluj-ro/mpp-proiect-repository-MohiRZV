using System;
using System.Collections.Generic;
using System.Threading;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using LabMPP.domain;
using network.protocol;
using services;


	public class ServerProxy : IServices
	{
		private string host;
		private int port;

		private IObserver client;

		private NetworkStream stream;
		
        private IFormatter formatter;
		private TcpClient connection;

		private Queue<Response> responses;
		private volatile bool finished;
        private EventWaitHandle _waitHandle;
		public ServerProxy(string host, int port)
		{
			this.host = host;
			this.port = port;
			responses=new Queue<Response>();
		}

		public virtual Account login(Account user, IObserver client)
		{
			initializeConnection();
			sendRequest(new LoginRequest(user));
			Response response =readResponse();
			if (response is OkResponse)
			{
				this.client=client;
				return null;
			}
			if (response is LoginResponse){
				this.client=client;
				return ((LoginResponse) response).Account;
			}
			if (response is ErrorResponse)
			{
				ErrorResponse err =(ErrorResponse)response;
				closeConnection();
				throw new ServiceException(err.Message);
			}

			return null;
		}

		public IEnumerable<FestivalDTO> searchByDate(DateTime date)
		{
			sendRequest(new GetRequest(date));
			Response response =readResponse();
			if (response is OkResponse)
			{
				return null;
			}
			if (response is GetResponse){
				return ((GetResponse)response).Data;
			}
			if (response is ErrorResponse)
			{
				ErrorResponse err =(ErrorResponse)response;
				closeConnection();
				throw new ServiceException(err.Message);
			}

			return null;
		}

		public IEnumerable<FestivalDTO> getAll()
		{
			sendRequest(new GetRequest());
			Response response =readResponse();
			if (response is OkResponse)
			{
				return null;
			}
			if (response is GetResponse){
				return ((GetResponse)response).Data;
			}
			if (response is ErrorResponse)
			{
				ErrorResponse err =(ErrorResponse)response;
				closeConnection();
				throw new ServiceException(err.Message);
			}

			return null;
		}

		public void sellTicket(int festivalID, long seats, string client)
		{
			sendRequest(new SellTicketRequest(new TicketDTO(festivalID,seats,client)));
			Response response =readResponse();
			
			if (response is ErrorResponse)
			{
				ErrorResponse err =(ErrorResponse)response;
				closeConnection();
				throw new ServiceException(err.Message);
			}

		}


		public virtual void logout(Account user, IObserver client)
		{
			sendRequest(new LogoutRequest(user));
			Response response =readResponse();
			closeConnection();
			if (response is ErrorResponse)
			{
				ErrorResponse err =(ErrorResponse)response;
				throw new ServiceException(err.Message);
			}
		}




		private void closeConnection()
		{
			finished=true;
			try
			{
				stream.Close();
				//output.close();
				connection.Close();
                _waitHandle.Close();
				client=null;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}

		}

		private void sendRequest(Request request)
		{
			try
			{
                formatter.Serialize(stream, request);
                stream.Flush();
			}
			catch (Exception e)
			{
				throw new ServiceException("Error sending object "+e);
			}

		}

		private Response readResponse()
		{
			Response response =null;
			try
			{
                _waitHandle.WaitOne();
				lock (responses)
				{
                    //Monitor.Wait(responses); 
                    response = responses.Dequeue();
                
				}
				

			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
			return response;
		}
		private void initializeConnection()
		{
			 try
			 {
				connection=new TcpClient(host,port);
				stream=connection.GetStream();
                formatter = new BinaryFormatter();
				finished=false;
                _waitHandle = new AutoResetEvent(false);
				startReader();
			}
			catch (Exception e)
			{
                Console.WriteLine(e.StackTrace);
			}
		}
		private void startReader()
		{
			Thread tw =new Thread(run);
			tw.Start();
		}


		private void handleUpdate(TicketSoldResponse update)
		{
			TicketDTO ticketDTO = update.TicketDto;
			Console.WriteLine("Some tickets were sold");
			try{
				client.ticketsSold(ticketDTO);
			}catch (ServiceException e){
				Console.WriteLine(e.StackTrace);
			}
		}
		public virtual void run()
			{
				while(!finished)
				{
					try
					{
                        object response = formatter.Deserialize(stream);
						Console.WriteLine("response received "+response);
						if (response is TicketSoldResponse)
						{
							 handleUpdate((TicketSoldResponse)response);
						}
						else
						{
							
							lock (responses)
							{
                                					
								 
                                responses.Enqueue((Response)response);
                               
							}
                            _waitHandle.Set();
						}
					}
					catch (Exception e)
					{
						Console.WriteLine("Reading error "+e);
					}
					
				}
			}
		//}
	}
	