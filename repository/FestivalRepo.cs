using Lab2MPP.domain;
using Lab2MPP.JdbcUtils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Lab2MPP.repository
{
    class FestivalRepo : FestivalRepoInterface
    {
        SqlConnection connection = DBCUtils.GetConnection();

        public FestivalRepo()
        {
        }

        public Festival add(Festival entity)
        {
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
            if (result == 0)
                return entity;
            return null;

        }

        public Festival delete(long id)
        {
            SqlCommand sql = new SqlCommand("delete from Festival where id=@id", connection);

            sql.Parameters.Add("@id", SqlDbType.Int).Value = id;

            connection.Open();

            var result = sql.ExecuteNonQuery();

            connection.Close();
            if (result == 0)
                return null;
            return null;
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
        public IEnumerable<Festival> getAll()
        {
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
                    Festival artist = new Festival(i,date,location, name, genre,seats,new Artist(id,"",""));
                    festivals.Add(artist);
                }
                connection.Close();
            }
            foreach(Festival f in festivals)
            {
                f.artist = getArtist(f.artist.id);
            }
            return festivals;
        }

        public Festival getOne(long id)
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

        public Festival update(Festival entity)
        {
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
            if (ret <= 0)
                return null;
            return entity;
        }
    }
}
