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
        IEnumerable<Models.Carro> carros = new ObservableCollection<Models.Carro>();
        CarroManager carroManager = new CarroManager();
        ModeloManager modeloManager = new ModeloManager();
        SedeManager sedeManager = new SedeManager();
        LoteManager loteManager = new LoteManager();
        ReservacionManager reservacionManager = new ReservacionManager();


        protected void Page_Load(object sender, EventArgs e)
        {
            InicializarControles();
        }

        private async void InicializarControles()
        {

            carros = await carroManager.ObtenerCarros(VG.usuarioActual.CadenaToken);
            grdCarros.DataSource = carros.ToList();
            grdCarros.DataBind();

            IEnumerable<Models.Modelo> escogeModelos= new ObservableCollection<Models.Modelo>();
            escogeModelos = await modeloManager.ObtenerModelos(VG.usuarioActual.CadenaToken);
            modeloList.DataSource = escogeModelos.ToList();
            modeloList.DataTextField = "MOD_NOMBRE";
            modeloList.DataValueField = "MOD_ID";
            modeloList.DataBind();

            IEnumerable<Models.Sede> escogeSedes = new ObservableCollection<Models.Sede>();
            escogeSedes = await sedeManager.ObtenerSedes(VG.usuarioActual.CadenaToken);
            sedeList.DataSource = escogeSedes.ToList();
            sedeList.DataTextField = "SED_NOMBRE";
            sedeList.DataValueField = "SED_ID";
            sedeList.DataBind();

            IEnumerable<Models.Lote> escogeLotes = new ObservableCollection<Models.Lote>();
            escogeLotes = await loteManager.ObtenerLotes(VG.usuarioActual.CadenaToken);
            loteList.DataSource = escogeLotes.ToList();

            loteList.DataTextField = "LOT_ID";
            loteList.DataValueField = "LOT_ID";
            loteList.DataBind();

        }



        async protected void btnIngresar_Click(object sender, EventArgs e)
        {
            //if (ValidarInsertar())
            //{
            //int reservacionNula2 = Convert.ToInt32(txtRes.Text);
            if (txtRes.Text == "" )
            {
                Models.Carro carroIngresado = new Models.Carro();
                Models.Carro carro = new Models.Carro()
                {
                    MOD_ID = Convert.ToInt32(modeloList.SelectedValue),
                    CAR_ESTADO = txtEstado.Text,
                    CAR_PLACA = txtPlaca.Text,
                    SED_ID = Convert.ToInt32(sedeList.SelectedValue),
                    LOT_ID = Convert.ToInt32(loteList.SelectedValue),
                    RES_ID = 0
                    //MOD_ID = Convert.ToInt32(txtModeloID.Text),
                    //CAR_ESTADO = txtEstado.Text,
                    //CAR_PLACA = txtPlaca.Text,
                    //SED_ID = Convert.ToInt32(txtSede.Text),
                    //LOT_ID = Convert.ToInt32(txtLote.Text),
                    //RES_ID = 0
                };

                carroIngresado = await carroManager.Ingresar(carro, VG.usuarioActual.CadenaToken);

                if (carroIngresado != null)
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
            else
            {
                Models.Carro carroIngresado = new Models.Carro();
                Models.Carro carro = new Models.Carro()
                {

                    MOD_ID = Convert.ToInt32(modeloList.SelectedValue),
                    CAR_ESTADO = txtEstado.Text,
                    CAR_PLACA = txtPlaca.Text,
                    SED_ID = Convert.ToInt32(sedeList.SelectedValue),
                    LOT_ID = Convert.ToInt32(loteList.SelectedValue),
                    RES_ID = Convert.ToInt32(txtRes.Text)
                };

                carroIngresado = await carroManager.Ingresar(carro, VG.usuarioActual.CadenaToken);

                if (carroIngresado != null)
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
            //if (ValidarModificar())
            //{
            if (txtRes.Text == "")
            {
                Models.Carro carroModificado = new Models.Carro();
                Models.Carro carro = new Models.Carro()
                {
                    CAR_ID = Convert.ToInt32(txtID.Text),
                    MOD_ID = Convert.ToInt32(modeloList.SelectedValue),
                    CAR_ESTADO = txtEstado.Text,
                    CAR_PLACA = txtPlaca.Text,
                    SED_ID = Convert.ToInt32(sedeList.SelectedValue),
                    LOT_ID = Convert.ToInt32(loteList.SelectedValue),
                    RES_ID = 0
                };

                carroModificado =
                    await carroManager.Actualizar(carro, VG.usuarioActual.CadenaToken);

                if (carroModificado != null)
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
            else
            {
                Models.Carro carroModificado = new Models.Carro();
                Models.Carro carro = new Models.Carro()
                {
                    CAR_ID = Convert.ToInt32(txtID.Text),
                    MOD_ID = Convert.ToInt32(modeloList.SelectedValue),
                    CAR_ESTADO = txtEstado.Text,
                    CAR_PLACA = txtPlaca.Text,
                    SED_ID = Convert.ToInt32(sedeList.SelectedValue),
                    LOT_ID = Convert.ToInt32(loteList.SelectedValue),
                    RES_ID = Convert.ToInt32(txtRes.Text)
                };

                carroModificado =
                    await carroManager.Actualizar(carro, VG.usuarioActual.CadenaToken);

                if (carroModificado != null)
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
                string idCarroEliminado = string.Empty;
                string idCarro = string.Empty;

                idCarro = txtID.Text;

                idCarroEliminado =
                    await carroManager.Eliminar(idCarro, VG.usuarioActual.CadenaToken);

                if (!string.IsNullOrEmpty(idCarroEliminado))
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

        
        //private bool ValidarInsertar()
        //{

        //    if (txtModeloID.Text.IsNullOrWhiteSpace())
        //    {
        //        lblStatus.Text = "Debe ingresar la sede";
        //        lblStatus.ForeColor = Color.Maroon;
        //        lblStatus.Visible = true;
        //        return false;
        //    }

        //    if (txtEstado.Text.IsNullOrWhiteSpace())
        //    {
        //        lblStatus.Text = "Debe ingresar el nombre del carro";
        //        lblStatus.ForeColor = Color.Maroon;
        //        lblStatus.Visible = true;
        //        return false;
        //    }

        //    if (txtPlaca.Text.IsNullOrWhiteSpace())
        //    {
        //        lblStatus.Text = "Debe ingresar la funcion del carro";
        //        lblStatus.ForeColor = Color.Maroon;
        //        lblStatus.Visible = true;
        //        return false;
        //    }
        //    if (txtSede.Text.IsNullOrWhiteSpace())
        //    {
        //        lblStatus.Text = "Debe ingresar la funcion del carro";
        //        lblStatus.ForeColor = Color.Maroon;
        //        lblStatus.Visible = true;
        //        return false;
        //    }
        //    if (txtLote.Text.IsNullOrWhiteSpace())
        //    {
        //        lblStatus.Text = "Debe ingresar la funcion del carro";
        //        lblStatus.ForeColor = Color.Maroon;
        //        lblStatus.Visible = true;
        //        return false;
        //    }
        //    return true;
        //}


        //private bool ValidarModificar()
        //{
        //    if (txtID.Text.IsNullOrWhiteSpace())
        //    {
        //        lblStatus.Text = "Debe ingresar el ID del carro";
        //        lblStatus.ForeColor = Color.Maroon;
        //        lblStatus.Visible = true;
        //        return false;
        //    }

        //    if (txtModeloID.Text.IsNullOrWhiteSpace())
        //    {
        //        lblStatus.Text = "Debe ingresar El Modelo del carro";
        //        lblStatus.ForeColor = Color.Maroon;
        //        lblStatus.Visible = true;
        //        return false;
        //    }

        //    if (txtEstado.Text.IsNullOrWhiteSpace())
        //    {
        //        lblStatus.Text = "Debe ingresar el estado del carro";
        //        lblStatus.ForeColor = Color.Maroon;
        //        lblStatus.Visible = true;
        //        return false;
        //    }

        //    if (txtPlaca.Text.IsNullOrWhiteSpace())
        //    {
        //        lblStatus.Text = "Debe ingresar la Placa del carro";
        //        lblStatus.ForeColor = Color.Maroon;
        //        lblStatus.Visible = true;
        //        return false;
        //    }
        //    if (txtSede.Text.IsNullOrWhiteSpace())
        //    {
        //        lblStatus.Text = "Debe ingresar la Sede del carro";
        //        lblStatus.ForeColor = Color.Maroon;
        //        lblStatus.Visible = true;
        //        return false;
        //    }
        //    if (txtLote.Text.IsNullOrWhiteSpace())
        //    {
        //        lblStatus.Text = "Debe ingresar el Lote del carro";
        //        lblStatus.ForeColor = Color.Maroon;
        //        lblStatus.Visible = true;
        //        return false;
        //    }

        //    return true;
        //}
    }
}