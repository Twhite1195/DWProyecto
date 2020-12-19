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
    public partial class Proveedor : System.Web.UI.Page
    {
        IEnumerable<Models.Proveedor> proveedores = new ObservableCollection<Models.Proveedor>();
        ProveedorManager proveedorManager = new ProveedorManager();
        SedeManager sedeManager = new SedeManager();


        protected void Page_Load(object sender, EventArgs e)
        {
            InicializarControles();
        }

        private async void InicializarControles()
        {

            proveedores = await proveedorManager.ObtenerProveedores(VG.usuarioActual.CadenaToken);
            grdProveedores.DataSource = proveedores.ToList();
            grdProveedores.DataBind();

            IEnumerable<Models.Sede> escogeSedes = new ObservableCollection<Models.Sede>();
            escogeSedes = await sedeManager.ObtenerSedes(VG.usuarioActual.CadenaToken);
            sedeList.DataSource = escogeSedes.ToList();
            sedeList.DataTextField = "SED_NOMBRE";
            sedeList.DataValueField = "SED_ID";
            sedeList.DataBind();
        }



        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar())
            {
                Models.Proveedor proveedorIngresado = new Models.Proveedor();
                Models.Proveedor proveedor = new Models.Proveedor()
                {
                    //SED_ID = Convert.ToInt32(txtSedeId.Text),
                    SED_ID = Convert.ToInt32(sedeList.SelectedValue),
                    PROVE_NOMBRE = txtNombre.Text,
                    PROVE_FUNCION = txtFuncion.Text
                };

                proveedorIngresado = await proveedorManager.Ingresar(proveedor, VG.usuarioActual.CadenaToken);

                if (proveedorIngresado != null)
                {
                    lblStatus.Text = "Proveedor ingresado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al ingresar el  proveedor";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
            //agregado
            else
            {
                Models.Proveedor proveedorIngresado = new Models.Proveedor();
                Models.Proveedor proveedor = new Models.Proveedor()
                {

                    SED_ID = Convert.ToInt32(sedeList.SelectedValue),
                    PROVE_NOMBRE = txtNombre.Text,
                    PROVE_FUNCION = txtFuncion.Text
                };

                proveedorIngresado = await proveedorManager.Ingresar(proveedor, VG.usuarioActual.CadenaToken);

                if (proveedorIngresado != null)
                {
                    lblStatus.Text = "Proveedor ingresado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al ingresar el proveedor";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }

        }//end 


        async protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarModificar())
            {
                Models.Proveedor proveedorModificado = new Models.Proveedor();
                Models.Proveedor proveedor = new Models.Proveedor()
                {
                    PROVE_ID = Convert.ToInt32(txtID.Text),
                    //SED_ID = Convert.ToInt32(txtSedeId.Text),
                    SED_ID = Convert.ToInt32(sedeList.SelectedValue),
                    PROVE_NOMBRE = txtNombre.Text,
                    PROVE_FUNCION = txtFuncion.Text
                };

                proveedorModificado =
                    await proveedorManager.Actualizar(proveedor, VG.usuarioActual.CadenaToken);

                if (proveedorModificado != null)
                {
                    lblStatus.Text = "Proveedor modificado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al modificar el proveedor";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
            //agregado
            else
            {
                Models.Proveedor proveedorModificado = new Models.Proveedor();
                Models.Proveedor proveedor = new Models.Proveedor()
                {
                    PROVE_ID = Convert.ToInt32(txtID.Text),
                    //SED_ID = Convert.ToInt32(txtSedeId.Text),
                    SED_ID = Convert.ToInt32(sedeList.SelectedValue),
                    PROVE_NOMBRE = txtNombre.Text,
                    PROVE_FUNCION = txtFuncion.Text
                };

                proveedorModificado =
                    await proveedorManager.Actualizar(proveedor, VG.usuarioActual.CadenaToken);

                if (proveedorModificado != null)
                {
                    lblStatus.Text = "Proveedor modificado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al modificar el proveedor";
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

                idProveedor = txtID.Text;

                idProveedorEliminado = await proveedorManager.Eliminar(idProveedor, VG.usuarioActual.CadenaToken);

                if (!string.IsNullOrEmpty(idProveedorEliminado))
                {
                    lblStatus.Text = "Proveedor eliminado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al eliminar el proveedor";
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



            // if (txtSedeId.Text.IsNullOrWhiteSpace())
            if (sedeList.SelectedValue.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la sede";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtNombre.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el nombre del proveedor";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtFuncion.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la funcion del proveedor";
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
                lblStatus.Text = "Debe ingresar el id del proveedor";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            // if (txtSedeId.Text.IsNullOrWhiteSpace())
            if (sedeList.SelectedValue.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la sede";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtNombre.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el nombre del proveedor";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtFuncion.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la funcion del proveedor";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }


            return true;
        }
    }
}