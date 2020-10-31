using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSegura.Models
{
    public class Proveedor
    {

        public int PROVE_ID { get; set; }
        public int SED_ID { get; set; }
        public string PROVE_NOMBRE { get; set; }
        public string PROVE_FUNCION { get; set; }

    }
}