namespace Pricing
{
    partial class PricingReportView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource5 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource6 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource7 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource8 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.conversionCostBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportDataSet = new Pricing.ReportDataSet();
            this.materialsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dyesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.addFinishBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.detailedViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.summaryViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.conversionCostBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.materialsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dyesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.addFinishBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // conversionCostBindingSource
            // 
            this.conversionCostBindingSource.DataMember = "conversionCost";
            this.conversionCostBindingSource.DataSource = this.reportDataSet;
            // 
            // reportDataSet
            // 
            this.reportDataSet.DataSetName = "ReportDataSet";
            this.reportDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // materialsBindingSource
            // 
            this.materialsBindingSource.DataMember = "materials";
            this.materialsBindingSource.DataSource = this.reportDataSet;
            // 
            // dyesBindingSource
            // 
            this.dyesBindingSource.DataMember = "dyes";
            this.dyesBindingSource.DataSource = this.reportDataSet;
            // 
            // addFinishBindingSource
            // 
            this.addFinishBindingSource.DataMember = "addFinish";
            this.addFinishBindingSource.DataSource = this.reportDataSet;
            // 
            // detailedViewer
            // 
            this.detailedViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "conversionDataset";
            reportDataSource1.Value = this.conversionCostBindingSource;
            reportDataSource2.Name = "materialDataset";
            reportDataSource2.Value = this.materialsBindingSource;
            reportDataSource3.Name = "dyeDataset";
            reportDataSource3.Value = this.dyesBindingSource;
            reportDataSource4.Name = "addFinDataset";
            reportDataSource4.Value = this.addFinishBindingSource;
            this.detailedViewer.LocalReport.DataSources.Add(reportDataSource1);
            this.detailedViewer.LocalReport.DataSources.Add(reportDataSource2);
            this.detailedViewer.LocalReport.DataSources.Add(reportDataSource3);
            this.detailedViewer.LocalReport.DataSources.Add(reportDataSource4);
            this.detailedViewer.LocalReport.ReportEmbeddedResource = "Pricing.PricingDetailed.rdlc";
            this.detailedViewer.Location = new System.Drawing.Point(0, 0);
            this.detailedViewer.Name = "detailedViewer";
            this.detailedViewer.Size = new System.Drawing.Size(1008, 443);
            this.detailedViewer.TabIndex = 0;
            // 
            // summaryViewer
            // 
            this.summaryViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource5.Name = "conversionDataset";
            reportDataSource5.Value = this.conversionCostBindingSource;
            reportDataSource6.Name = "materialDataset";
            reportDataSource6.Value = this.materialsBindingSource;
            reportDataSource7.Name = "dyeDataset";
            reportDataSource7.Value = this.dyesBindingSource;
            reportDataSource8.Name = "addFinDataset";
            reportDataSource8.Value = this.addFinishBindingSource;
            this.summaryViewer.LocalReport.DataSources.Add(reportDataSource5);
            this.summaryViewer.LocalReport.DataSources.Add(reportDataSource6);
            this.summaryViewer.LocalReport.DataSources.Add(reportDataSource7);
            this.summaryViewer.LocalReport.DataSources.Add(reportDataSource8);
            this.summaryViewer.LocalReport.ReportEmbeddedResource = "Pricing.PricingSummary.rdlc";
            this.summaryViewer.Location = new System.Drawing.Point(0, 0);
            this.summaryViewer.Name = "summaryViewer";
            this.summaryViewer.Size = new System.Drawing.Size(1008, 443);
            this.summaryViewer.TabIndex = 1;
            // 
            // PricingReportView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 443);
            this.Controls.Add(this.summaryViewer);
            this.Controls.Add(this.detailedViewer);
            this.Name = "PricingReportView";
            this.Text = "Pricing Report";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PricingReportView_FormClosing);
            this.Load += new System.EventHandler(this.PricingReportView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.conversionCostBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.materialsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dyesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.addFinishBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Microsoft.Reporting.WinForms.ReportViewer detailedViewer;
        private ReportDataSet reportDataSet;
        private System.Windows.Forms.BindingSource dyesBindingSource;
        private System.Windows.Forms.BindingSource materialsBindingSource;
        private System.Windows.Forms.BindingSource conversionCostBindingSource;
        private System.Windows.Forms.BindingSource addFinishBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer summaryViewer;
    }
}