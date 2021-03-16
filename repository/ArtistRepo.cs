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
    class ArtistRepo : ArtistRepoInterface
    {
        SqlConnection connection = DBCUtils.GetConnection();

        public ArtistRepo()
        {
        }

        public Artist add(Artist entity)
        {
            SqlCommand sql = new SqlCommand("insert into Artist (name,genre) values(@name,@genre)", connection);

            sql.Parameters.Add("@name", SqlDbType.VarChar).Value = entity.name;
            sql.Parameters.Add("@genre", SqlDbType.VarChar).Value = entity.genre;

            connection.Open();

            var result = sql.ExecuteNonQuery();

            connection.Close();
            if (result == 0)
                return entity;
            return null;

        }

        public Artist delete(long id)
        {
            SqlCommand sql = new SqlCommand("delete from Artist where id=@id", connection);

            sql.Parameters.Add("@id", SqlDbType.Int).Value = id;

            connection.Open();

            var result = sql.ExecuteNonQuery();

            connection.Close();
            if (result == 0)
                return null;
            return null;
        }

        public IEnumerable<Artist> getAll()
        {
            SqlCommand sql = new SqlCommand("select * from Artist", connection);
            List<Artist> artists = new List<Artist>();
            connection.Open();

            using (SqlDataReader reader = sql.ExecuteReader())
            {
                while (reader.Read())
                {
                    string name = reader["name"].ToString();
                    string genre = reader["genre"].ToString();
                    int i = int.Parse(reader["id"].ToString());
                    Artist artist = new Artist(i, name, genre);
                    artists.Add(artist);
                }
                connection.Close();
            }
            return artists;
        }

        public Artist getOne(long id)
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

        public Artist update(Artist entity)
        {
            SqlCommand sql = new SqlCommand("UPDATE artist SET name = @name, genre = @genre WHERE id=@id;", connection);

            sql.Parameters.Add("@id", SqlDbType.Int).Value = entity.id;
            sql.Parameters.Add("@name", SqlDbType.VarChar).Value = entity.name;
            sql.Parameters.Add("@genre", SqlDbType.VarChar).Value = entity.genre;
            connection.Open();
            int ret = sql.ExecuteNonQuery();
            connection.Close();
            if (ret <= 0)
                return null;
            return entity;
        }
    }
}
