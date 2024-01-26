using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataSetManually
{
    public partial class Form1 : Form
    {
        DataSet myData = new DataSet();

        public Form1()
        {
            InitializeComponent();
            CreateAndFillData();
        }

        private void CreateAndFillData()
        {
            DataColumn column;
            
            
            // Users
            DataTable users = new DataTable("Users");

            column = new DataColumn("UserID", typeof(Int32));
            column.ExtendedProperties.Add("Leiras", "Az egyedi azonosítója a felhasználónak");
            column.AutoIncrement = true;
            column.AutoIncrementSeed = 1;
            column.AutoIncrementStep = 1;
            users.Columns.Add(column);
            users.PrimaryKey = new DataColumn[] { column };


            column = new DataColumn("UserName", typeof(String));
            column.ExtendedProperties.Add("Leiras", "A felhasználó neve");
            users.Columns.Add(column);
            

            column = new DataColumn("Email", typeof(String));
            column.ExtendedProperties.Add("Leiras", "A felhasználó e-mail címe");
            users.Columns.Add(column);

            column = new DataColumn("RegistrationDate", typeof(DateTime));
            column.ExtendedProperties.Add("Leiras", "A felhasználó regisztrációjának dátuma");
            column.DefaultValue = new DateTime(1900, 01, 01, 00, 00, 00);
            users.Columns.Add(column);

            



            //Orders
            DataTable orders = new DataTable("Orders");

            column = new DataColumn("OrderID", typeof(Int32));
            column.ExtendedProperties.Add("Leiras", "Az egyedi azonosítója a rendelésnek");
            column.AutoIncrement = true;
            column.AutoIncrementSeed = 1;
            column.AutoIncrementStep = 1;
            orders.Columns.Add(column);
            orders.PrimaryKey = new DataColumn[] { column };


            column = new DataColumn("UserID", typeof(Int32));
            column.ExtendedProperties.Add("Leiras", "Azonosítója a rendelést leadó felhasználónak");
            orders.Columns.Add(column);
            
            
            column = new DataColumn("OrderDate", typeof(DateTime));
            column.ExtendedProperties.Add("Leiras", "A rendelés dátuma");
            column.DefaultValue = new DateTime(1900, 01, 01, 00, 00, 00);
            orders.Columns.Add(column);


            column = new DataColumn("TotalAmount", typeof(Decimal));
            column.ExtendedProperties.Add("Leiras", "A rendelés teljes összege");
            column.DefaultValue = 0;
            orders.Columns.Add(column);



            // Products
            DataTable products = new DataTable("Products");

            column = new DataColumn("ProductID", typeof(Int32));
            column.ExtendedProperties.Add("Leiras", "Az egyedi azonosítója a terméknek");
            column.AutoIncrement = true;
            column.AutoIncrementSeed = 1;
            column.AutoIncrementStep = 1;
            products.Columns.Add(column);
            products.PrimaryKey = new DataColumn[] { column };


            column = new DataColumn("ProductName", typeof(String));
            column.ExtendedProperties.Add("Leiras", "A termék neve");
            products.Columns.Add(column);


            column = new DataColumn("Category", typeof(String));
            column.ExtendedProperties.Add("Leiras", "A termék kategóriája");
            products.Columns.Add(column);


            column = new DataColumn("Price", typeof(Decimal));
            column.ExtendedProperties.Add("Leiras", "A termék ára");
            column.DefaultValue = 0;
            products.Columns.Add(column);





            myData.Tables.Add(users);
            myData.Tables.Add(orders);
            myData.Tables.Add(products);

            myData.WriteXmlSchema("data.xml");

        }

        private string DataStringHTML()
        {
            string tableHTML = "<table>";

            foreach (DataTable table in myData.Tables)
            {
                tableHTML += 
                    $"<tr><th colspan='5'>{table.TableName}</th></tr>" +
                    $"<tr>" +
                    $"  <th>PK</th>" +
                    $"  <th>Név</th>" +
                    $"  <th>Típus</th>" +
                    $"  <th>Default</th>" +
                    $"  <th>Leiras</th>" +
                    $"</tr>";
                foreach (DataColumn col in table.Columns)
                {
                    tableHTML +=
                        $"<tr>" +
                        $"  <td>{(table.PrimaryKey[0] == col ? "PK" : "")}</td>" +
                        $"  <td>{col.ColumnName}</td>" +
                        $"  <td>{Convert.ToString(col.DataType).Replace("System.", "")}</td>" +
                        $"  <td>{col.DefaultValue}</td>" +
                        $"  <td>{col.ExtendedProperties["Leiras"]}</td>" +
                        $"</tr>";
                }
            }

            tableHTML += "</table>";

            string stringHTML =
                $"<!DOCTYPE html>" +
                $"<html>" +
                $"  <head>" +
                $"      <meta charset='UTF-8'>" +
                $"      <meta name='viewport' content='width=device-width, initial-scale=1.0'>" +
                $"      <title>Table</title>" +
                $"      <style>" +
                "           * {padding: 0; margin: 0; box-sizing: border-box}" +
                "           body {" +
                "               width: 100%;" +
                "               height: 100vh;" +
                "               display: grid;" +
                "               place-items: center" +
                "           }" +
                "           table, td, th {" +
                "               border: 1px solid black" +
                "           }" +
                "           th, td {" +
                "               padding: 5px" +
                "           }" +
                $"      </style>" +
                $"  </head>" +
                $"  <body>" +
                $"      {tableHTML}" +
                $"  </body>" +
                $"</html>";
            return stringHTML;
        }

        private void htmlBtn_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(@"H:\data.html", false, Encoding.UTF8))
            {
                sw.WriteLine(DataStringHTML());
                Process process = new Process();
                process.StartInfo.FileName = "msedge.exe";
                process.StartInfo.Arguments = @"H:\data.html";
                process.Start();

            }
        }
    }
}
