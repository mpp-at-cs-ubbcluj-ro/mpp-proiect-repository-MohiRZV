using Lab2MPP.domain;
using Lab2MPP.JdbcUtils;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Lab2MPP.repository
{
    class TicketRepo : TicketRepoInterface
    {
        SqlConnection connection = DBCUtils.GetConnection();

        public TicketRepo()
        {
        }

        public Ticket add(Ticket entity)
        {
            SqlCommand sql = new SqlCommand("insert into Ticket (price,seats,customer_id,festival_id) values(@price,@seats,@cid,@fid)", connection);

            sql.Parameters.Add("@price", SqlDbType.Float).Value = entity.price;
            sql.Parameters.Add("@seats", SqlDbType.Int).Value = entity.seats;
            sql.Parameters.Add("@fid", SqlDbType.Int).Value = entity.festival.id;
            sql.Parameters.Add("@cid", SqlDbType.Int).Value = entity.client.id;

            connection.Open();
            var result = sql.ExecuteNonQuery();

            connection.Close();
            if (result == 0)
                return entity;
            return null;

        }

        public Ticket delete(long id)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.DeleteCommand = new SqlCommand("delete from Ticket where id=@id", connection);

            adapter.DeleteCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;

            connection.Open();
            var result = adapter.DeleteCommand.ExecuteNonQuery();

            connection.Close();
            if (result == 0)
                return null;
            return null;
        }

        public Customer getClient(long id)
        {
            SqlCommand sql = new SqlCommand("select * from Customer where id=@id", connection);

            sql.Parameters.Add("@id", SqlDbType.Int).Value = id;
            Customer customer = null;
            connection.Open();
            using (SqlDataReader reader = sql.ExecuteReader())
            {
                while (reader.Read())
                {
                    string name = reader["name"].ToString();
                    string address = reader["address"].ToString();
                    int i = int.Parse(reader["id"].ToString());
                    customer = new Customer(id, name, address);
                }
                connection.Close();
            }

            return customer;
        }
        private Artist getArtist(long id)
        {
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

            return artist;
        }
        public Festival getFestival(long id)
        {
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
                    festival = new Festival(i, date, location, name, genre, seats, new Artist(aid,"",""));
                }
                connection.Close();
            }
            if (festival == null)
                return null;
            festival.artist = getArtist(festival.artist.id);

            return festival;
        }
        public IEnumerable<Ticket> getAll()
        {
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
                    int cid = int.Parse(reader["customer_id"].ToString());

                    Ticket ticket = new Ticket(i, new Festival(fid, DateTime.Now, "", "", "", 0, null), price, new Customer(cid, "", ""), seats);
                    tickets.Add(ticket);
                }
                connection.Close();
            }
            foreach(Ticket t in tickets)
            {
                t.client = getClient(t.client.id);
                t.festival = getFestival(t.festival.id);
            }
            return tickets;
        }

        public Ticket getOne(long id)
        {
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
                    int cid = int.Parse(reader["customer_id"].ToString());

                   ticket = new Ticket(i, new Festival(fid,DateTime.Now,"","","",0,null), price, new Customer(cid,"",""), seats);
                }
                connection.Close();
            }
            if (ticket == null)
                return null;
            ticket.festival = getFestival(ticket.festival.id);
            ticket.client = getClient(ticket.client.id);

            return ticket;
        }

        public Ticket update(Ticket entity)
        {
            SqlCommand sql = new SqlCommand("UPDATE ticket SET price=@price,seats=@seats,customer_id=@cid,festival_id=@fid WHERE id=@id;", connection);

            sql.Parameters.Add("@id", SqlDbType.Int).Value = entity.id;
            sql.Parameters.Add("@price", SqlDbType.Float).Value = entity.price;
            sql.Parameters.Add("@seats", SqlDbType.Int).Value = entity.seats;
            sql.Parameters.Add("@cid", SqlDbType.Int).Value = entity.client.id;
            sql.Parameters.Add("@fid", SqlDbType.Int).Value = entity.festival.id;
            connection.Open();
            int ret = sql.ExecuteNonQuery();
            connection.Close();
            if (ret <= 0)
                return null;
            return entity;
        }
    }
}
