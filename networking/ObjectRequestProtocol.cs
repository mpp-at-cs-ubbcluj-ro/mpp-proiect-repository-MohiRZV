using System;
using System.Collections.Generic;
using LabMPP.domain;

namespace network.protocol
{

	public interface Request 
	{
	}


	[Serializable]
	public class LoginRequest : Request
	{
		private Account user;

		public LoginRequest(Account user)
		{
			this.user = user;
		}

		public virtual Account User
		{
			get
			{
				return user;
			}
		}
	}

	[Serializable]
	public class GetRequest : Request
	{
		private DateTime date;

		public GetRequest(DateTime date)
		{
			this.date = date;
		}

		public GetRequest()
		{
			
		}

		public virtual DateTime Date
		{
			get
			{
				return date;
			}
		}
	}
	
	[Serializable]
	public class SellTicketRequest : Request
	{
		private TicketDTO _ticketDto;

		public SellTicketRequest(TicketDTO ticketDto)
		{
			_ticketDto = ticketDto;
		}

		public virtual TicketDTO TicketDto
		{
			get
			{
				return _ticketDto;
			}
		}
	}
	
	[Serializable]
	public class LogoutRequest : Request
	{
		private Account user;

		public LogoutRequest(Account user)
		{
			this.user = user;
		}

		public virtual Account User
		{
			get
			{
				return user;
			}
		}
	}
	


}