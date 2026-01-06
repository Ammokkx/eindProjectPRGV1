using ClientSimulatorBL.Domain;
using ClientSimulatorBL.Interfaces;
using ClientSimulatorDL.Readers;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ClientSimulatorDL.DataBaseRepos
{
    public class StreetRepository : IStreetRepository
    {
        private string _connectionString;
        private CountryRepository _countryRepository;

        public StreetRepository(string connectionstring)
        {
            _connectionString = connectionstring;
            _countryRepository = new CountryRepository(_connectionString);
        }

        public void UploadToDatabase(List<Street> data, string country, int year)
        {
            int countryID = _countryRepository.GetCountryIDByName(country);
            string SQLStreet = "INSERT INTO Street (Name, Municipality_ID, Country_Year_ID) VALUES (@name, @munID, @countryYearID)";
            string SQLMunicipality = "INSERT INTO Municipality(Country_Year_ID, Name) output INSERTED.ID VALUES (@countryYearID, @name)";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmdStreet = conn.CreateCommand())
            using (SqlCommand cmdMunicipality = conn.CreateCommand())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                cmdStreet.Transaction = tran;
                cmdMunicipality.Transaction = tran;

                cmdMunicipality.CommandText = SQLMunicipality;
                cmdStreet.CommandText = SQLStreet;

                cmdStreet.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar));
                cmdStreet.Parameters.Add(new SqlParameter("@munID", SqlDbType.Int));
                cmdStreet.Parameters.Add(new SqlParameter("@countryYearID", SqlDbType.Int));

                cmdMunicipality.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar));
                cmdMunicipality.Parameters.Add(new SqlParameter("@countryYearID", SqlDbType.Int));
                int countryYearID, munID;
                List<string> municipalities = data.Select(x => x.Municipality).Distinct().ToList();
                List<Street> distinctStreets = data.DistinctBy(x => x.Name).ToList();

                try
                {
                    if (!_countryRepository.IsCountryYearInData(country, year)) _countryRepository.UploadCountryYearVersion(country, year);

                    countryYearID = _countryRepository.GetCountryYearVersionIDByYear(country, year);

                    foreach (string m in municipalities)
                    {
                        cmdMunicipality.Parameters["@name"].Value = m;
                        cmdMunicipality.Parameters["@countryYearID"].Value = countryYearID;
                        munID = (int)cmdMunicipality.ExecuteScalar();

                        foreach (Street street in distinctStreets.Where(x => x.Municipality == m).ToList())
                        {
                            cmdStreet.Parameters["@name"].Value = street.Name;
                            cmdStreet.Parameters["@munID"].Value = munID;
                            cmdStreet.Parameters["@countryYearID"].Value = countryYearID;
                            cmdStreet.ExecuteNonQuery();

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
        public List<string> GetAllMunicipalitiesByCountryIDAndYear(int id, int year)
        {
            string SQL = "SELECT str.Name as Name FROM Municipality as str join country_year as ctry on str.country_year_id = ctry.id join country as ctr on ctry.country_id = ctr.id where ctr.id = @id and ctry.Year_Uploaded = @year";
            try
            {
                List<string> data = new();
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
                            string name = reader.GetString(reader.GetOrdinal("Name"));
                            data.Add(name);
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

        public List<Street> GetAllStreetsByCountryIDAndYear(int id, int year)
        {
            string SQL = "SELECT str.Name as Name, str.Municipality as Municipality FROM Street as str join country_year as ctry on str.country_year_id = ctry.id join country as ctr on ctry.country_id = ctr.id where ctr.id = @id and ctry.Year_Uploaded = @year";
            try
            {
                List<Street> data = new();
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
                            string municipality = (string)reader["Municipality"];
                            data.Add(new(name, municipality));
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