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
    public partial class frmFiltroAvanzado : Form
    {

        public List<Articulo> FilteredArticles { get; private set; }
        public frmFiltroAvanzado()
        {
            InitializeComponent();
        }

        private void btnCancelarFiltro_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedOption = cboCampo.SelectedItem.ToString();

            if(selectedOption == "Precio")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Mayor a");
                cboCriterio.Items.Add("Menor a");
                cboCriterio.Items.Add("Igual a");
            }
            else
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Comienza con");
                cboCriterio.Items.Add("Termina con");
                cboCriterio.Items.Add("Contiene");
            }


        }

        private void frmFiltroAvanzado_Load(object sender, EventArgs e)
        {
            MarcaService marca = new MarcaService();
            CategoriaService categoria = new CategoriaService();

            try
            {
                cboCampo.Items.Add("Codigo");
                cboCampo.Items.Add("Nombre");
                cboCampo.Items.Add("Marca");
                cboCampo.Items.Add("Categoria");
                cboCampo.Items.Add("Precio");


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnFiltrarAvanzado_Click(object sender, EventArgs e)
        {
            ArticuloService service = new ArticuloService();

            try
            {

                if (validateFilter())
                {
                    return;
                }

                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtFiltroAvanzado.Text;

               FilteredArticles = service.filteredList(campo, criterio, filtro);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private bool validateFilter()
        {
            Validations validate = new Validations();

            if(cboCampo.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor seleccione el campo de búsqueda");
                return true;
            }

            if(cboCriterio.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor seleccione un criterio");
                return true;
            }

            if(txtFiltroAvanzado == null)
            {
                MessageBox.Show("Por favor escriba el filtro");
                return true;
            }
            if(cboCampo.SelectedItem.ToString() == "Precio")
            {
                if (string.IsNullOrEmpty(txtFiltroAvanzado.Text))
                {
                    MessageBox.Show("Por favor ingrese un número");
                    return true;
                }

                if (!(validate.onlyNumbersAllowed(txtFiltroAvanzado.Text)))
                {
                    MessageBox.Show("Por favor ingrese solo números");
                    return true;
                }


            }

            return false;
        }

       

    }
}
