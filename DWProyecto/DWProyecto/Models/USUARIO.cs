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
    
    public partial class USUARIO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USUARIO()
        {
            this.RESERVACION = new HashSet<RESERVACION>();
        }
    
        public int USU_ID { get; set; }
        public string USU_CEDULA { get; set; }
        public string USU_NOMBRE { get; set; }
        public string USU_APELLIDO { get; set; }
        public int USU_TELEFONO { get; set; }
        public string USU_RESIDENCIA { get; set; }
        public string USU_ESTADO { get; set; }
        public string USU_CORREO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RESERVACION> RESERVACION { get; set; }
    }
}
