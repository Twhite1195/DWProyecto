//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DWProyecto.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LOTE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LOTE()
        {
            this.CARRO = new HashSet<CARRO>();
            this.RESERVACION = new HashSet<RESERVACION>();
        }
    
        public int LOT_ID { get; set; }
        public Nullable<int> SED_ID { get; set; }
        public bool LOTE_DISPONIBILIDAD { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CARRO> CARRO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RESERVACION> RESERVACION { get; set; }
    }
}