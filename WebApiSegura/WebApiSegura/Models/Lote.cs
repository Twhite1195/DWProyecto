using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSegura.Models
{
    public class Lote
    {

        public int LOT_ID { get; set; }
        public int SED_ID { get; set; }
        public bool LOTE_DISPONIBILIDAD { get; set; }
    }
}