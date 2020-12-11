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
    public class LoteManager
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
        public async Task<IEnumerable<Lote>> ObtenerLotes(string token)
        {
            HttpClient client = GetClient(token);
            string resultado = await client.GetStringAsync(URL);

            return JsonConvert.DeserializeObject<IEnumerable<Lote>>(resultado);
        }

        public async Task<IEnumerable<Lote>> ObtenerLote(string token, string codigo)
        {
            HttpClient client = GetClient(token);
            string resultado = await client.GetStringAsync(URL + codigo);
            return JsonConvert.DeserializeObject<IEnumerable<Lote>>(resultado);
        }
        public async Task<Lote> Ingresar(Lote lote, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PostAsync(URLIngresar,
                new StringContent(JsonConvert.SerializeObject(lote), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Lote>(await response.Content.ReadAsStringAsync());
        }
        public async Task<Lote> Actualizar(Lote lote, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PutAsync(URL,
                new StringContent(JsonConvert.SerializeObject(lote), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Lote>(await response.Content.ReadAsStringAsync());
        }
        public async Task<string> Eliminar(string codigo, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.DeleteAsync(URL + codigo);

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}
