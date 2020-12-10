using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReservasSW.Models
{
    public class Empleado
    {
        public int EMP_ID { get; set; }
        public int SED_ID { get; set; }
        public int PUES_ID { get; set; }
        public int EMP_CEDULA { get; set; }
        public string EMP_NOMBRE { get; set; }
        public string EMP_APELLIDO { get; set; }
        public string EMP_TELEFONO { get; set; }
        public string EMP_RESIDENCIA { get; set; }
        public string EMP_ESTADO { get; set; }

        internal void Add(Empleado empleado)
        {
            throw new NotImplementedException();
        }
    }
}