using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Sockets;
using System.Threading;
using Google.Protobuf;
using Protobuffprotocol;
using services;
using LabMPP.domain;
using LabMPP.service;
using TicketDTO = LabMPP.domain.TicketDTO;

namespace protobuf
{
	public class ProtoWorker : IObserver
	{
		private IServices server;
		private TcpClient connection;

		private NetworkStream stream;
		private volatile bool connected;

		public ProtoWorker(IServices server, TcpClient connection)
		{
			this.server = server;
			this.connection = connection;
			try
			{

				stream = connection.GetStream();
				connected = true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
		}

		public virtual void run()
		{
			while (connected)
			{
				try
				{

					Request request = Request.Parser.ParseDelimitedFrom(stream);
					Response response = handleRequest(request);
					if (response != null)
					{
						sendResponse(response);
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
				Console.WriteLine("Error " + e);
			}
		}

		private Response handleRequest(Request request)
		{
			Response response = null;
			Request.Types.Type reqType = request.Type;
			switch (reqType)
			{
				case Request.Types.Type.Login:
				{
					Console.WriteLine("Login request ...");
					LabMPP.domain.Account user = ProtoUtils.getUser(request);
					try
					{
						LabMPP.domain.Account account = server.login(user, this);
						return ProtoUtils.createLoginResponse(account);
					}
					catch (ServiceException e)
					{
						connected = false;
						return ProtoUtils.createErrorResponse(e.Message);
					}
				}

				case Request.Types.Type.Logout:
				{
					Console.WriteLine("Logout request");
					LabMPP.domain.Account user = ProtoUtils.getUser(request);
					try
					{
						server.logout(user, this);
						connected = false;
						return ProtoUtils.createOkResponse();

					}
					catch (ServiceException e)
					{
						return ProtoUtils.createErrorResponse(e.Message);
					}
				}

				case Request.Types.Type.SearchByDate:
				{
					Console.WriteLine("Festivals request");
					DateTime date = default;


					try
					{
						IEnumerable<LabMPP.domain.FestivalDTO> festivalDTOS;
						if (request.Date.Length > 0)
						{
							CultureInfo provider = CultureInfo.InvariantCulture;
							date = DateTime.ParseExact(request.Date, "yyyy-MM-dd", provider);
							festivalDTOS = server.searchByDate(date);
						}
						else
						{
							festivalDTOS = server.getAll();
						}

						return ProtoUtils.createFestivalResponse(festivalDTOS);

					}
					catch (ServiceException e)
					{
						return ProtoUtils.createErrorResponse(e.Message);
					}
				}

				case Request.Types.Type.SellTicket:
				{
					Console.WriteLine("Sell ticket request");
					TicketDTO ticketDTO = ProtoUtils.getTicket(request.Ticket);
					try
					{
						server.sellTicket(ticketDTO.festivalID, ticketDTO.seats, ticketDTO.client);
						return ProtoUtils.createOkResponse();
					}
					catch (ServiceException e)
					{
						return ProtoUtils.createErrorResponse(e.Message);
					}
				}
			}

			return response;
		}

		private void sendResponse(Response response)
		{
			Console.WriteLine("sending response " + response);
			lock (stream)
			{
				response.WriteDelimitedTo(stream);
				stream.Flush();
			}

		}

		public void ticketsSold(TicketDTO ticket)
		{
			Console.WriteLine("sending sold ticket response " + ticket);
			sendResponse(ProtoUtils.createTicketSoldResponse(ticket));
		}
	}
}