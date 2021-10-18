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
   public class RolesBLL
    {
        /// <summary>
        /// Permite ingresar un dato en la Base de Datos
        /// <summary>
        /// <param name="roles"> Entidad que se quiere guardar </param>
        public static bool Guardar(Roles roles)
        {
            if (!Existe(roles.RolID))
            {
                return Insertar(roles);
            }
            else
            {
                return Modificar(roles);
            }
        }
        /// <summary>
        /// Permite ingresar una entidad en la Base de Datos
        /// <summary>
        /// <param name="roles"> Entidad que se quiere ingresar </param>

        private static bool Insertar(Roles roles)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                //Agregar la entidad que se desea insertar al contexto
                contexto.Roles.Add(roles);
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
        /// <param name="roles"> Entidad que se quiere modificar </param> 

        public static bool Modificar(Roles roles)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Database.ExecuteSqlRaw($"'Delete FROM RolesDetalles Where RolID={roles.RolID}");

                foreach (var item in roles.RolesDetalle)
                {
                    contexto.Entry(item).State = EntityState.Added;
                }

                contexto.Entry(roles).State = EntityState.Modified;
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
        public static Roles Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Roles roles = new Roles();

            try
            {
                roles = contexto.Roles.Include(x => x.RolesDetalle).Where(p => p.RolID == id).SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return roles;
        }

        /// <summary>
        /// Permite obtener una lista filtrada por un criterio de busqueda
        /// </summary>
        /// <param name="criterio">La expresión que define el criterio de busqueda</param>
        /// <returns></returns
        public static List<Roles> GetList(Expression<Func<Roles, bool>> criterio)
        {
            List<Roles> lista = new List<Roles>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Roles.Where(criterio).ToList();
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
        public static bool ExisteDescripcion(string descripcion)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;
            try
            {
                encontrado = contexto.Roles.Any(r => r.Descripcion == descripcion);
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

        public static bool ExisteID(int ID)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;
            try
            {
                encontrado = contexto.Roles.Any(r => r.RolID == ID);
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
        public static List<Roles> GetRoles()
        {
            List<Roles> lista = new List<Roles>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Roles.ToList();
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
    }
}
