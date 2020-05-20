using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace populationDatabase1
{
    public class Db
    {
        #region command strings
        private const string CONN_STRING
            = "Server=.\\SQLEXPRESS;Trusted_Connection=True;";

        private const string CREATEDATABASE_COMMAND
            = "IF (db_id(N'Population') IS NULL) CREATE DATABASE Population";

        private const string CREATETABLE_COMMAND
            = "USE Population " +
              "IF NOT EXISTS (SELECT NAME FROM sysobjects WHERE name = 'City') CREATE TABLE City ( " +
              " CityName nvarchar(255) " +
              "     CONSTRAINT City_pk PRIMARY KEY, " +
              " Population float" +
              ")";

        private const string INSERT_COMMAND
            = "USE Population " +
              "IF NOT EXISTS (SELECT cityName FROM City WHERE CityName = @CityName) " +
              "INSERT INTO City (CityName, Population) " +
              "VALUES (@CityName, @Population) ";

        private const string READ_COMMAND
            = "USE Population " +
              "IF EXISTS (SELECT NAME FROM sysobjects WHERE name = 'City') " +
              "SELECT * FROM City";

        private const string UPDATE_COMMAND
            = "USE Population " +
              "UPDATE City SET Population = @Population " +
              "WHERE CityName = @CityName";

        private const string DELETE_COMMAND
            = "USE Population " +
              "DELETE FROM City WHERE CityName = @CityName";

        private const string AVERAGE_POPULATION
            = "USE Population " +
              "SELECT SUM(Population) / COUNT(POPULATION) FROM City";

        private const string TOTAL_POPULATION
            = "USE Population " +
              "SELECT SUM(Population) FROM City";

        private const string HIGHEST_POPULATION
            = "USE Population " +
              "SELECT MAX(Population) FROM City";

        private const string LOWEST_POPULATION
            = "USE Population " +
              "SELECT MIN(Population) FROM City";

        #endregion connection strings

        private static Db db;

        private readonly SqlConnection conn;

        public static Db GetInstance()
        {
            if (db == null)
                db = new Db();
            return db;
        }

        private Db()
        {
            conn = new SqlConnection(CONN_STRING);
            conn.Open();
        }

        #region CRUD Functions
        public void CreateDatabase()
        {
            var cmd = new SqlCommand(CREATEDATABASE_COMMAND, conn);
            cmd.ExecuteNonQuery();
        }

        public void CreateTable()
        {
            var cmd = new SqlCommand(CREATETABLE_COMMAND, conn);
            cmd.ExecuteNonQuery();
        }

        public List<City> Read()
        {
            var cities = new List<City>();
            var cmd = new SqlCommand(READ_COMMAND, conn);
            var rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                var cn = rdr.GetOrdinal("CityName");
                var po = rdr.GetOrdinal("Population");
                cities.Add(new City
                {
                    CityName = rdr.IsDBNull(cn) ? null : rdr.GetString(cn),
                    Population = rdr.GetDouble(po)
                });
            }
            rdr.Close();
            return cities;
        }

        public int Insert(City c)
        {
            SqlCommand cmd = new SqlCommand(INSERT_COMMAND, conn);
            cmd.Parameters.AddWithValue("@CityName", c.CityName);
            cmd.Parameters.AddWithValue("@Population", c.Population);
            return cmd.ExecuteNonQuery();
        }

        public int Update(City c)
        {
            SqlCommand cmd = new SqlCommand(UPDATE_COMMAND, conn);
            
            cmd.Parameters.AddWithValue("@CityName", c.CityName);
            cmd.Parameters.AddWithValue("@Population", c.Population);
            return cmd.ExecuteNonQuery();
        }

        public int Delete(City c)
        {
            SqlCommand cmd = new SqlCommand(DELETE_COMMAND, conn);
            cmd.Parameters.AddWithValue("@CityName", c.CityName);
            return cmd.ExecuteNonQuery();
        }

        #endregion

        #region Scalar Functions
        public double Average()
        {
            var cmd = new SqlCommand(AVERAGE_POPULATION, conn);
            double average = (double)cmd.ExecuteScalar();
            return average; 
        }
        public double Total()
        {
            var cmd = new SqlCommand(TOTAL_POPULATION, conn);
            double average = (double)cmd.ExecuteScalar();
            return average;
        }
        public double Highest()
        {
            var cmd = new SqlCommand(HIGHEST_POPULATION, conn);
            double average = (double)cmd.ExecuteScalar();
            return average;
        }
        public double Lowest()
        {
            var cmd = new SqlCommand(LOWEST_POPULATION, conn);
            double average = (double)cmd.ExecuteScalar();
            return average;
        }
        #endregion
    }
}
