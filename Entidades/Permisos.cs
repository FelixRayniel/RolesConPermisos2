using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolesConPermisos2.Entidades
{
    public class Permisos
    {
        [Key]
        public int PermisoID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int VecesAsignado { get; set; }

        [ForeignKey("PermisoID")]
        public List<RolesDetalles> Detalle { get; set; } = new List<RolesDetalles>();
    }
}
