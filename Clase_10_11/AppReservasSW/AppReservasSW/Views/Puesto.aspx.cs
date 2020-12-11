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
    public partial class Puesto : System.Web.UI.Page
    {
        IEnumerable<Models.Puesto> puestos = new ObservableCollection<Models.Puesto>();
        PuestoManager puestoManager = new PuestoManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            InicializarControles();
        }

        private async void InicializarControles()
        {
            //si hay alguna modificacion de inmediato aparece en la lista 
            puestos = await puestoManager.ObtenerPuestos(VG.usuarioActual.CadenaToken);
            grdPuestos.DataSource = puestos.ToList();
            grdPuestos.DataBind();
        }

        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            if (ValidarInsertar())
            {
                Models.Puesto puestoIngresado = new Models.Puesto();
                Models.Puesto puesto = new Models.Puesto()
                {

                    PUES_NOMBRE = txt_puesto.Text

                };

                puestoIngresado = await puestoManager.Ingresar(puesto, VG.usuarioActual.CadenaToken);

                if (puestoIngresado != null)
                {
                    lblStatus.Text = "Puesto ingresado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al ingresar el Puesto";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
        }


        private bool ValidarInsertar()
        {


            if (txt_puesto.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar un puesto";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }


            return true;
        }


        private bool ValidarModificar()
        {
            if (txt_puesto_ID.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el id del Puesto";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }



            if (txt_puesto.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar el puesto";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }


            return true;
        }


        protected void Txt_puesto_TextChanged(object sender, EventArgs e)
        {

        }

        async protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarModificar())
            {
                Models.Puesto puestoModificado = new Models.Puesto();
                Models.Puesto puesto = new Models.Puesto()
                {
                    PUES_ID = Convert.ToInt32(txt_puesto_ID.Text),

                    PUES_NOMBRE = txt_puesto.Text
                };

                puestoModificado =
                    await puestoManager.Actualizar(puesto, VG.usuarioActual.CadenaToken);

                if (puestoModificado != null)
                {
                    lblStatus.Text = "Puesto modificado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al modificar el puesto";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
        }

        async protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_puesto_ID.Text))
            {
                string idPuestoEliminado = string.Empty;
                string idPuesto = string.Empty;

                idPuesto = txt_puesto_ID.Text;

                idPuestoEliminado =
                    await puestoManager.Eliminar(idPuesto, VG.usuarioActual.CadenaToken);

                if (!string.IsNullOrEmpty(idPuestoEliminado))
                {
                    lblStatus.Text = "Puesto eliminado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al eliminar el puesto";
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

        protected void grdPuestos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}


