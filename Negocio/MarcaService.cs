using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseDeDatos;
using Dominio;

namespace Negocio
{
    public class MarcaService
    {

        public List<Marca> listar()
        {
            List<Marca> marcas = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setQuery("SELECT Id, Descripcion FROM MARCAS");
                datos.executeReader();

                while (datos.Reader.Read())
                {
                    Marca marca = new Marca();
                    marca.Id = (int)datos.Reader["Id"];
                    marca.Descripcion = (string)datos.Reader["Descripcion"];


                    marcas.Add(marca);
                }



                return marcas;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }



    }
}
