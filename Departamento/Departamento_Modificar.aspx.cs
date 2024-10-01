using FrameWork.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FrameWork.Departamento
{
    public partial class Departamento_Modificar : System.Web.UI.Page
    {

        private static int DepartamentoId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["DepartamentoId"] != null)
                {
                    DepartamentoId = Convert.ToInt32(Request.QueryString["DepartamentoId"].ToString());

                    if (DepartamentoId != 0)
                    {
                        lblTitulo.Text = "Editar Departamento";
                        btnSubmit.Text = "Actualizar";


                        modDepartamento departamento = leerDepartamento();
                        if (departamento == null) { return; }

                        this.txtDepartamentoId.Text = departamento.DepartamentoId.ToString();
                        this.txtNombre.Text = departamento.Nombre.ToString().Trim();
                        this.txtEstado.Text = departamento.Estado.ToString().Trim();

                    }
                    else
                    {
                        lblTitulo.Text = "Nuevo Departamento";
                        btnSubmit.Text = "Guardar";

                    }
                }
                else
                    Response.Redirect("~/Departamento/Departamento_Listado.aspx");
            }

        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (DepartamentoId == 0)
                {
                    var url = ConfigurationManager.AppSettings.Get("BaseURL").ToString() + "Departamento/Insertar";


                    string json = "{'Nombre':'" + txtNombre.Text.ToString().Trim() + "'}";
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

                    HttpWebResponse httpWebRes = (HttpWebResponse)request.GetResponse();

                    var x = Convert.ToString(httpWebRes.StatusCode).ToString().Trim();
                    Response.Redirect("~/Departamento/Departamento_Listado.aspx");
                }
                else
                {
                    var url = ConfigurationManager.AppSettings.Get("BaseURL").ToString() + "Departamento/Actualizar";


                    string json = "{'DepartamentoId': " + this.txtDepartamentoId.Text.ToString().Trim() + ", 'Nombre':'" + txtNombre.Text.ToString().Trim() + "'}";
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

                    HttpWebResponse httpWebRes = (HttpWebResponse)request.GetResponse();

                    var x = Convert.ToString(httpWebRes.StatusCode).ToString().Trim();
                    Response.Redirect("~/Departamento/Departamento_Listado.aspx");

                }



            }
            catch (Exception ex) { }

        }






        protected modDepartamento leerDepartamento()
        {
            try
            {

                string url = ConfigurationManager.AppSettings.Get("BaseURL").ToString() + "Departamento/" + DepartamentoId.ToString();

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

                            modDepartamento tmp = JsonConvert.DeserializeObject<modDepartamento>(responseBody, settings);

                            return tmp;

                        }
                    }
                }


            }
            catch (Exception ex) { return null; }
        }






    }
}