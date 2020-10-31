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
    [RoutePrefix("api/proveedor")]//esta es la ruta para acceder a este proveedor
    public class ProveedorController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Proveedor proveedor = new Proveedor();//instancia de la clase proveedor

            try
            {
                using (SqlConnection sqlConnection = new
                   SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT PROVE_ID, SED_ID, PROVE_NOMBRE, 
                                                            PROVE_FUNCION
                                                            FROM   PROVEEDOR WHERE PROVE_ID = @PROVE_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@PROVE_ID", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        proveedor.PROVE_ID = sqlDataReader.GetInt32(0);
                        proveedor.SED_ID = sqlDataReader.GetInt32(1);
                        proveedor.PROVE_NOMBRE = sqlDataReader.GetString(2);
                        proveedor.PROVE_FUNCION = sqlDataReader.GetString(3);

                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok(proveedor);
        }


        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Proveedor> proveedores = new List<Proveedor>();

            try
            {
                using (SqlConnection sqlConnection = new
                   SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT PROVE_ID, SED_ID, PROVE_NOMBRE, 
                                                            PROVE_FUNCION
                                                            FROM   PROVEEDOR", sqlConnection);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Proveedor proveedor = new Proveedor()
                        {
                            PROVE_ID = sqlDataReader.GetInt32(0),
                            SED_ID = sqlDataReader.GetInt32(1),
                            PROVE_NOMBRE = sqlDataReader.GetString(2),
                            PROVE_FUNCION = sqlDataReader.GetString(3)

                        };
                        proveedores.Add(proveedor);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok(proveedores);
        }


        [HttpPost]
        [Route("ingresar")]
        public IHttpActionResult Ingresar(Proveedor proveedor)
        {
            if (proveedor == null)
                return BadRequest();

            if (RegistrarProveedor(proveedor))
                return Ok(proveedor);
            else
                return InternalServerError();

        }


        private bool RegistrarProveedor(Proveedor proveedor)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new
                   SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@" INSERT INTO PROVEEDOR (SED_ID, 
                                                        PROVE_NOMBRE, PROVE_FUNCION) VALUES
                                                        (@SED_ID, @PROVE_NOMBRE, @PROVE_FUNCION )", sqlConnection);


                sqlCommand.Parameters.AddWithValue("@SED_ID", proveedor.SED_ID);
                sqlCommand.Parameters.AddWithValue("@PROVE_NOMBRE", proveedor.PROVE_NOMBRE);
                sqlCommand.Parameters.AddWithValue("@PROVE_FUNCION", proveedor.PROVE_FUNCION);
                sqlConnection.Open();

                int filasAfectadas = sqlCommand.ExecuteNonQuery();

                if (filasAfectadas > 0)
                    resultado = true;

                sqlConnection.Close();
            }


            return resultado;
        }


        [HttpPut]
        public IHttpActionResult Put(Proveedor proveedor)
        {
            if (proveedor == null)
                return BadRequest();

            if (ActualizarProveedor(proveedor))
                return Ok(proveedor);
            else
                return InternalServerError();

        }

        private bool ActualizarProveedor(Proveedor proveedor)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new
                  SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@" UPDATE PROVEEDOR SET
                                                            SED_ID = @SED_ID,
                                                            PROVE_NOMBRE = @PROVE_NOMBRE,
                                                            PROVE_FUNCION = @PROVE_FUNCION
                                                            WHERE PROVE_ID = @PROVE_ID ", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@PROVE_ID", proveedor.PROVE_ID);
                sqlCommand.Parameters.AddWithValue("@SED_ID", proveedor.SED_ID);
                sqlCommand.Parameters.AddWithValue("@PROVE_NOMBRE", proveedor.PROVE_NOMBRE);
                sqlCommand.Parameters.AddWithValue("@PROVE_FUNCION", proveedor.PROVE_FUNCION);

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

            if (EliminarProveedor(id))
                return Ok(id);
            else
                return InternalServerError();
        }

        private bool EliminarProveedor(int id)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@" DELETE PROVEEDOR
                                                          WHERE PROVE_ID = @PROVE_ID ", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@PROVE_ID", id);
                sqlConnection.Open();
                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                if (filasAfectadas > 0)
                    resultado = true;

                sqlConnection.Close();
            }

            return resultado;
        }


    }//end proveedor
}
