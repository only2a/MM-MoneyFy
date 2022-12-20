using Microsoft.Data.SqlClient;
using System;
using System.Configuration;


namespace MM_MoneyFy.Model.Data
{
    public class ConnectionManager
    {
        
        private static string conUser = ConfigurationManager.ConnectionStrings["DatabaseUser"].ToString();
        private static string conAdmin = ConfigurationManager.ConnectionStrings["DatabaseAdmin"].ToString();

        public static SqlConnection GetConnection(string type)
        {
            switch (type)
            {
                case "User":
                    try
                    {
                        SqlConnection connection = new SqlConnection(conUser);
                        connection.Open();
                        return connection;
                    }
                    catch
                    {
                        end();
                        return null;
                    }
                case "Admin":
                    try
                    {
                        SqlConnection connection = new SqlConnection(conAdmin);
                        connection.Open();
                        return connection;
                    }
                    catch
                    {
                        end();
                        return null;
                    }
                //case "Read":
                //    try
                //    {
                //        SqlConnection connection = new SqlConnection(conUser2);
                //        connection.Open();
                //        return connection;
                //    }
                //    catch
                //    {
                //        try
                //        {
                //            SqlConnection connection = new SqlConnection(conUser);
                //            connection.Open();
                //            return connection;
                //        }
                //        catch
                //        {
                //            end();
                //            return null;
                //        }
                //    }
                default: return null;
            }
        }

       

        private static void end()
        {
            System.Windows.MessageBox.Show("Server error, try later");
            Environment.Exit(0);
        }
    }
}
