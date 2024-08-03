using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseDeDatos;
using Dominio;

namespace Negocio
{
    public class CategoriaService
    {

        public List<Categoria> listar()
        {
            List<Categoria> categories = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setQuery("SELECT Id, Descripcion FROM CATEGORIAS");
                datos.executeReader();

                while (datos.Reader.Read())
                {
                    Categoria category = new Categoria();
                    category.Id = (int)datos.Reader["Id"];
                    category.Descripcion = (string)datos.Reader["Descripcion"];

                    categories.Add(category);
                }

                return categories;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.closeConection();
            }

           
        }


       


    }
}
