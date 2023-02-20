using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WriteDataToCsv.Data;
using WriteDataToCsv.Model;

namespace WriteDataToCsv;

public class formGenerate : Form
{
    public class CsvRow : List<string>
    {
        public string LineText { get; set; }
    }

    public class CsvFileWriter : StreamWriter
    {
        public CsvFileWriter(Stream stream)
            : base(stream)
        {
        }

        public CsvFileWriter(string filename)
            : base(filename)
        {
        }

        public void WriteRow(CsvRow row)
        {
            StringBuilder stringBuilder = new StringBuilder();
            bool flag = true;
            foreach (string item in row)
            {
                if (!flag)
                {
                    stringBuilder.Append(',');
                }
                if (item.IndexOfAny(new char[2] { '"', ',' }) != -1)
                {
                    stringBuilder.AppendFormat("\"{0}\"", item.Replace("\"", "\"\""));
                }
                else
                {
                    stringBuilder.Append(item);
                }
                flag = false;
            }
            row.LineText = stringBuilder.ToString();
            WriteLine(row.LineText);
        }
    }

    private string CustomerID = "";

    private IContainer components = null;

    private Button btnGenerate;

    private DateTimePicker dtpStartDate;

    private Label label2;

    private DateTimePicker dtpEndDate;

    private Label label1;

    private Panel panel1;

    private Label label3;

    public formGenerate()
    {
        InitializeComponent();
    }

    private async void WriteDataFromDB(DateTime startDate, DateTime endDate)
    {
        GenerateData GD = new GenerateData();
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
        if (GD.IsConnected(dBConfig))
        {
            DataTable dt = await GD.GetBillingDetails(CustomerID, startDate, endDate, macgridDatabase, dBConfig);
            string fileName = fileNameFormat + startDate.ToString("dd-MM-yyyy") + " to " + endDate.ToString("dd-MM-yyyy") + ".csv";
            string file = folderPath + fileName;
            try
            {
                using (CsvFileWriter writer = new CsvFileWriter(file))
                {
                    CsvRow rowHeader = new CsvRow();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        rowHeader.Add(dc.ColumnName);
                    }
                    writer.WriteRow(rowHeader);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CsvRow row = new CsvRow();
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            row.Add(dt.Rows[i][j].ToString());
                        }
                        writer.WriteRow(row);
                    }
                }
                MessageBox.Show("File generated successfully in the path:" + file);
            }
            catch (Exception)
            {
                MessageBox.Show("Please close the generated file before generation");
            }
        }
        else
        {
            MessageBox.Show("Unable to Connect Server.");
        }
    }

    private void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            WriteDataFromDB(dtpStartDate.Value, dtpEndDate.Value);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void Form1_Load(object sender, EventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            this.btnGenerate = new System.Windows.Forms.Button();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.Location = new System.Drawing.Point(249, 181);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "dd MM yyyy";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(94, 12);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(110, 20);
            this.dtpStartDate.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(237, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "End Date";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "dd MM yyyy";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(307, 12);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(109, 20);
            this.dtpEndDate.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Start Date";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtpEndDate);
            this.panel1.Controls.Add(this.dtpStartDate);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(60, 109);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(463, 53);
            this.panel1.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(225, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 24);
            this.label3.TabIndex = 6;
            this.label3.Text = "Generate Excel";
            // 
            // formGenerate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 321);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnGenerate);
            this.Name = "formGenerate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generate Excel";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }
}
