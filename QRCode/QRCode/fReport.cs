using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace QRCode
{
    public partial class fReport : Form
    {
        public fReport()
        {
            InitializeComponent();
        }

        private void FrmReportcs_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }

        public void Report1(DataTable dt, string ReportName, double totalSub, double promotion, double totalAll)
        {
            reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            reportViewer1.LocalReport.ReportPath = $"./../../{ReportName}";
            ReportDataSource dts = new ReportDataSource();
            dts.Name = "ds";
            dts.Value = dt;
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(dts);

            ReportParameter[] parameters = new ReportParameter[4];
            parameters[0] = new ReportParameter("day", DateTime.Now.ToString(), true);
            parameters[1] = new ReportParameter("totalSub", totalSub.ToString(), true);
            parameters[2] = new ReportParameter("promotion", promotion.ToString(), true);
            parameters[3] = new ReportParameter("totalAll", totalAll.ToString(), true);
            reportViewer1.LocalReport.SetParameters(parameters);

            reportViewer1.RefreshReport();
        }


        public void Report2(DataTable dt, string ReportName)
        {
            reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            reportViewer1.LocalReport.ReportPath = ReportName;
            ReportDataSource dts = new ReportDataSource();
            dts.Name = "DataSet1";
            dts.Value = dt;
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(dts);
            //ReportParameter[] parameters = new ReportParameter[1];
            //parameters[0] = new ReportParameter("signer", "Nguyễn Văn An", true);
            //reportViewer1.LocalReport.SetParameters(parameters);
            reportViewer1.RefreshReport();
        }
    }
}
