using Microsoft.Reporting.WinForms;
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
    public partial class PricingReportView : Form
    {
        int reqId;
        bool isDetailed=false;
        string capacity;
        public PricingReportView(int id,bool isDetailed)
        {
            reqId = id;
            InitializeComponent();
            this.isDetailed = isDetailed;
            
             DialogResult practicalCapacity=MessageBox.Show("Do you want to used the practical capacity?\nClick 'Yes' for practical capacity\nClick 'No' for budgeted capacity", "", MessageBoxButtons.YesNoCancel);
            if (practicalCapacity.Equals(DialogResult.Yes))
            {
                capacity = "Practical_Capacity";
            }
            else if (practicalCapacity.Equals(DialogResult.No))
            {
                capacity = "Budgeted_Capacity";
            }
            else
            {
                this.Close();
            }

        }

        private void PricingReportView_Load(object sender, EventArgs e)
        {
            if (isDetailed)
            {
                summaryViewer.SendToBack();
                detailedViewer.BringToFront();
                summaryViewer.Enabled = false;
            }
            else
            {
                detailedViewer.SendToBack();
                summaryViewer.BringToFront();
                detailedViewer.Enabled = false;
            }

            try
            {
                
                reportDataSet.materials.Merge(Program.programController.getMaterials(reqId));
                reportDataSet.addFinish.Merge(Program.programController.getAddFinish(reqId));
                reportDataSet.dyes.Merge(Program.programController.getDyes(reqId));
                reportDataSet.conversionCost.Merge(Program.programController.getConversion(reqId, capacity));
                addFinishBindingSource.EndEdit();
                materialsBindingSource.EndEdit();
                dyesBindingSource.EndEdit();
                conversionCostBindingSource.EndEdit();
                if (isDetailed)
                {
                    detailedViewer.LocalReport.SetParameters(Program.programController.setReportParameters(reqId));
                    detailedViewer.RefreshReport();
                }
                else
                {
                    summaryViewer.LocalReport.SetParameters(Program.programController.setReportParameters(reqId));
                    summaryViewer.RefreshReport();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occured or Information in the database is incomplete");
                this.Close();
                return;
            }



        
        }

        private void PricingReportView_FormClosing(object sender, FormClosingEventArgs e)
        {
            reportDataSet.addFinish.Clear();
            reportDataSet.dyes.Clear();
            reportDataSet.materials.Clear();
            reportDataSet.conversionCost.Clear();
        }
    }
}
