using FrameWork.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FrameWork.Empleado
{
    public partial class Empleado_Listado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarEmpleados();
        }

        protected void Nuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Empleado/Empleado_Modificar.aspx?DPI=0");
        }

        private void CargarEmpleados()
        {
            List<modEmpleados> empleados = leerEmpleados();

            GVEmpleado.DataSource = empleados;
            GVEmpleado.DataBind();
        }

        protected List<modEmpleados> leerEmpleados()
        {
            try
            {

                string url = ConfigurationManager.AppSettings.Get("BaseURL").ToString() + "Empleado";

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

                            var tmp = JsonConvert.DeserializeObject<List<modEmpleados>>(responseBody, settings);

                            return tmp;

                        }
                    }
                }


            }
            catch (Exception ex) { return null; }
        }



        protected bool borrarEmpleado(string p_DPI)



        {
            try
            {

                var url = ConfigurationManager.AppSettings.Get("BaseURL").ToString() + "Empleado/Desactivar";


                string json = "{'DPI': " + p_DPI.ToString().Trim() + "}";
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


                if (x == "Ok")
                {
                    return true;

                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected void Editar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string DPI = btn.CommandArgument;

            Response.Redirect($"~/Empleado/Empleado_Modificar.aspx?DPI={DPI}");
        }

        protected void Eliminar_Click(object sender, EventArgs e)
        {

            LinkButton btn = (LinkButton)sender;
            string DPI = btn.CommandArgument;

            borrarEmpleado(DPI);

            CargarEmpleados();

        }
    }
}