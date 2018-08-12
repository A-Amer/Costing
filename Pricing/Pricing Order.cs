using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace Pricing
{
    
    public partial class Pricing_Order : MetroForm
    {
        object[] pricingReqParams = { "N2", 1, 0, 0.0, 0.0, 0, 0,DateTime.Today.ToShortDateString(), 0, 17.5 };
        Dictionary<string, int> expenseIndex = new Dictionary<string, int>();
        Double euroRate;
        CheckBox[] outSourcing;
        NumericUpDown[] outSourcingValue;
        public Pricing_Order()
        {
            InitializeComponent();

            expenseIndex.Add("Staff", 3);
            expenseIndex.Add("Utility", 4);
            expenseIndex.Add("Other Cost", 5);
            expenseIndex.Add("Depreciation", 6);
            expenseIndex.Add("Finance Exp", 7);
            expenseIndex.Add("Commission", 8);

            outSourcing = new CheckBox[] { dyeCheckBox , spinCheckBox , weaveCheckBox, mendCheckBox
                , finishCheckBox, finalCheckBox, supportCheckBox};

            outSourcingValue = new NumericUpDown[] { dyeUpDown, spinUpDown, weaveUpDown, mendUpDown
                , finishUpDown, finalUpDown, supportUpDown};
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            New_Item newItemForm = new New_Item();
            newItemForm.Show(this);
            this.Hide();
            
        }

        private void addClient_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Add_Client addClientForm = new Add_Client();
            addClientForm.Show(this);
            this.Hide();

        }

        private void Pricing_Order_FormClosing(object sender, FormClosingEventArgs e)
        {
            Owner.Show();
        }

        private void Pricing_Order_Load(object sender, EventArgs e)
        {
            clientCombobBx.ValueMember = "Code";
            clientCombobBx.DisplayMember = "Client Name";
            clientCombobBx.DataSource = Program.programController.getAllClients();
            itemComboBx.ValueMember = "Item_Code";
            itemComboBx.DisplayMember = "Item_Code";
            itemComboBx.DataSource = Program.programController.getAllItems();
        }
        private void setRequestParams()
        {
            pricingReqParams[0]= itemComboBx.SelectedValue.ToString();
            pricingReqParams[1]= Int32.Parse(clientCombobBx.SelectedValue.ToString());
            pricingReqParams[2]= Int32.Parse(quantityUpDown.Value.ToString());
            pricingReqParams[3] = Double.Parse(customUpDown.Value.ToString());
            pricingReqParams[4] = Double.Parse(wasteUpDown.Value.ToString());
            pricingReqParams[5] = Int32.Parse(secondUpDown.Value.ToString());
            pricingReqParams[6] = Int32.Parse(profitUpDown.Value.ToString());
            pricingReqParams[8] = Int32.Parse(noUpDown.Value.ToString());
            double[] currencyRate = Program.programController.getCurrencyRates();
            euroRate = currencyRate[0];
            pricingReqParams[9] = currencyRate[1];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string[] department = { "Dyeing", "Spinning", "Weaving", "Mending", "Finishing", "Final Inspection", "Supporting Functions" };
            try
            {
                setRequestParams();
                int pricingOrderID = Program.programController.addPricingRequest(pricingReqParams);
                
                object[] reqChemicals = { 0, "", 0, 0, false};
                DataTable capacity;
                for (int i=0; i<7; i++)
                {                   
                    object[] reqConversion = { pricingOrderID, 0, 0, 0, 0, 0, 0, 0, 0, department[i], 0, 0 };
                    capacity = Program.programController.getDepartmentProductivities(department[i]);
                    if (capacity.Rows.Count!=0)
                    {
                        reqConversion[10] = double.Parse (capacity.Rows[0]["Practical"].ToString());
                        reqConversion[11] = double.Parse(capacity.Rows[0]["Budgeted"].ToString());
                    }
                    
                    if (outSourcing[i].Checked)
                    {
                        reqConversion[2] = outSourcingValue[i].Value;                                               
                    }
                    else
                    {
                        DataTable OHNameAmount = Program.programController.getDepartmentOH(department[i]);
                        for(int j=0; j<OHNameAmount.Rows.Count; j++)
                        {
                            reqConversion[expenseIndex[OHNameAmount.Rows[j]["Name"].ToString()]] = double.Parse(OHNameAmount.Rows[j]["Amount"].ToString());
                        }
                        
                    }
                    if (Program.programController.addRequestConversion(reqConversion) <=0)
                    {
                        Program.programController.deleteRequest_Conversion(pricingOrderID);
                        Program.programController.deletePricingRequest(pricingOrderID);
                        MessageBox.Show("Something went wrong with the pricing rquest please try again");
                        return;
                    }                  
                }

                DataTable dyes = Program.programController.getItem_Dyes(itemComboBx.SelectedValue.ToString());
                if(dyes.Rows.Count!=0)              
                {
                    reqChemicals[0] = pricingOrderID;
                    for (int i=0; i< dyes.Rows.Count; i++)
                    {
                        reqChemicals[1] = dyes.Rows[i]["Material_Name"].ToString();
                        reqChemicals[2] = double.Parse(dyes.Rows[i]["Quantity"].ToString());
                        reqChemicals[3] = double.Parse(dyes.Rows[i]["Price"].ToString());
                        reqChemicals[4] = Boolean.Parse(dyes.Rows[i]["Dyes"].ToString());
                        if (Program.programController.addRequest_Chemicals(reqChemicals) <=0 )
                        {
                            Program.programController.deleteRequest_Chemicals(pricingOrderID);
                            Program.programController.deleteRequest_Conversion(pricingOrderID);
                            Program.programController.deletePricingRequest(pricingOrderID);
                            MessageBox.Show("Something went wrong with the pricing rquest please try again");
                            return;
                        }
                    }
                }

                DataTable mixes = Program.programController.getItemMix(itemComboBx.SelectedValue.ToString());
                Dictionary<string, double> materials = new Dictionary<string, double>();
                double value;
                for(int i=0; i<mixes.Rows.Count; i++)
                {
                    string[] row = mixes.Rows[i]["Mix"].ToString().Split(';');
                    for(int j=0; j<row.Length-1;j++)
                    {
                        string[] element = row[j].Split(',');
                        if(materials.TryGetValue(element[0],out value))
                        {
                            value += (double.Parse(element[1]) / 100) * double.Parse(mixes.Rows[i]["Weight"].ToString());
                            materials[element[0]] = value;
                        }
                        else
                        {
                            value = (double.Parse(element[1]) / 100) * double.Parse(mixes.Rows[i]["Weight"].ToString());
                            materials.Add(element[0], value);
                        }
                    }
                }
                DataTable materialNamePrice;
                string name;
                double price;
                foreach (var pair in materials)
                {
                    materialNamePrice = Program.programController.getMaterialNameAndPrice(pair.Key);
                    name = materialNamePrice.Rows[0]["Name"].ToString();
                    price = double.Parse(materialNamePrice.Rows[0]["price"].ToString());
                    if (Program.programController.addRequest_Materials(pricingOrderID, name, price, pair.Value) <= 0)
                    {                        
                        Program.programController.deleteRequest_Chemicals(pricingOrderID);
                        Program.programController.deleteRequest_Conversion(pricingOrderID);
                        Program.programController.deleteRequest_Materials(pricingOrderID);
                        Program.programController.deletePricingRequest(pricingOrderID);
                        MessageBox.Show("Something went wrong with the pricing rquest please try again");
                        return;
                    }
                }
                MessageBox.Show("Pricing Request Inserted Sucessfully");
                
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Something went wrong please check the database and try again");
                return;
            }
        }
        private void dyeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (dyeCheckBox.Checked)
                dyeUpDown.Enabled = true;
            else
                dyeUpDown.Enabled = false;
        }

        private void spinCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (spinCheckBox.Checked)
                spinUpDown.Enabled = true;
            else
                spinUpDown.Enabled = false;
        }

        private void weaveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (weaveCheckBox.Checked)
                weaveUpDown.Enabled = true;
            else
                weaveUpDown.Enabled = false;
        }

        private void mendCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (mendCheckBox.Checked)
                mendUpDown.Enabled = true;
            else
                mendUpDown.Enabled = false;
        }

        private void finishCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (finishCheckBox.Checked)
                finishUpDown.Enabled = true;
            else
                finishUpDown.Enabled = false;
        }

        private void finalCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (finalCheckBox.Checked)
                finalUpDown.Enabled = true;
            else
                finalUpDown.Enabled = false;
        }

        private void supportCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (supportCheckBox.Checked)
                supportUpDown.Enabled = true;
            else
                supportUpDown.Enabled = false;
        }

    }
}
