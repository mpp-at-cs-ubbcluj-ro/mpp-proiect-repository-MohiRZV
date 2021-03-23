using LabMPP.domain;
using LabMPP.domain.Validators;
using LabMPP.JdbcUtils;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMPP.repository
{
    class AccountRepo : AccountRepoInterface
    {
        SqlConnection connection = DBCUtils.GetConnection();
        private static readonly ILog log = LogManager.GetLogger("AccountRepo");
        public Account add(Account entity)
        {
            log.InfoFormat("entering add with value {0}", entity);
            SqlCommand sql = new SqlCommand("insert into account (username,password,idUser) values(@username,@password,@idUser)", connection);

            sql.Parameters.Add("@username", SqlDbType.VarChar).Value = entity.getUsername();
            sql.Parameters.Add("@password", SqlDbType.VarChar).Value = entity.password;
            sql.Parameters.Add("@idUser", SqlDbType.Int).Value = entity.idUser;

            connection.Open();

            var result = sql.ExecuteNonQuery();

            connection.Close();
            log.InfoFormat("exiting add with value {0}", result);
            if (result == 0)
                return entity;
            return null;
        }

        public Account delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> getAll()
        {
            throw new NotImplementedException();
        }

        public Account getOne(string id)
        {
            log.InfoFormat("entering getOne with value {0}", id);
            SqlCommand sql = new SqlCommand("select * from account where username=@id", connection);

            sql.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
            Account account = null;
            connection.Open();
            using (SqlDataReader reader = sql.ExecuteReader())
            {
                while (reader.Read())
                {
                    string username = reader["username"].ToString();
                    string password = reader["password"].ToString();
                    int i = int.Parse(reader["idUser"].ToString());
                    account = new Account(username, i, password);
                }
                connection.Close();
            }
            log.InfoFormat("exiting getOne with value {0}", account);
            return account;
        }

        public Account update(Account entity)
        {
            throw new NotImplementedException();
        }
    }
}
