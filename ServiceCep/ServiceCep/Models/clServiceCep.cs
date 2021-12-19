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
        //declaração do manipulador de mensagens padrão usado pelo HttpClient
        private static readonly HttpClientHandler _clientHandler;

        // declaração do httpClinet para enviar solicitações HTTP e receber respostas HTTP
        private static HttpClient GetHttpClient() => _clientHandler == null ? new HttpClient() : new HttpClient(_clientHandler);

        //Contém as configurações de proxy HTTP para a classe WebRequest.

        public clServiceCep(string proxyHost, int proxyPort, string proxyUser, string proxyPassword)
        {
           // isntansiando o manipulador de mensagens
            var _clientHandler = new HttpClientHandler
            {
                //instansiando o proxy HTTP
                Proxy = new WebProxy(proxyHost, proxyPort)
                {
                    //instansiando esquemas de autenticação
                    Credentials = new NetworkCredential(proxyUser, proxyPassword)
                }
            };
        }

        //metodo de busca realizando a chamada de forma Asincrona 
        public static CepDTO Search(string zipCode) => SearchAsync(zipCode).GetAwaiter().GetResult();

        public static async Task<CepDTO> SearchAsync(string zipCode)
        {
            // declaração para chamar o httpClient
            using (var client = GetHttpClient())
            {

                try
                {
                    //definindo a url de pesquisa com o zipcode recebido por url
                    //Instanciando o httpClient

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