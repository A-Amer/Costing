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
    public partial class New_Item : MetroForm
    {
        private System.Windows.Forms.ComboBox [] WThNo ;
        private System.Windows.Forms.ComboBox[] WThTy;
        private MetroFramework.Controls.MetroGrid [] WMix;
        private System.Windows.Forms.ComboBox[] WTwist;
        private System.Windows.Forms.NumericUpDown[] WWeight;
        public New_Item()
        {
            InitializeComponent();
            WThNo = new ComboBox[] { Wr1ThNocomboBox , Wr2ThNocomboBox , Wr3ThNocomboBox 
                , Wf1ThNocomboBox , Wf2ThNocomboBox , Wf3ThNocomboBox };
            WThTy = new ComboBox[] { Wr1ThTycomboBox , Wr2ThTycomboBox , Wr3ThTycomboBox 
                , Wf1ThTycomboBox , Wf2ThTycomboBox , Wf3ThTycomboBox };
            WMix = new MetroFramework.Controls.MetroGrid [] { Wr1MixmetroGrid , Wr2MixmetroGrid , Wr3MixmetroGrid 
                , Wf1MixmetroGrid , Wf2MixmetroGrid , Wf3MixmetroGrid};
            WTwist = new ComboBox[] { Wr1TwistcomboBox , Wr2TwistcomboBox , Wr3TwistcomboBox 
                , Wf1TwistcomboBox , Wf2TwistcomboBox , Wf3TwistcomboBox };
            WWeight = new NumericUpDown[] { Wr1WeightUpDown, Wr2WeightUpDown , Wr3WeightUpDown 
                , Wf1WeightUpDown , Wf2WeightUpDown , Wf3WeightUpDown};
        }

        private void New_Item_Load(object sender, EventArgs e)
        {
            newItemTab.SelectedIndex= 0;

            for (int i=0;i<6;i++)
            {
                WThNo[i].ValueMember = "thread_No";
                WThNo[i].DisplayMember = "thread_No";
                WThNo[i].DataSource = Program.programController.getAllThreadNo();

                WThTy[i].ValueMember = "thread_Type";
                WThTy[i].DisplayMember = "thread_Type";
                WThTy[i].DataSource = Program.programController.getAllThreadType();

                DataGridViewComboBoxColumn MixMatComboBox = new DataGridViewComboBoxColumn();
                MixMatComboBox.HeaderText = "Material";
                MixMatComboBox.DisplayMember = "MaterialDisplay";
                MixMatComboBox.ValueMember = "Code";
                MixMatComboBox.DataSource = Program.programController.getAllMaterialCodeAndName();
                WMix[i].Columns.Insert(0, MixMatComboBox);

                WTwist[i].DisplayMember = "twist";
                WTwist[i].ValueMember = "twist";
                WTwist[i].DataSource = Program.programController.getAllThreadTwist();

            }

            DataGridViewComboBoxColumn DyesComboBox = new DataGridViewComboBoxColumn();
            DyesComboBox.HeaderText = "Dyes-Chemicals";
            DyesComboBox.DisplayMember = "Material_Name";
            DyesComboBox.ValueMember = "Code";
            DyesComboBox.DataSource = Program.programController.getAllDyes();
            dyesChemmetroGrid.Columns.Insert(0, DyesComboBox);

            DataGridViewComboBoxColumn AddFinishComboBox = new DataGridViewComboBoxColumn();
            AddFinishComboBox.HeaderText = "AddFinishing";
            AddFinishComboBox.DisplayMember = "Material_Name";
            AddFinishComboBox.ValueMember = "Code";
            AddFinishComboBox.DataSource = Program.programController.getAllAddFinishing();
            addFinishmetroGrid.Columns.Insert(0, AddFinishComboBox);


        }

        private void warp2CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (warp2CheckBox.Checked)
                tableWarp2.Enabled = true;
            else
                tableWarp2.Enabled = false;

        }

        private void warp3CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (warp3CheckBox.Checked)
                tableWarp3.Enabled = true;
            else
                tableWarp3.Enabled = false;
        }

        private void weft2CheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (weft2CheckBox.Checked)
                tableWeft2.Enabled = true;
            else
                tableWeft2.Enabled = false;
        }

        private void weft3ComboBox_CheckStateChanged(object sender, EventArgs e)
        {
            
            if (weft3CheckBox.Checked)
                tableWeft3.Enabled = true;
            else
                tableWeft3.Enabled = false;
        }

        private void New_Item_FormClosed(object sender, FormClosedEventArgs e)
        {
            Owner.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool[] exist = { true, warp2CheckBox.Checked, warp3CheckBox.Checked
                    , true, weft2CheckBox.Checked, weft3CheckBox.Checked };
            
            if (itemCodeText.Text == "")
            {
                MessageBox.Show("Please enter item code");
                return;
            }
            
            Double sum;
            for (int i=0; i<6; i++)
            {
                sum = 0;
                if(exist[i])
                {
                    for (int j = 0; j < WMix[i].RowCount - 1; j++)
                    {
                        
                        if (WMix[i].Rows[j].Cells[0].Value==null)
                        {
                            MessageBox.Show("Please select a material for the mix");
                            return;
                        }
                        if (WMix[i].Rows[j].Cells[1].Value == null)
                        {
                            MessageBox.Show("Please enter a percentage for material mix");
                            return;
                        }
                        if (Double.Parse(WMix[i].Rows[j].Cells[1].Value.ToString()) <= 0
                            || Double.Parse(WMix[i].Rows[j].Cells[1].Value.ToString()) > 100)
                        {
                            MessageBox.Show("Mix Percentage must not be greater than 100 or less than 0");
                            return;
                        }
                        sum += Double.Parse(WMix[i].Rows[j].Cells[1].Value.ToString());
                        if (sum > 100)
                        {
                            MessageBox.Show("Mix Sum Percentages must not be greater than 100");
                            return;
                        }
                    }
                    if (sum != 100)
                    {
                        MessageBox.Show("Mix Sum Percentages must be equal to 100");
                        return;
                    }
                }
            }
            for (int j = 0; j < dyesChemmetroGrid.RowCount - 1; j++)
            {

                if (dyesChemmetroGrid.Rows[j].Cells[0].Value == null)
                {
                    MessageBox.Show("Please select a Dye/chemical");
                    return;
                }
                if (dyesChemmetroGrid.Rows[j].Cells[1].Value == null)
                {
                    MessageBox.Show("Please enter a quantity for Dye/Chemicals");
                    return;
                }
                if (Double.Parse(dyesChemmetroGrid.Rows[j].Cells[1].Value.ToString()) <= 0)
                {
                    MessageBox.Show("Quantity of Sye/Chemical can not be less than 0");
                    return;
                }
            }
            for (int j = 0; j < addFinishmetroGrid.RowCount - 1; j++)
            {

                if (addFinishmetroGrid.Rows[j].Cells[0].Value == null)
                {
                    MessageBox.Show("Please select an Add Finish");
                    return;
                }
                if (addFinishmetroGrid.Rows[j].Cells[1].Value == null)
                {
                    MessageBox.Show("Please enter a quantity for Add Finishing");
                    return;
                }
                if (Double.Parse(addFinishmetroGrid.Rows[j].Cells[1].Value.ToString()) <= 0)
                {
                    MessageBox.Show("Quantity of Add Finishing can not be less than 0");
                    return;
                }
            }
            string itemCode = itemCodeText.Text;
            bool [] warpArray = { true, true, true, false, false, false };
            bool warp;
            string threadNo;
            string threadType;
            string twist;
            string mix;
            double weight;

            object[] parameters;
            
            for (int i = 0; i < 6; i++)
            {
                if (exist[i])
                {

                    threadNo = WThNo[i].SelectedValue.ToString();
                    warp = warpArray[i];
                    threadType = WThTy[i].SelectedValue.ToString();
                    twist = WTwist[i].SelectedValue.ToString();
                    weight = Double.Parse(WWeight[i].Value.ToString());
                    mix = "";
                    for (int j = 0; j < WMix[i].RowCount - 1; j++)
                    {
                        mix += WMix[i].Rows[j].Cells[0].Value.ToString() + "," 
                            + WMix[i].Rows[j].Cells[1].Value.ToString() + ";";

                    }
                    parameters = new object [] { itemCode, warp, threadNo, threadType, mix, twist, weight };
                    if (Program.programController.addItemWarpWeft(parameters) <= 0)
                    {
                        Program.programController.deleteItemWarpWeft(itemCode);
                        MessageBox.Show("Something went wrong while inserting a new item please try again");
                        return;
                    }
                } 
            }
            for (int j = 0; j < dyesChemmetroGrid.RowCount - 1; j++)
            {
                if (Program.programController.addItemDyes(itemCode, dyesChemmetroGrid.Rows[j].Cells[0].Value.ToString()
                    , true, Double.Parse(dyesChemmetroGrid.Rows[j].Cells[1].Value.ToString())) <=0)
                {
                    Program.programController.deleteItemWarpWeft(itemCode);
                    Program.programController.deleteItemDyes(itemCode);
                    MessageBox.Show("Something went wrong while inserting a new item please try again");
                    return;
                }
            }
            for (int j = 0; j < addFinishmetroGrid.RowCount - 1; j++)
            {
                if (Program.programController.addItemDyes(itemCode, addFinishmetroGrid.Rows[j].Cells[0].Value.ToString()
                    , false, Double.Parse(addFinishmetroGrid.Rows[j].Cells[1].Value.ToString())) <=0)
                {
                    Program.programController.deleteItemWarpWeft(itemCode);
                    Program.programController.deleteItemDyes(itemCode);
                    MessageBox.Show("Something went wrong while inserting a new item please try again");
                    return;
                }
            }

            if (Program.programController.addItemCode(itemCodeText.Text) <=0)
            {
                Program.programController.deleteItemWarpWeft(itemCode);
                Program.programController.deleteItemDyes(itemCode);
                Program.programController.deleteItem(itemCode);
                MessageBox.Show("Something went wrong while inserting a new item please try again");
                return;
            }
            MessageBox.Show("Item inserted successfully");

        }

        private void Wf1ThNocomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Wr1ThNocomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}