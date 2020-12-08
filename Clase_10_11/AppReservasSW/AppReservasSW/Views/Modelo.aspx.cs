using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppReservasSW.Models;
using AppReservasSW.Controllers;
using System.Collections.ObjectModel;
using System.Drawing;
using Microsoft.Ajax.Utilities;

namespace AppReservasSW.Views
{
    public partial class Modelo : System.Web.UI.Page
    {
        IEnumerable<Models.Modelo> modelos = new ObservableCollection<Models.Modelo>();
        ModeloManager modeloManager = new ModeloManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            InicializarControles();
        }

        private async void InicializarControles()
        {

            modelos = await modeloManager.ObtenerModelos(VG.usuarioActual.CadenaToken);
            grdModelos.DataSource = modelos.ToList();
            grdModelos.DataBind();
        }



        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar())
            {
                Models.Modelo modeloIngresado = new Models.Modelo();
                Models.Modelo Modelo = new Models.Modelo()
                {
                    MOD_MARCA = txtMarca.Text,
                    MOD_NOMBRE = txtNombre.Text
                };

                modeloIngresado =
                    await modeloManager.Ingresar(Modelo, VG.usuarioActual.CadenaToken);

                if (modeloIngresado != null)
                {
                    lblStatus.Text = "Modelo ingresado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al ingresar el Modelo";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
        }


        async protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarModificar())
            {
                Models.Modelo modeloModificado = new Models.Modelo();
                Models.Modelo Modelo = new Models.Modelo()
                {
                    MOD_ID = Convert.ToInt32(txtID.Text),
                    MOD_MARCA = txtMarca.Text,
                    MOD_NOMBRE = txtNombre.Text
                };

                modeloModificado =
                    await modeloManager.Actualizar(Modelo, VG.usuarioActual.CadenaToken);

                if (modeloModificado != null)
                {
                    lblStatus.Text = "Modelo modificado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al modificar el Modelo";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
        }

        async protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtID.Text))
            {
                string codigoHotelEliminado = string.Empty;
                string codigoHotel = string.Empty;

                codigoHotel = txtID.Text;

                codigoHotelEliminado =
                    await modeloManager.Eliminar(codigoHotel, VG.usuarioActual.CadenaToken);

                if (!string.IsNullOrEmpty(codigoHotelEliminado))
                {
                    lblStatus.Text = "Modelo eliminado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al eliminar el Modelo";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
            else
            {
                lblStatus.Text = "Debe ingresar el codigo";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {

        }


        private bool ValidarInsertar()
        {

            if (txtMarca.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la marca del Modelo";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtNombre.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el nombre del Modelo";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            return true;
        }


        private bool ValidarModificar()
        {
            if (txtID.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la marca del Modelo";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtNombre.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el nombre del Modelo";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            return true;
        }
    }
}