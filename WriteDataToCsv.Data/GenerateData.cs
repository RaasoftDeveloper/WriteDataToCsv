using DBLayer;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WriteDataToCsv.Model;

namespace WriteDataToCsv.Data;

public class GenerateData
{
    public bool IsConnected(DBConfig config)
    {
        string text = "";
        text = "Data Source=" + config.IP + ";Initial Catalog=" + config.Database + ";User ID=" + config.UserName + ";Password=" + config.Password + ";Persist Security Info=True;Max Pool Size=200;Timeout=15;";
        using SqlConnection sqlConnection = new SqlConnection(text);
        using SqlCommand sqlCommand = new SqlCommand("SELECT 1", sqlConnection);
        try
        {
            sqlConnection.Open();
            sqlCommand.ExecuteScalar();
            return true;
        }
        catch (SqlException)
        {
            return false;
        }
    }

    public string setDefaultConnectionString(DBConfig config)
    {
        string text = "";
        return "Data Source=" + config.IP + ";Initial Catalog=" + config.macgridDatabase + ";User ID=" + config.UserName + ";Password=" + config.Password + ";Persist Security Info=True;Max Pool Size=200;Timeout=15;";
    }

    public string setConnectionString(DBConfig config)
    {
        string text = "";
        return "Data Source=" + config.IP + ";Initial Catalog=" + config.customerDatabase + ";User ID=" + config.UserName + ";Password=" + config.Password + ";Persist Security Info=True;Max Pool Size=200;Timeout=15;";
    }

    public async Task<DataTable> GetBillingDetails(string CustomerId, DateTime startDate, DateTime endDate, string macgridDatabase, DBConfig dBConfig)
    {
        new LoginKey(CustomerId, dBConfig);
        DataTable table = new DataTable();
        try
        {
            string ConStr = setConnectionString(dBConfig);
            SqlConnection con = new SqlConnection(ConStr);
            using SqlCommand cmd = new SqlCommand("SP_GETBILLDETAILS_NEW1", con);
            cmd.Parameters.Add("@Query", SqlDbType.Int).Value = 1;
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = startDate.Date.AddHours(0.0).AddMinutes(0.0).AddSeconds(0.0);
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = endDate.Date.AddHours(23.0).AddMinutes(59.0).AddSeconds(59.0);
            using SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.StoredProcedure;
            da.Fill(table);
            return table;
        }
        catch (Exception ex)
        {
            ErrorLog.Error("ClientListGet," + ex.Message.ToString() + " ");
            return table;
        }
    }
}
