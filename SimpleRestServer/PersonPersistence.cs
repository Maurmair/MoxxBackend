using System;
using System.Collections;
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
            myConnectionString = "server=108.167.172.114;uid=maurmair_moxAdm;pwd=RandomPw!;database=maurmair_moxDb";
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

        public ArrayList getPersons()
        {

            ArrayList personArray = new ArrayList();
            MySql.Data.MySqlClient.MySqlDataReader mySqlReader = null;
            String sqlString = "SELECT * FROM tblpersonnel";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            mySqlReader = cmd.ExecuteReader();
            while  (mySqlReader.Read())
            {
                Person p = new Person();
                p.ID = mySqlReader.GetInt32(0);
                p.FirstName = mySqlReader.GetString(1);
                p.LastName = mySqlReader.GetString(2);
                p.PayRate = mySqlReader.GetString(3);
                p.StartDate = mySqlReader.GetDateTime(4);
                p.EndDate = mySqlReader.GetDateTime(5);
                personArray.Add(p);
            }
            return personArray;

        }

        public bool deletePerson(long ID)
        {
            Person p = new Person();
            MySql.Data.MySqlClient.MySqlDataReader mySqlReader = null;

            String sqlString = "SELECT * FROM tblpersonnel WHERE ID = " + ID.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            mySqlReader = cmd.ExecuteReader();
            if (mySqlReader.Read())
            {
                mySqlReader.Close();
                sqlString = "DELETE FROM tblpersonnel WHERE ID = " + ID.ToString();
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

                cmd.ExecuteNonQuery();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool updatePerson(long ID, Person personToSave)
        {
            MySql.Data.MySqlClient.MySqlDataReader mySqlReader = null;

            String sqlString = "SELECT * FROM tblpersonnel WHERE ID = " + ID.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            mySqlReader = cmd.ExecuteReader();
            if (mySqlReader.Read())
            {

                mySqlReader.Close();
                sqlString = "UPDATE tblpersonnel SET FirstName='" + personToSave.FirstName + "', LastName='" + personToSave.LastName + "', PayRate=" + personToSave.PayRate + ", StartDate='" + personToSave.StartDate.ToString("yyyy-MM-dd HH:mm:ss") + "', EndDate='" + personToSave.EndDate.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE ID = " + ID.ToString();
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
                cmd.ExecuteNonQuery();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}