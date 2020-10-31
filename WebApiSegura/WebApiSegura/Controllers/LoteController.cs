using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiSegura.Models;

namespace WebApiSegura.Controllers
{

    [Authorize]
    [RoutePrefix("api/lote")]//esta es la ruta para acceder a este lote
    public class LoteController : ApiController
    {

        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Lote lote = new Lote();//instancia de la clase lote

            try
            {
                using (SqlConnection sqlConnection = new
                   SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT LOT_ID, SED_ID, LOTE_DISPONIBILIDAD                                                        
                                                            FROM   LOTE WHERE LOT_ID = @LOT_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@LOT_ID", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        lote.LOT_ID = sqlDataReader.GetInt32(0);
                        lote.SED_ID = sqlDataReader.GetInt32(1);
                        lote.LOTE_DISPONIBILIDAD = sqlDataReader.GetBoolean(2);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok(lote);
        }


        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Lote> lotes = new List<Lote>();

            try
            {
                using (SqlConnection sqlConnection = new
                   SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT LOT_ID, SED_ID, LOTE_DISPONIBILIDAD                                                           
                                                            FROM   LOTE", sqlConnection);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Lote lote = new Lote()
                        {
                            LOT_ID = sqlDataReader.GetInt32(0),
                            SED_ID = sqlDataReader.GetInt32(1),
                            LOTE_DISPONIBILIDAD = sqlDataReader.GetBoolean(2),
                        };
                        lotes.Add(lote);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok(lotes);
        }


        [HttpPost]
        [Route("ingresar")]
        public IHttpActionResult Ingresar(Lote lote)
        {
            if (lote == null)
                return BadRequest();

            if (RegistrarLote(lote))
                return Ok(lote);
            else
                return InternalServerError();

        }


        private bool RegistrarLote(Lote lote)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new
                   SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@" INSERT INTO LOTE ( 
                                                        SED_ID, LOTE_DISPONIBILIDAD) VALUES
                                                        (@SED_ID, @LOTE_DISPONIBILIDAD )", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@SED_ID", lote.SED_ID);
                sqlCommand.Parameters.AddWithValue("@LOTE_DISPONIBILIDAD", lote.LOTE_DISPONIBILIDAD);

                sqlConnection.Open();

                int filasAfectadas = sqlCommand.ExecuteNonQuery();

                if (filasAfectadas > 0)
                    resultado = true;

                sqlConnection.Close();
            }


            return resultado;
        }


        [HttpPut]
        public IHttpActionResult Put(Lote lote)
        {
            if (lote == null)
                return BadRequest();

            if (ActualizarLote(lote))
                return Ok(lote);
            else
                return InternalServerError();

        }

        private bool ActualizarLote(Lote lote)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new
                  SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@" UPDATE LOTE SET                                                            
                                                            SED_ID = @SED_ID,
                                                            LOTE_DISPONIBILIDAD = @LOTE_DISPONIBILIDAD                                                            
                                                            WHERE LOT_ID = @LOT_ID ", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@LOT_ID", lote.LOT_ID);
                sqlCommand.Parameters.AddWithValue("@SED_ID", lote.SED_ID);
                sqlCommand.Parameters.AddWithValue("@LOTE_DISPONIBILIDAD", lote.LOTE_DISPONIBILIDAD);


                sqlConnection.Open();

                int filasAfectadas = sqlCommand.ExecuteNonQuery();

                if (filasAfectadas > 0)
                    resultado = true;

                sqlConnection.Close();
            }

            return resultado;
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            if (EliminarLote(id))
                return Ok(id);
            else
                return InternalServerError();
        }

        private bool EliminarLote(int id)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@" DELETE LOTE
                                                          WHERE LOT_ID = @LOT_ID ", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@LOT_ID", id);
                sqlConnection.Open();
                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                if (filasAfectadas > 0)
                    resultado = true;

                sqlConnection.Close();
            }

            return resultado;
        }

    }//end class
}
