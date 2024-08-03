using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace Presentacion
{
    public partial class Form1 : Form
    {

        private List<Articulo> articlesList;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadArticles();
        }


        private void loadArticles()
        {
            ArticuloService service = new ArticuloService();

            articlesList = service.listarArticulos();
            dgvArticulos.DataSource = articlesList;

            dgvArticulos.Columns["Id"].Visible = false;
            dgvArticulos.Columns["UrlImagen"].Visible = false;

        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null) {

                Articulo selected = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                loadImage(selected.UrlImagen);

            }
        }

        private void loadImage(string image)
        {
            try
            {
                pbFotos.Load(image);
            }
            catch (Exception)
            {

                pbFotos.Load("https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Placeholder_view_vector.svg/991px-Placeholder_view_vector.svg.png");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaArticulo altaArticulo = new frmAltaArticulo();
            altaArticulo.ShowDialog();
            loadArticles();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            if(dgvArticulos.CurrentRow == null)
            {
                MessageBox.Show("Por favor seleccione un artículo para modificar");
            }

            Articulo selectedArticle = new Articulo();

            selectedArticle = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

            frmAltaArticulo modificarArticulo = new frmAltaArticulo(selectedArticle);

            modificarArticulo.ShowDialog();
            loadArticles();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Articulo selectedArticle = new Articulo();

            selectedArticle = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

            ArticuloService service = new ArticuloService();

            DialogResult respuestaUsuario = MessageBox.Show("Seguro que deseas borrar este articulo?", "Eliminar Articulo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (respuestaUsuario == DialogResult.Yes)
            {
                service.deleteArticle(selectedArticle.Id);
                MessageBox.Show("Articulo Eliminado correctamente!");
                loadArticles();
            }
        }

        private void btnFiltroAvanzado_Click(object sender, EventArgs e)
        {
            frmFiltroAvanzado filtroAvanzado = new frmFiltroAvanzado();
            

            if(filtroAvanzado.ShowDialog() == DialogResult.OK)
            {
                articlesList = filtroAvanzado.FilteredArticles;
                dgvArticulos.DataSource = articlesList;

                if (articlesList.Count == 0)
                {
                    MessageBox.Show("Ningún resultado coincide con su búsqueda");
                    loadArticles();
                }

                dgvArticulos.Columns["Id"].Visible = false;
                dgvArticulos.Columns["UrlImagen"].Visible = false;
            }

           
        }

        private void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
           
            loadArticles();
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            Articulo selectedArticle = new Articulo();

            selectedArticle = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

            frmDetalleArticulo detalleArticulo = new frmDetalleArticulo(selectedArticle);
            detalleArticulo.ShowDialog();
        }
    }
}
