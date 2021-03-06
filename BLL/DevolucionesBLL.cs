using System;
using System.Collections.Generic;
using System.Text;
//Using agregados
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.DAL;
using Proyecto_Final.Entidades;

namespace Proyecto_Final.BLL
{
    public class DevolucionesBLL
    {
        //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧夕 GUARDAR ]覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧�
        public static bool Guardar(Devoluciones devoluciones)
        {
            bool paso;

            if (!Existe(devoluciones.DevolucionId))
                paso = Insertar(devoluciones);
            else
                paso = Modificar(devoluciones);

            return paso;
        }
        //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧夕 INSERTAR ]覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧�
        public static bool Insertar(Devoluciones devoluciones)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                foreach (var item in devoluciones.Detalle)
                {
                    item.productos.Existencia += item.Cantidad;
                    contexto.Entry(item.productos).State = EntityState.Modified;
                }

                contexto.Devoluciones.Add(devoluciones);
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
        //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧夕 MODIFICAR ]覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧�
        public static bool Modificar(Devoluciones devoluciones)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                contexto.Database.ExecuteSqlRaw($"DELETE FROM DevolucionesDetalle WHERE DevolucionId={devoluciones.DevolucionId}");

                foreach (var item in devoluciones.Detalle)
                {
                    contexto.Entry(item).State = EntityState.Added;
                }

                contexto.Entry(devoluciones).State = EntityState.Modified;
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
        //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧夕 ELIMINAR ]覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧�
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                var devolucion = DevolucionesBLL.Buscar(id);
                if (devolucion != null)
                {
                    contexto.Devoluciones.Remove(devolucion);
                    paso = contexto.SaveChanges() > 0;
                }
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
        //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧夕 GETLIST ]覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧�
        public static List<Devoluciones> GetList(Expression<Func<Devoluciones, bool>> criterio)
        {
            List<Devoluciones> lista = new List<Devoluciones>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Devoluciones.Where(criterio).ToList();
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
        //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧夕 EXISTE ]覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧�
        public static bool Existe(int id)
        {
            bool encontrado = false;
            Contexto contexto = new Contexto();

            try
            {
                encontrado = contexto.Devoluciones.Any(p => p.DevolucionId == id);
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
        //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧夕 BUSCAR ]覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧
        public static Devoluciones Buscar(int id)
        {
            Devoluciones devoluciones = new Devoluciones();
            Contexto contexto = new Contexto();

            try
            {
                devoluciones = contexto.Devoluciones
                    .Where(d => d.DevolucionId == id)
                    .Include(d => d.Detalle)
                    .ThenInclude(dl => dl.productos)
                    .SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return devoluciones;
        }
    }
}