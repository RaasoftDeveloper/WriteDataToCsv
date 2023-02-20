using DBLayer;
using System;
using System.Data.SqlClient;
using WriteDataToCsv.Data;

namespace WriteDataToCsv.Model;

public class LoginKey
{
    private GenerateData GD = new GenerateData();

    public string CustomerID { get; set; }

    public string DatabaseName { get; set; }

    public string UploadTime { get; set; }

    public string Billingcycledate { get; set; }

    public string CustomerName { get; set; }

    public decimal EBUnitrate { get; set; }

    public decimal DGUnitrate { get; set; }

    public string CurrencyType { get; set; }

    public TimeSpan BillingTime { get; set; }

    public string ProductID { get; set; }

    public LoginKey(string _CustomerID, DBConfig dbconfig)
    {
        Get(_CustomerID, dbconfig);
    }

    public void Get(string CustomerID, DBConfig dbconfig)
    {
        try
        {
            string connectionString = GD.setDefaultConnectionString(dbconfig);
            DBHelpers dBHelpers = new DBHelpers("select * from [dbo].[SETTINGS_CustomerMaster] where CustomerId='" + CustomerID + "'", connectionString);
            dBHelpers.DataReader = dBHelpers.ExecuteDataReader();
            if (dBHelpers.DataReader.HasRows)
            {
                SqlDataReader dataReader = dBHelpers.DataReader;
                while (dataReader.Read())
                {
                    this.CustomerID = dataReader["CustomerId"].ToString();
                    DatabaseName = dataReader["CustomerDB"].ToString();
                    UploadTime = dataReader["DataUploadTime"].ToString();
                    Billingcycledate = dataReader["BillingCycleDate"].ToString();
                    CustomerName = dataReader["CustomerName"].ToString();
                    EBUnitrate = Convert.ToDecimal(dataReader["KwhUnitRate"]);
                    DGUnitrate = Convert.ToDecimal(dataReader["DGUnitRate"]);
                    CurrencyType = dataReader["CurrencyType"].ToString();
                    BillingTime = TimeSpan.Parse(dataReader["BillingTime"].ToString());
                    ProductID = dataReader["ProductID"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.Error(ex.Message.ToString());
        }
    }
}
