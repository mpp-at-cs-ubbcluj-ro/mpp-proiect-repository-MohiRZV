using LabMPP.domain;
using LabMPP.domain.Validators;
using LabMPP.JdbcUtils;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace LabMPP.repository
{
    public class TicketRepo : TicketRepoInterface
    {
        SqlConnection connection = DBCUtils.GetConnection();
        private static readonly ILog log = LogManager.GetLogger("TicketRepo");
        private Validator<Ticket> validator = new TicketValidator();
        public TicketRepo()
        {
            log.Info("creating TicketRepo");
        }

        public Ticket add(Ticket entity)
        {
            log.InfoFormat("entering add with {0}", entity);
            SqlCommand sql = new SqlCommand("insert into Ticket (price,seats,customer,festival_id) values(@price,@seats,@cid,@fid)", connection);

            sql.Parameters.Add("@price", SqlDbType.Float).Value = entity.price;
            sql.Parameters.Add("@seats", SqlDbType.Int).Value = entity.seats;
            sql.Parameters.Add("@fid", SqlDbType.Int).Value = entity.festival.id;
            sql.Parameters.Add("@cid", SqlDbType.VarChar).Value = entity.client;

            connection.Open();
            var result = sql.ExecuteNonQuery();

            connection.Close();
            log.InfoFormat("exiting add with {0}", result);
            if (result == 0)
                return entity;
            return null;

        }

        public Ticket delete(long id)
        {
            log.InfoFormat("entering delete with {0}", id);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.DeleteCommand = new SqlCommand("delete from Ticket where id=@id", connection);

            adapter.DeleteCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;

            connection.Open();
            var result = adapter.DeleteCommand.ExecuteNonQuery();

            connection.Close();
            log.InfoFormat("exiting delete with {0}", result);
            if (result == 0)
                return null;
            return null;
        }

        private Artist getArtist(long id)
        {
            log.InfoFormat("entering getArtist with {0}", id);
            SqlCommand sql = new SqlCommand("select * from Artist where id=@id", connection);

            sql.Parameters.Add("@id", SqlDbType.Int).Value = id;
            Artist artist = null;
            connection.Open();
            using (SqlDataReader reader = sql.ExecuteReader())
            {
                while (reader.Read())
                {
                    string name = reader["name"].ToString();
                    string genre = reader["genre"].ToString();
                    int i = int.Parse(reader["id"].ToString());
                    artist = new Artist(id, name, genre);
                }
                connection.Close();
            }
            log.InfoFormat("exiting getArtist with {0}", artist);
            return artist;
        }
        private Festival getFestival(long id)
        {
            log.InfoFormat("entering getFestival with {0}", id);
            SqlCommand sql = new SqlCommand("select * from Festival where id=@id", connection);

            sql.Parameters.Add("@id", SqlDbType.Int).Value = id;
            Festival festival = null;
            connection.Open();
            using (SqlDataReader reader = sql.ExecuteReader())
            {
                while (reader.Read())
                {
                    string name = reader["name"].ToString();
                    string genre = reader["genre"].ToString();
                    int i = int.Parse(reader["id"].ToString());
                    DateTime date = DateTime.Parse(reader["date"].ToString());
                    string location = reader["location"].ToString();
                    int seats = int.Parse(reader["seats"].ToString());
                    int aid = int.Parse(reader["artist_id"].ToString());
                    TimeSpan time = TimeSpan.Parse(reader["Time"].ToString());
                    festival = new Festival(i, date, time, location, name, genre, seats, new Artist(aid,"",""));
                }
                connection.Close();
            }
            log.InfoFormat("exiting getFestival with {0}", festival);
            if (festival == null)
                return null;
            festival.artist = getArtist(festival.artist.id);

            return festival;
        }
        public IEnumerable<Ticket> getAll()
        {
            log.Info("entering getAll");
            SqlCommand sql = new SqlCommand("select * from Ticket", connection);
            List<Ticket> tickets = new List<Ticket>();
            connection.Open();

            using (SqlDataReader reader = sql.ExecuteReader())
            {
                while (reader.Read())
                {
                    float price = float.Parse(reader["price"].ToString());
                    int seats = int.Parse(reader["seats"].ToString());
                    int i = int.Parse(reader["id"].ToString());
                    int fid = int.Parse(reader["festival_id"].ToString());
                    string cid = reader["customer"].ToString();

                    Ticket ticket = new Ticket(i, new Festival(fid, DateTime.Now,TimeSpan.FromMinutes(20), "", "", "", 0, null), price, cid, seats);
                    tickets.Add(ticket);
                }
                connection.Close();
            }
            foreach(Ticket t in tickets)
            {
                t.festival = getFestival(t.festival.id);
            }
            log.InfoFormat("exiting getAll with {0} entities found", tickets.Count);
            return tickets;
        }

        public Ticket getOne(long id)
        {
            log.InfoFormat("entering getOne with {0}", id);
            SqlCommand sql = new SqlCommand("select * from Customer where id=@id", connection);

            sql.Parameters.Add("@id", SqlDbType.Int).Value = id;
            Ticket ticket = null;
            connection.Open();
            using (SqlDataReader reader = sql.ExecuteReader())
            {
                while (reader.Read())
                {
                    float price = float.Parse(reader["price"].ToString());
                    int seats = int.Parse(reader["seats"].ToString());
                    int i = int.Parse(reader["id"].ToString());
                    int fid = int.Parse(reader["festival_id"].ToString());
                    string cid = reader["customer_id"].ToString();

                   ticket = new Ticket(i, new Festival(fid,DateTime.Now,TimeSpan.FromMinutes(20),"","","",0,null), price, cid, seats);
                }
                connection.Close();
            }
            log.InfoFormat("exiting getOne with {0}", ticket);
            if (ticket == null)
                return null;
            ticket.festival = getFestival(ticket.festival.id);

            return ticket;
        }

        public Ticket update(Ticket entity)
        {
            log.InfoFormat("entering update with {0}", entity);
            SqlCommand sql = new SqlCommand("UPDATE ticket SET price=@price,seats=@seats,customer=@customer,festival_id=@fid WHERE id=@id;", connection);

            sql.Parameters.Add("@id", SqlDbType.Int).Value = entity.id;
            sql.Parameters.Add("@price", SqlDbType.Float).Value = entity.price;
            sql.Parameters.Add("@seats", SqlDbType.Int).Value = entity.seats;
            sql.Parameters.Add("@customer", SqlDbType.VarChar).Value = entity.client;
            sql.Parameters.Add("@fid", SqlDbType.Int).Value = entity.festival.id;
            connection.Open();
            int ret = sql.ExecuteNonQuery();
            connection.Close();
            log.InfoFormat("exiting update with {0}", ret);
            if (ret <= 0)
                return null;
            return entity;
        }

        public long getSoldSeats(long festival_id)
        {
            log.InfoFormat("entering getSoldSeats with {0}", festival_id);
            SqlCommand sql = new SqlCommand("select sum(seats) as Sold from ticket where festival_id=@id;", connection);

            sql.Parameters.Add("@id", SqlDbType.Int).Value = festival_id;
            int seats = 0;
            connection.Open();
            using (SqlDataReader reader = sql.ExecuteReader())
            {
                while (reader.Read())
                {
                    try
                    {
                        seats = int.Parse(reader["Sold"].ToString());
                    }
                    catch (Exception ex)
                    {

                    }
                }
                connection.Close();
            }
            log.InfoFormat("exiting getSoldSeats with {0}", seats);

            return seats;
        }
    }
}
