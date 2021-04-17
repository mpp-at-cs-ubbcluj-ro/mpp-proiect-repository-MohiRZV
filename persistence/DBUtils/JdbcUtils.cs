﻿
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;

namespace LabMPP.JdbcUtils
{
    public static class DBUtils
	{


		private static IDbConnection instance = null;

		public static IDbConnection getConnection()
		{
			if (instance == null || instance.State == System.Data.ConnectionState.Closed)
			{
				instance = getNewConnection();
				instance.Open();
			}
			return instance;
		}

		private static IDbConnection getNewConnection()
		{

			return ConnectionFactory.getInstance().createConnection();

		}
	}
	
}
