using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Dominio;

namespace Presentacion
{
    public partial class frmDetalleArticulo : Form
    {
        Articulo article = null;
        public frmDetalleArticulo()
        {
            InitializeComponent();
        }

        public frmDetalleArticulo(Articulo article)
        {
            InitializeComponent();
            this.article = article;
        }

        private void frmDetalleArticulo_Load(object sender, EventArgs e)
        {

            try
            {

                if (article != null)
                {
                    lblTituloDetalles.Text = article.Nombre;
                    txtIdDetalle.Text = article.Id.ToString();
                    txtNombreDetalle.Text = article.Nombre;
                    txtCodigoDetalle.Text = article.Codigo;
                    txtDescripcionDetalle.Text = article.Descripcion;
                    txtMarcaDetalle.Text = article.Marca.Id.ToString();
                    txtCategoriaDetalle.Text = article.Categoria.Id.ToString();
                    txtPrecioDetalle.Text = article.Precio.ToString();
                    txtUrlImagenDetalle.Text = article.UrlImagen;
                    loadImage(txtUrlImagenDetalle.Text);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private void loadImage(string image)
        {
            try
            {
                pboDetalleImagen.Load(image);
            }
            catch (Exception)
            {

                pboDetalleImagen.Load("https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Placeholder_view_vector.svg/991px-Placeholder_view_vector.svg.png");
            }
        }
    }
}
