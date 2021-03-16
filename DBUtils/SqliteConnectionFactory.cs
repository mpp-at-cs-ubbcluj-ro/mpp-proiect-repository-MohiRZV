using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Lab2MPP.JdbcUtils
{
	public class SqliteConnectionFactory : ConnectionFactory
	{
		public override IDbConnection createConnection()
		{
			//Mono Sqlite Connection
			String connectionString = "URI=file:D:/An2Sem2/MPP/Lab2MPP/festivals.db,Version=3";
			return new SqliteConnection(connectionString);

			// Windows Sqlite Connection, fisierul .db ar trebuie sa fie in directorul debug/bin
			//tring connectionString = "Data Source=D:/An2Sem2/MPP/Lab2MPP/festivals.db";
			//return new SqliteConnection(connectionString);
		}
	}
}
