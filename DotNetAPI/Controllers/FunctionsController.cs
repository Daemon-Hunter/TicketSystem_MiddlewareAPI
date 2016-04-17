using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DotNetAPI.Models;

namespace DotNetAPI.Controllers
{
    public class FunctionsController : ApiController
    {
        private DBConn db = new DBConn();

        [HttpGet]
        [Route("api/functions/comparepasswords/{id}/{password}")]
        public int comparePassword(int id, string password)
        {
            return db.COMPAREPASSWORDS(id, password);
        }
    }
}
