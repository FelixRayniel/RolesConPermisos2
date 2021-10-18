using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolesConPermisos2.Entidades
{
   public  class RolesDetalles
    {
        [Key]
        public int ID { get; set; }
        public int RolID { get; set; }
        public int PermisoID { get; set; }
        public string Nombre { get; set; }
        public bool esAsignado { get; set; }

        public RolesDetalles()
        {
            ID = 0;
            RolID = 0;
            PermisoID = 0;
            esAsignado = false;
        }

        public RolesDetalles(int RolId, int permisoId, string nombre, bool esasignado)
        {
            ID = 0;
            RolID = RolId;
            PermisoID = permisoId;
            Nombre = nombre;
            esAsignado = esasignado;
        }

    }
}
