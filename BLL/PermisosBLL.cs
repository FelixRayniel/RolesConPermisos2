using Microsoft.EntityFrameworkCore;
using RolesConPermisos2.DAL;
using RolesConPermisos2.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RolesConPermisos2.BLL
{
  public  class PermisosBLL
    {
        /// <summary>
        /// Permite ingresar un dato en la Base de Datos
        /// <summary>
        /// <param name="permiso"> Entidad que se quiere guardar </param>
        public static bool Guardar(Permisos permiso)
        {
            if (!Existe(permiso.PermisoID))
            {
                return Insertar(permiso);
            }
            else
            {
                return Modificar(permiso);
            }
        }
        /// <summary>
        /// Permite ingresar una entidad en la Base de Datos
        /// <summary>
        /// <param name="permiso"> Entidad que se quiere ingresar </param>

        private static bool Insertar(Permisos permiso)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                //Agregar la entidad que se desea insertar al contexto
                contexto.Permisos.Add(permiso);
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }
        /// <summary>
        /// Permite modificar una entidad en la Base de Datos
        /// </summary>
        /// <param name="permiso"> Entidad que se quiere modificar </param> 

        public static bool Modificar(Permisos permiso)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Database.ExecuteSqlRaw($"'Delete FROM RolesDetalles Where RolID={permiso.PermisoID}");

                foreach (var item in permiso.Detalle)
                {
                    contexto.Entry(item).State = EntityState.Added;
                }

                contexto.Entry(permiso).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        /// <summary>
        /// Permite eliminar una entidad de la Base de Datos
        /// </summary>
        /// <param name="id"> Id de la entidad que se quiere eliminar </param> 

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                var roles = contexto.Roles.Find(id);
                contexto.Entry(roles).State = EntityState.Deleted;
                paso = (contexto.SaveChanges() > 0);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        /// <summary>
        /// Permite buscar una entidad en la Base de Datos
        /// </summary>
        /// <param name="id"> Id de la entidad que se quiere buscar </param>
        /// 

        public static Permisos Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Permisos permiso;

            try
            {
                permiso = contexto.Permisos.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return permiso;
        }

        /// <summary>
        /// Permite obtener una lista filtrada por un criterio de busqueda
        /// </summary>
        /// <param name="criterio">La expresión que define el criterio de busqueda</param>
        /// <returns></returns
        public static List<Permisos> GetList(Expression<Func<Permisos, bool>> criterio)
        {
            List<Permisos> lista = new List<Permisos>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Permisos.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }

        public static List<Permisos> GetPermisos()
        {
            List<Permisos> lista = new List<Permisos>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Permisos.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;
            try
            {
                encontrado = contexto.Roles.Any(r => r.RolID == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return encontrado;
        }
    }
}
