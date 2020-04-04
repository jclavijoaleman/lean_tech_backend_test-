using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;
using lean_tech_backend_test.Models;


namespace lean_tech_backend_test.Controllers
{
    public class HelloWorldController : ApiController
    {
        [HttpGet]
        [Route("holamundo/txt")]
        public HttpResponseMessage GetTxt()
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, "Hello world");

            return response;
        }

        [HttpGet]
        [Route("helloworld/json")]
        public HttpResponseMessage GetJson()
        {
            Message obj = new Message { message = "Hello world" };

            // Create the response
            var mt = new MediaTypeWithQualityHeaderValue("application/json");
            var response = Request.CreateResponse(HttpStatusCode.OK, obj, mt);

            return response;
        }
    }
}
