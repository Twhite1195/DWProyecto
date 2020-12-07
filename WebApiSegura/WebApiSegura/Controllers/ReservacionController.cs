using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiSegura.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApiSegura.Controllers
{
    public class ReservacionController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Reservacion reservacion = new Reservacion();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT RES_ID, LOT_ID, USU_ID, CAR_ID, RES_FECHA_INI, RES_FECHA_FIN FROM RESERVACION WHERE RES_ID = @RES_ID", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@RES_ID", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        reservacion.RES_ID = sqlDataReader.GetInt32(0);
                        reservacion.LOT_ID = sqlDataReader.GetInt32(1);
                        reservacion.USU_ID = sqlDataReader.GetInt32(2);
                        reservacion.CAR_ID = sqlDataReader.GetInt32(3);
                        reservacion.RES_FECHA_INI = sqlDataReader.GetDateTime(4);
                        reservacion.RES_FECHA_FIN = sqlDataReader.GetDateTime(5);


                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok(reservacion);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Reservacion> reservaciones = new List<Reservacion>();

            try
            {
                using (SqlConnection sqlConnection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand =
                        new SqlCommand(@"SELECT RES_ID, LOT_ID, USU_ID, CAR_ID, RES_FECHA_INI, RES_FECHA_FIN
                                        FROM RESERVACION", sqlConnection);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Reservacion reservacion = new Reservacion()
                        {
                            RES_ID = sqlDataReader.GetInt32(0),
                            LOT_ID = sqlDataReader.GetInt32(1),
                            USU_ID = sqlDataReader.GetInt32(2),
                            CAR_ID = sqlDataReader.GetInt32(3),
                            RES_FECHA_INI = sqlDataReader.GetDateTime(4),
                            RES_FECHA_FIN = sqlDataReader.GetDateTime(5)

                        };
                        reservaciones.Add(reservacion);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok(reservaciones);
        }
        [HttpPost]
        [Route("api/Reservacion/ingresar")]
        public IHttpActionResult Ingresar(Reservacion reservacion)
        {
            if (reservacion == null)
                return BadRequest();

            if (RegistrarReservacion(reservacion))
                return Ok(reservacion);
            else
                return InternalServerError();
        }

        private bool RegistrarReservacion(Reservacion reservacion)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO RESERVACION ( LOT_ID, USU_ID, CAR_ID, RES_FECHA_INI, RES_FECHA_FIN) 
                    VALUES ( @LOT_ID, @USU_ID, @CAR_ID, @RES_FECHA_INI, @RES_FECHA_FIN)", sqlConnection);


                    sqlCommand.Parameters.AddWithValue("@LOT_ID", reservacion.LOT_ID);
                    sqlCommand.Parameters.AddWithValue("@USU_ID", reservacion.USU_ID);
                    sqlCommand.Parameters.AddWithValue("@CAR_ID", reservacion.CAR_ID);
                    sqlCommand.Parameters.AddWithValue("@RES_FECHA_INI", reservacion.RES_FECHA_INI);
                    sqlCommand.Parameters.AddWithValue("@RES_FECHA_FIN", reservacion.RES_FECHA_FIN);


                    sqlConnection.Open();
                    int filasAfectadas = sqlCommand.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                        resultado = true;

                    sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return resultado;
        }

        [HttpPut]
        public IHttpActionResult Put(Reservacion reservacion)
        {
            if (reservacion == null)
                return BadRequest();
            if (ActualizarReservacion(reservacion))
                return Ok(reservacion);
            else
                return InternalServerError();

        }
        private bool ActualizarReservacion(Reservacion reservacion)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection sqlConnection = new
         SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@" UPDATE RESERVACION SET
                                                                LOT_ID = @LOT_ID,
                                                                USU_ID = @USU_ID,
                                                                CAR_ID = @CAR_ID,
                                                                RES_FECHA_INI= @RES_FECHA_INI,
                                                                RES_FECHA_FIN= @RES_FECHA_FIN
                                                               WHERE RES_ID = @RES_ID ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@RES_ID", reservacion.RES_ID);
                    sqlCommand.Parameters.AddWithValue("@LOT_ID", reservacion.LOT_ID);
                    sqlCommand.Parameters.AddWithValue("@USU_ID", reservacion.USU_ID);
                    sqlCommand.Parameters.AddWithValue("@CAR_ID", reservacion.CAR_ID);
                    sqlCommand.Parameters.AddWithValue("@RES_FECHA_INI", reservacion.RES_FECHA_INI);
                    sqlCommand.Parameters.AddWithValue("@RES_FECHA_FIN", reservacion.RES_FECHA_FIN);
                    sqlConnection.Open();
                    int filasAfectadas = sqlCommand.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                        resultado = true;

                    sqlConnection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            if (EliminarReservacion(id))
                return Ok(id);
            else
                return InternalServerError();
        }
        private bool EliminarReservacion(int id)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE RESERVACION                                                           
                                                            WHERE RES_ID= @RES_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@RES_ID", id);
                    sqlConnection.Open();
                    int filasAfectadas = sqlCommand.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                        resultado = true;

                    sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return resultado;
        }



    }
}

