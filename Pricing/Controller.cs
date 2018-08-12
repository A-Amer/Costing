using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pricing
{
    class Controller
    {
        DbManager dbMan;
  
        public Controller()
        {
            dbMan = new DbManager(Properties.Settings.Default.acessConnectionString);
           
        }
        public void addClient(object[] insertParams)
        {
            string query = "Insert into Clients([Client Name], Export,Address,Contact) Values (@name,@export,@address,@phone)";
            OleDbCommand cmd = new OleDbCommand(query);
            cmd.Parameters.Add("@name", OleDbType.VarChar).Value = insertParams[0];
            cmd.Parameters.Add("@export", OleDbType.Boolean).Value = insertParams[1];
            cmd.Parameters.Add("@address", OleDbType.VarChar).Value = insertParams[2];
            cmd.Parameters.Add("@phone", OleDbType.VarChar).Value = insertParams[3];
            dbMan.ExecuteNonQuery(cmd);

        }
        public int addItemCode(string itemCode)
        {
            string query = "Insert into Items (Item_Code) Values (@itemCode)";
            OleDbCommand cmd = new OleDbCommand(query);
            cmd.Parameters.Add("@itemCode",OleDbType.VarChar).Value = itemCode;
            return dbMan.ExecuteNonQuery(cmd);
        }
        public DataTable getDepartmentOH(string department)
        {
            string query = "Select OH_Latest.Name as Name, OH_Latest.Amount as Amount from OH_Latest where Department = '" + department + "' order by OH_Latest.Name ASC;";
            return dbMan.ExecuteReader(query);
        }
        public DataTable getDepartmentProductivities(string department)
        {
            string query = "Select Latest_Productivities.Practical_Capacity as Practical , Latest_Productivities.Budgeted_Capacity as Budgeted  from Latest_Productivities where Department = '" + department + "' ;";
            return dbMan.ExecuteReader(query);
        }
        public void deleteItemWarpWeft(string itemCode)
        {
            string query = "Delete from Item_WarpWeft where Item_Code = '" + itemCode + "' ;";
            dbMan.ExecuteNonQuery(query);
        }
        public void deleteItem(string itemCode)
        {
            string query = "Delete from Items where Item_Code = '" + itemCode + "' ;";
            dbMan.ExecuteNonQuery(query);
        }
        public void deleteItemDyes(string itemCode)
        {
            string query = "Delete from Item_Dyes where Item_Code = '" + itemCode + "' ;";
            dbMan.ExecuteNonQuery(query);
        }
        public int addItemWarpWeft(object [] parameters)
        {
            string query = "Insert into Item_WarpWeft (Item_Code,Warp,[Thread No],[Thread Type],Mix,Twist,Weight) " +
                "Values (@itemCode,@warp,@thNo,@thTy,@mix,@twist,@weight)";
            OleDbCommand cmd = new OleDbCommand(query);
            cmd.Parameters.Add("@itemCode", OleDbType.VarChar).Value = parameters[0].ToString();
            cmd.Parameters.Add("@warp", OleDbType.Boolean).Value =bool.Parse( parameters[1].ToString());
            cmd.Parameters.Add("@thNo", OleDbType.VarChar).Value = parameters[2].ToString();
            cmd.Parameters.Add("@thTy", OleDbType.VarChar).Value = parameters[3].ToString();
            cmd.Parameters.Add("@mix", OleDbType.VarChar).Value = parameters[4].ToString();
            cmd.Parameters.Add("@twist", OleDbType.VarChar).Value = parameters[5].ToString();
            cmd.Parameters.Add("@weight", OleDbType.Double).Value = Double.Parse(parameters[6].ToString());
            return dbMan.ExecuteNonQuery(cmd);
        }
        public int addRequestConversion(object [] parameters)
        {
            string query = "Insert into Request_Conversion (Request_ID,[Purchased Materials],Outsourcing,[Staff Cost],Utility,[Other Cost],Depreciation,[Finance Exp],Commissions,Department,Practical_Capacity,Budgeted_Capacity)" +
                "values (@reqID,@purchMaterial,@outsourcing,@staffCost,@utility,@otherCost,@depreciation,@financeExp" +
                ",@commision,@department,@practicalCap,@budgetedCap)";
            OleDbCommand cmd = new OleDbCommand(query);
            cmd.Parameters.Add("@reqID", OleDbType.Double).Value = Double.Parse(parameters[0].ToString());
            cmd.Parameters.Add("@purchMaterial", OleDbType.Double).Value = Double.Parse(parameters[1].ToString());
            cmd.Parameters.Add("@outsourcing", OleDbType.Double).Value = Double.Parse(parameters[2].ToString());
            cmd.Parameters.Add("@staffCost", OleDbType.Double).Value = Double.Parse(parameters[3].ToString());
            cmd.Parameters.Add("@utility", OleDbType.Double).Value = Double.Parse(parameters[4].ToString());
            cmd.Parameters.Add("@otherCost", OleDbType.Double).Value = Double.Parse(parameters[5].ToString());
            cmd.Parameters.Add("@depreciation", OleDbType.Double).Value = Double.Parse(parameters[6].ToString());
            cmd.Parameters.Add("@financeExp", OleDbType.Double).Value = Double.Parse(parameters[7].ToString());

            cmd.Parameters.Add("@commision", OleDbType.Double).Value = Double.Parse(parameters[8].ToString());
            cmd.Parameters.Add("@department", OleDbType.VarChar).Value = parameters[9].ToString();
            cmd.Parameters.Add("@practicalCap", OleDbType.Double).Value = Double.Parse(parameters[10].ToString());
            cmd.Parameters.Add("@budgetedCap", OleDbType.Double).Value = Double.Parse(parameters[11].ToString());
            return dbMan.ExecuteNonQuery(cmd);
        }
        public int addItemDyes(string itemCode,string materialCode,bool type, double quantity)
        {
            string query = "Insert into Item_Dyes (Item_Code,[Dye/chemicals],Material_Code,Quantity) " +
                "Values (@itemCode,@type,@materialCode,@quantity)";
            OleDbCommand cmd = new OleDbCommand(query);
            cmd.Parameters.Add("@itemCode", OleDbType.VarChar).Value = itemCode;
            cmd.Parameters.Add("@type", OleDbType.Boolean).Value = type;
            cmd.Parameters.Add("@materialCode", OleDbType.VarChar).Value = materialCode;
            cmd.Parameters.Add("@quantity", OleDbType.Double).Value = quantity;
            return dbMan.ExecuteNonQuery(cmd);
        }
        public DataTable getItem_Dyes(string itemCode)
        {
            string query = "SELECT Materials.Material_Name As Material_Name, Item_Dyes.Quantity As Quantity," +
                " [Materials Latest].Price As Price, Item_Dyes.[Dye/Chemicals] As Dyes,[Materials Latest].Currency " +
                "FROM (Item_Dyes INNER JOIN [Materials Latest] ON Item_Dyes.Material_Code = [Materials Latest].Material_Code) " +
                "INNER JOIN Materials ON Item_Dyes.Material_Code = Materials.Code " +
                "where Item_Dyes.Item_Code = '" + itemCode + "';";
            return dbMan.ExecuteReader(query);
        }
        public DataTable getMaterialNameAndPrice(string materialCode)
        {
            string query = "select Materials.MaterialDisplay as Name , [Materials Latest].Price As Price,[Materials Latest].Currency " +
                "from Materials INNER JOIN [Materials Latest] ON Materials.Code = [Materials Latest].Material_Code " +
                "where Materials.Code = '" + materialCode + "';";
            return dbMan.ExecuteReader(query);
        }
        public int addRequest_Materials(double reqID, string name, double price, double weight)
        {
            string query = "Insert into Request_Materials (Request_ID,[Material Name], [Material Price], [Material Weight]) " +
                "values ( "+reqID+" , '"+name+"' , "+price+" , "+weight+" );";
            return dbMan.ExecuteNonQuery(query);
        }
        public DataTable getItemMix(string itemCode)
        {
            string query = "SELECT Item_WarpWeft.Mix AS Mix , Item_WarpWeft.Weight AS Weight FROM Item_WarpWeft WHERE Item_Code = '" + itemCode + "';";
            return dbMan.ExecuteReader(query);
        }
        public DataTable getAddFinish(int reqNo)
        {
            string query= "SELECT Request_Chemicals.[Material  Name] AS name, Request_Chemicals.Price AS price, Request_Chemicals.Quantity AS quantity "+
                "FROM Request_Chemicals WHERE Request_Chemicals.Dye = No AND Request_Chemicals.Request_ID ="+reqNo+"; ";
            DataTable dt= dbMan.ExecuteReader(query);
            if (dt.DataSet==null) {
                object[] nullParams = { "", 0, 0 };
                dt.Rows.Add(nullParams);
            }
            return dt;
        }
        public int addRequest_Chemicals(object [] parameters)
        {
            string query = "Insert Into Request_Chemicals (Request_ID,[Material Name],Quantity,Price,Dye) values " +
                "(@reqID,@matName,@quantity,@price,@dye);";
            OleDbCommand cmd = new OleDbCommand(query);
            cmd.Parameters.Add("@reqID", OleDbType.Double).Value = Double.Parse(parameters[0].ToString());
            cmd.Parameters.Add("@matName", OleDbType.VarChar).Value = parameters[1].ToString();
            cmd.Parameters.Add("@quantity", OleDbType.Double).Value = Double.Parse(parameters[2].ToString());
            cmd.Parameters.Add("@price", OleDbType.Double).Value = Double.Parse(parameters[3].ToString());
            cmd.Parameters.Add("@dye", OleDbType.Boolean).Value = Boolean.Parse(parameters[4].ToString());
            return dbMan.ExecuteNonQuery(cmd);
        }
        public DataTable getDyes(int reqNo)
        {
            string query = "SELECT Request_Chemicals.[Material  Name] AS dyeName, Request_Chemicals.Price AS price, Request_Chemicals.Quantity AS quantity " +
                "FROM Request_Chemicals WHERE Request_Chemicals.Dye = Yes AND Request_Chemicals.Request_ID =" + reqNo + "; ";
            DataTable dt = dbMan.ExecuteReader(query);
            if (dt.DataSet == null)
            {
                object[] nullParams = { "", 0, 0 };
                dt.Rows.Add(nullParams);
            }
            return dt;
        }

        public double[] getCurrencyRates()
        {
            string query = "SELECT [Currency Latest].* FROM [Currency Latest];";
            DataTable dt = dbMan.ExecuteReader(query); 
            double[] rateArray = { 0.0, 0.0 };
            for (int i = 0; i < dt.Rows.Count ; i++)
            {
                if (dt.Rows[i].Field<string>(0).Equals("Euro"))
                {
                    rateArray[0] = dt.Rows[i].Field<double>(1);
                }
                else
                {
                    rateArray[1] = dt.Rows[i].Field<double>(1);
                }
            }
            return rateArray;
        }
        public int addPricingRequest(object[] parameters)
        {
            string query = "Insert into [Pricing Request] (Item,Client_Code,Quantity,[Custom duties%],[Waste%],[2nd Choice%],[Profit Margin%],Request_date,[عدد الحدفات],[USD rate])" +
                "values (@item,@client,@quantity,@customDuties,@waste,@secChoice,@profitMargin,@reqDate,@hadafat,@USDRate)";
            OleDbCommand cmd = new OleDbCommand(query);
            cmd.Parameters.Add("@item", OleDbType.VarChar).Value = parameters[0].ToString();
            cmd.Parameters.Add("@client", OleDbType.Integer).Value =double.Parse(parameters[1].ToString());
            cmd.Parameters.Add("@quantity", OleDbType.Double).Value = double.Parse(parameters[2].ToString());
            cmd.Parameters.Add("@customDuties", OleDbType.Double).Value = double.Parse(parameters[3].ToString());
            cmd.Parameters.Add("@waste", OleDbType.Double).Value = double.Parse(parameters[4].ToString());
            cmd.Parameters.Add("@secChoice", OleDbType.Double).Value = double.Parse(parameters[5].ToString());
            cmd.Parameters.Add("@profitMargin", OleDbType.Double).Value = double.Parse(parameters[6].ToString());
            cmd.Parameters.Add("@reqDate", OleDbType.DBDate).Value = DateTime.Parse(parameters[7].ToString());
            cmd.Parameters.Add("@hadafat", OleDbType.Double).Value = double.Parse(parameters[8].ToString());
            cmd.Parameters.Add("@USDRate", OleDbType.Double).Value = double.Parse(parameters[9].ToString());
            dbMan.ExecuteNonQuery(cmd);
            return int.Parse(dbMan.ExecuteScalar("select @@IDENTITY").ToString());
            //return 0;
        }
        public DataTable getMaterials(int reqNo)
        {
            string query = "SELECT Request_Materials.[Material Name] AS materialName, Request_Materials.[Material Price] AS rawMaterialPrice, Request_Materials.[Material Weight] AS weightInMeter " +
                "FROM Request_Materials WHERE(Request_Materials.Request_ID =" + reqNo + "); ";
            return dbMan.ExecuteReader(query);
            
        }
        public DataTable getAllRequests()
        {
            string query = "SELECT Pricing_requests.* FROM Pricing_requests; " ;
            return dbMan.ExecuteReader(query);

        }
        public DataTable getAllClients()
        {
            string query = "SELECT Clients.Code, Clients.[Client Name] FROM Clients; ";
            return dbMan.ExecuteReader(query);

        }
        public DataTable getAllItems()
        {
            string query = "SELECT Items.* FROM Items; ";
            return dbMan.ExecuteReader(query);
        }
        public DataTable getAllThreadNo()
        {
            string query = "SELECT Thread_Details.thread_No FROM Thread_Details ";
            return dbMan.ExecuteReader(query);
        }
        public DataTable getAllThreadType()
        {
            string query = "SELECT Thread_Details.thread_Type FROM Thread_Details";
            return dbMan.ExecuteReader(query);
        }
        public DataTable getAllThreadTwist()
        {
            string query = "SELECT Thread_Details.twist FROM Thread_Details";
            return dbMan.ExecuteReader(query);
        }
        public DataTable getAllMaterialCodeAndName()
        {
            string query = "SELECT Materials.MaterialDisplay,Materials.Code FROM Materials WHERE Materials.[Main Class] NOT IN ('Dye','AddFinish') ";
            return dbMan.ExecuteReader(query);
        }
        public DataTable getAllDyes()
        {
            string query = "SELECT Materials.Material_Name , Materials.Code FROM Materials WHERE Materials.[Main Class] = 'Dye'";
            return dbMan.ExecuteReader(query);
        }
        public DataTable getAllAddFinishing()
        {
            string query = "SELECT Materials.Material_Name , Materials.Code FROM Materials WHERE Materials.[Main Class] = 'AddFinish'";
            return dbMan.ExecuteReader(query);
        }
        public DataTable getConversion(int reqNo,string capacity)
        {
            string query = "SELECT Request_Conversion.[Purchased Materials] AS purchasedMaterials" +
    ", Request_Conversion.Outsourcing AS outsourcing, Request_Conversion.[Staff Cost] AS staffCost, Request_Conversion.Utility AS utility" +
    ", Request_Conversion.[Other Cost] AS otherCost, Request_Conversion.Depreciation AS depreciation" +
    ", Request_Conversion.[Finance Exp] AS financeExp, Request_Conversion.Commissions AS commissions" +
    ", Request_Conversion.Department AS department, Request_Conversion."+capacity+" AS capacity " +
    "FROM Departments INNER JOIN Request_Conversion ON Departments.Department_Name = Request_Conversion.Department" +
    " WHERE(Request_Conversion.Request_ID =" + reqNo + ") ORDER BY Departments.Department_No; ";

            string[] departments = { "Dyeing", "Spinning", "Weaving", "Mending", "Finishing", "Final Inspection", "Supporting Functions" };
            DataTable dt = dbMan.ExecuteReader(query);
            for (int i = 0; i < 7; i++)//pass through all departments
            {
                if (dt.Rows.Count<=i || dt.Rows[i].Field<string>("department") != departments[i])
                {
                    DataRow newRow = dt.NewRow();
                    object[] nullParams = { 0, 0, 0, 0, 0, 0, 0, 0, departments[i], 1 };//add empty row if stage not included
                    newRow.ItemArray = nullParams;
                    dt.Rows.InsertAt(newRow, i);

                }
            }
            return dt;  
        }

        public ReportParameterCollection setReportParameters( int reqNo)
        {
            string query = "SELECT Clients.[Client Name], Clients.Export, [Pricing Request].Item, [Pricing Request].Quantity, [Pricing Request].[Custom duties%],"
                + " [Pricing Request].[Waste%], [Pricing Request].[2nd Choice%], [Pricing Request].[Profit Margin%], [Pricing Request].Request_date, [Pricing Request].[USD rate] " +
                "FROM Clients INNER JOIN[Pricing Request] ON Clients.Code = [Pricing Request].Client_Code WHERE([Pricing Request].ID =" + reqNo + "); ";
            object[] rowItems = dbMan.ExecuteReader(query).Rows[0].ItemArray; 
            ReportParameterCollection parameters = new ReportParameterCollection();
            string market="";
            if ((bool)rowItems[1])
            {
                market = "Export";
            }
            else
            {
                market = "Import";
            }
            parameters.Add(new ReportParameter("requestNo", reqNo.ToString()));
            parameters.Add(new ReportParameter("client", rowItems[0].ToString()));
            parameters.Add(new ReportParameter("market", market));
            parameters.Add(new ReportParameter("itemNo", rowItems[2].ToString()));
            parameters.Add(new ReportParameter("quantity", rowItems[3].ToString()));
            parameters.Add(new ReportParameter("custom", rowItems[4].ToString()));
            parameters.Add(new ReportParameter("waste", rowItems[5].ToString()));
            parameters.Add(new ReportParameter("second", rowItems[6].ToString()));
            parameters.Add(new ReportParameter("profit", rowItems[7].ToString()));
            parameters.Add(new ReportParameter("dateRecv", rowItems[8].ToString()));
            parameters.Add(new ReportParameter("usdRate", rowItems[9].ToString()));
            return parameters;

        }
        public void deletePricingRequest(double reqID)
        {
            string query = "Delete from [Pricing Request] where [Pricing Request].ID = " + reqID + " ;";
            dbMan.ExecuteNonQuery(query);
        }
        public void deleteRequest_Chemicals(double reqID)
        {
            string query = "Delete from Request_Chemicals where Request_Chemicals.Request_ID = " + reqID + " ;";
            dbMan.ExecuteNonQuery(query);
        }
        public void deleteRequest_Materials(double reqID)
        {
            string query = "Delete from Request_Materials where Request_Materials.Request_ID = " + reqID + " ;";
            dbMan.ExecuteNonQuery(query);
        }
        public void deleteRequest_Conversion(double reqID)
        {
            string query = "Delete from Request_Conversion where Request_Conversion.Request_ID = " + reqID + " ;";
            dbMan.ExecuteNonQuery(query);
        }
        public void TerminateConnection()
        {
            dbMan.CloseConnection();
        }
    }
}
