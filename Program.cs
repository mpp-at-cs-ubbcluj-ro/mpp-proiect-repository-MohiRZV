﻿using LabMPP.domain;
using LabMPP.repository;
using LabMPP.service;
using LabMPP.ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabMPP
{
    static class Program
    {
        static void testCustomer()
        {
            CustomerRepo repo = new CustomerRepo();
            foreach (Customer customer in repo.getAll())
            {
                repo.delete(customer.id);
            }
            repo.add(new Customer(0, "Jiji", "Acolo"));
            Customer e = repo.getAll().ToList().ElementAt(0);
            repo.getOne(e.id);
            repo.update(e);

        }
        static void testArtist()
        {
            ArtistRepo repo = new ArtistRepo();
            foreach (Artist ent in repo.getAll())
            {
                repo.delete(ent.id);
            }
            repo.add(new Artist(0, "Li", "Roacer"));
            Artist e = repo.getAll().ToList().ElementAt(0);
            repo.getOne(e.id);
            repo.update(e);

        }
        static void testFestival()
        {
            FestivalRepo repo = new FestivalRepo();
            foreach (Festival ent in repo.getAll())
            {
                repo.delete(ent.id);
            }
            new ArtistRepo().add(new Artist(0, "Li", "Roacer"));
            Artist artist = new ArtistRepo().getAll().ToList()[0];
            repo.add(new Festival(0, DateTime.Now, "Aici", "FestivalUL", "Rock", 10, artist));
            Festival e = repo.getAll().ToList().ElementAt(0);
            repo.getOne(e.id);
            repo.update(e);

        }
        static void testTicket()
        {
            TicketRepo repo = new TicketRepo();
            foreach (Ticket ent in repo.getAll())
            {
                repo.delete(ent.id);
            }
            FestivalRepo frepo = new FestivalRepo();
            ArtistRepo arepo = new ArtistRepo();
            CustomerRepo crepo = new CustomerRepo();
            arepo.add(new Artist(0, "Li", "Roacer"));
            Artist artist = arepo.getAll().ToList()[0];
            frepo.add(new Festival(0, DateTime.Now, "Aici", "FestivalUL", "Rock", 10, artist));
            crepo.add(new Customer(0, "Client", "TicketStreet"));
            repo.add(new Ticket(0, new FestivalRepo().getAll().ToList()[0], 25, "Cli", 100));
            Ticket e = repo.getAll().ToList().ElementAt(0);
            repo.getOne(e.id);
            repo.update(e);

        }
        static void test()
        {
            testCustomer();
            testArtist();
            testFestival();
            testTicket();
            Console.WriteLine("passed all tests");
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //test();
            AccountRepo accountRepo = new AccountRepo();
            CustomerRepo customerRepo = new CustomerRepo();
            ArtistRepo artistRepo = new ArtistRepo();
            FestivalRepo festivalRepo = new FestivalRepo();
            TicketRepo ticketRepo = new TicketRepo();

            LoginService loginService = new LoginService(accountRepo);
            ArtistService artistService = new ArtistService(artistRepo);
            CustomerService customerService = new CustomerService(customerRepo);
            FestivalService festivalService = new FestivalService(festivalRepo, artistRepo);
            TicketService ticketService = new TicketService(ticketRepo, festivalRepo, customerRepo);


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var loginForm = new LoginForm();
            loginForm.setService(loginService,artistService,customerService,festivalService,ticketService);
            Application.Run(loginForm);
        }
    }
}
