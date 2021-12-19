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
            CepDTO cepResult = new CepDTO();
            cepResult = clServiceCep.Search(zipCode);
            return cepResult;
        }
    }
}
