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
    public class PuestoManager
    {
        const string URL = "http://localhost:49220/api/lote/";
        const string URLIngresar = "http://localhost:49220/api/lote/ingresar/";

        HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }
        public async Task<IEnumerable<Puesto>> ObtenerPuestos(string token)
        {
            HttpClient client = GetClient(token);
            string resultado = await client.GetStringAsync(URL);

            return JsonConvert.DeserializeObject<IEnumerable<Puesto>>(resultado);
        }

        public async Task<IEnumerable<Puesto>> ObtenerLote(string token, string codigo)
        {
            HttpClient client = GetClient(token);
            string resultado = await client.GetStringAsync(URL + codigo);
            return JsonConvert.DeserializeObject<IEnumerable<Puesto>>(resultado);
        }
        public async Task<Puesto> Ingresar(Puesto puesto, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PostAsync(URLIngresar,
                new StringContent(JsonConvert.SerializeObject(puesto), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Puesto>(await response.Content.ReadAsStringAsync());
        }
        public async Task<Puesto> Actualizar(Puesto puesto, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PutAsync(URL,
                new StringContent(JsonConvert.SerializeObject(puesto), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Puesto>(await response.Content.ReadAsStringAsync());
        }
        public async Task<string> Eliminar(string codigo, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.DeleteAsync(URL + codigo);

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}
