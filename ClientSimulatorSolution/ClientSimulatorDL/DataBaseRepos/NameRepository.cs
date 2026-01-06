using ClientSimulatorBL.Domain;
using ClientSimulatorBL.Enums;
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
    public class NameRepository : INameRepository
    {
        private string _connectionString;
        private CountryRepository _countryRepository;

        public NameRepository(string connectionstring)
        {
            _connectionString = connectionstring;
            _countryRepository = new CountryRepository(_connectionString);
        }


        public void UploadToDatabase(List<Name> data, string country, int year, NameType nameType)
        {
            int countryID = new CountryRepository(_connectionString).GetCountryIDByName(country);
            string SQLName;

            if (nameType == NameType.First_Name)
            {
                SQLName = "INSERT INTO Person_Firstname (Name, Gender, Frequency, Country_Year_ID) VALUES (@name, @gender, @frequency, @countryYearID)";
            }
            else
            {
                SQLName = "INSERT INTO Person_Lastname (Name, Frequency, Country_Year_ID) VALUES (@name, @frequency, @countryYearID)";
            }

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                cmd.Transaction = tran;

                cmd.CommandText = SQLName;

                cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar));
                cmd.Parameters.Add(new SqlParameter("@frequency", SqlDbType.Int));
                cmd.Parameters.Add(new SqlParameter("@countryYearID", SqlDbType.NVarChar));
                if (nameType == NameType.First_Name) cmd.Parameters.Add(new SqlParameter("@gender", SqlDbType.NVarChar));

                int countryYearID;

                try
                {
                    if (!_countryRepository.IsCountryYearInData(country, year)) _countryRepository.UploadCountryYearVersion(country, year);

                    countryYearID = _countryRepository.GetCountryYearVersionIDByYear(country, year);

                    if (nameType == NameType.First_Name)
                    {
                        foreach (Firstname n in data)
                        {
                            cmd.Parameters["@name"].Value = n.NameName;
                            cmd.Parameters["@frequency"].Value = n.Frequency;
                            cmd.Parameters["@gender"].Value = n.Gender;
                            cmd.Parameters["@countryYearID"].Value = countryYearID;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        foreach (Lastname n in data)
                        {
                            cmd.Parameters["@name"].Value = n.NameName;
                            cmd.Parameters["@frequency"].Value = n.Frequency;
                            cmd.Parameters["@countryYearID"].Value = countryYearID;
                            cmd.ExecuteNonQuery();
                        }
                    }


                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                }

            }

        }

        public List<Firstname> GetFirstNamesByCountryIDAndYear(int id, int year)
        {
            string SQL = "SELECT p.Name as Name, p.Gender as Gender, p.Frequency as Frequency FROM Person_Firstname as p join country_year as ctry on p.country_year_id = ctry.id join country as ctr on ctry.country_id = ctr.id where ctr.id = @id and ctry.Year_Uploaded = @year";
            try
            {
                List<Firstname> data = new();
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = SQL;
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                    cmd.Parameters["@id"].Value = id;
                    cmd.Parameters.Add(new SqlParameter("@year", SqlDbType.Int));
                    cmd.Parameters["@year"].Value = year;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = (string)reader["Name"];
                            string? gender = (string?)reader["Gender"];
                            int? frequency = (int?)reader["Frequency"];

                            data.Add(new(name, frequency, gender));
                        }
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<Lastname> GetLastNamesByCountryIDAndYear(int id, int year)
        {
            string SQL = "SELECT p.Name as Name, p.Frequency as Frequency FROM Person_Lastname as p join country_year as ctry on p.country_year_id = ctry.id join country as ctr on ctry.country_id = ctr.id where ctr.id = @id and ctry.Year_Uploaded = @year";
            try
            {
                List<Lastname> data = new();
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = SQL;
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                    cmd.Parameters["@id"].Value = id;
                    cmd.Parameters.Add(new SqlParameter("@year", SqlDbType.Int));
                    cmd.Parameters["@year"].Value = year;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = (string)reader["Name"];
                            int? frequency = (int?)reader["Frequency"];

                            data.Add(new(name, frequency));
                        }
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
