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
    [RoutePrefix("api/modelo")]
    public class ModeloController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Modelo modelo = new Modelo();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT MOD_ID, MOD_MARCA, MOD_NOMBRE FROM MODELO WHERE MOD_ID = @MOD_ID", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@MOD_ID", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        modelo.MOD_ID = sqlDataReader.GetInt32(0);
                        modelo.MOD_MARCA = sqlDataReader.GetString(1);
                        modelo.MOD_NOMBRE = sqlDataReader.GetString(2);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok(modelo);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Modelo> modelos = new List<Modelo>();

            try
            {
                using (SqlConnection sqlConnection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand =
                        new SqlCommand(@"SELECT MOD_ID, MOD_MARCA, MOD_NOMBRE FROM MODELO", sqlConnection);


                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Modelo modelo = new Modelo()
                        {
                            MOD_ID = sqlDataReader.GetInt32(0),
                            MOD_MARCA = sqlDataReader.GetString(1),
                            MOD_NOMBRE = sqlDataReader.GetString(2)
                        };

                        modelos.Add(modelo);

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
        public IHttpActionResult Ingresar(Modelo modelo)
        {
            if (modelo == null)
                return BadRequest();
            if (RegistrarModelo(modelo))
            {
                return Ok(modelo);
            }
            else return InternalServerError();
        }

        private bool RegistrarModelo(Modelo modelo)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO MODELO (MOD_MARCA, MOD_NOMBRE) 
                    VALUES (@MOD_MARCA, @MOD_NOMBRE)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@MOD_MARCA", modelo.MOD_MARCA);
                    sqlCommand.Parameters.AddWithValue("@MOD_NOMBRE", modelo.MOD_NOMBRE);


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
        public IHttpActionResult Put(Modelo modelo)
        {
            if (modelo == null)
                return BadRequest();
            if (ActualizarModelo(modelo))
                return Ok(modelo);
            else
                return InternalServerError();

        }

        private bool ActualizarModelo(Modelo modelo)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE MODELO SET
                                                                MOD_MARCA = @MOD_MARCA,
                                                                MOD_NOMBRE = @MOD_NOMBRE
                                                               WHERE MOD_ID = @MOD_ID ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@MOD_ID", modelo.MOD_ID);
                    sqlCommand.Parameters.AddWithValue("@MOD_MARCA", modelo.MOD_MARCA);
                    sqlCommand.Parameters.AddWithValue("@MOD_NOMBRE", modelo.MOD_NOMBRE);

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
            if (EliminarModelo(id))
                return Ok(id);
            else
                return InternalServerError();
        }

        private bool EliminarModelo(int id)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE MODELO WHERE MOD_ID = @MOD_ID ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@MOD_ID", id);

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
