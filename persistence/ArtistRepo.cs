using LabMPP.domain;
using LabMPP.domain.Validators;
using LabMPP.JdbcUtils;
using log4net;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LabMPP.repository
{
    public class ArtistRepo : ArtistRepoInterface
    {
        SqlConnection connection = DBCUtils.GetConnection();
        private static readonly ILog log = LogManager.GetLogger("ArtistRepo");
        private Validator<Artist> validator = new ArtistValidator();
        public ArtistRepo()
        {
            log.Info("creating ArtistRepo");
        }

        public Artist add(Artist entity)
        {
            log.InfoFormat("entering add with value {0}", entity);
            SqlCommand sql = new SqlCommand("insert into Artist (name,genre) values(@name,@genre)", connection);

            sql.Parameters.Add("@name", SqlDbType.VarChar).Value = entity.name;
            sql.Parameters.Add("@genre", SqlDbType.VarChar).Value = entity.genre;

            connection.Open();

            var result = sql.ExecuteNonQuery();

            connection.Close();
            log.InfoFormat("exiting add with value {0}", result);
            if (result == 0)
                return entity;
            return null;

        }

        public Artist delete(long id)
        {
            log.InfoFormat("entering delete with value {0}", id);
            SqlCommand sql = new SqlCommand("delete from Artist where id=@id", connection);

            sql.Parameters.Add("@id", SqlDbType.Int).Value = id;

            connection.Open();

            var result = sql.ExecuteNonQuery();

            connection.Close();
            log.InfoFormat("exiting delete with {0}", result);
            if (result == 0)
                return null;
            return null;
        }

        public IEnumerable<Artist> getAll()
        {
            log.Info("entering getAll");
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
            log.InfoFormat("exiting getAll with {0} entities found", artists.Count);
            return artists;
        }

        public Artist getOne(long id)
        {
            log.InfoFormat("entering getOne with value {0}", id);
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
            log.InfoFormat("exiting getOne with value {0}", artist);
            return artist;
        }

        public Artist update(Artist entity)
        {
            log.InfoFormat("entering update with value {0}", entity);
            SqlCommand sql = new SqlCommand("UPDATE artist SET name = @name, genre = @genre WHERE id=@id;", connection);

            sql.Parameters.Add("@id", SqlDbType.Int).Value = entity.id;
            sql.Parameters.Add("@name", SqlDbType.VarChar).Value = entity.name;
            sql.Parameters.Add("@genre", SqlDbType.VarChar).Value = entity.genre;
            connection.Open();
            int ret = sql.ExecuteNonQuery();
            connection.Close();
            if (ret <= 0)
            {
                log.InfoFormat("exiting update with value {0}", null);
                return null;
            }
            log.InfoFormat("exiting getOne with value {0}", entity);
            return entity;
        }
    }
}
