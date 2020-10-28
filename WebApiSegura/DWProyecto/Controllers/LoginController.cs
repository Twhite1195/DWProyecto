using System;
using System.Net;
using System.Threading;
using System.Web.Http;
using WebApiSegura.Models;
using System.Data.SqlClient;
using System.Configuration;
using DWProyecto.Models;
using System.Web.Http.Description;


namespace WebApiSegura.Controllers
{
    /// <summary>
    /// login controller class for authenticate users
    /// </summary>
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }

        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }

        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(LoginRequest login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            USUARIO usuario = ValidarUsuario(login);

            
            if (!string.IsNullOrEmpty(usuario.USU_CEDULA))
            {
                var token = TokenGenerator.GenerateTokenJwt(login.Username);
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }

        private USUARIO ValidarUsuario(LoginRequest loginRequest)
        {
            USUARIO usuario = new USUARIO();

            using (SqlConnection sqlConnection = new
                SqlConnection(ConfigurationManager.ConnectionStrings["Proyecto"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"SELECT USU_ID, USU_CEDULA, 
                    USU_PASSWORD, USU_NOMBRE, USU_APELLIDO, USU_TELEFONO, USU_ESTADO, USU_CORREO FROM USUARIO
                    WHERE USU_IDENTIFICACION = @USU_IDENTIFICACION", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@USU_IDENTIFICACION", loginRequest.Username);

                sqlConnection.Open();
                SqlDataReader dr = sqlCommand.ExecuteReader();
                while (dr.Read())
                {
                    if (loginRequest.Password.Equals(dr.GetString(3)))
                    {
                        usuario.USU_ID = dr.GetInt32(0);
                        usuario.USU_CEDULA = dr.GetString(1);
                        usuario.USU_PASSWORD = dr.GetString(2);
                        usuario.USU_NOMBRE = dr.GetString(3);
                        usuario.USU_APELLIDO = dr.GetString(4);
                        usuario.USU_TELEFONO = dr.GetInt32(5);
                        usuario.USU_ESTADO = dr.GetString(6);
                        usuario.USU_CORREO = dr.GetString(7);
                    }
                }

                sqlConnection.Close();
            }

            return usuario;
        }

        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register(USUARIO usuario)
        {
            if (usuario == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            try
            {
                using(SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Proyecto"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@" INSERT INTO USUARIO 
                                      (USU_CEDULA, USU_PASSWORD, USU_NOMBRE, USU_APELLIDO,
                                       USU_TELEFONO, USU_ESTADO, USU_CORREO) VALUES 
                                        (@USU_CEDULA, @USU_PASSWORD, @USU_NOMBRE, @USU_APELLIDO,
                                       @USU_TELEFONO, @USU_ESTADO, @USU_CORREO)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@USU_IDENTIFICACION", usuario.USU_CEDULA);
                    sqlCommand.Parameters.AddWithValue("@USU_NOMBRE", usuario.USU_PASSWORD);
                    sqlCommand.Parameters.AddWithValue("@USU_PASSWORD", usuario.USU_NOMBRE);
                    sqlCommand.Parameters.AddWithValue("@USU_EMAIL", usuario.USU_APELLIDO);
                    sqlCommand.Parameters.AddWithValue("@USU_FEC_NAC", usuario.USU_TELEFONO);
                    sqlCommand.Parameters.AddWithValue("@USU_ESTADO", usuario.USU_ESTADO);
                    sqlCommand.Parameters.AddWithValue("@USU_TELEFONO", usuario.USU_CORREO);

                    sqlConnection.Open();

                    int FilasAfectadas = sqlCommand.ExecuteNonQuery();


                    sqlConnection.Close();
                    
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
            

            return Ok (usuario);
        }
    }
}