using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using WriteDataToCsv.Model;

namespace JohnsonControlHelper
{
    public partial class formEdit : Form
    {

        public string CustomerID = "";
        public formEdit()
        {
            InitializeComponent();
        }

        private void FormEdit_Load(object sender, EventArgs e)
        {
            string path2 = Application.StartupPath;
            path2 += "\\DBSettings.txt";
            string[] lines = File.ReadAllLines(path2);
            int lineNumber = 1;
            string macgridDatabase = "";
            string folderPath = "";
            string fileNameFormat = "";
            DBConfig dBConfig = new DBConfig();
            string[] array = lines;
            foreach (string line in array)
            {
                switch (lineNumber)
                {
                    case 1:
                        dBConfig.IP = line;
                        break;
                    case 2:
                        dBConfig.UserName = line;
                        break;
                    case 3:
                        macgridDatabase = line;
                        dBConfig.macgridDatabase = line;
                        break;
                    case 4:
                        dBConfig.Password = line;
                        break;
                    case 5:
                        dBConfig.Database = line;
                        dBConfig.customerDatabase = line;
                        break;
                    case 6:
                        CustomerID = line;
                        break;
                    case 7:
                        folderPath = line;
                        break;
                    case 8:
                        fileNameFormat = line;
                        break;
                }
                lineNumber++;
            }

            //string text = "";
            string text = "Data Source=" + dBConfig.IP + ";Initial Catalog=" + dBConfig.Database + ";User ID=" + dBConfig.UserName + ";Password=" + dBConfig.Password + ";Persist Security Info=True;Max Pool Size=200;Timeout=15;";
            using SqlConnection sqlConnection = new SqlConnection(text);

            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT MeterName from JCI_Data", sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                comboBox1.DisplayMember = "MeterName";
                comboBox1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {

            string path2 = Application.StartupPath;
            path2 += "\\DBSettings.txt";
            string[] lines = File.ReadAllLines(path2);
            int lineNumber = 1;
            string macgridDatabase = "";
            string folderPath = "";
            string fileNameFormat = "";
            DBConfig dBConfig = new DBConfig();
            string[] array = lines;
            foreach (string line in array)
            {
                switch (lineNumber)
                {
                    case 1:
                        dBConfig.IP = line;
                        break;
                    case 2:
                        dBConfig.UserName = line;
                        break;
                    case 3:
                        macgridDatabase = line;
                        dBConfig.macgridDatabase = line;
                        break;
                    case 4:
                        dBConfig.Password = line;
                        break;
                    case 5:
                        dBConfig.Database = line;
                        dBConfig.customerDatabase = line;
                        break;
                    case 6:
                        CustomerID = line;
                        break;
                    case 7:
                        folderPath = line;
                        break;
                    case 8:
                        fileNameFormat = line;
                        break;
                }
                lineNumber++;
            }

            //string text = "";
            string text = "Data Source=" + dBConfig.IP + ";Initial Catalog=" + dBConfig.Database + ";User ID=" + dBConfig.UserName + ";Password=" + dBConfig.Password + ";Persist Security Info=True;Max Pool Size=200;Timeout=15;";
            using SqlConnection sqlConnection = new SqlConnection(text);
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = "Update JCI_Data set BusinessPartnerName='" + txt_business_partner_name.Text + "',Contract='" + txt_contract.Text + "',Building='" + txt_building.Text + "',DewaPremiseNo='" + txt_dewa_premise_no.Text + "',DewaMeterNoMain='" + txt_dewa_meter_no.Text + "',UtilityType='" + txt_utility.Text + "', BusinessEntity='" + txt_Business_Entity.Text + "',FloorName='" + txt_Floor.Text + "',MDB='" + txt_MDB.Text + "',DewaContractAccountNoMain='" + txt_dewa_contract_account.Text + "'where MeterName='" + comboBox1.Text + "'";
                sqlCommand.ExecuteNonQuery();
                
                MessageBox.Show("Updated Successfully");
                
                txt_business_partner_name.Text = "";
                txt_contract.Text = "";
                txt_building.Text = "";
                txt_dewa_premise_no.Text = "";
                txt_dewa_meter_no.Text = "";
                txt_utility.Text = "";
                txt_Business_Entity.Text = "";
                txt_Floor.Text = "";
                txt_MDB.Text = "";
                txt_dewa_contract_account.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            string MeterName = comboBox1.Text;
            string path2 = Application.StartupPath;
            path2 += "\\DBSettings.txt";
            string[] lines = File.ReadAllLines(path2);
            int lineNumber = 1;
            string macgridDatabase = "";
            string folderPath = "";
            string fileNameFormat = "";
            DBConfig dBConfig = new DBConfig();
            string[] array = lines;
            foreach (string line in array)
            {
                switch (lineNumber)
                {
                    case 1:
                        dBConfig.IP = line;
                        break;
                    case 2:
                        dBConfig.UserName = line;
                        break;
                    case 3:
                        macgridDatabase = line;
                        dBConfig.macgridDatabase = line;
                        break;
                    case 4:
                        dBConfig.Password = line;
                        break;
                    case 5:
                        dBConfig.Database = line;
                        dBConfig.customerDatabase = line;
                        break;
                    case 6:
                        CustomerID = line;
                        break;
                    case 7:
                        folderPath = line;
                        break;
                    case 8:
                        fileNameFormat = line;
                        break;
                }
                lineNumber++;
            }

            //string text = "";
            string text = "Data Source=" + dBConfig.IP + ";Initial Catalog=" + dBConfig.Database + ";User ID=" + dBConfig.UserName + ";Password=" + dBConfig.Password + ";Persist Security Info=True;Max Pool Size=200;Timeout=15;";
            using SqlConnection sqlConnection = new SqlConnection(text);
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM JCI_DATA WHERE (MeterName='" + MeterName + "') ", sqlConnection);
                SqlDataReader sqlDataReader;
                sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    txt_business_partner_name.Text = sqlDataReader.GetValue(2).ToString();
                    txt_contract.Text = sqlDataReader.GetValue(3).ToString();
                    txt_building.Text = sqlDataReader.GetValue(4).ToString();
                    txt_dewa_premise_no.Text = sqlDataReader.GetValue(5).ToString();
                    txt_dewa_meter_no.Text = sqlDataReader.GetValue(6).ToString();
                    txt_utility.Text = sqlDataReader.GetValue(7).ToString();
                    txt_Business_Entity.Text = sqlDataReader.GetValue(8).ToString();
                    txt_Floor.Text = sqlDataReader.GetValue(9).ToString();
                    txt_MDB.Text = sqlDataReader.GetValue(10).ToString();
                    txt_dewa_contract_account.Text = sqlDataReader.GetValue(11).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
