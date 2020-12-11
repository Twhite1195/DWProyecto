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
    public partial class Reservacion : System.Web.UI.Page
    {
        IEnumerable<Models.Reservacion> reservaciones = new ObservableCollection<Models.Reservacion>();
        ReservacionManager reservacionManager = new ReservacionManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            InicializarControles();

        }
        private async void InicializarControles()
        {

            reservaciones = await reservacionManager.ObtenerReservaciones(VG.usuarioActual.CadenaToken);
            grdReservaciones.DataSource = reservaciones.ToList();
            grdReservaciones.DataBind();
        }

        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar())
            {
                Models.Reservacion reservacionIngresado = new Models.Reservacion();
                Models.Reservacion reservacion = new Models.Reservacion()
                {
                    LOT_ID = Convert.ToInt32(txtLoteId.Text),
                    USU_ID = Convert.ToInt32(txtUsuarioId.Text),
                    CAR_ID = Convert.ToInt32(txtCarroId.Text),
                    RES_FECHA_INI = Calendar1.SelectedDate,
                    RES_FECHA_FIN = Calendar2.SelectedDate
                };

                reservacionIngresado =
                    await reservacionManager.Ingresar(reservacion, VG.usuarioActual.CadenaToken);

                if (reservacionIngresado != null)
                {
                    lblStatus.Text = "Reservacion ingresado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al ingresar la reservacion";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }

        }




        async protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarModificar())
            {
                Models.Reservacion hotelModificado = new Models.Reservacion();
                Models.Reservacion reservacion = new Models.Reservacion()
                {
                    RES_ID = Convert.ToInt32(txtReservaId.Text),
                    LOT_ID = Convert.ToInt32(txtLoteId.Text),
                    USU_ID = Convert.ToInt32(txtUsuarioId.Text),
                    CAR_ID = Convert.ToInt32(txtCarroId.Text),
                    RES_FECHA_INI = Calendar1.SelectedDate,
                    RES_FECHA_FIN = Calendar2.SelectedDate
                };

                hotelModificado =
                    await reservacionManager.Actualizar(reservacion, VG.usuarioActual.CadenaToken);

                if (hotelModificado != null)
                {
                    lblStatus.Text = "Reservacion modificado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al modificar la reservacion";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
        }

        async protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtReservaId.Text))
            {
                string codigoReservacionEliminado = string.Empty;
                string codigoReservacion = string.Empty;

                codigoReservacion = txtReservaId.Text;

                codigoReservacionEliminado =
                    await reservacionManager.Eliminar(codigoReservacion, VG.usuarioActual.CadenaToken);

                if (!string.IsNullOrEmpty(codigoReservacionEliminado))
                {
                    lblStatus.Text = "Reservacion eliminado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al eliminar la reservacion";
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




        private bool ValidarInsertar()
        {


            if (txtLoteId.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el nombre del reservacion";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtUsuarioId.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el usuario";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtCarroId.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el ID del carro";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (Calendar1.SelectedDate.ToShortDateString().Equals("01/01/0001"))
            {
                lblStatus.Text = "Debe seleccionar una fecha de inicio";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;

            }

            if (Calendar2.SelectedDate.ToShortDateString().Equals("01/01/0001"))
            {
                lblStatus.Text = "Debe seleccionar una fecha de inicio";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;

            }

            return true;
        }


        private bool ValidarModificar()
        {
            if (txtReservaId.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el codigo del reservacion";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtLoteId.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el nombre del reservacion";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtUsuarioId.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el usuario";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtCarroId.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el ID del carro";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (Calendar1.SelectedDate.ToShortDateString().Equals("01/01/0001"))/*Calendar de ingreso*/
            {
                lblStatus.Text = "Debe seleccionar una fecha de inicio";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;

            }

            if (Calendar2.SelectedDate.ToShortDateString().Equals("01/01/0001"))/*Calendar de ingreso*/
            {
                lblStatus.Text = "Debe seleccionar una fecha de inicio";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;

            }

            return true;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Calendar1.Visible = true;

        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            lblFechaInicio.Text = Calendar1.SelectedDate.ToShortDataString();
            lblFechaInicio.Visible = true;
            Calendar1.Visible = false;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Calendar2.Visible = true;
        }

        protected void Calendar2_SelectionChanged(object sender, EventArgs e)
        {
            lblFechaFin.Text = Calendar2.SelectedDate.ToShortDataString();
            lblFechaFin.Visible = true;
            Calendar2.Visible = false;
        }
    }
}