using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pricing
{
    public partial class Add_Client : MetroForm
    {
        public Add_Client()
        {
            InitializeComponent();
        }

        private void Add_Client_FormClosed(object sender, FormClosedEventArgs e)
        {
            Owner.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text == "")
            {
                MessageBox.Show("Please enter client name");
                return;
            }
            if (phoneTextbox.Text == "")
            {
                MessageBox.Show("Please enter client phone");
                return;
            }
            string name=nameTextBox.Text;
            bool export=exportCheckBox.Checked;
            string address=addressTextbox.Text;
            string phone=phoneTextbox.Text;
            object[] insertParams = { name, export, address, phone };
            try
            {
                Program.programController.addClient(insertParams);
                MessageBox.Show("Client inserted successfully");
                this.Close();
            }
            catch
            {
                MessageBox.Show("Client was not inserted pleaase try again");

            }
            

        }

        private void Add_Client_Load(object sender, EventArgs e)
        {

        }

        private void nameTextBox_Click(object sender, EventArgs e)
        {

        }

        private void exportCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
