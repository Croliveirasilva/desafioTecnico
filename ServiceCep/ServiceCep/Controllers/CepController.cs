using System.Collections.Generic;
using System.Web.Http;
using ServiceCep.DTO;
using ServiceCep.Models;

namespace ServiceCep.Controllers
{
    public class CepController : ApiController
    {
        
        [HttpGet]
        public CepDTO Search(string zipCode)

        {
            //Instansiando o objeto Cep 
            CepDTO cepResult = new CepDTO();


            //chama o metodo de pesquisa do banco de dados
            cepResult = clServiceCep.SearchCep(zipCode);

            // se o metodo retornar vazio ele realiza a busca na api via cep
            if (cepResult == "")
            {
                //Chamando o metodo de busca via cep
                cepResult = clServiceCep.Search(zipCode);

            }

           
           // caso o retorno do banco venha com resultado ele será apresentado, mas se o registro não existir o retorno da api traz o resultado
            return cepResult;
        }
    }
}
