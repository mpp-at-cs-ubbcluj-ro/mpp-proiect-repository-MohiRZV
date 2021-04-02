using System;
using System.Collections.Generic;
using LabMPP.domain;

namespace network.protocol
{
	public interface Response 
	{
	}

	[Serializable]
	public class OkResponse : Response
	{
		
	}
	
	[Serializable]
	public class LoginResponse : Response
	{
		private Account _account;

		public LoginResponse(Account account)
		{
			_account = account;
		}

		public Account Account
		{
			get => _account;
			set => _account = value;
		}
	}
	
	[Serializable]
	public class GetResponse : Response
	{
		private IEnumerable<FestivalDTO> _festivalDtos;

		public GetResponse(IEnumerable<FestivalDTO> festivalDtos)
		{
			_festivalDtos = festivalDtos;
		}

		public IEnumerable<FestivalDTO> Data
		{
			get => _festivalDtos;
			set => _festivalDtos = value;
		}
	}

	[Serializable]
	public class TicketSoldResponse : Response
	{
		private TicketDTO _ticketDto;

		public TicketDTO TicketDto
		{
			get => _ticketDto;
			set => _ticketDto = value;
		}

		public TicketSoldResponse(TicketDTO ticketDto)
		{
			_ticketDto = ticketDto;
		}
	}
    [Serializable]
	public class ErrorResponse : Response
	{
		private string message;

		public ErrorResponse(string message)
		{
			this.message = message;
		}

		public virtual string Message
		{
			get
			{
				return message;
			}
		}
	}
	
	public interface UpdateResponse : Response
	{
	}



}