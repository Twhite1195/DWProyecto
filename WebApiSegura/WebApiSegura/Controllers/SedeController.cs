using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiSegura.Models;
using System.Configuration;

namespace WebApiSegura.Controllers
{


    [Authorize]
    [RoutePrefix("api/sede")]
    public class SedeController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Sede sede = new Sede();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("SELECT SED_ID, SED_NOMBRE, SED_UBICACION FROM SEDE WHERE SED_ID = @SED_ID", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@SED_ID", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        sede.SED_ID = sqlDataReader.GetInt32(0);
                        sede.SED_NOMBRE = sqlDataReader.GetString(1);
                        sede.SED_UBICACION = sqlDataReader.GetString(2);

                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok(sede);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Sede> sedes = new List<Sede>();

            try
            {
                using (SqlConnection sqlConnection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand =
                        new SqlCommand(@"SELECT SED_ID, SED_NOMBRE, SED_UBICACION
                                        FROM SEDE", sqlConnection);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Sede sede = new Sede()
                        {
                            SED_ID = sqlDataReader.GetInt32(0),
                            SED_NOMBRE = sqlDataReader.GetString(1),
                            SED_UBICACION = sqlDataReader.GetString(2)
                        };
                        sedes.Add(sede);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok(sedes);
        }
        [HttpPost]
        [Route("ingresar")]
        public IHttpActionResult Ingresar(Sede sede)
        {
            if (sede == null)
                return BadRequest();

            if (RegistrarSede(sede))
                return Ok(sede);
            else
                return InternalServerError();
        }

        private bool RegistrarSede(Sede sede)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO SEDE ( SED_NOMBRE, SED_UBICACION) 
                    VALUES (@SED_NOMBRE, @SED_UBICACION)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@SED_NOMBRE", sede.SED_NOMBRE);
                    sqlCommand.Parameters.AddWithValue("@SED_UBICACION", sede.SED_UBICACION);


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
        public IHttpActionResult Put(Sede sede)
        {
            if (sede == null)
                return BadRequest();
            if (ActualizarSede(sede))
                return Ok(sede);
            else
                return InternalServerError();

        }
        private bool ActualizarSede(Sede sede)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection sqlConnection = new
         SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@" UPDATE SEDE SET
                                                                SED_NOMBRE = @SED_NOMBRE,
                                                                SED_UBICACION = @SED_UBICACION
                                                                WHERE SED_ID = @SED_ID ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@SED_ID", sede.SED_ID);
                    sqlCommand.Parameters.AddWithValue("@SED_NOMBRE", sede.SED_NOMBRE);
                    sqlCommand.Parameters.AddWithValue("@SED_UBICACION", sede.SED_UBICACION);
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

            if (EliminarSede(id))
                return Ok(id);
            else
                return InternalServerError();
        }
        private bool EliminarSede(int id)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE SEDE                                                           
                                                            WHERE SED_ID= @SED_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@SED_ID", id);
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

