using FrameWork.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FrameWork.Departamento
{
    public partial class Departamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarDepartamentos();
        }

        protected void Nuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Departamento/Departamento_Modificar.aspx?DepartamentoId=0");
        }

        private void CargarDepartamentos()
        { 
            List<modDepartamento> departamentos = leerDepartamentos();

            GVDepartamento.DataSource = departamentos;
            GVDepartamento.DataBind();
        }

        protected List<modDepartamento> leerDepartamentos()
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

                            var tmp = JsonConvert.DeserializeObject<List<modDepartamento>>(responseBody, settings);

                            return tmp;

                        }
                    }
                }


            }
            catch (Exception ex) { return null; }
        }



        protected bool borrarDepartamento(int p_DepartamentoId)



        {
            try
            {

                var url = ConfigurationManager.AppSettings.Get("BaseURL").ToString() + "Departamento/Desactivar";


                string json = "{'DepartamentoId': " + p_DepartamentoId.ToString().Trim() + "}";
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
            string DepartamentoId = btn.CommandArgument;

            Response.Redirect($"~/Departamento/Departamento_Modificar.aspx?DepartamentoId={DepartamentoId}");
        }

        protected void Eliminar_Click(object sender, EventArgs e)
        {

            LinkButton btn = (LinkButton)sender;
            string DepartamentoId = btn.CommandArgument;

            borrarDepartamento(Convert.ToInt32(DepartamentoId));

            CargarDepartamentos();

        }

    }
}