using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BaseDeDatos;
using Dominio;

namespace Negocio
{
    public class ArticuloService
    {

       public List<Articulo> listarArticulos()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Articulo> articulos = new List<Articulo>();

            try
            {
                datos.setQuery("SELECT A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, A.IdMarca, A.IdCategoria, ImagenUrl, Precio FROM ARTICULOS A JOIN MARCAS M ON M.Id = A.IdMarca JOIN CATEGORIAS C ON C.Id = A.IdCategoria");
                datos.executeReader();

                while (datos.Reader.Read())
                {
                    Articulo articulo = new Articulo();
                    articulo.Id = (int)datos.Reader["Id"];
                    articulo.Codigo = (string)datos.Reader["Codigo"];
                    articulo.Nombre = (string)datos.Reader["Nombre"];
                    articulo.Descripcion = (string)datos.Reader["Descripcion"];

                    decimal precioOriginal = (decimal)datos.Reader["Precio"];
                    articulo.Precio = Math.Truncate(precioOriginal * 1000) / 1000;

                    if (!(datos.Reader["ImagenUrl"] is DBNull))
                    {
                        articulo.UrlImagen = (string)datos.Reader["ImagenUrl"];
                    }

                    articulo.Marca = new Marca();
                    articulo.Marca.Id = (int)datos.Reader["IdMarca"];
                    articulo.Marca.Descripcion = (string)datos.Reader["Marca"];

                    articulo.Categoria = new Categoria();
                    articulo.Categoria.Id = (int)datos.Reader["IdCategoria"];
                    articulo.Categoria.Descripcion = (string)datos.Reader["Categoria"];



                    articulos.Add(articulo);
                }

                return articulos;
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
        public void AddArticules(Articulo newArticle)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setQuery("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) VALUES (@Codigo, @Nombre, @Descripcion, @Marca, @Categoria, @ImagenUrl, @Precio)");

                datos.setParameter("@Codigo", newArticle.Codigo);
                datos.setParameter("@Nombre", newArticle.Nombre);
                datos.setParameter("@Descripcion", newArticle.Descripcion);
                datos.setParameter("@Marca", newArticle.Marca.Id);
                datos.setParameter("@Categoria", newArticle.Categoria.Id);
                datos.setParameter("@ImagenUrl", newArticle.UrlImagen);
                datos.setParameter("@Precio", newArticle.Precio);

                datos.executeAction();
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
        public void modifyArticle(Articulo article)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setQuery("UPDATE ARTICULOS SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, ImagenUrl = @ImagenUrl, Precio = @Precio WHERE Id = @Id");
                
                datos.setParameter("@Codigo", article.Codigo);
                datos.setParameter("@Nombre", article.Nombre);
                datos.setParameter("@Descripcion", article.Descripcion);
                datos.setParameter("@IdMarca", article.Marca.Id);
                datos.setParameter("@IdCategoria", article.Categoria.Id);
                datos.setParameter("@ImagenUrl", article.UrlImagen);
                datos.setParameter("@Precio", article.Precio);
                datos.setParameter("@Id", article.Id);

                datos.executeAction();


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
        public void deleteArticle(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setQuery("DELETE FROM ARTICULOS WHERE Id = @Id");

                datos.setParameter("@id", id);

                datos.executeAction();

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
        public List<Articulo> filteredList(string campo, string criterio, string filtro)
        {
            List<Articulo> articulos = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio, A.ImagenUrl,A.IdMarca, A.IdCategoria, M.Descripcion AS Marca, C.Descripcion AS Categoria " +
                                  "FROM Articulos A " +
                                  "INNER JOIN Marcas M ON M.Id = A.IdMarca " +
                                  "INNER JOIN Categorias C ON C.Id = A.IdCategoria " +
                                  "AND ";

                if (campo == "Precio")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "A.Precio > " + filtro;
                            break;
                        case "Menor a":
                            consulta += "A.Precio < " + filtro;
                            break;
                        default:
                            consulta += "A.Precio = " + filtro;
                            break;
                    }
                }
                else if (campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "A.Nombre LIKE '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "A.Nombre LIKE '%" + filtro + "'";
                            break;
                        default:
                            consulta += "A.Nombre LIKE '%" + filtro + "%'";
                            break;
                    }
                }
                else if (campo == "Marca")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "M.Descripcion LIKE '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "M.Descripcion LIKE '%" + filtro + "'";
                            break;
                        default:
                            consulta += "M.Descripcion LIKE '%" + filtro + "%'";
                            break;
                    }
                }
                else if (campo == "Categoria")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "C.Descripcion LIKE '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "C.Descripcion LIKE '%" + filtro + "'";
                            break;
                        default:
                            consulta += "C.Descripcion LIKE '%" + filtro + "%'";
                            break;
                    }
                }
                else if (campo == "Codigo")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "A.Codigo LIKE '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "A.Codigo LIKE '%" + filtro + "'";
                            break;
                        default:
                            consulta += "A.Codigo LIKE '%" + filtro + "%'";
                            break;
                    }
                }

                datos.setQuery(consulta);
                datos.executeReader();

                while (datos.Reader.Read())
                {
                    Articulo articulo = new Articulo();
                    articulo.Id = (int)datos.Reader["Id"];
                    articulo.Codigo = (string)datos.Reader["Codigo"];
                    articulo.Nombre = (string)datos.Reader["Nombre"];
                    articulo.Descripcion = (string)datos.Reader["Descripcion"];
                    decimal precioOriginal = (decimal)datos.Reader["Precio"];
                    articulo.Precio = Math.Truncate(precioOriginal * 1000) / 1000;
                    articulo.UrlImagen = datos.Reader["ImagenUrl"] is DBNull ? null : (string)datos.Reader["ImagenUrl"];

                    articulo.Marca = new Marca();
                    articulo.Marca.Id = (int)datos.Reader["IdMarca"];
                    articulo.Marca.Descripcion = (string)datos.Reader["Marca"];

                    articulo.Categoria = new Categoria();
                    articulo.Categoria.Id = (int)datos.Reader["IdCategoria"];
                    articulo.Categoria.Descripcion = (string)datos.Reader["Categoria"];

                    articulos.Add(articulo);
                }

                return articulos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
