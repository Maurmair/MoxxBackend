using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using MySql.Data;
using SimpleRestServer.Models;

namespace SimpleRestServer
{
    public class PersonPersistence
    {

        private MySql.Data.MySqlClient.MySqlConnection conn;
        public PersonPersistence()
        {
            string myConnectionString;
            myConnectionString = "server=127.0.0.1;uid=root;pwd=RandomPw!;database=employeedb";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

            }
        }

        public long savePerson(Person personToSave)
        {
            string sqlString = "INSERT INTO tblpersonnel (FirstName, LastName, PayRate, StartDate, EndDate) VALUES('" 
                + personToSave.FirstName + "','" 
                + personToSave.LastName + "'," 
                + personToSave.PayRate + ",'" 
                + personToSave.StartDate.ToString("yyyy-MM-dd HH:mm:ss") + "','" 
                + personToSave.EndDate.ToString("yyyy-MM-dd HH:mm:ss") + "')".ToString(CultureInfo.CreateSpecificCulture("sv-SE"));
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            cmd.ExecuteNonQuery();
            long id = cmd.LastInsertedId;
            return id;
            //.ToString(CultureInfo.CreateSpecificCulture("sv-SE"))
        }

        public Person getPerson(long ID)
        {
            Person p = new Person();
            MySql.Data.MySqlClient.MySqlDataReader mySqlReader = null;
            String sqlString = "SELECT * FROM tblpersonnel WHERE ID = " + ID.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            mySqlReader = cmd.ExecuteReader();
            if (mySqlReader.Read())
            {
                p.ID = mySqlReader.GetInt32(0);
                p.FirstName = mySqlReader.GetString(1);
                p.LastName = mySqlReader.GetString(2);
                p.PayRate = mySqlReader.GetString(3);
                p.StartDate = mySqlReader.GetDateTime(4);
                p.EndDate = mySqlReader.GetDateTime(5);
                return p;
            }
            else
            {
                return null;
            }
        }
    }
}