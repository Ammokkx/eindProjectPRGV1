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
    public class SimulationRepository : ISimulationRepository
    {
        private string _connectionString;

        public SimulationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        //public void UploadToDatabase(List<SimulatedPerson> data, string country, int year, string client, DateTime date, int seed)
        //{
        //    int countryID = new CountryRepository(_connectionString).GetCountryIDByName(country);
        //    string SQLName;

        //    if (nameType == NameType.First_Name)
        //    {
        //        SQLName = "INSERT INTO Person_Firstname (Name, Gender, Frequency, Country_Year_ID) VALUES (@name, @gender, @frequency, @countryYearID)";
        //    }
        //    else
        //    {
        //        SQLName = "INSERT INTO Person_Lastname (Name, Frequency, Country_Year_ID) VALUES (@name, @frequency, @countryYearID)";
        //    }

        //    using (SqlConnection conn = new SqlConnection(_connectionString))
        //    using (SqlCommand cmd = conn.CreateCommand())
        //    {
        //        conn.Open();
        //        SqlTransaction tran = conn.BeginTransaction();
        //        cmd.Transaction = tran;

        //        cmd.CommandText = SQLName;

        //        cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar));
        //        cmd.Parameters.Add(new SqlParameter("@frequency", SqlDbType.Int));
        //        cmd.Parameters.Add(new SqlParameter("@countryYearID", SqlDbType.NVarChar));
        //        if (nameType == NameType.First_Name) cmd.Parameters.Add(new SqlParameter("@gender", SqlDbType.NVarChar));

        //        int countryYearID;

        //        try
        //        {
        //            if (!_countryRepository.IsCountryYearInData(country, year)) _countryRepository.UploadCountryYearVersion(country, year);

        //            countryYearID = _countryRepository.GetCountryYearVersionIDByYear(country, year);

        //            if (nameType == NameType.First_Name)
        //            {
        //                foreach (Firstname n in data)
        //                {
        //                    cmd.Parameters["@name"].Value = n.NameName;
        //                    cmd.Parameters["@frequency"].Value = n.Frequency;
        //                    cmd.Parameters["@gender"].Value = n.Gender;
        //                    cmd.Parameters["@countryYearID"].Value = countryYearID;
        //                    cmd.ExecuteNonQuery();
        //                }
        //            }
        //            else
        //            {
        //                foreach (Lastname n in data)
        //                {
        //                    cmd.Parameters["@name"].Value = n.NameName;
        //                    cmd.Parameters["@frequency"].Value = n.Frequency;
        //                    cmd.Parameters["@countryYearID"].Value = countryYearID;
        //                    cmd.ExecuteNonQuery();
        //                }
        //            }


        //            tran.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            tran.Rollback();
        //        }

        //    }

        //}
    }
}
