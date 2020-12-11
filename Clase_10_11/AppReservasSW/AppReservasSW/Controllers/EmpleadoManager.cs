using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppReservasSW.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text;


namespace AppReservasSW.Controllers
{
    public class EmpleadoManager
    {
        const string URL = "http://localhost:49220/api/Empleado/";
        const string URLIngresar = "http://localhost:49220/api/lote/Empleado/";

        HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }
        public async Task<IEnumerable<Empleado>> ObtenerEmpleados(string token)
        {
            HttpClient client = GetClient(token);
            string resultado = await client.GetStringAsync(URL);

            return JsonConvert.DeserializeObject<IEnumerable<Empleado>>(resultado);
        }

        public async Task<IEnumerable<Empleado>> ObtenerEmpleados(string token, string codigo)
        {
            HttpClient client = GetClient(token);
            string resultado = await client.GetStringAsync(URL + codigo);
            return JsonConvert.DeserializeObject<IEnumerable<Empleado>>(resultado);
        }
        public async Task<Empleado> Ingresar(Empleado empleado, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PostAsync(URLIngresar,
                new StringContent(JsonConvert.SerializeObject(empleado), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Empleado>(await response.Content.ReadAsStringAsync());
        }
        public async Task<Empleado> Actualizar(Empleado empleado, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PutAsync(URL,
                new StringContent(JsonConvert.SerializeObject(empleado), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Empleado>(await response.Content.ReadAsStringAsync());
        }
        public async Task<string> Eliminar(string codigo, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.DeleteAsync(URL + codigo);

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}
