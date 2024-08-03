using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace Presentacion
{
    public partial class frmAltaArticulo : Form
    {

        Articulo article = null;

        public frmAltaArticulo()
        {
            InitializeComponent();
            Text = "Agregar Articulo";
        }

        public frmAltaArticulo(Articulo article)
        {
            InitializeComponent();
            this.article = article;
            Text = "Modificar Articulo";
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            MarcaService marcas = new MarcaService();
            CategoriaService categorias = new CategoriaService();

            try
            {
              
                cboCategoria.DataSource = categorias.listar();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";
                cboMarca.DataSource = marcas.listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";

                cboMarca.Text = "";
                cboCategoria.Text = "";


                if (article != null)
                {
                    txtCodigo.Text = article.Codigo;
                    txtNombre.Text = article.Nombre;
                    txtDescripcion.Text = article.Descripcion;
                    txtUrlImagen.Text = article.UrlImagen;
                    loadImage(article.UrlImagen);
                    txtPrecio.Text = article.Precio.ToString();
                    cboCategoria.SelectedValue = article.Categoria.Id;
                    cboMarca.SelectedValue = article.Marca.Id;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void btnAgregarArticulo_Click(object sender, EventArgs e)
        {
            try
            {

                if(article == null)
                {
                    article = new Articulo();
                }
                if (validateAltaArticulo())
                {
                    return;
                }

                article.Codigo = txtCodigo.Text;
                article.Nombre = txtNombre.Text;
                article.Descripcion = txtDescripcion.Text;
                article.Marca = (Marca)cboMarca.SelectedItem;
                article.Categoria = (Categoria)cboCategoria.SelectedItem;
                article.UrlImagen = txtUrlImagen.Text;
                article.Precio = decimal.Parse(txtPrecio.Text);

                ArticuloService service = new ArticuloService();

                if(article.Id == 0)
                {
                    service.AddArticules(article);
                    MessageBox.Show("Agregado exitosamente");
                }
                else
                {
                    service.modifyArticle(article);
                    MessageBox.Show("Modificado exitosamente");
                }

                this.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }
        private void loadImage(string image)
        {
            try
            {
                pbImagenAgregar.Load(image);
            }
            catch (Exception)
            {

                pbImagenAgregar.Load("https://upload.wikimedia.org/wikipedia/commons/thumb/3/3f/Placeholder_view_vector.svg/991px-Placeholder_view_vector.svg.png");
            }
        }

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            loadImage(txtUrlImagen.Text);
        }


        private bool validateAltaArticulo()
        {
            Validations validations = new Validations();

            CategoriaService categoriaService = new CategoriaService();
            MarcaService marcaService = new MarcaService();
            List<Categoria> listaCompleta = categoriaService.listar();
            List<Marca> listaCompletaMarcas = marcaService.listar();


            string textoCategoria = cboCategoria.Text.ToLower();
            Categoria categoriaSeleccionada = listaCompleta.FirstOrDefault(x => x.Descripcion.ToLower() == textoCategoria);

            string textoMarca = cboMarca.Text.ToLower();
            Marca marcaSeleccionada = listaCompletaMarcas.FirstOrDefault(x => x.Descripcion.ToLower() == textoMarca);


            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                MessageBox.Show("Ingrese un Codigo");
                return true;
            }

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Ingrese un Nombre");
                return true;
            }
            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                MessageBox.Show("Ingrese una descripción para el articulo");
                return true;
            }
            if (cboMarca.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una Marca");
                return true;
            }

            if (marcaSeleccionada == null)
            {
                MessageBox.Show("La marca seleccionada no es válida. Por favor, elige una marca existente.");
                return true;
            }

            if (cboCategoria.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una Categoria");
                return true;
            }

            if (categoriaSeleccionada == null)
            {
                MessageBox.Show("La categoría seleccionada no es válida. Por favor, elige una categoría existente.");
                return true;
            }


            if (string.IsNullOrEmpty(txtUrlImagen.Text))
            {
                MessageBox.Show("Ingrese una imagen para mostrar");
                return true;
            }
            if (!validations.onlyNumbersAllowed(txtPrecio.Text))
            {
                MessageBox.Show("El precio solo puede ser expresado en Números. Ingrese un precio valido");
                return true;
            }

            return false;
        }

        private void cboMarca_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void cboMarca_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                List<Marca> listaFiltrada;
                MarcaService service = new MarcaService();
                List<Marca> listaCompleta = service.listar();


                string textoIngresado = cboMarca.Text.ToLower();

                listaFiltrada = listaCompleta.FindAll(x => x.Descripcion.ToLower().Contains(textoIngresado));


                if (listaFiltrada.Count > 0 && cboMarca.DataSource != listaFiltrada)
                {
                    cboMarca.DataSource = null;
                    cboMarca.Items.Clear(); 
                    cboMarca.DataSource = listaFiltrada; 
                    cboMarca.DisplayMember = "Descripcion";
                    cboMarca.ValueMember = "Id";

                    cboMarca.Text = textoIngresado;
                    cboMarca.DroppedDown = true; 
                    cboMarca.SelectionStart = textoIngresado.Length; 
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void cboMarca_TextChanged(object sender, EventArgs e)
        {
            //Tildaba la aplicación. No se por qué.
        }

        private void cboCategoria_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                List<Categoria> listaFiltrada;
                CategoriaService service = new CategoriaService();
                List<Categoria> listaCompleta = service.listar();


                string textoIngresado = cboCategoria.Text.ToLower();

                listaFiltrada = listaCompleta.FindAll(x => x.Descripcion.ToLower().Contains(textoIngresado));


                if (listaFiltrada.Count > 0 && cboCategoria.DataSource != listaFiltrada)
                {
                    cboCategoria.DataSource = null; 
                    cboCategoria.Items.Clear(); 
                    cboCategoria.DataSource = listaFiltrada; 
                    cboCategoria.DisplayMember = "Descripcion";
                    cboCategoria.ValueMember = "Id";

                    cboCategoria.Text = textoIngresado;
                    cboCategoria.DroppedDown = true;
                    cboCategoria.SelectionStart = textoIngresado.Length; 
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
