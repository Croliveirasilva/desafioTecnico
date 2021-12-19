using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using ServiceCep.DTO;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Diagnostics;

namespace ServiceCep.Models
{
    public class clServiceCep
    {
        private static readonly HttpClientHandler _clientHandler;

        private static HttpClient GetHttpClient() => _clientHandler == null ? new HttpClient() : new HttpClient(_clientHandler);


        public clServiceCep(string proxyHost, int proxyPort, string proxyUser, string proxyPassword)
        {
            var _clientHandler = new HttpClientHandler
            {
                Proxy = new WebProxy(proxyHost, proxyPort)
                {
                    Credentials = new NetworkCredential(proxyUser, proxyPassword)
                }
            };
        }

        public static CepDTO Search(string zipCode) => SearchAsync(zipCode).GetAwaiter().GetResult();

        public static async Task<CepDTO> SearchAsync(string zipCode)
        {
            using (var client = GetHttpClient())
            {

                try
                {
                    string uri = "http://viacep.com.br/ws/" + zipCode.Trim() + "/json/";

                    HttpClient cliente = new HttpClient();

                    HttpResponseMessage resposta = await cliente.GetAsync(uri).ConfigureAwait(false);

                    HttpContent conteudo = resposta.Content;

                    string resultado = await conteudo.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<CepDTO>(resultado);


                }
                catch (Exception exception)
                {
                    Debug.Print($"Ocorreu um erro na sua pesquisa {exception}");

                    throw;
                }

            }
        }
    }
}