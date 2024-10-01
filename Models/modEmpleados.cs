using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrameWork.Models
{
    public class modEmpleados
    {
        public string dpi { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public int sexoId { get; set; }
        public string sexo { get; set; }
        public DateTime fecha_Ingreso { get; set; }
        public int edad { get; set; }
        public string direccion { get; set; }
        public string nit { get; set; }
        public int departamentoId { get; set; }
        public string   departamento { get; set; }
        public string estado { get; set; }
        public string fecha_Cambio_Estado { get; set; }

    }
}