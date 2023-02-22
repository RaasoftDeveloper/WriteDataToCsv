namespace WriteDataToCsv.Model;

public class DBConfig
{
	public string IP { get; set; }

	public string UserName { get; set; }

	public string Password { get; set; }

	public string Database { get; set; } = "Master";


	public string macgridDatabase { get; set; }

	public string customerDatabase { get; set; }
}
