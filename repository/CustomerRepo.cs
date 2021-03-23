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
    class CustomerRepo : CustomerRepoInterface
    {
        SqlConnection connection = DBCUtils.GetConnection();
        private static readonly ILog log = LogManager.GetLogger("CustomerRepo");
        private Validator<Customer> validator = new CustomerValidator();
        public CustomerRepo()
        {
            log.Info("creating CustomerRepo");
        }

        public Customer add(Customer entity)
        {
            log.InfoFormat("entering add with {0}", entity);
            SqlCommand sql = new SqlCommand("insert into Customer (name,address) values(@name,@address)", connection);

            sql.Parameters.Add("@name", SqlDbType.VarChar).Value = entity.name;
            sql.Parameters.Add("@address", SqlDbType.VarChar).Value = entity.address;

            connection.Open();
            var result = sql.ExecuteNonQuery();

            connection.Close();
            log.InfoFormat("exiting add with {0}", result);
            if (result == 0)
                return entity;
            return null;

        }

        public Customer delete(long id)
        {
            log.InfoFormat("entering delete with {0}", id);
            SqlCommand sql = new SqlCommand("delete from Customer where id=@id", connection);
            
            sql.Parameters.Add("@id", SqlDbType.Int).Value = id;
            
            connection.Open();
            var result = sql.ExecuteNonQuery();

            connection.Close();
            log.InfoFormat("exiting delete with {0}", result);
            if (result == 0)
                return null;
            return null;
        }

        public IEnumerable<Customer> getAll()
        {
            log.Info("entering getAll");
            SqlCommand sql = new SqlCommand("select * from Customer", connection);
            List<Customer> customers = new List<Customer>();
            connection.Open();
            
            using (SqlDataReader reader = sql.ExecuteReader())
            {
                while (reader.Read())
                {
                    string name = reader["name"].ToString();
                    string address = reader["address"].ToString();
                    int i = int.Parse(reader["id"].ToString());
                    Customer customer = new Customer(i, name, address);
                    customers.Add(customer);
                }
                connection.Close();
            }
            log.InfoFormat("exiting getAll with {0} entities found", customers.Count.ToString());
            return customers;
        }

        public Customer getOne(long id)
        {
            log.InfoFormat("entering getOne with {0}", id);
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
                    customer= new Customer(id, name, address);
                }
                connection.Close();
            }
            log.InfoFormat("exiting getOne with {0}", customer);
            return customer;
        }

        public Customer update(Customer entity)
        {
            log.InfoFormat("entering update with {0}", entity);
            SqlCommand sql = new SqlCommand("UPDATE customer SET name = @name, address = @address WHERE id=@id;", connection);
            
            sql.Parameters.Add("@id", SqlDbType.Int).Value = entity.id;
            sql.Parameters.Add("@name", SqlDbType.VarChar).Value = entity.name;
            sql.Parameters.Add("@address", SqlDbType.VarChar).Value = entity.address;
            connection.Open();
            int ret=sql.ExecuteNonQuery();
            connection.Close();
            log.InfoFormat("exiting update with {0}", ret);
            if(ret<=0)
                return null;
            return entity;
        }
    }
}
