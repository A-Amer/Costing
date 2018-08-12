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
    public partial class Main_Menu : MetroForm
    {
        public Main_Menu()
        {
            InitializeComponent();
        }

        private void Main_Menu_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Program.programController.getAllRequests();
            dataGridView1.Update();
            mainTabControl1.SelectedIndex = 0;
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Pricing_Order po = new Pricing_Order();
            po.Show(this);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                bool isDetailed = false;
                if (e.ColumnIndex == 0) {
                    isDetailed = true;
                }
                PricingReportView reportDetailed = new PricingReportView(Int32.Parse(senderGrid.Rows[e.RowIndex].Cells["Request ID"].Value.ToString()),isDetailed);
                reportDetailed.Show(this);

            }
        }

        private void Main_Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.programController.TerminateConnection();
        }

        private void itemBox_Click(object sender, EventArgs e)
        {
            New_Item newItemForm = new New_Item();
            newItemForm.Show(this);
        }

        private void clientBox1_Click(object sender, EventArgs e)
        {
            Add_Client addClientForm = new Add_Client();
            addClientForm.Show(this);
        }
    }
}
