using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class Validations
    {

        Marca marca = new Marca();
        public bool validateFilter(int campoIndex, string campo, int criterioIndex, string texto, out string errorMessage)
        {

            errorMessage = "";

            if (campoIndex == -1)
            {
                errorMessage="Por favor seleccione el campo de búsqueda";
                return true;
            }

            if (criterioIndex == -1)
            {
                errorMessage="Por favor seleccione un criterio";
                return true;
            }

            if (texto == null)
            {
                errorMessage = "Por favor escriba el filtro";
                return true;
            }
            if (campo == "Precio")
            {
                if (string.IsNullOrEmpty(texto))
                {
                    errorMessage = "Por favor ingrese un número";
                    return true;
                }

                if (!onlyNumbersAllowed(texto))
                {
                    errorMessage = "Por favor ingrese solo números";
                    return true;
                }


            }

            return false;
        }

        public bool onlyNumbersAllowed(string texto)
        {
            foreach (char caracter in texto)
            {
                if (!char.IsNumber(caracter))
                {
                    return false;
                }
            }
            return true;
        }

        public bool validateAltaArticulo(string txtCodigo, string txtNombre, string txtDescripcion, int marcaIndex, int categoriaIndex, string txtUrlImagen, decimal txtPrecio, out string errorMessage)
        {

            errorMessage = "";

            if (string.IsNullOrEmpty(txtCodigo))
            {
                errorMessage="Ingrese un Codigo";
                return true;
            }

            if (string.IsNullOrEmpty(txtNombre))
            {
                errorMessage = "Ingrese un Nombre";
                return true;
            }
            if (string.IsNullOrEmpty(txtDescripcion))
            {
                errorMessage = "Ingrese una descripción para el articulo";
                return true;
            }
            if (marcaIndex == -1)
            {
                errorMessage = "Seleccione una Marca";
                return true;
            }
            if (categoriaIndex == -1)
            {
                errorMessage = "Seleccione una Categoria";
                return true;
            }
            if (string.IsNullOrEmpty(txtUrlImagen))
            {
                errorMessage = "Ingrese una imagen para mostrar";
                return true;
            }
          

            return false;
        }




    }
}
