using System.Configuration;

namespace WriteDataToCsv.Model;

public static class GlobalData
{
	public static string ConStr(string ClientDatabaseName = "")
	{
		string text = "";
		if (ClientDatabaseName == "")
		{
			text = ConfigurationManager.ConnectionStrings["MacgridDB"].ConnectionString;
			return text.Replace("Initial Catalog=MACGRID15", "Initial Catalog=" + ClientDatabaseName);
		}
		text = ConfigurationManager.ConnectionStrings["CustomerDB"].ConnectionString;
		return text.Replace("Initial Catalog=CustomerDB", "Initial Catalog=" + ClientDatabaseName);
	}
}
