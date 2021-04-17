using LabMPP.domain;
using LabMPP.domain.Validators;
using LabMPP.JdbcUtils;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace LabMPP.repository
{
    public class FestivalRepo : FestivalRepoInterface
    {
        SqlConnection connection = DBCUtils.GetConnection();
        private static readonly ILog log = LogManager.GetLogger("FestivalRepo");
        private Validator<Festival> validator = new FestivalValidator();
        public FestivalRepo()
        {
            log.Info("creating FestivalRepo");
        }

        public Festival add(Festival entity)
        {
            log.InfoFormat("entering add with {0}",entity);
            SqlCommand sql = new SqlCommand("insert into Festival (date,location,name,genre,seats,artist_id) values(@date,@location,@name,@genre,@seats,@aid)", connection);

            sql.Parameters.Add("@date", SqlDbType.Date).Value = entity.date;
            sql.Parameters.Add("@location", SqlDbType.VarChar).Value = entity.location;
            sql.Parameters.Add("@name", SqlDbType.VarChar).Value = entity.name;
            sql.Parameters.Add("@genre", SqlDbType.VarChar).Value = entity.genre;
            sql.Parameters.Add("@seats", SqlDbType.Int).Value = entity.seats;
            sql.Parameters.Add("@aid", SqlDbType.Int).Value = entity.artist.id;

            connection.Open();

            var result = sql.ExecuteNonQuery();

            connection.Close();
            log.InfoFormat("exiting add with {0}",result);
            if (result == 0)
                return entity;
            return null;

        }

        public Festival delete(long id)
        {
            log.InfoFormat("entering delete with {0}",id);
            SqlCommand sql = new SqlCommand("delete from Festival where id=@id", connection);

            sql.Parameters.Add("@id", SqlDbType.Int).Value = id;

            connection.Open();

            var result = sql.ExecuteNonQuery();

            connection.Close();
            log.InfoFormat("exiting delete with {0}",result);
            if (result == 0)
                return null;
            return null;
        }
        private Artist getArtist(long id)
        {
            log.InfoFormat("entering getArtist from FestivalRepo with {0}",id);
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
            log.InfoFormat("exiting getArtist from FestivalRepo with {0}",artist);
            return artist;
        }
        public IEnumerable<Festival> getAll()
        {
            log.Info("entering getAll");
            SqlCommand sql = new SqlCommand("select * from Festival", connection);
            List<Festival> festivals = new List<Festival>();
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
                    int id = int.Parse(reader["artist_id"].ToString());
                    TimeSpan time = TimeSpan.Parse(reader["Time"].ToString());
                    Festival artist = new Festival(i,date,time,location, name, genre,seats,new Artist(id,"",""));
                    festivals.Add(artist);
                }
                connection.Close();
            }
            foreach(Festival f in festivals)
            {
                f.artist = getArtist(f.artist.id);
            }
            log.InfoFormat("exiting getAll with {0} entities found",festivals.Count);
            return festivals;
        }

        public Festival getOne(long id)
        {
            log.InfoFormat("entering getOne with {0}",id);
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
            log.InfoFormat("exiting getOne with {0}", festival);
            if (festival == null)
                return null;
            festival.artist = getArtist(festival.artist.id);

            return festival;
        }

        public Festival update(Festival entity)
        {
            log.InfoFormat("entering update with {0}", entity);
            SqlCommand sql = new SqlCommand("UPDATE festival SET date=@date, location=@location, name=@name, genre=@genre, seats=@seats, artist_id=@aid WHERE id=@id;", connection);

            sql.Parameters.Add("@date", SqlDbType.Date).Value = entity.date;
            sql.Parameters.Add("@location", SqlDbType.VarChar).Value = entity.location;
            sql.Parameters.Add("@name", SqlDbType.VarChar).Value = entity.name;
            sql.Parameters.Add("@genre", SqlDbType.VarChar).Value = entity.genre;
            sql.Parameters.Add("@seats", SqlDbType.Int).Value = entity.seats;
            sql.Parameters.Add("@aid", SqlDbType.Int).Value = entity.artist.id;
            sql.Parameters.Add("@id", SqlDbType.Int).Value = entity.id;
            connection.Open();
            int ret = sql.ExecuteNonQuery();
            connection.Close();
            log.InfoFormat("exiting update with {0}",ret);
            if (ret <= 0)
                return null;
            return entity;
        }

        public IEnumerable<Festival> FindByDate(DateTime date)
        {
            log.Info("entering FindByDate");
            SqlCommand sql = new SqlCommand("select * from Festival where date=@date", connection);
            sql.Parameters.Add("@date", SqlDbType.Date).Value = date.Date;
            List<Festival> festivals = new List<Festival>();
            connection.Open();

            using (SqlDataReader reader = sql.ExecuteReader())
            {
                while (reader.Read())
                {
                    string name = reader["name"].ToString();
                    string genre = reader["genre"].ToString();
                    int i = int.Parse(reader["id"].ToString());
                    DateTime data = DateTime.Parse(reader["date"].ToString());
                    string location = reader["location"].ToString();
                    int seats = int.Parse(reader["seats"].ToString());
                    int id = int.Parse(reader["artist_id"].ToString());
                    TimeSpan time = TimeSpan.Parse(reader["Time"].ToString());
                    Festival artist = new Festival(i, data, time, location, name, genre, seats, new Artist(id, "", ""));
                    festivals.Add(artist);
                }
                connection.Close();
            }
            foreach (Festival f in festivals)
            {
                f.artist = getArtist(f.artist.id);
            }
            log.InfoFormat("exiting FindByDate with {0} entities found", festivals.Count);
            return festivals;
        }
    }
}
