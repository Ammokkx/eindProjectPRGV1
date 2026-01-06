using ClientSimulatorBL.Domain;
using ClientSimulatorBL.Domain.DTO;
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
        public List<SimulationDTO> GetAllSimplifiedSimulations()
        {
            string SQL = "SELECT ID, Client, Country FROM Simulation";
            try
            {
                List<SimulationDTO> data = new();
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = SQL;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            data.Add(new((int)reader["ID"], (string)reader["Client"], (string)reader["Country"]));
                        }
                    }
                    return data;

                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public Simulation GetAllSimDetails(int id)
        {
            string SQLSimulation = "SELECT ID as simID, Date as simDate, Seed as simSeed, Client as simClient, Country as simCountry, year as simYear FROM Simulation where ID = @id";
            try
            {
                Simulation data;
                List<SimulatedPerson> simulatedPeople = GetSimulatedPeopleByID(id);
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmdSim = conn.CreateCommand())
                {
                    conn.Open();
                    cmdSim.CommandText = SQLSimulation;
                    cmdSim.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                    cmdSim.Parameters["@id"].Value = id;
                    using (SqlDataReader reader = cmdSim.ExecuteReader())
                    {
                        reader.Read();
                        data = new Simulation((int)reader["simID"], (DateTime)reader["simDate"], (int)reader["simSeed"], (string)reader["simClient"], (string)reader["simCountry"], (int)reader["simYear"], simulatedPeople);

                    }

                    return data;

                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public List<SimulatedPerson> GetSimulatedPeopleByID(int id)
        {
            string SQLPeople = "SELECT pers.First_Name as pFN, pers.Last_Name as pLN, pers.Street as pSTR, pers.Municipality as pMUN, pers.Gender as pGEN, pers.Age as pDATE, pers.HouseNR as pHNUM FROM Simulation as sim join Client as pers on sim.ID = pers.Simulation_ID where sim.ID = @id";

            List<SimulatedPerson> data = new();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmdSim = conn.CreateCommand())
            {
                conn.Open();
                cmdSim.CommandText = SQLPeople;
                cmdSim.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                cmdSim.Parameters["@id"].Value = id;

                using (SqlDataReader reader = cmdSim.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Street tempstr = new((string)reader["pSTR"], (string)reader["pMUN"]);
                        data.Add(new((string)reader["pFN"], (string)reader["pLN"], (string)reader["pGEN"], tempstr, (string)reader["pHNUM"], (DateTime)reader["pDATE"]));

                    }
                }
                return data;
            }
        }
    }
}
