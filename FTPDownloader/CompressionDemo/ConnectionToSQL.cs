/*
 * Created by SharpDevelop.
 * User: igor.kabic
 * Date: 19/03/2015
 * Time: 14:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPDownloader
{
	/// <summary>
	/// Description of ConnectionToSQL.
	/// </summary>
	public class ConnectionToSQL
	{
		
	public void ConfigFTPDownladerTableInsert(string fileName, string statusColumnValue)
        {
	 		using (SqlConnection conn = new SqlConnection())
            {
                // Create the connectionString
                // Trusted_Connection is used to denote the connection uses Windows Authentication
                //conn.ConnectionString = "DataSource=[WIN-VLL0GU3V8GH];Database=[Sandbox];Trusted_Connection=true";
            
				//conn.ConnectionString ="Data Source=WIN-VLL0GU3V8GH;" + "Initial Catalog=Sandbox;" + "Integrated Security=SSPI;";
				conn.ConnectionString ="Data Source=BIDB-EU-LUX003\\EUBI_PRODUCTION1;" + "Initial Catalog=DatawarehouseConfig;" + "Integrated Security=SSPI;";
				conn.Open();
               
                // Create the command
                //SqlCommand command = new SqlCommand("INSERT INTO Sandbox.dbo.ConfigFTPDownloader Select 'testing', 'test'", conn);
                SqlCommand command = new SqlCommand("INSERT INTO DatawarehouseConfig.LOG.FTPDownloader Select '" + fileName + "', '" + statusColumnValue + "'", conn);
                command.ExecuteReader();
			}
		}
	}
}
