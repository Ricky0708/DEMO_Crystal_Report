using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using System.Data;

namespace DEMO_CrystalReport.Views.Home
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //WriteToXml(CreateDs());
            //when you create a new report, you have to fill a schema into this report make you design a report
            //so mark below code and make a xml schema and then load the xml file when you design report

            if (CrystalReportViewer1.ReportSource != null)
            {
            }
            else
            {
                ReportDocument rpt = new ReportDocument();
                rpt.Load(Server.MapPath("DemoReport.rpt"));
                rpt.SetDataSource(CreateDs());
                rpt.SetParameterValue("Companyname", "NorthWind Co.");
                //show in report viewer
                //CrystalReportViewer1.ReportSource = rpt;
                //export to pdf file
                rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, "");
            }
        }
        /// <summary>
        /// Get DataSet for report document
        /// </summary>
        /// <returns>dataset</returns>
        private DataSet CreateDs()
        {

            //you can generat dataset from anywhere
            //ex: use ado.net load from database or model to dataset

            DataSet ds = new DataSet("DemoCrystalReport");
            DataTable dtMaster = new DataTable("DemoMasterRecord");
            dtMaster.Columns.Add(new DataColumn("OrderNo", typeof(string)));
            dtMaster.Columns.Add(new DataColumn("CustomeName", typeof(string)));
            dtMaster.Columns.Add(new DataColumn("OrderDate", typeof(DateTime)));

            DataTable dtDetail = new DataTable("DemoDetailRecord");
            dtDetail.Columns.Add(new DataColumn("OrderNo", typeof(string)));
            dtDetail.Columns.Add(new DataColumn("ProductName", typeof(string)));
            dtDetail.Columns.Add(new DataColumn("Quantity", typeof(int)));
            dtDetail.Columns.Add(new DataColumn("Price", typeof(int)));

            ds.Tables.Add(dtMaster);
            ds.Tables.Add(dtDetail);

            DataRelation rel = new DataRelation("MasterDetail", dtMaster.Columns["OrderNo"], dtDetail.Columns["OrderNo"]);
            ds.Relations.Add(rel);

            for (int i = 0; i < 3; i++)
            {
                DataRow mdr = dtMaster.NewRow();
                mdr[0] = i + 1;
                mdr[1] = "NorthWindCompony_" + i.ToString();
                mdr[2] = DateTime.Today.AddDays(i);
                dtMaster.Rows.Add(mdr);
                for (int n = 0; n < 4; n++)
                {
                    DataRow ddr = dtDetail.NewRow();
                    ddr[0] = i + 1;
                    ddr[1] = "Product_" + n.ToString();
                    ddr[2] = n;
                    ddr[3] = n + i;
                    dtDetail.Rows.Add(ddr);
                }
            }

            return ds;
        }

        private void WriteToXml(DataSet ds)
        {
            ds.WriteXmlSchema(Server.MapPath("\\") + "Demo.xml");
        }

    }
}