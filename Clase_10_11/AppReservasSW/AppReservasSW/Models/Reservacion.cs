using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReservasSW.Models
{
    public class Reservacion
    {
        public int RES_ID { get; set; }
        public int? LOT_ID { get; set; }
        public int? USU_ID { get; set; }
        public int? CAR_ID { get; set; }
        public DateTime RES_FECHA_INI { get; set; }
        public DateTime RES_FECHA_FIN { get; set; }
    }
}