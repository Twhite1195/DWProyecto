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
    [RoutePrefix("api/carro")]
    public class CarroController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Carro carro = new Carro();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT CAR_ID, MOD_ID, CAR_ESTADO, CAR_PLACA, SED_ID, LOT_ID, RES_ID FROM CARRO WHERE CAR_ID = @CAR_ID", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@CAR_ID", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        carro.CAR_ID = sqlDataReader.GetInt32(0);
                        carro.MOD_ID = sqlDataReader.IsDBNull(1) ? (int?)null : sqlDataReader.GetInt32(1);
                        carro.CAR_ESTADO = sqlDataReader.GetString(2);
                        carro.CAR_PLACA = sqlDataReader.GetString(3);
                        carro.SED_ID = sqlDataReader.IsDBNull(4) ? (int?)null : sqlDataReader.GetInt32(4);
                        carro.LOT_ID = sqlDataReader.IsDBNull(5) ? (int?)null : sqlDataReader.GetInt32(5);
                        carro.RES_ID = sqlDataReader.IsDBNull(6) ? (int?)null : sqlDataReader.GetInt32(6);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok(carro);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Carro> modelos = new List<Carro>();

            try
            {
                using (SqlConnection sqlConnection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand =
                        new SqlCommand(@"SELECT CAR_ID, MOD_ID, CAR_ESTADO, CAR_PLACA, SED_ID, LOT_ID, RES_ID FROM CARRO", sqlConnection);


                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Carro carro = new Carro()
                        {
                            CAR_ID = sqlDataReader.GetInt32(0),
                            MOD_ID = sqlDataReader.IsDBNull(1) ? (int?)null : sqlDataReader.GetInt32(1),
                            CAR_ESTADO = sqlDataReader.GetString(2),
                            CAR_PLACA = sqlDataReader.GetString(3),
                            SED_ID = sqlDataReader.IsDBNull(4) ? (int?)null : sqlDataReader.GetInt32(4),
                            LOT_ID = sqlDataReader.IsDBNull(5) ? (int?)null : sqlDataReader.GetInt32(5),
                            RES_ID = sqlDataReader.IsDBNull(6) ? (int?)null : sqlDataReader.GetInt32(6)
                        };

                        modelos.Add(carro);

                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok(modelos);
        }

        [HttpPost]
        [Route("ingresar")]
        public IHttpActionResult Ingresar(Carro carro)
        {
            if (carro == null)
                return BadRequest();
            if (RegistrarCarro(carro))
            {
                return Ok();
            }
            else return InternalServerError();
            return Ok(carro);
        }

        private bool RegistrarCarro(Carro carro)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO CARRO (MOD_ID, CAR_ESTADO, CAR_PLACA, SED_ID, LOT_ID) 
                    VALUES (@MOD_ID, @CAR_ESTADO, @CAR_PLACA, @SED_ID, @LOT_ID)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@MOD_ID", carro.MOD_ID);
                    sqlCommand.Parameters.AddWithValue("@CAR_ESTADO", carro.CAR_ESTADO);
                    sqlCommand.Parameters.AddWithValue("@CAR_PLACA", carro.CAR_PLACA);
                    sqlCommand.Parameters.AddWithValue("@SED_ID", carro.SED_ID);
                    sqlCommand.Parameters.AddWithValue("@LOT_ID", carro.LOT_ID);
                    


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
        public IHttpActionResult Put(Carro carro)
        {
            if (carro == null)
                return BadRequest();
            if (ActualizarCarro(carro))
                return Ok(carro);
            else
                return InternalServerError();

        }

        private bool ActualizarCarro(Carro carro)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE CARRO SET
                                                               MOD_ID = @MOD_ID,
                                                               CAR_ESTADO = @CAR_ESTADO,
                                                               CAR_PLACA = @CAR_PLACA,
                                                               SED_ID = @SED_ID,
                                                               LOT_ID = @LOT_ID,
                                                               RES_ID = @RES_ID
                                                               WHERE CAR_ID = @CAR_ID ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@CAR_ID", carro.CAR_ID);
                    sqlCommand.Parameters.AddWithValue("@MOD_ID", carro.MOD_ID);
                    sqlCommand.Parameters.AddWithValue("@CAR_ESTADO", carro.CAR_ESTADO);
                    sqlCommand.Parameters.AddWithValue("@CAR_PLACA", carro.CAR_PLACA);
                    sqlCommand.Parameters.AddWithValue("@SED_ID", carro.SED_ID);
                    sqlCommand.Parameters.AddWithValue("@LOT_ID", carro.LOT_ID);
                    sqlCommand.Parameters.AddWithValue("@RES_ID", carro.RES_ID);

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

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();
            if (EliminarCarro(id))
                return Ok(id);
            else
                return InternalServerError();
        }

        private bool EliminarCarro(int id)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE CARRO WHERE CAR_ID = @CAR_ID ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@CAR_ID", id);

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
