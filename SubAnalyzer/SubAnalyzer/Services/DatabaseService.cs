using SubAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubAnalyzer.Services
{
    public class DatabaseService
    {

        public static void UpdateDatabase(PostMain post)
        {
            try
            {
                

                using(System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\jmcbr\\Source\\Repos\\subanalyzer\\SubAnalyzer\\SubAnalyzer\\subanalyzer.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                    var query = String.Format("INSERT INTO [PostMain] (Title, UserName, TitleLink, PostId, PostDate) VALUES('{0}', '{1}', '{2}', '{3}', '{4}')",
                        post.Title,
                        post.UserName,
                        post.TitleLink,
                        post.PostId,
                        post.PostDate
                        );


                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Connection = sqlConnection;

                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                }

                
            }
            catch(Exception e)
            {
                throw;
            }      
        }
    }
}
