using FrameWork.Models;
using FrameWork.Properties;
using Microsoft.Reporting.WebForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FrameWork.Reporte
{
    public partial class Reporte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                this.ReportViewer1.Visible = false;
                CargarEstado();
                CargarDepartamento();
            }
        }



        private void CargarEstado()
        {
            List<modEstado> Estado = leerEstado();

            ddlEstado.DataTextField = "Estado";
            ddlEstado.DataValueField = "Status";

            ddlEstado.DataSource = Estado;
            ddlEstado.DataBind();

        }

        private void CargarDepartamento()
        {
            List<modDepartamento> departamento = leerDepartamento();

            ddlDepartamento.DataTextField = "Nombre";
            ddlDepartamento.DataValueField = "departamentoId";

            ddlDepartamento.DataSource = departamento;
            ddlDepartamento.DataBind();

        }

        protected List<modDepartamento> leerDepartamento()
        {
            try
            {

                string url = ConfigurationManager.AppSettings.Get("BaseURL").ToString() + "Departamento";

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/json";
                request.Accept = "application/json";
                request.Method = "GET";

                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null)
                        {
                            return null;
                        }
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();

                            var settings = new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore,
                                MissingMemberHandling = MissingMemberHandling.Ignore,
                            };

                            List<modDepartamento> tmp = JsonConvert.DeserializeObject<List<modDepartamento>>(responseBody, settings);

                            modDepartamento l = new modDepartamento();

                            l.DepartamentoId = 0;
                            l.Nombre = "(Todos)";

                            tmp.Insert(0, l);

                            return tmp;

                        }
                    }
                }


            }
            catch (Exception ex) { return null; }
        }

        protected List<modEstado> leerEstado()
        {
            try
            {

                string url = ConfigurationManager.AppSettings.Get("BaseURL").ToString() + "Empleado/" + "Estado";

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/json";
                request.Accept = "application/json";
                request.Method = "GET";

                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null)
                        {
                            return null;
                        }
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();

                            var settings = new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore,
                                MissingMemberHandling = MissingMemberHandling.Ignore,
                            };

                            List<modEstado> tmp = JsonConvert.DeserializeObject<List<modEstado>>(responseBody, settings);


                            modEstado l = new modEstado();

                            l.Status = "0";
                            l.Estado = "(Todos)";

                            tmp.Insert(0, l);


                            return tmp;

                        }
                    }
                }


            }
            catch (Exception ex) { return null; }
        }



        protected List<modReporte> leerDatosReporte()
        {
            try
            {

                int lDepartamentoId = int.Parse(this.ddlDepartamento.SelectedValue);
                string lEstado = this.ddlEstado.SelectedValue;

                var url = ConfigurationManager.AppSettings.Get("BaseURL").ToString() + "Reporte/Reporte";

                string json = "{'DepartamentoId':" + lDepartamentoId.ToString() + ""
                    + ",'EstadoId':'" + lEstado + "'"
                    + "}";
                json = json.Replace("'", "\"");


                var request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/json";
                request.Method = "POST";


                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                //HttpWebResponse httpWebRes = (HttpWebResponse)request.GetResponse();

                //List<modReporte> tmp = JsonConvert.DeserializeObject<List<modReporte>>(responseBody, settings);

                try
                {
                    using (WebResponse response = request.GetResponse())
                    {
                        using (Stream strReader = response.GetResponseStream())
                        {
                            if (strReader == null) return null;
                            using (StreamReader objReader = new StreamReader(strReader))
                            {
                                string responseBody = objReader.ReadToEnd();

                                var settings = new JsonSerializerSettings
                                {
                                    NullValueHandling = NullValueHandling.Ignore,
                                    MissingMemberHandling = MissingMemberHandling.Ignore
                                };

                                List<modReporte> tmp = JsonConvert.DeserializeObject<List<modReporte>>(responseBody, settings);

                                return tmp;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }

            }
            catch { return null; }
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            PropertyInfo[] Props  = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance );

            foreach (PropertyInfo prop in Props)
            {
                dataTable.Columns.Add(prop.Name);
            }

            foreach(T item in items)
            {
                var values = new object[Props.Length];

                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item,null);
                }
                dataTable.Rows.Add(values);
            }


            return dataTable;
        }

        protected void btnReporte_Click(object sender, EventArgs e)
        {
            List<modReporte> rpt = new List<modReporte>();
                        
            rpt = leerDatosReporte();

            DataTable dt = new DataTable();

            dt = ToDataTable(rpt);

            this.ReportViewer1.Visible = true;
               
            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;            
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("dsReporte", dt);
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.Refresh();
        }

        protected void btnPdf_Click(object sender, EventArgs e) {

            List<modReporte> rpt = new List<modReporte>();

            rpt = leerDatosReporte();

            DataTable dt = new DataTable();

            dt = ToDataTable(rpt);

            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("dsReporte", dt);
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.Refresh();


        }

    }
}