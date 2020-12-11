using AppReservasSW.Controllers;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppReservasSW.Views
{
    public partial class Sede : System.Web.UI.Page
    {
        IEnumerable<Models.Sede> modelos = new ObservableCollection<Models.Sede>();
        SedeManager sedeManager = new SedeManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            InicializarControles();

        }
        private async void InicializarControles()
        {

            modelos = await sedeManager.ObtenerSedes(VG.usuarioActual.CadenaToken);
            grdSedes.DataSource = modelos.ToList();
            grdSedes.DataBind();
        }



        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar())
            {
                Models.Sede sedeIngresado = new Models.Sede();
                Models.Sede Sede = new Models.Sede()
                {
                    SED_NOMBRE = txtNombre.Text,
                    SED_UBICACION = txtUbicacion.Text
                };

                sedeIngresado =
                    await sedeManager.Ingresar(Sede, VG.usuarioActual.CadenaToken);

                if (sedeIngresado != null)
                {
                    lblStatus.Text = "Sede ingresado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al ingresar la sede";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
        }


        async protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarModificar())
            {
                Models.Sede sedeModificado = new Models.Sede();
                Models.Sede Sede = new Models.Sede()
                {
                    SED_ID = Convert.ToInt32(txtID.Text),
                    SED_NOMBRE = txtNombre.Text,
                    SED_UBICACION = txtUbicacion.Text
                };

                sedeModificado =
                    await sedeManager.Actualizar(Sede, VG.usuarioActual.CadenaToken);

                if (sedeModificado != null)
                {
                    lblStatus.Text = "Sede modificado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al modificar la sede";
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
                    await sedeManager.Eliminar(codigoHotel, VG.usuarioActual.CadenaToken);

                if (!string.IsNullOrEmpty(codigoHotelEliminado))
                {
                    lblStatus.Text = "Sede eliminado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al eliminar la sede";
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

            if (txtNombre.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el nombre de la sede";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtNombre.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el nombre de la sede";
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
                lblStatus.Text = "Debe ingresar el nombre de la sede";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtNombre.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la ubicacion de la sede";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            return true;
        }
    }
}