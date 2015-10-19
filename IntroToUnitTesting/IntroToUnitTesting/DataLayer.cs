using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace GangManager
{
    /// <summary>
    /// This class accesses the BadassGangDB using SQL queries to create the GangMember objects.
    /// </summary>
    public class DataLayer
    {
        private const string CONNECTIONSTRING = "Data Source=localhost;Initial Catalog=BadassGangDB;Integrated Security=True";

        /// <summary>
        /// This method gives an example that is closely related to the project. The corresponding unit test is helpful when
        /// trying to debug the project. It ensures that you are connecting to the database and creating the correct object, without
        /// the need to have a working user interface to run the method.
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public GangMember GetGangMemberByMemberID(int memberID)
        {
            GangMember localGangMember = null;
            using (SqlConnection sqlConn = new SqlConnection(CONNECTIONSTRING))
            {
                sqlConn.Open();
                string sqlQuery = "SELECT * FROM [Members] WHERE MemberID=@memberID";
                using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    sqlCmd.Parameters.AddWithValue("memberID", memberID);
                    using (SqlDataReader sqlDataReader = sqlCmd.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            localGangMember = new GangMember();
                            localGangMember.MemberID = sqlDataReader.GetInt32(0);
                            localGangMember.MemberName = sqlDataReader.GetString(1);
                            localGangMember.MemberRank = sqlDataReader.GetString(2);
                        }
                    }
                }
            }
            return localGangMember;
        }

        /// <summary>
        /// This method does the same job as the previous one, but this time doing things async. We are not required to
        /// do anything like this in the project, but it might be helpful when you come to this topic in your own studies.
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public async Task<GangMember> GetGangMemberByIDAsync(int memberID)
        {
            GangMember localGangMember = null;
            using (SqlConnection sqlConn = new SqlConnection(CONNECTIONSTRING))
            {
                await sqlConn.OpenAsync();
                string sqlQuery = "SELECT * FROM [Members] WHERE MemberID=@memberID";
                using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    sqlCmd.Parameters.AddWithValue("memberID", memberID);
                    using (SqlDataReader sqlDataReader = await sqlCmd.ExecuteReaderAsync())
                    {
                        while (await sqlDataReader.ReadAsync())
                        {
                            localGangMember = new GangMember();
                            localGangMember.MemberID = sqlDataReader.GetInt32(0);
                            localGangMember.MemberName = sqlDataReader.GetString(1);
                            localGangMember.MemberRank = sqlDataReader.GetString(2);
                        }
                    }
                }
            }
            return localGangMember;
        }

        /// <summary>
        /// This method returns all the gang members from the database and stores them into a collection.
        /// Notice how in the corresponding test we can loop through all the members to check that they match.
        /// </summary>
        /// <returns></returns>
        public List<GangMember> GetAllGangMembers()
        {
            List<GangMember> gangMembers = new List<GangMember>();
            using (SqlConnection sqlConn = new SqlConnection(CONNECTIONSTRING))
            {
                sqlConn.Open();
                string sqlQuery = "SELECT * FROM [Members]";
                using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    using (SqlDataReader sqlDataReader = sqlCmd.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            GangMember localGangMember = new GangMember();
                            localGangMember.MemberID = sqlDataReader.GetInt32(0);
                            localGangMember.MemberName = sqlDataReader.GetString(1);
                            localGangMember.MemberRank = sqlDataReader.GetString(2);
                            gangMembers.Add(localGangMember);
                        }
                    }
                }
            }
            return gangMembers;
        }

        public async Task<List<GangMember>> GetAllGangMembersAsync()
        {
            List<GangMember> gangMembers = new List<GangMember>();

            using (SqlConnection sqlConn = new SqlConnection(CONNECTIONSTRING))
            {
                await sqlConn.OpenAsync();
                string sqlQuery = "SELECT * FROM [Members]";
                using (SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlConn))
                {
                    using (SqlDataReader sqlDataReader = await sqlCmd.ExecuteReaderAsync())
                    {
                        while (await sqlDataReader.ReadAsync())
                        {
                            GangMember localGangMember = new GangMember();
                            localGangMember.MemberID = sqlDataReader.GetInt32(0);
                            localGangMember.MemberName = sqlDataReader.GetString(1);
                            localGangMember.MemberRank = sqlDataReader.GetString(2);
                            // Comment out the following line of code to make the test fail, this was something that I forgot to do
                            // and the unit test picked it up - reinforcing why tests are so helpful.
                            gangMembers.Add(localGangMember);
                        }
                    }
                }
            }
            return gangMembers;
        }
    }
}
