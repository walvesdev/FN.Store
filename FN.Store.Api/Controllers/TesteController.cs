using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FN.Store.Api.Controllers
{
    [Route("Teste")]
    public class TesteController
    {
        [HttpGet("Ping")]
        public string ping() => "Pong";
    }
}
