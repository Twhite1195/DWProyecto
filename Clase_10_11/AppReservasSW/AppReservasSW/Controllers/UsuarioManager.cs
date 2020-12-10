using AppReservasSW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace AppReservasSW.Controllers
{
    public class UsuarioManager
    {
        const string UrlAuthenticate = "http://localhost:49220/api/login/authenticate/";
        const string UrlRegister = "http://localhost:49220/api/login/register/";

        public async Task<Usuario> Validar(string username, string password)
        {
            LoginRequest loginRequest = new 
                LoginRequest() { Username = username, Password = password };

            HttpClient httpClient = new HttpClient(); // paquete ppara especificar el header application json, body, etc

            var response = await
                httpClient.PostAsync(UrlAuthenticate,// lo envía por post a esa url 
                new StringContent(JsonConvert.SerializeObject(loginRequest),// tomamos el objeto y lo convertimos en algo transportable ejm json, xml etc 
                Encoding.UTF8,"application/json"));// utf8 para que reconozca todos los caracterez

            return 
                JsonConvert.DeserializeObject<Usuario>// nos devuelve un objeto y hay que deserializar mas el token 
                (await response.Content.ReadAsStringAsync());
        }


        public async Task<Usuario> Registrar(Usuario usuario)
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.PostAsync(UrlRegister,
                new StringContent(JsonConvert.SerializeObject(usuario),
                Encoding.UTF8, "application/json"));

            return
                JsonConvert.DeserializeObject<Usuario>(
                    await response.Content.ReadAsStringAsync());
        }

    }
}