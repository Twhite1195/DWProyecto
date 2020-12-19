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
    public partial class Lote : System.Web.UI.Page
    {

        IEnumerable<Models.Lote> lotes = new ObservableCollection<Models.Lote>();
        LoteManager loteManager = new LoteManager();
        SedeManager sedeManager = new SedeManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            InicializarControles();
        }

        private async void InicializarControles()
        {

            lotes = await loteManager.ObtenerLotes(VG.usuarioActual.CadenaToken);
            grdLotes.DataSource = lotes.ToList();
            grdLotes.DataBind();


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
                Models.Lote loteIngresado = new Models.Lote();
                Models.Lote lote = new Models.Lote()
                {
                    //SED_ID = Convert.ToInt32(txtSedeId.Text),
                    SED_ID = Convert.ToInt32(sedeList.SelectedValue),
                    LOTE_DISPONIBILIDAD = Convert.ToBoolean(txtDisponibilidad.Text)
                };

                loteIngresado =
                    await loteManager.Ingresar(lote, VG.usuarioActual.CadenaToken);

                if (loteIngresado != null)
                {
                    lblStatus.Text = "Lote ingresado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al ingresar el lote";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
            //agregado nuevo
            else
            {
                Models.Lote loteIngresado = new Models.Lote();
                Models.Lote lote = new Models.Lote()
                {

                    SED_ID = Convert.ToInt32(sedeList.SelectedValue),
                    LOTE_DISPONIBILIDAD = Convert.ToBoolean(txtDisponibilidad.Text)
                };

                loteIngresado = await loteManager.Ingresar(lote, VG.usuarioActual.CadenaToken);

                if (loteIngresado != null)
                {
                    lblStatus.Text = "Lote ingresado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al ingresar el lote";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
        } //end 


        async protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (ValidarModificar())
            {
                Models.Lote loteModificado = new Models.Lote();
                Models.Lote lote = new Models.Lote()
                {
                    LOT_ID = Convert.ToInt32(txtID.Text),
                    //SED_ID = Convert.ToInt32(txtSedeId.Text),
                    SED_ID = Convert.ToInt32(sedeList.SelectedValue),
                    LOTE_DISPONIBILIDAD = Convert.ToBoolean(txtDisponibilidad.Text),
                };

                loteModificado = await loteManager.Actualizar(lote, VG.usuarioActual.CadenaToken);

                if (loteModificado != null)
                {
                    lblStatus.Text = "Lote modificado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al modificar el lote";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }
            //agregado nuevo
            else
            {
                Models.Lote loteModificado = new Models.Lote();
                Models.Lote lote = new Models.Lote()
                {
                    LOT_ID = Convert.ToInt32(txtID.Text),
                    //SED_ID = Convert.ToInt32(txtSedeId.Text),
                    SED_ID = Convert.ToInt32(sedeList.SelectedValue),
                    LOTE_DISPONIBILIDAD = Convert.ToBoolean(txtDisponibilidad.Text),
                };

                loteModificado =
                    await loteManager.Actualizar(lote, VG.usuarioActual.CadenaToken);

                if (loteModificado != null)
                {
                    lblStatus.Text = "Lote modificado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al modificar el lote";
                    lblStatus.ForeColor = Color.Maroon;
                    lblStatus.Visible = true;
                }
            }

        }//end 

        async protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtID.Text))
            {
                string idLoteEliminado = string.Empty;
                string idLote = string.Empty;

                idLote = txtID.Text;

                idLoteEliminado = await loteManager.Eliminar(idLote, VG.usuarioActual.CadenaToken);

                if (!string.IsNullOrEmpty(idLoteEliminado))
                {
                    lblStatus.Text = "Lote eliminado correctamente";
                    lblStatus.Visible = true;
                    InicializarControles();
                }
                else
                {
                    lblStatus.Text = "Hubo un error al eliminar el lote";
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



            //if (txtSedeId.Text.IsNullOrWhiteSpace())
            if (sedeList.SelectedValue.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la sede";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }

            if (txtDisponibilidad.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la disponibilidad del lote";
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
                lblStatus.Text = "Debe ingresar el id del lote";
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

            if (txtDisponibilidad.Text.IsNullOrWhiteSpace())
            {
                lblStatus.Text = "Debe ingresar la disponilidad del lote";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
                return false;
            }
     

            return true;
        }



    }
}