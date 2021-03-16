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
    class CustomerRepo : CustomerRepoInterface
    {
        SqlConnection connection = DBCUtils.GetConnection();
       
        public CustomerRepo()
        {
        }

        public Customer add(Customer entity)
        {
            SqlCommand sql = new SqlCommand("insert into Customer (name,address) values(@name,@address)", connection);

            sql.Parameters.Add("@name", SqlDbType.VarChar).Value = entity.name;
            sql.Parameters.Add("@address", SqlDbType.VarChar).Value = entity.address;

            connection.Open();
            var result = sql.ExecuteNonQuery();

            connection.Close();
            if (result == 0)
                return entity;
            return null;

        }

        public Customer delete(long id)
        {
            SqlCommand sql = new SqlCommand("delete from Customer where id=@id", connection);
            
            sql.Parameters.Add("@id", SqlDbType.Int).Value = id;
            
            connection.Open();
            var result = sql.ExecuteNonQuery();

            connection.Close();
            if (result == 0)
                return null;
            return null;
        }

        public IEnumerable<Customer> getAll()
        {
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
            return customers;
        }

        public Customer getOne(long id)
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
                    customer= new Customer(id, name, address);
                }
                connection.Close();
            }

            return customer;
        }

        public Customer update(Customer entity)
        {
            SqlCommand sql = new SqlCommand("UPDATE customer SET name = @name, address = @address WHERE id=@id;", connection);
            
            sql.Parameters.Add("@id", SqlDbType.Int).Value = entity.id;
            sql.Parameters.Add("@name", SqlDbType.VarChar).Value = entity.name;
            sql.Parameters.Add("@address", SqlDbType.VarChar).Value = entity.address;
            connection.Open();
            int ret=sql.ExecuteNonQuery();
            connection.Close();
            if(ret<=0)
                return null;
            return entity;
        }
    }
}
