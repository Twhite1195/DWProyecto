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
    public partial class Carro : System.Web.UI.Page
    {
        IEnumerable<Models.Carro> proveedores = new ObservableCollection<Models.Carro>();
        CarroManager carroManager = new CarroManager();



        protected void Page_Load(object sender, EventArgs e)
        {
            InicializarControles();
        }

        private async void InicializarControles()
        {

            proveedores = await carroManager.ObtenerCarros(VG.usuarioActual.CadenaToken);
            grdCarros.DataSource = proveedores.ToList();
            grdCarros.DataBind();
        }



        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar())
            {
                Models.Carro proveedorIngresado = new Models.Carro();
                Models.Carro carro = new Models.Carro()
                {

                    MOD_ID = Convert.ToInt32(txtModeloID.Text),
                    CAR_ESTADO = txtEstado.Text,
                    CAR_PLACA = txtPlaca.Text,
                    SED_ID = Convert.ToInt32(txtSede.Text),
                    LOT_ID = Convert.ToInt32(txtLote.Text),
                    RES_ID = Convert.ToInt32(txtRes.Text)
                };

                proveedorIngresado =
                    await carroManager.Ingresar(carro, VG.usuarioActual.CadenaToken);

                if (proveedorIngresado != null)
                {
                    lblStatus.Text = "Carro ingresado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al ingresar el carro";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
        }


        async protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarModificar())
            {
                Models.Carro proveedorModificado = new Models.Carro();
                Models.Carro carro = new Models.Carro()
                {
                    CAR_ID = Convert.ToInt32(txtID.Text),
                    MOD_ID = Convert.ToInt32(txtModeloID.Text),
                    CAR_ESTADO = txtEstado.Text,
                    CAR_PLACA = txtPlaca.Text,
                    SED_ID = Convert.ToInt32(txtSede.Text),
                    LOT_ID = Convert.ToInt32(txtLote.Text),
                    RES_ID = Convert.ToInt32(txtRes.Text)
                };

                proveedorModificado =
                    await carroManager.Actualizar(carro, VG.usuarioActual.CadenaToken);

                if (proveedorModificado != null)
                {
                    lblStatus.Text = "Carro modificado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al modificar el carro";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
        }

        async protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtID.Text))
            {
                string idProveedorEliminado = string.Empty;
                string idProveedor = string.Empty;

                idProveedorEliminado = txtID.Text;

                idProveedorEliminado =
                    await carroManager.Eliminar(idProveedor, VG.usuarioActual.CadenaToken);

                if (!string.IsNullOrEmpty(idProveedorEliminado))
                {
                    lblStatus.Text = "Carro eliminado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al eliminar el carro";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
            else
            {
                lblStatus.Text = "Debe ingresar el id";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {

        }


        private bool ValidarInsertar()
        {



            if (txtModeloID.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la sede";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtEstado.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el nombre del carro";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtPlaca.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la funcion del carro";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }
            if (txtSede.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la funcion del carro";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }
            if (txtLote.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la funcion del carro";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }
            if (txtRes.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la funcion del carro";
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
                lblStatus.Text = "Debe ingresar la sede";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtModeloID.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la sede";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtEstado.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el nombre del carro";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtPlaca.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la funcion del carro";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }
            if (txtSede.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la funcion del carro";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }
            if (txtLote.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la funcion del carro";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }
            if (txtRes.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la funcion del carro";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }


            return true;
        }
    }
}