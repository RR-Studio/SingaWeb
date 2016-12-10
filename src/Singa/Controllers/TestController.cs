using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Singa.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Test")]
    public class TestController : Controller
    {
        [HttpGet()]
        [Route("anon")]
        [AllowAnonymous]
        public string Anon()
        {
            return "anonymous";
        }

        [Route("authorize")]
        [HttpGet()]       
        public string Authorize()
        {
            return "authorize";
        }
    }
}