using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrameWork.Models
{
    public class modReporte
    {
        public string DPI { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Sexo { get; set; }
        public string Departamento { get; set; }

        public string Estado { get; set; }

        public string EstadoId { get; set; }
        public int DepartamentoId { get; set; }

    }
}