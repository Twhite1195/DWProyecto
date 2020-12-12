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
    [RoutePrefix("api/empleado")]
    public class EmpleadoController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Empleado empleado = new Empleado();

            try
            {
                using (SqlConnection sqlConnection = new
                   SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT EMP_ID,
	                                                                SED_ID,
	                                                                PUES_ID,
	                                                                EMP_CEDULA,
	                                                                EMP_NOMBRE,
	                                                                EMP_APELLIDO,
	                                                                EMP_TELEFONO,
	                                                                EMP_RESIDENCIA,
	                                                                EMP_ESTADO
                                                            FROM   EMPLEADO WHERE EMP_ID = @EMP_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@EMP_ID", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        empleado.EMP_ID = sqlDataReader.GetInt32(0);
                        empleado.SED_ID = sqlDataReader.GetInt32(1);
                        empleado.PUES_ID = sqlDataReader.GetInt32(2);
                        empleado.EMP_CEDULA = sqlDataReader.GetInt32(3);
                        empleado.EMP_NOMBRE = sqlDataReader.GetString(4);
                        empleado.EMP_APELLIDO = sqlDataReader.GetString(5);
                        empleado.EMP_TELEFONO = sqlDataReader.GetString(6);
                        empleado.EMP_RESIDENCIA = sqlDataReader.GetString(7);
                        empleado.EMP_ESTADO = sqlDataReader.GetString(8);

                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok(empleado);
        }



        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Empleado> empleados = new List<Empleado>();

            try
            {
                using (SqlConnection sqlConnection = new
                   SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT EMP_ID,SED_ID,PUES_ID,
	                                          EMP_CEDULA,EMP_NOMBRE,EMP_APELLIDO, EMP_TELEFONO,EMP_RESIDENCIA,EMP_ESTADO
                                                            FROM EMPLEADO ", sqlConnection);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Empleado empleado = new Empleado()
                        {
                            EMP_ID = sqlDataReader.GetInt32(0),
                            SED_ID = sqlDataReader.GetInt32(1),
                            PUES_ID = sqlDataReader.GetInt32(2),
                            EMP_CEDULA = sqlDataReader.GetInt32(3),
                            EMP_NOMBRE = sqlDataReader.GetString(4),
                            EMP_APELLIDO = sqlDataReader.GetString(5),
                            EMP_TELEFONO = sqlDataReader.GetString(6),
                            EMP_RESIDENCIA = sqlDataReader.GetString(7),
                            EMP_ESTADO = sqlDataReader.GetString(8)


                        };
                        empleados.Add(empleado);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok(empleados);
        }


        [HttpPost]
        [Route("ingresar")]
        public IHttpActionResult Ingresar(Empleado empleado)
        {
            if (empleado == null)
                return BadRequest();

            if (RegistrarEmpleado(empleado))
                return Ok(empleado);
            else
                return InternalServerError();

        }


        public bool RegistrarEmpleado(Empleado empleado)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new
                   SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@" INSERT INTO EMPLEADO (
                                                                    SED_ID,
	                                                                PUES_ID,
	                                                                EMP_CEDULA,
	                                                                EMP_NOMBRE,
	                                                                EMP_APELLIDO,
	                                                                EMP_TELEFONO,
	                                                                EMP_RESIDENCIA,
	                                                                EMP_ESTADO
                                                                    ) VALUES
                                                                   (@SED_ID,
	                                                                @PUES_ID,
	                                                                @EMP_CEDULA,
	                                                                @EMP_NOMBRE,
	                                                                @EMP_APELLIDO,
	                                                                @EMP_TELEFONO,
	                                                                @EMP_RESIDENCIA,
	                                                                @EMP_ESTADO)", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@SED_ID", empleado.SED_ID);
                sqlCommand.Parameters.AddWithValue("@PUES_ID", empleado.PUES_ID);
                sqlCommand.Parameters.AddWithValue("@EMP_CEDULA", empleado.EMP_CEDULA);
                sqlCommand.Parameters.AddWithValue("@EMP_NOMBRE", empleado.EMP_NOMBRE);
                sqlCommand.Parameters.AddWithValue("@EMP_APELLIDO", empleado.EMP_APELLIDO);
                sqlCommand.Parameters.AddWithValue("@EMP_TELEFONO", empleado.EMP_TELEFONO);
                sqlCommand.Parameters.AddWithValue("@EMP_RESIDENCIA", empleado.EMP_RESIDENCIA);
                sqlCommand.Parameters.AddWithValue("@EMP_ESTADO", empleado.EMP_ESTADO);

                sqlConnection.Open();

                int filasAfectadas = sqlCommand.ExecuteNonQuery();

                if (filasAfectadas > 0)
                    resultado = true;

                sqlConnection.Close();
            }


            return resultado;
        }


        [HttpPut]
        public IHttpActionResult Put(Empleado empleado
            )
        {
            if (empleado == null)
                return BadRequest();

            if (ActualizarEmpleado(empleado))
                return Ok(empleado
                    );
            else
                return InternalServerError();

        }

        public bool ActualizarEmpleado(Empleado empleado
            )
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new
                  SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@" UPDATE EMPLEADO SET
                                                                    SED_ID = @SED_ID,
	                                                                PUES_ID = @PUES_ID,
	                                                                EMP_CEDULA =  @EMP_CEDULA,
	                                                                EMP_NOMBRE = @EMP_NOMBRE,
	                                                                EMP_APELLIDO = @EMP_APELLIDO,
	                                                                EMP_TELEFONO = @EMP_TELEFONO,
	                                                                EMP_RESIDENCIA = @EMP_RESIDENCIA,
	                                                                EMP_ESTADO = @EMP_ESTADO
                                                                    WHERE EMP_ID = @EMP_ID ", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@EMP_ID", empleado.EMP_ID);
                sqlCommand.Parameters.AddWithValue("@SED_ID", empleado.SED_ID);
                sqlCommand.Parameters.AddWithValue("@PUES_ID", empleado.PUES_ID);
                sqlCommand.Parameters.AddWithValue("@EMP_CEDULA", empleado.EMP_CEDULA);
                sqlCommand.Parameters.AddWithValue("@EMP_NOMBRE", empleado.EMP_NOMBRE);
                sqlCommand.Parameters.AddWithValue("@EMP_APELLIDO", empleado.EMP_APELLIDO);
                sqlCommand.Parameters.AddWithValue("@EMP_TELEFONO", empleado.EMP_TELEFONO);
                sqlCommand.Parameters.AddWithValue("@EMP_RESIDENCIA", empleado.EMP_RESIDENCIA);
                sqlCommand.Parameters.AddWithValue("@EMP_ESTADO", empleado.EMP_ESTADO);


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

            if (EliminarEmpleado(id))
                return Ok(id);
            else
                return InternalServerError();
        }

        public bool EliminarEmpleado(int id)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["RESERVAS"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@" DELETE EMPLEADO
                                                          WHERE EMP_ID = @EMP_ID ", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@EMP_ID", id);
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