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
    [RoutePrefix("api/puesto")]

    public class PuestoController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Puesto puesto = new Puesto();

            try
            {
                using (SqlConnection sqlConnection = new
                   SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT PUES_ID, PUES_NOMBRE
                                                            FROM   PUESTO WHERE PUES_ID = @PUES_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@PUES_ID", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        puesto.PUES_ID = sqlDataReader.GetInt32(0);
                        puesto.PUES_NOMBRE = sqlDataReader.GetString(1);

                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok(puesto);
        }



        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Puesto> puestos = new List<Puesto>();

            try
            {
                using (SqlConnection sqlConnection = new
                   SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT PUES_ID, PUES_NOMBRE
                                                            FROM   PUESTO", sqlConnection);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Puesto puesto = new Puesto()
                        {
                            PUES_ID = sqlDataReader.GetInt32(0),
                            PUES_NOMBRE = sqlDataReader.GetString(1),

                        };
                        puestos.Add(puesto);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok(puestos);
        }


        [HttpPost]
        [Route("ingresar")]
        public IHttpActionResult Ingresar(Puesto puesto)
        {
            if (puesto == null)
                return BadRequest();

            if (RegistrarPuesto(puesto))
                return Ok(puesto);
            else
                return InternalServerError();

        }


        private bool RegistrarPuesto(Puesto puesto)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new
                   SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@" INSERT INTO PUESTO (PUES_NOMBRE) VALUES
                                        (@PUES_NOMBRE)", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@PUES_NOMBRE", puesto.PUES_NOMBRE);

                sqlConnection.Open();

                int filasAfectadas = sqlCommand.ExecuteNonQuery();

                if (filasAfectadas > 0)
                    resultado = true;

                sqlConnection.Close();
            }


            return resultado;
        }


        [HttpPut]
        public IHttpActionResult Put(Puesto puesto)
        {
            if (puesto == null)
                return BadRequest();

            if (ActualizarPuesto(puesto))
                return Ok(puesto);
            else
                return InternalServerError();

        }

        private bool ActualizarPuesto(Puesto puesto)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new
                  SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@" UPDATE PUESTO SET
                                                            PUES_NOMBRE = @PUES_NOMBRE
                                                          WHERE PUES_ID = @PUES_ID ", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@PUES_ID", puesto.PUES_ID);
                sqlCommand.Parameters.AddWithValue("@PUES_NOMBRE", puesto.PUES_NOMBRE);

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

            if (EliminarPuesto(id))
                return Ok(id);
            else
                return InternalServerError();
        }

        private bool EliminarPuesto(int id)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@" DELETE PUESTO
                                                          WHERE PUES_ID = @PUES_ID ", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@PUES_ID", id);
                sqlConnection.Open();
                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                if (filasAfectadas > 0)
                    resultado = true;

                sqlConnection.Close();
            }

            return resultado;
        }


    }
}

