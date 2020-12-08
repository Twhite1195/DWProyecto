using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReservasSW.Models
{
    public class Carro
    {
        public int CAR_ID { get; set; }
        public int? MOD_ID { get; set; }
        public string CAR_ESTADO { get; set; }
        public string CAR_PLACA { get; set; }
        public int? SED_ID { get; set; }
        public int? LOT_ID { get; set; }
        public int? RES_ID { get; set; }
    }
}