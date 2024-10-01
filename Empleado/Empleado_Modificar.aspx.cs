using FrameWork.Departamento;
using FrameWork.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FrameWork.Empleado
{
    public partial class Empleado_Modificar : System.Web.UI.Page
    {
        private static string DPI = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["DPI"] != null)
                {
                    DPI = Request.QueryString["DPI"].ToString();

                    if (DPI != "0")
                    {
                        lblTitulo.Text = "Editar Empleado";
                        btnSubmit.Text = "Actualizar";

                        CargarSexo();
                        CargarDepartamento();


                        modEmpleados empleado = leerEmpleados();

                        if (empleado == null) { return; }

                        this.txtDPI.Text = empleado.dpi.ToString();
                        this.txtNombre.Text = empleado.nombres.ToString().Trim();
                        this.txtApellido.Text = empleado.apellidos.ToString();

                    
                            ddlSexo.SelectedValue = empleado.sexoId.ToString();
                        
                        this.txtDateIngreso.Text = empleado.fecha_Ingreso.ToString("dd-MM-yyyy");
                        this.txtDateNacimiento.Text = empleado.fechaNacimiento.ToString("dd-MM-yyyy");
                        this.txtEdad.Text = empleado.edad.ToString();
                        this.txtDireccion.Text = empleado.direccion.ToString();
                        this.txtNit.Text = empleado.nit.ToString();

                        ddlDepartamento.SelectedValue = empleado.departamentoId.ToString();


                        this.txtEstado.Text = empleado.estado.ToString().Trim();

                    }
                    else
                    {
                        lblTitulo.Text = "Nuevo Empleado";
                        btnSubmit.Text = "Guardar";

                        CargarSexo();
                        CargarDepartamento();

                    }
                }
                else
                    Response.Redirect("~/Empleado/Empleado_Listado.aspx");
            }

        }

        private void CargarSexo()
        {
            List<modSexo> sexo = leerSexo();

            ddlSexo.DataTextField = "descripcion";
            ddlSexo.DataValueField = "sexoId";

            ddlSexo.DataSource = sexo;
            ddlSexo.DataBind();

        }

        private void CargarDepartamento()
        {
            List<modDepartamento> departamento = leerDepartamento();

            ddlDepartamento.DataTextField = "Nombre";
            ddlDepartamento.DataValueField = "departamentoId";

            ddlDepartamento.DataSource = departamento;
            ddlDepartamento.DataBind();

        }




        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (DPI == "0")
                {
                    var url = ConfigurationManager.AppSettings.Get("BaseURL").ToString() + "Empleado/Insertar";

                    string json = "{'DPI':'" + this.txtDPI.Text + "'"
                        + ",'Nombres':'" + this.txtNombre.Text + "'"
                        + ",'Apellidos':'" + this.txtApellido.Text + "'"
                        + ",'FechaNacimiento':'" + System.DateTime.Now.AddYears(-15).ToString("yyyy-MM-dd") + "'"
                        + ",'SexoId':'" + this.ddlSexo.SelectedValue + "'"
                        + ",'Fecha_Ingreso':'" + System.DateTime.Now.AddYears(-15).ToString("yyyy-MM-dd") + "'"
                        + ",'Edad':'15'"
                        + ",'Direccion':'" + this.txtDireccion.Text + "'"
                        + ",'NIT':'" +  this.txtNit.Text +  "'"
                        + ",'DepartamentoId':'" + this.ddlDepartamento.SelectedValue + "'"
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

                    HttpWebResponse httpWebRes = (HttpWebResponse)request.GetResponse();

                    var x = Convert.ToString(httpWebRes.StatusCode).ToString().Trim();
                    Response.Redirect("~/Empleado/Empleado_Listado.aspx");
                }
                else
                {
                    var url = ConfigurationManager.AppSettings.Get("BaseURL").ToString() + "Empleado/Actualizar";


                    string json = "{'DPI':'" + this.txtDPI.Text + "'"
                       + ",'Nombres':'" + this.txtNombre.Text + "'"
                       + ",'Apellidos':'" + this.txtApellido.Text + "'"
                       + ",'FechaNacimiento':'" + System.DateTime.Now.AddYears(-15).ToString("yyyy-MM-dd") + "'"
                       + ",'SexoId':'" + this.ddlSexo.SelectedValue + "'"
                       + ",'Fecha_Ingreso':'" + System.DateTime.Now.AddYears(-15).ToString("yyyy-MM-dd") + "'"
                       + ",'Edad':'15'"
                       + ",'Direccion':'" + this.txtDireccion.Text + "'"
                       + ",'NIT':'" + this.txtNit.Text + "'"
                       + ",'DepartamentoId':'" + this.ddlDepartamento.SelectedValue + "'"
                       + "}";
                    json = json.Replace("'", "\"");
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
                    Response.Redirect("~/Empleado/Empleado_Listado.aspx");

                }



            }
            catch (Exception ex) { }

        }






        protected modEmpleados leerEmpleados()
        {
            try
            {

                string url = ConfigurationManager.AppSettings.Get("BaseURL").ToString() + "Empleado/" + DPI.ToString();

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

                            modEmpleados tmp = JsonConvert.DeserializeObject<modEmpleados>(responseBody, settings);

                            return tmp;

                        }
                    }
                }


            }
            catch (Exception ex) { return null; }
        }



        protected List<modSexo> leerSexo()
        {
            try
            {

                string url = ConfigurationManager.AppSettings.Get("BaseURL").ToString() + "Sexo";

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

                            List<modSexo> tmp = JsonConvert.DeserializeObject<List<modSexo>>(responseBody, settings);

                            return tmp;

                        }
                    }
                }


            }
            catch (Exception ex) { return null; }
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

                            return tmp;

                        }
                    }
                }


            }
            catch (Exception ex) { return null; }
        }


    }
}