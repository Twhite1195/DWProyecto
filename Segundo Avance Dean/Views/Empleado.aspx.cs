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
    public partial class Empleado : System.Web.UI.Page
    {


        IEnumerable<Models.Empleado> empleados = new ObservableCollection<Models.Empleado>();
        EmpleadoManager empleadoManager = new EmpleadoManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            InicializarControles();
        }

        private async void InicializarControles()
        {

            empleados = await empleadoManager.ObtenerEmpleados(VG.usuarioActual.CadenaToken);
            grdEmpleados.DataSource = empleados.ToList();
            grdEmpleados.DataBind();
        }

        protected void txtID_TextChanged(object sender, EventArgs e)
        {

        }

        protected void drpPuesto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar())
            {
                Models.Empleado empleadoIngresado = new Models.Empleado();
                Models.Empleado empleado = new Models.Empleado()
                {
                    SED_ID = Convert.ToInt32(drpSede.SelectedValue.ToString()),
                    PUES_ID = Convert.ToInt32(drpPuesto.SelectedValue.ToString()),
                    EMP_CEDULA = Convert.ToInt32(txt_cedula.Text),
                    EMP_NOMBRE = txt_nombre.Text,
                    EMP_APELLIDO = txt_apellido.Text,
                    EMP_TELEFONO = txt_telefono.Text,
                    EMP_RESIDENCIA = txt_residencia.Text,
                    EMP_ESTADO = "A"
                };

                empleadoIngresado =
                    await empleadoManager.Ingresar(empleado, VG.usuarioActual.CadenaToken);

                if (empleadoIngresado != null)
                {
                    lblStatus.Text = "Empleado ingresado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al ingresar el empleado";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
        }



        private bool ValidarInsertar()
        {

            if (txt_cedula.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la cédula de identidad";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txt_nombre.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el nombre";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txt_apellido.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el apellido";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txt_telefono.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el número de telefono";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txt_residencia.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la residencia";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            return true;
        }



        private bool ValidarModificar()
        {
            if (txt_empleado_id.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el codigo del empleado";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txt_cedula.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la cédula de identidad";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txt_nombre.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el nombre";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txt_apellido.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el apellido";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txt_telefono.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el número de telefono";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txt_residencia.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la residencia";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }
            return true;
        }

       async protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarModificar())
            {
                Models.Empleado empleadoModificado = new Models.Empleado();
                Models.Empleado empleado = new Models.Empleado()
                {
                    EMP_ID = Convert.ToInt32(txt_empleado_id.Text),
                    SED_ID = Convert.ToInt32(drpSede.SelectedValue.ToString()),
                    PUES_ID = Convert.ToInt32(drpPuesto.SelectedValue.ToString()),
                    EMP_CEDULA = Convert.ToInt32(txt_cedula.Text),
                    EMP_NOMBRE = txt_nombre.Text,
                    EMP_APELLIDO = txt_apellido.Text,
                    EMP_TELEFONO = txt_telefono.Text,
                    EMP_RESIDENCIA = txt_residencia.Text,
                    EMP_ESTADO = "A"
                };

                empleadoModificado =
                    await empleadoManager.Actualizar(empleado, VG.usuarioActual.CadenaToken);

                if (empleadoModificado != null)
                {
                    lblStatus.Text = "Empleado modificado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al modificar el empleado";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
        }

       async  protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_empleado_id.Text))
            {
                string codigoEmpleadoEliminado = string.Empty;
                string codigoEmpleado = string.Empty;

                codigoEmpleado = txt_empleado_id.Text;

                codigoEmpleadoEliminado =
                    await empleadoManager.Eliminar(codigoEmpleado, VG.usuarioActual.CadenaToken);

                if (!string.IsNullOrEmpty(codigoEmpleadoEliminado))
                {
                    lblStatus.Text = "Empleado eliminado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al eliminar el empleado";
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

    }
}