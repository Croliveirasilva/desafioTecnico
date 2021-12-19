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

            //Chamando o metodo de busca 
            cepResult = clServiceCep.Search(zipCode);

            return cepResult;
        }
    }
}
