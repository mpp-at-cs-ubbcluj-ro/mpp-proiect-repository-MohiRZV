﻿using LabMPP.domain;
using LabMPP.domain.Validators;
using LabMPP.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMPP.service
{
    public class TicketService
    {
        private TicketRepo ticketRepo;
        private FestivalRepo festivalRepo;
        private Validator<Ticket> validator = new TicketValidator();

        public TicketService(TicketRepo ticketRepo, FestivalRepo festivalRepo)
        {
            this.ticketRepo = ticketRepo;
            this.festivalRepo = festivalRepo;
        }

        public Ticket addTicket(double price, string customer, long festival_id, int seats)
        {
            Ticket ticket = new Ticket(0, festivalRepo.getOne(festival_id), price, customer, seats);
            validator.validate(ticket);
            return ticketRepo.add(ticket);
        }
        public long getSoldSeats(long festival_id)
        {
            return ticketRepo.getSoldSeats(festival_id);
        }
    }
}
