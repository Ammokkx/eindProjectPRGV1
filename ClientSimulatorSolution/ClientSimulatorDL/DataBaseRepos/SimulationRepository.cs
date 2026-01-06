using ClientSimulatorBL.Domain;
using ClientSimulatorBL.Enums;
using ClientSimulatorBL.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
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

        public void UploadToDatabase(List<SimulatedPerson> data, string country, int year, string client, DateTime date, int seed)
        {
            string SQLSimulation;
            string SQLClient;

            SQLSimulation = "INSERT INTO Simulation (Seed, Date, Client, Country, year) output INSERTED.ID VALUES (@seed, @date, @client, @country, @year)";
            SQLClient = "INSERT INTO Client(Simulation_ID, First_Name, Last_Name, Street, Municipality, Gender, Age, HouseNr) VALUES (@simID, @fname, @lname, @str, @mun, @gender, @age, @housenr)";
           

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmdSim = conn.CreateCommand())
            using (SqlCommand cmdClient = conn.CreateCommand())
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                cmdSim.Transaction = tran;
                cmdClient.Transaction = tran;

                cmdSim.CommandText = SQLSimulation;
                cmdSim.Parameters.Add(new SqlParameter("@seed", SqlDbType.Int));
                cmdSim.Parameters["@seed"].Value = seed;
                cmdSim.Parameters.Add(new SqlParameter("@date", SqlDbType.DateTime));
                cmdSim.Parameters["@date"].Value = date;
                cmdSim.Parameters.Add(new SqlParameter("@client", SqlDbType.NVarChar));
                cmdSim.Parameters["@client"].Value = client;
                cmdSim.Parameters.Add(new SqlParameter("@country", SqlDbType.NVarChar));
                cmdSim.Parameters["@country"].Value = country;
                cmdSim.Parameters.Add(new SqlParameter("@year", SqlDbType.Int));
                cmdSim.Parameters["@year"].Value = year;

                cmdClient.CommandText = SQLClient;
                cmdClient.Parameters.Add(new SqlParameter("@simID", SqlDbType.Int)); cmdClient.Parameters.Add(new SqlParameter("@fname", SqlDbType.NVarChar)); cmdClient.Parameters.Add(new SqlParameter("@lname", SqlDbType.NVarChar)); cmdClient.Parameters.Add(new SqlParameter("@str", SqlDbType.NVarChar));
                cmdClient.Parameters.Add(new SqlParameter("@mun", SqlDbType.NVarChar));
                cmdClient.Parameters.Add(new SqlParameter("@gender", SqlDbType.NVarChar));
                cmdClient.Parameters.Add(new SqlParameter("@age", SqlDbType.DateTime));
                cmdClient.Parameters.Add(new SqlParameter("@housenr", SqlDbType.NVarChar));

                int simID;

                try
                {

                    simID = (int)cmdSim.ExecuteScalar();

                        foreach (SimulatedPerson n in data)
                        {
                        cmdClient.Parameters["@simID"].Value = simID;
                        cmdClient.Parameters["@fname"].Value = n.Firstname;
                        cmdClient.Parameters["@lname"].Value = n.Lastname;
                        cmdClient.Parameters["@str"].Value = n.Address.Name;
                        cmdClient.Parameters["@mun"].Value = n.Address.Municipality;
                        cmdClient.Parameters["@gender"].Value = n.Gender;
                        cmdClient.Parameters["@age"].Value = n.BirthDate;
                        cmdClient.Parameters["@housenr"].Value = n.HouseNr;
                        cmdClient.ExecuteNonQuery();
                        }
                    

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                }

            }

        }
        public List<Simulation> GetAllSimulations()
        {

        }
    }
}
