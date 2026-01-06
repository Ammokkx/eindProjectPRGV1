using ClientSimulatorBL.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSimulatorDL.DataBaseRepos
{
    public class CountryRepository : ICountryRepository
    {
        private string _connectionString;

        public CountryRepository(string connectionstring)
        {
            _connectionString = connectionstring;
        }






        public void UploadCountry(string country)
        {
            string SQL = "INSERT INTO Country(Name) VALUES(@Name)";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@Name", country);
                cmd.ExecuteNonQuery();
            }
        }

        public bool IsCountryInData(string country)
        {
            string SQL = "SELECT count(Name) FROM Country WHERE Name=@Name";


            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@Name", country);
                int count = (int)cmd.ExecuteScalar();
                if (count > 0)
                {

                    return true;
                }

            }
            return false;
        }

        public int GetCountryIDByName(string country)
        {
            string SQL = "SELECT ID FROM Country WHERE Name=@Name";


            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@Name", country);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    int id = reader.GetInt32(reader.GetOrdinal("ID"));

                    return id;
                }

            }

        }

        public void UploadCountryYearVersion(string country, int year)
        {
            string SQLCountryYearID = "INSERT INTO Country_Year (Country_ID, Year_Uploaded) output VALUES (@countryID, @year)";
            int countryID = GetCountryIDByName(country);

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmdCountryYearID = conn.CreateCommand())
            {
                conn.Open();
                cmdCountryYearID.CommandText = SQLCountryYearID;
                cmdCountryYearID.Parameters.AddWithValue("@countryID", countryID);
                cmdCountryYearID.Parameters.AddWithValue("@year", year);
                cmdCountryYearID.ExecuteNonQuery();
            }
        }

        public int GetCountryYearVersionIDByYear(string country, int year)
        {
            string SQL = "SELECT ID FROM Country_Year WHERE Country_ID=@countryID AND Year_Uploaded=@year";
            int countryID = GetCountryIDByName(country);

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@countryID", countryID);
                cmd.Parameters.AddWithValue("@year", year);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    int id = reader.GetInt32(reader.GetOrdinal("ID"));

                    return id;
                }

            }
        }
        
        public bool IsCountryYearInData(string country, int year)
        {
            string SQL = "SELECT count(ID) FROM Country_Year WHERE Country_ID=@countryID AND Year_Uploaded=@year";
            int countryID = GetCountryIDByName(country);

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {

                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@countryID", countryID);
                cmd.Parameters.AddWithValue("@year", year);
                int count = (int)cmd.ExecuteScalar();
                if (count > 0)
                {
                    return true;
                }

            }
            return false;
        }
    }
}