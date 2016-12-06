using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Swagger.Controllers
{
    public class SwaggerController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[]{"Swagger Msg1","Swagger Msg2"};
        }
    }
}
